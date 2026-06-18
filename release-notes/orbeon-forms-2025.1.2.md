# Orbeon Forms 2025.1.2

**Friday, June 19, 2026**

Today we released Orbeon Forms 2025.1.2! This maintenance release mainly contains bug-fixes and is recommended for all users of:

* [Orbeon Forms 2025.1.1 PE](orbeon-forms-2025.1.1.md)
* [Orbeon Forms 2025.1 PE](orbeon-forms-2025.1.md)

## New features

### Model Context Protocol (MCP) server for AI agents

This release includes a new Model Context Protocol (MCP) server which exposes Form Builder functionality to AI agents. This allows AI agents to:

- create forms based on user instructions
- modify existing forms based on user instructions
- retrieve information about forms, such as their structure and metadata

You can connect to the MCP server using any AI agent that supports the MCP protocol.

You can also use MCP directly from your web browser using WebMCP, which allows you to interact with the MCP server without needing to set up a separate AI agent. For this, you currently need a Gemini API key.

![WebMCP session](/form-builder/images/webmcp-example.webp)

See the [documentation](/form-builder/mcp.md) for more details.

## Performance enhancements

In this version, the performance of PDF processing is improved by about 30% for large forms.

## Issues addressed

In this release, we have addressed over 50 issues, including:

- Form Builder
    - `fr:code-mirror`: ability to change the theme ([\#7653](https://github.com/orbeon/orbeon-forms/issues/7653))
    - Confirmation page: ability to choose PDF to download ([\#7538](https://github.com/orbeon/orbeon-forms/issues/7538))
    - CSS class with AVT and if/then/else fails ([\#7625](https://github.com/orbeon/orbeon-forms/issues/7625))
    - Add missing Form Builder resources for Swedish ([\#7638](https://github.com/orbeon/orbeon-forms/issues/7638))
    - Template drag and drop in Email Settings not working reliably the second time the dialog is opened ([\#6827](https://github.com/orbeon/orbeon-forms/issues/6827))
    - Explanatory Text editor uses `id="1"` ([\#7664](https://github.com/orbeon/orbeon-forms/issues/7664))
    - Dropdown in the Actions Editor do not show the whole text ([\#7726](https://github.com/orbeon/orbeon-forms/issues/7726))
    - FB form template selector: cursor keys no longer scroll templates ([\#7734](https://github.com/orbeon/orbeon-forms/issues/7734))
    - Property to disable the templates and/or general settings pages in the new form wizard ([\#7735](https://github.com/orbeon/orbeon-forms/issues/7735))
    - Drag and drop from toolbox no longer works ([\#7749](https://github.com/orbeon/orbeon-forms/issues/7749))
- Form Runner
    - Confirmation page: PDF download doesn't always work ([\#7652](https://github.com/orbeon/orbeon-forms/issues/7652))
    - Section bottom borders are rendered incorrectly in PDF ([\#7393](https://github.com/orbeon/orbeon-forms/issues/7393))
    - Optimize form data loading in `HistoryDiff.formDiffs` ([\#7306](https://github.com/orbeon/orbeon-forms/issues/7306))
    - Mirrored inner `fr-form-instance` not propagating from setvalue ([\#7644](https://github.com/orbeon/orbeon-forms/issues/7644))
    - Improve `color-scheme` property names ([\#7645](https://github.com/orbeon/orbeon-forms/issues/7645))
    - Revision history shows all entries with "saved with no changes detected." ([\#7650](https://github.com/orbeon/orbeon-forms/issues/7650))
    - `xf:dispatch` with delay behaving differently from what is depicted in documentation ([\#7648](https://github.com/orbeon/orbeon-forms/issues/7648))
    - If `allow-url-param` is false, never update `fr-wizard-page` ([\#7659](https://github.com/orbeon/orbeon-forms/issues/7659))
    - Excel import reports rows as invalid ([\#7660](https://github.com/orbeon/orbeon-forms/issues/7660))
    - Import: message for invalid counts shows in green ([\#7661](https://github.com/orbeon/orbeon-forms/issues/7661))
    - `--orbeon-control-font-family` doesn't override font family in textareas ([\#7663](https://github.com/orbeon/orbeon-forms/issues/7663))
    - Use native crypto/weak reference native JS implementations ([\#7665](https://github.com/orbeon/orbeon-forms/issues/7665))
    - Minor UX bug when clicking left-column in repeated-grid ([\#7662](https://github.com/orbeon/orbeon-forms/issues/7662))
    - PDF image rotation error with alpha channel ([\#7673](https://github.com/orbeon/orbeon-forms/issues/7673))
    - Add public DOM events for Ajax lifecycle for updates applied and event queued ([\#7634](https://github.com/orbeon/orbeon-forms/issues/7634))
    - Published Forms sorted by title shows title for forms not available in the current language first ([\#7695](https://github.com/orbeon/orbeon-forms/issues/7695))
    - Error in data migration possibly due to `LazyList` ([\#7700](https://github.com/orbeon/orbeon-forms/issues/7700))
    - Error summary to filter anchors from control labels ([\#7705](https://github.com/orbeon/orbeon-forms/issues/7705)) 
    - Icon broken in emergency-medical-consent demo form description ([\#7710](https://github.com/orbeon/orbeon-forms/issues/7710))
    - Process with suspend and an expression with double quote inside single quote leads to XPath syntax error ([\#7712](https://github.com/orbeon/orbeon-forms/issues/7712))
    - Invalid class name `fr-view-appearance-fr:wizard` is being produced ([\#7335](https://github.com/orbeon/orbeon-forms/issues/7335))
    - I18n: don't split sentences into multiple resources ([\#7724](https://github.com/orbeon/orbeon-forms/issues/7724))
    - Fully localize all XBL components in all the languages supported by Form Runner / Form Builder ([\#7725](https://github.com/orbeon/orbeon-forms/issues/7725))
    - Share token validity might not survive restarts ([\#7727](https://github.com/orbeon/orbeon-forms/issues/7727))
    - Top left/right corners of signature field cropped in PDF ([\#7721](https://github.com/orbeon/orbeon-forms/issues/7721))
    - Property to disable warning the user when data is unsafe is not honored anymore ([\#7736](https://github.com/orbeon/orbeon-forms/issues/7736))
    - `send()` with Excel file doesn't work ([\#7747](https://github.com/orbeon/orbeon-forms/issues/7747))
- Offline
    - Offline: Allow attachment download in new form session ([\#7154](https://github.com/orbeon/orbeon-forms/issues/7154))
- Form controls
    - Image attachment fails to show the image out of the box ([\#7632](https://github.com/orbeon/orbeon-forms/issues/7632))
    - Help for dropdown with search isn't positioned correctly ([\#7406](https://github.com/orbeon/orbeon-forms/issues/7406))
    - Rich Text: character counter doesn't count what the user sees ([\#2624](https://github.com/orbeon/orbeon-forms/issues/2624))
    - Support for TinyMCE incremental mode ([\#1424](https://github.com/orbeon/orbeon-forms/issues/1424))
    - Character counter not to be incremental ([\#5272](https://github.com/orbeon/orbeon-forms/issues/5272))
    - Date control using browser datepicker resets after typing the first digit of the year ([\#7674](https://github.com/orbeon/orbeon-forms/issues/7674))
    - MySQL error: User-level lock name 'orbeon/fr/app-form/…' should not exceed 64 characters ([\#7677](https://github.com/orbeon/orbeon-forms/issues/7677))
    - Image Attachment allows uploading other files ([\#4784](https://github.com/orbeon/orbeon-forms/issues/4784))
    - In some demo forms, image attachments allows uploading other file types ([\#7697](https://github.com/orbeon/orbeon-forms/issues/7697))
    - Upload: incorrect message for incorrect mediatype ([\#7696](https://github.com/orbeon/orbeon-forms/issues/7696))
    - Date and Time incorrectly shown as invalid during internal navigation if previously visited ([\#7699](https://github.com/orbeon/orbeon-forms/issues/7699))
    - Missing translations in several XBL controls for polish, arabian, norwegian ([\#7693](https://github.com/orbeon/orbeon-forms/issues/7693))
- Performance
    - Automatic PDF production performance is improved for large forms. 
- Security
    - Upload/attachments: consider using file extension as source of truth ([\#7698](https://github.com/orbeon/orbeon-forms/issues/7698))
    - Third-party library updates
- Other
    - When running JS tests: "TypeError: Property description must be an object: undefined" ([\#7739](https://github.com/orbeon/orbeon-forms/issues/7739)) 

You can download the latest version of Orbeon Forms from the [downloads](https://www.orbeon.com/download) page, or use our Docker images.

Don't forget to [grab a trial license](https://prod.orbeon.com/prod/fr/orbeon/register/new) for the PE version.

Please send feedback via [Twitter](https://twitter.com/orbeon), [Bluesky](https://bsky.app/profile/orbeon.bsky.social), or the [forum](https://groups.google.com/g/orbeon).

We hope you enjoy this release!
