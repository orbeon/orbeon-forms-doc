# Email properties

## Email transport configuration

[SINCE Orbeon Forms 2025.1]

You can choose between SMTP and SendGrid as the email transport mechanism:

```xml
<property
    as="xs:string"
    name="oxf.fr.email.transport.*.*"
    value="sendgrid"/>
```

Supported values:

- `smtp`: Use SMTP server (default, and the only transport available before Orbeon Forms 2025.1)
- `sendgrid`: Use SendGrid API

## Connection to the SMTP server

The following properties control the connection to the SMTP server when using the `smtp` transport:

- `host`: required SMTP host name
- `port`: optional SMTP port override. If not specified, the defaults are:
    * plain SMTP: 25
    * TLS: 587
    * SSL: 465
- `encryption`:
    * blank: none (plain SMTP)
    * `tls`: use TLS
    * `ssl`: use SSL
- `username`: SMTP username (required if TLS or SSL is used, optional otherwise)
- `credentials`: SMTP password

```xml
<property
    as="xs:string"
    name="oxf.fr.email.smtp.host.*.*"
    value="my.outgoing.smtp.server.org"/>

<property
    as="xs:string"
    name="oxf.fr.email.smtp.port.*.*"
    value="587"/>

<property
    as="xs:string"
    name="oxf.fr.email.smtp.encryption.*.*"
    value="tls"/>

<property
    as="xs:string"
    name="oxf.fr.email.smtp.username.*.*"
    value="jdoe"/>

<property
    as="xs:string"
    name="oxf.fr.email.smtp.credentials.*.*"
    value="secret"/>
```

## SendGrid configuration

[SINCE Orbeon Forms 2025.1]

When using SendGrid as the email transport, you need to configure your SendGrid API key:

```xml
<property
    as="xs:string"
    name="oxf.fr.email.transport.*.*"
    value="sendgrid"/>

<property
    as="xs:string"
    name="oxf.fr.email.sendgrid.api-key.*.*"
    value="YOUR_SENDGRID_API_KEY"/>
```

The `api-key` property is required when using the SendGrid transport. You can obtain your API key from the SendGrid dashboard. All other email configuration properties (addresses, attachments, etc.) work the same way regardless of which transport you choose.

## Email addresses properties

- `from`: 
    - sender email address(es) appearing in the email sent
- `reply-to`:
    - SINCE Orbeon Forms 2020.1
    - message `Reply-To` address(es)
- `to`: 
    - recipient email address(es) of the email sent
- `cc`:
    - SINCE Orbeon Forms 2017.1
    - carbon copy recipient email address(es) of the email sent
- `bcc`:
    - SINCE Orbeon Forms 2017.1
    - blind carbon copy email address(es) of the email sent

List of emails are space- or comma- separated.

```xml
<property
    as="xs:string"
    name="oxf.fr.email.from.*.*"
    value="john@example.org"/>

<property
    as="xs:string"
    name="oxf.fr.email.reply-to.*.*"
    value="john@acme.org"/>

<property
    as="xs:string"
    name="oxf.fr.email.to.*.*"
    value="mary@example.org,nancy@example.org"/>
    
<property
    as="xs:string"
    name="oxf.fr.email.cc.*.*"
    value="mary@example.org,nancy@example.org"/>

<property
    as="xs:string"
    name="oxf.fr.email.bcc.*.*"
    value="mary@example.org,nancy@example.org"/>
```

## Format of email addresses

In the examples above, only raw email addresses are shown. Multiple addresses must be separated by commas.

[SINCE Orbeon Forms 2021.4]

The configuration properties can now contain a name, following the standard syntax:

```
John Smith <john@example.org>
```

Or:

```
"John Smith" <john@example.org>
```

Keep in mind that if the addresses are stored in an XML attribute in your `properties-local.xml`, you need to escape some characters:

```xml
<property
    as="xs:string"
    name="oxf.fr.email.from.*.*"
    value="John Smith &lt;john@example.org&gt;"/>
```

Similarly, if you use quotes, you need to escape them:

```xml
<property
    as="xs:string"
    name="oxf.fr.email.from.*.*"
    value="&quot;John Smith&quot; &lt;john@example.org&gt;"/>
```

When more than one email is present, if you specify names, you must use commas exclusively as a separator (with whitespace allowed around commas):

```xml
<property
    as="xs:string"
    name="oxf.fr.email.from.*.*"
    value="&quot;John Smith&quot; &lt;john@example.org&gt;, Alice &lt;alice@acme.org&gt;"/>
```

## Attachment properties

- `attach-pdf`: whether the PDF representation is attached to the email
- `attach-tiff`: whether the TIFF representation is attached to the email
- `attach-xml`:  whether the XML data is attached to the email

```xml
<property
    as="xs:boolean"
    name="oxf.fr.email.attach-pdf.*.*"
    value="true"/>

<property
    as="xs:boolean"
    name="oxf.fr.email.attach-tiff.*.*"
    value="true"/>

<property
    as="xs:boolean"
    name="oxf.fr.email.attach-xml.*.*"
    value="true"/>
```

[SINCE Orbeon Forms 2016.1]

The following property controls whether file and image form attachments are attached to the email.

- `all`: all form attachments are included (this is the default)
- `none`: no form attachments is included
- `selected`: only form attachments selected in the Form Builder with "Include as Email Attachment" are included
    
```xml
<property
    as="xs:string"
    name="oxf.fr.email.attach-files.*.*"
    value="all"/>
```
    
[SINCE Orbeon Forms 2018.1]

The following properties control the name of the PDF, TIFF and XML attachments:

- `oxf.fr.email.pdf.filename`:
    - filename of the PDF attachment, when present
- `oxf.fr.email.tiff.filename`
    - filename of the TIFF attachment, when present
- `oxf.fr.email.xml.filename`
    - filename of the XML attachment, when present
    
The property contains an XPath expression which generates the filename. The expression runs in the context of the current
form data but does *not* have a access to controls. Only a limited set of Form Runner XPath functions can be used, in
particular:

- `fr:form-title()`
- `fr:app-name()`
- `fr:form-name()`
- `fr:form-version()`
- `fr:document-id()`
- `fr:mode()`
- `fr:is-readonly-mode()`
- `fr:is-design-time()`

*NOTE: Control values must be extracted by searching for element values within the XML document. In the future, we hope
to provide a function for that purpose.* 

```xml
<property as="xs:string" name="oxf.fr.email.pdf.filename.*.*">
    concat(
        fr:form-title(),
        ' - ',
        //case-id,
        '.pdf'
    )
</property>

<property as="xs:string" name="oxf.fr.email.tiff.filename.*.*">
    concat(
        fr:form-title(),
        ' - ',
        //case-id,
        '.tiff'
    )

<property as="xs:string" name="oxf.fr.email.xml.filename.*.*">
    concat(
        fr:form-title(),
        ' - ',
        //case-id,
        '.xml'
    )
</property>
```
    
## Email subject and body

NOTE: Since Orbeon Forms 2018.1, you can set a form's email subject and body in the Form Builder user interface. You can
also use template placeholders. See [Email settings](/form-builder/email-settings.md).

With any Orbeon Forms version, the following properties can be used to set default and per app/form email subject and
body templates. 

```xml
<property 
    as="xs:string"
    name="oxf.fr.resource.*.*.en.email.subject"
    value="Here is your confirmation: "/>

<property 
    as="xs:string"
    name="oxf.fr.resource.*.*.en.email.body"
    value="Hi, here is an email from Orbeon Forms!"/>
```

## Styling HTML emails

[SINCE Orbeon Forms 2019.1]

When using HTML for an email body template in [Email settings](/form-builder/email-settings.md), you can provide inline CSS that will be included in the email messages. For example"

```xml
<property as="xs:string" name="oxf.fr.email.css.custom.inline.*.*">
    ul li { list-style-type: none; margin-left: 0; }
</property>
```

This is known to work with Gmail at least.  

## See also 

- [Email settings](/form-builder/email-settings.md)
- Configuration properties
    - [Detail page](form-runner-detail-page.md)
    - [Attachments](form-runner-attachments.md)
    - [PDF](form-runner-pdf.md)
    - [Table of contents](form-runner-toc.md)