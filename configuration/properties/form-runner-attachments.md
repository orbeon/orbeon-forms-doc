# Attachments configuration properties



## Maximum attachment size

[SINCE Orbeon Forms 2017.1]

The following property sets the maximum size in bytes of an uploaded attachment. For example, if you set it to `1000000` (1 MB), and the user attempts to upload a larger attachment, an error is reported.

```xml
<property 
    as="xs:string"  
    name="oxf.fr.detail.attachment.max-size.*.*"                      
    value="1000000"/>
```

If the value is blank (the default), then the value of the following backward compatibility property is used:

```xml
<property
    as="xs:integer" 
    processor-name="oxf:request"   
    name="max-upload-size"          
    value="100000000"/>
```

The value of `oxf.fr.detail.attachment.max-size` can be overridden:

- for a specific form, from the Form Builder "Form Settings" dialog
- for a specific control, using a common constraint the Form Builder "Control Settings" dialog 

## Maximum aggregate attachment size

[SINCE Orbeon Forms 2017.1]

The following property sets the maximum aggregate size in bytes of all uploaded attachments for a given instance of form data. For example, if you set it to `1000000` (1 MB), and the form has two attachment controls, and you upload a 600 KB attachment using the first control, then only 400 KB can be uploaded using the second control, even if a larger maximum size per control was set using the `oxf.fr.detail.attachment.max-size` property or a Form Builder setting. If you attempt to upload a larger attachment, an error is reported.

```xml
<property 
    as="xs:string"  
    name="oxf.fr.detail.attachment.max-size-aggregate.*.*"                      
    value="10000000"/>
```

The value of `oxf.fr.detail.attachment.max-size-aggregate` can be overridden:

- for a specific form, from the Form Builder "Form Settings} dialog

## Allowed file types

[SINCE Orbeon Forms 2017.1]

The following property specifies which file types (also known as "mediatypes") are allowed for attachments. For example, the following values of `image/png image/jpeg` specify that JPEG images and PDF files are allowed but no other files.

```xml
<property 
    as="xs:string"  
    name="oxf.fr.detail.attachment.mediatypes.*.*"                    
    value="image/jpeg application/pdf"/>
```

*NOTE: This property impacts regular Form Runner file attachments, but also Form Runner image attachments. If you have image attachments and they were created with a build of Orbeon Forms prior to 2017.1, make sure to include `image/*` in that property as well or new image attachments entered by users will be rejected. Image attachments created with 2017.1 or newer limit the mediatypes to `image/*` out of the box. You can also edit the form definition and set explicitly, on each image attachment control, the "Supported File Types" validation.*

The format is as follows:

<!-- TODO: Duplicated from xforms.md -->
- the value is a list of space- or comma-separated mediatype ranges
- a mediatype range is one of:
  - `*/*`: all mediatypes allowed
  - `type/*`: all mediatypes with prefix `type` are allowed (for example `image/*`)
  - `type/subtype`: specific mediatype such as `image/jpeg`, `application/atom+xml`, `video/mp4`, etc.
  
The value of `oxf.fr.detail.attachment.mediatypes` can be overridden:

- for a specific form, from the Form Builder "Form Settings} dialog
- for a specific control, using a common constraint the Form Builder "Control Settings" dialog

## See also

- Blog post: [Improved constraints on attachments uploads](https://blog.orbeon.com/2017/04/improved-constraints-on-attachments.html)
