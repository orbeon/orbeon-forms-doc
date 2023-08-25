# File scan API

## Rationale

When a user uploads files via one of the Form Runner attachment controls, a virus scanner can verify the file before it gets attached to the current form.

Orbeon Forms provides a simple Java API which allows integrating with virus scanning solutions.

_NOTE: At this time Orbeon doesn't provide integrations with specific virus scanning solutions._

## API Version 2

### Availability

[SINCE Orbeon Forms 2022.1]

This is an Orbeon Forms PE feature.

Version 2 of the API adds the following enhancements to the original version (called now Version 1):

- The provider can return a modified file name, content type, or even replace the entire content.
- The provider can return custom error messages in the current language of the form.
- An extension mechanism via hash maps is provided.

While we recommend using Version 2 of the API, Version 1 is still supported. If a Version 2 provider is present, it takes precedence over a Version 1 provider.

### API

Orbeon Forms provide the following `FileScanProvider2` interface as a base for concrete providers:

```java
package org.orbeon.oxf.xforms.upload.api.java;

interface FileScanProvider2 {

  void init();
  void destroy();
  
  FileScan2 startStream(
      String                filename,
      Map<String, String[]> headers,
      String                language,
      Map<String, Object>   extension
  );
}
```

When Orbeon Forms initializes the provider, it:

- creates an instance of the class
- calls the `init()` method

When Orbeon Forms is starting to receive an upload file, it calls the `startStream()` method. The method receives:

- `filename`: a file name as provided by the web browser (which means that it must not be trusted)
- `headers`: HTTP header name/values as they are received by Orbeon Forms
- `language`: the current form language at the time the user started the upload
- `extension`: an extension immutable hashmap
    - *NOTE: This is an empty hashmap from Orbeon Forms 2022.1.0 until 2022.1.4 included.* 

The `extension` hashmap contains the following keys and values:

| Key           | Value type     | Description                                          | Since Orbeon Forms Version |
|---------------|----------------|------------------------------------------------------|----------------------------|
| `request.uri` | `java.net.URI` | path and query of the form containing the attachment | 2022.1.5                   |

The `java.net.URI` associated with `request.uri` contains the following parts:

| Part         | Description                                                                                            |
|--------------|--------------------------------------------------------------------------------------------------------|
| `getPath()`  | Orbeon Forms path that loaded the form, for example `/fr/acme/order/new` or `/fr/acme/order/edit/1234` |
| `getQuery()` | the query string, for example `form-version=2`                                                         |
| other        | set to `null`                                                                                          |

The provider implementation must return an implementation of a `FileScan2`.

The `FileScan2` interface is as follows :

```java
package org.orbeon.oxf.xforms.upload.api.java;

interface FileScan2 {
    FileScanResult bytesReceived(byte[] bytes, int offset, int length);
    FileScanResult complete(File file);
    void abort();
}
```

The `FileScanResult` type replaces the earlier API's `FileScanStatus`:

```java
package org.orbeon.oxf.xforms.upload.api.java;


public abstract class FileScanResult {

    public final String message;

    private FileScanResult(String message) {
        this.message = message;
    }

    public static class FileScanAcceptResult extends FileScanResult {

        public final String                        mediatype;
        public final String                        filename;
        public final java.io.InputStream           content;
        public final java.util.Map<String, Object> extension;

        public FileScanAcceptResult() {
            this(null, null, null, null, null);
        }

        public FileScanAcceptResult(
            String                        message,
            String                        mediatype,
            String                        filename,
            java.io.InputStream           content,
            java.util.Map<String, Object> extension
        ) {
            super(message);
            this.mediatype = mediatype;
            this.filename  = filename;
            this.content   = content;
            this.extension = extension;
        }
    }

    public static class FileScanRejectResult extends FileScanResult {
        public FileScanRejectResult(String message) {
            super(message);
        }
    }

    public static class FileScanErrorResult extends FileScanResult {

        public final Throwable throwable;

        public FileScanErrorResult(String message, Throwable throwable) {
            super(message);
            this.throwable = throwable;
        }
    }
}
```

This essentially defines three possible results, all implementing `FileScanResult`:

- `FileScanAcceptResult`: accept the uploaded content, possibly with modifications
- `FileScanRejectResult`: reject the uploaded content (for example because it is suspected to contain a virus), possibly with a message
- `FileScanErrorResult`: any other error happened, possibly with a message and `Throwable`

As bytes are received, the `bytesReceived()` method is called. This method allows incremental scanning of a file. A provider may or may not make use of this method. If it is unused, the provider must return a `FileScanAcceptResult` with all `null` content, for example with:

```java
public FileScanResult bytesReceived(byte[] bytes, int offset, int length) {
    return new FileScanResult.FileScanAcceptResult();
}
```

When the file is completely uploaded and stored into a temporary file, but not yet attached to the user's form, the
`complete()` method is called with a pointer to the temporary file. The scanner can use this method to perform the
entirety of the scan in case `bytesReceived()` is not used.

Upon processing the `complete()` method, resources associated with the file scan must be cleared by the provider, whether
`FileScanRejectResult` or `FileScanErrorResult` is returned, or if any exception is thrown as part of the processing of `complete()`.

Both `bytesReceived()` and `complete()` must return a value from the `FileScanStatus` enumeration:

If `FileScanRejectResult` or `FileScanErrorResult` is returned, or if any exception is thrown as part of `startStream()`, `bytesReceived()`, or
`complete()`, the uploaded file is rejected and an error is shown to the user.

![File scan error](../../images/file-scan.png)

If the user cancels the upload, or if any other error occurs on the Orbeon Forms side, the `abort()` method is called.
In this case, the file scan for the given file must be stopped and associated resources must be cleared, if any.

The `abort()` method may be called after the `complete()` method.

The constructor parameters for `FileScanAcceptResult`, `FileScanRejectResult`, and `FileScanErrorResult` can all be set to `null`.

If the provider wants to accept the file but replace any of the following:

- filename
- mediatype
- actual content

It can do so with a `FileScanAcceptResult` response, simply by passing non-`null` values to the respective fields in the constructor.

Replacing the entire content of the uploaded file is useful for example to:

- strip metadata in images
- recompress images (although there is a built-in Orbeon Forms feature for that)
- standardize file formats

### Registering a provider

The file scanning API uses the standard Java [`ServiceLoader`](https://docs.oracle.com/javase/8/docs/api/java/util/ServiceLoader.html)
API with a provider name of `org.orbeon.oxf.xforms.upload.api.java.FileScanProvider2`.

The provider must:

- be a public class
- have a public no-arguments constructor
- implement the `FileScanProvider2` Java interface

To enable a provider with Orbeon Forms:

- create your provider as per the standard Java [`ServiceLoader`](https://docs.oracle.com/javase/8/docs/api/java/util/ServiceLoader.html) 
- create a JAR file containing the code or your provider
- place your JAR file under the Orbeon Forms `WEB-INF/lib` directory

The Orbeon Forms log files will log errors if any when starting if the provider was found but could not be instantiated.

### Example

[`AcmeFileScanProvider2`](https://github.com/orbeon/orbeon-forms/blob/master/file-scan-v2-example/src/main/java/acme/filescan/AcmeFileScanProvider2.java)
is a Java example of a provider which does nothing but:

- log method calls
- reject files which contain in their name the string "virus"
- replace files which contain in their name the string "replace"
- prefix the file name with "My "
- return messages in English and French

*NOTE: This obviously is not the right way to determine whether a file contains a virus or not!*

The service provider is described with this [`META-INF/services/org.orbeon.oxf.xforms.upload.api.java.FileScanProvider2`](https://github.com/orbeon/orbeon-forms/blob/master/file-scan-v2-example/src/main/resources/META-INF/services/org.orbeon.oxf.xforms.upload.api.java.FileScanProvider2) file. 

The example project is provided [here](https://github.com/orbeon/orbeon-forms/tree/master/file-scan-v2-example).

## API Version 1

### Availability

[SINCE Orbeon Forms 2017.2]

This is an Orbeon Forms PE feature.

### API

Orbeon Forms provide the following `FileScanProvider` abstract class as a base for concrete providers:

```java
package org.orbeon.oxf.xforms.upload.api.java;

public abstract class FileScanProvider {

    public void init();
    public void destroy();

    public FileScan startStream(String filename, Map<String, String[]> headers);
}
```

When Orbeon Forms initializes the provider, it:

- creates an instance of the class
- calls the `init()` method

When Orbeon Forms is starting to receive an upload file, it calls the `startStream()` method. The method receives a
file name as provided by the web browser (which means that it must not be trusted), and HTTP header name/values as they
are received by Orbeon Forms. 

The provider implementation must return an implementation of a `FileScan`.

The `FileScan` interface is as follows :

```java
package org.orbeon.oxf.xforms.upload.api.java;

interface FileScan {
    FileScanStatus bytesReceived(byte[] bytes, int offset, int length);
    FileScanStatus complete(File file);
    void abort();
}
```

As bytes are received, the `bytesReceived()` method is called. A provider may or may not make use of this method. If it
is unused, the provider must return an `ACCEPT` `FileScanStatus` (see below). This method allows incremental scanning
of a file.

When the file is completely uploaded and stored into a temporary file, but not yet attached to the user's form, the
`complete()` method is called with a pointer to the temporary file. The scanner can use this method to perform the
entirety of the scan in case `bytesReceived()` is not used.

Upon processing the `complete()` method, resources associated with the file scan must be cleared by the provider, whether
`REJECT` or `ERROR` is returned, or if any exception is thrown as part of the processing of `complete()`.

Both `bytesReceived()` and `complete()` must return a value from the `FileScanStatus` enumeration:  


```java
package org.orbeon.oxf.xforms.upload.api.java;

public enum FileScanStatus { ACCEPT, REJECT, ERROR }
```

- `ACCEPT`: accept the uploaded content
- `REJECT`: reject the uploaded content (for example because it is suspected to contain a virus)
- `ERROR`: any other error happened

If `REJECT` or `ERROR` is returned, or if any exception is thrown as part of `startStream()`, `bytesReceived()`, or
`complete()`, the uploaded file is rejected and an error is shown to the user.

![File scan error](../../images/file-scan.png)

If the user cancels the upload, or if any other error occurs on the Orbeon Forms side, the `abort()` method is called.
In this case, the file scan for the given file must be stopped and associated resources must be cleared, if any.

The `abort()` method may be called after the `complete()` method.

### Registering a provider

The file scanning API uses the standard Java [`ServiceLoader`](https://docs.oracle.com/javase/8/docs/api/java/util/ServiceLoader.html)
API with a provider name of `org.orbeon.oxf.xforms.upload.api.FileScanProvider`.

The provider must:

- be a public class
- have a public no-arguments constructor
- extend the `FileScanProvider` Java abstract class

To enable a provider with Orbeon Forms:

- create your provider as per the standard Java [`ServiceLoader`](https://docs.oracle.com/javase/8/docs/api/java/util/ServiceLoader.html) 
- create a JAR file containing the code or your provider
- place your JAR file under the Orbeon Forms `WEB-INF/lib` directory

The Orbeon Forms log files will log errors if any when starting if the provider was found but could not be instantiated.

### Example

[`AcmeFileScanProvider`](https://github.com/orbeon/orbeon-forms/blob/master/file-scan-example/src/main/java/acme/filescan/AcmeFileScanProvider.java)
is a Java example of a provider which does nothing but:

- log method calls
- reject files which contain in their name the string "virus"

*NOTE: This obviously is not the right way to determine whether a file contains a virus or not.*

The service provider is described with this [`META-INF/services/org.orbeon.oxf.xforms.upload.api.FileScanProvider`](https://github.com/orbeon/orbeon-forms/blob/master/file-scan-example/src/main/resources/META-INF/services/org.orbeon.oxf.xforms.upload.api.FileScanProvider) file. 

The example project is provided [here](https://github.com/orbeon/orbeon-forms/tree/master/file-scan-example).