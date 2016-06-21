# PDF production

<!-- toc -->

## Introduction

Form Builder and Form Runner can produce PDF output in two ways:

- __Automatic__
    - The PDF is produced based on the appearance of the form in your web browser. This is similar to printing a read-only version of your form.
    - As a form author, you do not need to take any action to enable this mode.
- __Template-based__
    - You upload a PDF file using the Upload PDF dialog in the toolbox. At runtime, Form Runner fills-out Acrobat fields in the template.
    - This mode is automatically enabled for a form if a PDF template is attached.
    - For more, see [PDF Templates](../form-builder/pdf-templates.md).
    
## What you can do with a PDF

You can:

- __Show the PDF in the browser:__ see [Predefined buttons](../form-runner/advanced/buttons-and-processes/predefined.html#predefined-buttons).
- __Attach the PDF to an email:__ see [Email properties](../form-runner/advanced/buttons-and-processes/actions-form-runner.html#email).
- __Send an URL to the PDF:__ see [Sending a PDF URL](../form-runner/advanced/buttons-and-processes/actions-form-runner.html#sending-a-PDF-URL).

## Availability

- Automatic PDF generation is available in Orbeon Forms CE and PE
- Templates are an [Orbeon Forms PE](http://www.orbeon.com/download) feature.

## Production of TIFF images

[SINCE Orbeon Forms 2016.1]

PDF files can be converted to TIFF images. See [TIFF Production](../form-runner/feature/tiff-production.md).
