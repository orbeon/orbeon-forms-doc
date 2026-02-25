# VPAT® 2.5 — Revised Section 508 Edition

**Voluntary Product Accessibility Template**

---

## Product Information

| | |
|---|---|
| **Product Name** | Orbeon Forms (Form Runner) |
| **Product Version** | 2025.1 |
| **Report Date** | February 2026 |
| **Report Version** | 1.0 |
| **Product Description** | Orbeon Forms is a web-based form management platform. Form Runner is the end-user runtime component that renders and processes HTML forms in a browser. It supports a wide variety of form controls, repeating sections and grids, multi-page wizard layouts, validation, and data submission. |
| **Contact Information** | Orbeon, Inc. — support@orbeon.com — https://www.orbeon.com |
| **Notes** | This report covers Form Runner, the end-user-facing component. It does not cover Form Builder (the form authoring tool) or the administrative Summary page. The scope is the Web tier only; Chapter 4 (Hardware) does not apply. |
| **Evaluation Methods Used** | This conformance report is based on developer self-evaluation of the HTML output, ARIA markup, CSS, and keyboard behavior of Form Runner. No independent third-party accessibility audit has been performed. |

---

## Applicable Standards and Guidelines

This report covers the following accessibility standards:

- **[Web Content Accessibility Guidelines (WCAG) 2.1](https://www.w3.org/TR/WCAG21/)**, Levels A and AA
- **[Revised Section 508 standards](https://www.access-board.gov/ict/)** (36 CFR Part 1194), which incorporate WCAG 2.0 Level AA by reference for Web content and software

---

## Terms

| Term | Definition |
|---|---|
| **Supports** | The functionality meets the criterion without known limitations. |
| **Partially Supports** | Some aspects of the functionality meet the criterion, but there are known limitations. |
| **Does Not Support** | The functionality does not meet the criterion. |
| **Not Applicable** | The criterion is not relevant to this product. |
| **Not Evaluated** | The criterion has not been assessed. |

---

## WCAG 2.x Report

### Table 1: Success Criteria, Level A

| Criteria | Conformance Level | Remarks and Explanations |
|---|---|---|
| **1.1.1 Non-text Content** (Level A) | Supports | Form controls are associated with text labels via `<label>` elements and ARIA attributes (`aria-labelledby`). Icons used decoratively carry `aria-hidden="true"`. Attachment and image controls expose text alternatives through their labels. |
| **1.2.1 Audio-only and Video-only (Prerecorded)** (Level A) | Not Applicable | Form Runner does not include pre-recorded audio-only or video-only content. |
| **1.2.2 Captions (Prerecorded)** (Level A) | Not Applicable | No pre-recorded synchronized media. |
| **1.2.3 Audio Description or Media Alternative (Prerecorded)** (Level A) | Not Applicable | No pre-recorded synchronized media. |
| **1.3.1 Info and Relationships** (Level A) | Supports | Semantic HTML elements are used throughout. Form control labels are programmatically associated via `<label>` and `aria-labelledby`. Section headings use appropriate heading elements. Groups of radio buttons and checkboxes use `<fieldset>`/`<legend>` or equivalent ARIA grouping. Layout tables carry `role="presentation"`. Error messages are associated with their controls via `aria-describedby`. |
| **1.3.2 Meaningful Sequence** (Level A) | Supports | The reading order of the DOM matches the visual order of form controls. |
| **1.3.3 Sensory Characteristics** (Level A) | Partially Supports | Most instructions rely on text labels. Some UI affordances (e.g., drag-and-drop reordering of repeat rows) rely partly on visual proximity. |
| **1.4.1 Use of Color** (Level A) | Partially Supports | Required field indicators use both a visual marker (asterisk) and text. Validation states (error, warning, info) use color together with icons. In Windows High Contrast mode, radio buttons and checkboxes display correctly (fixed in 2023.1.3). Some status indicators may still rely on color alone in certain themes. |
| **1.4.2 Audio Control** (Level A) | Not Applicable | No audio content plays automatically. |
| **2.1.1 Keyboard** (Level A) | Supports | All form controls and buttons are keyboard-accessible via the Tab key. Dropdowns, date pickers, checkboxes, and radio buttons can be operated with the keyboard. Custom XBL components implement keyboard interaction patterns consistent with ARIA Authoring Practices. Keyboard events (`keypress`, `keydown`, `keyup`) are fully supported. |
| **2.1.2 No Keyboard Trap** (Level A) | Supports | Focus can be moved away from any component using standard keyboard keys. Modal dialogs trap focus within the dialog while open and restore focus to the triggering element on close, conforming to the ARIA dialog pattern. |
| **2.2.1 Timing Adjustable** (Level A) | Not Applicable | Form Runner does not impose session time limits by default. Any session timeout is a server-side application configuration outside the scope of this report. |
| **2.2.2 Pause, Stop, Hide** (Level A) | Not Applicable | No auto-updating, blinking, or scrolling content is present by default in Form Runner. |
| **2.3.1 Three Flashes or Below Threshold** (Level A) | Not Applicable | Form Runner does not contain flashing content. |
| **2.4.1 Bypass Blocks** (Level A) | Partially Supports | A `<main>` landmark is present since Orbeon Forms 2024.1.4. Navigation landmarks (`<header>`, `<nav>`, `<main>`, `<footer>`) allow screen reader users to skip directly to the form content. A skip-to-main-content link is not currently provided for keyboard users who do not use assistive technology. |
| **2.4.2 Page Titled** (Level A) | Supports | Each page rendered by Form Runner carries a descriptive `<title>` element that includes the form name. |
| **2.4.3 Focus Order** (Level A) | Supports | The Tab order follows the logical reading order of the form. Grid tab order can be configured as row-first (default) or column-first via a property. Focus management in dialogs follows the expected ARIA dialog pattern. |
| **2.4.4 Link Purpose (In Context)** (Level A) | Supports | Links and buttons carry descriptive labels or are accompanied by visible text. Icon-only buttons include accessible labels via `aria-label` or visually hidden text. |
| **3.1.1 Language of Page** (Level A) | Supports | The `lang` attribute on the `<html>` element is set to the active language of the form, which is determined by the user's language selection or the browser locale. |
| **3.2.1 On Focus** (Level A) | Supports | Moving focus to a control does not trigger unexpected context changes. |
| **3.2.2 On Input** (Level A) | Supports | Most input fields do not trigger context changes on input. Dynamic dropdowns that load items from a service trigger asynchronous data loads, which update dependent controls. These updates are announced via live regions where applicable. |
| **3.3.1 Error Identification** (Level A) | Supports | Validation errors are identified in the Error Summary component, which lists all invalid controls with their labels and error messages. Inline alert messages are associated with the relevant control. Controls are marked with `aria-invalid="true"` when they fail validation. |
| **3.3.2 Labels or Instructions** (Level A) | Supports | Every form control has a visible label. Hint text provides additional instructions. Required fields are marked with both a visual indicator and programmatic `aria-required="true"`. |
| **4.1.1 Parsing** (Level A) | Supports | Form Runner produces well-formed HTML. Elements have complete start and end tags. IDs are unique per page. Nesting of elements conforms to the HTML specification. |
| **4.1.2 Name, Role, Value** (Level A) | Supports | Form controls expose their name via labels (`aria-labelledby`), role via semantic HTML elements or explicit ARIA roles, and state/value via ARIA attributes (`aria-required`, `aria-invalid`, `aria-readonly`, `aria-expanded`, `aria-checked`, `aria-selected`). XBL custom components expose the same attributes. |

### Table 2: Success Criteria, Level AA

| Criteria | Conformance Level | Remarks and Explanations |
|---|---|---|
| **1.2.4 Captions (Live)** (Level AA) | Not Applicable | No live audio content. |
| **1.2.5 Audio Description (Prerecorded)** (Level AA) | Not Applicable | No pre-recorded synchronized media. |
| **1.3.4 Orientation** (Level AA) | Supports | Form Runner does not restrict display to a single orientation. Forms render in both portrait and landscape layouts. |
| **1.3.5 Identify Input Purpose** (Level AA) | Supports | Common personal data fields (name, email, phone, address) support browser autofill when standard HTML `autocomplete` attributes are present. This depends on the form author configuring the appropriate input types. Orbeon Forms does not automatically inject `autocomplete` attributes for all personal data fields. |
| **1.4.3 Contrast (Minimum)** (Level AA) | Partially Supports | The default color scheme has been designed with contrast ratios meeting WCAG AA for body text and interactive controls. However, a full third-party contrast audit has not been performed, and custom themes may introduce colors that do not meet the 4.5:1 text contrast ratio or 3:1 UI component contrast ratio. Orbeon Forms 2025.1 introduced CSS variables that simplify theming while making it easier to verify contrast compliance. |
| **1.4.4 Resize Text** (Level AA) | Supports | Text can be resized up to 200% without loss of content or functionality. Form Runner uses relative units (em, rem) for font sizes. |
| **1.4.5 Images of Text** (Level AA) | Supports | Text is rendered as HTML text, not as images. Labels, hints, and alert messages use text elements. |
| **1.4.10 Reflow** (Level AA) | Supports | Form Runner supports responsive layout. At narrow viewport widths (equivalent to 320 CSS pixels), form controls reflow into a single-column layout without horizontal scrolling. |
| **1.4.11 Non-text Contrast** (Level AA) | Partially Supports | Focus indicators, form control borders, and checkbox/radio button outlines have been updated to improve contrast. Windows High Contrast mode is supported (fixed in 2023.1.3). A comprehensive audit against the 3:1 non-text contrast ratio has not been performed for all UI components and states. |
| **1.4.12 Text Spacing** (Level AA) | Partially Supports | Form Runner's layout is largely flexible and tolerates increased text spacing. However, some complex custom controls (e.g., date/time pickers with fixed-size layouts) may clip or overlap text when letter spacing, word spacing, line height, and paragraph spacing are adjusted to the WCAG 1.4.12 thresholds. |
| **1.4.13 Content on Hover or Focus** (Level AA) | Supports | Hint text displayed on hover or focus can be dismissed by moving the pointer. Tooltip content is persistent while the pointer remains over the control. |
| **2.4.5 Multiple Ways** (Level AA) | Supports | On multi-page (wizard) forms, a table of contents provides an alternative way to navigate between sections. The Error Summary provides navigation to invalid controls by name. |
| **2.4.6 Headings and Labels** (Level AA) | Supports | Section titles are rendered as heading elements (`<h2>`, `<h3>`, etc.) in a logical hierarchy. Form control labels are descriptive. |
| **2.4.7 Focus Visible** (Level AA) | Supports | All focusable controls display a visible focus outline. Since Orbeon Forms 2022.1, focus highlights are rendered consistently across standard controls and control groups. Read-only fields also have a visible focus outline (improved in 2021.1.5). |
| **2.5.3 Label in Name** (Level AA) | Supports | Visible label text is included in the accessible name of controls. Icon buttons whose accessible name differs from their visible icon label include the visual label text in `aria-label`. |
| **2.5.4 Motion Actuation** (Level AA) | Not Applicable | No functionality is triggered solely by device motion or user movement. |
| **3.1.2 Language of Parts** (Level AA) | Partially Supports | The primary language is set at the page level. Inline content in a different language (e.g., multilingual help text) is not currently marked up with `lang` attributes on the individual elements. |
| **3.2.3 Consistent Navigation** (Level AA) | Supports | Navigation components (wizard navigation bar, error summary, action buttons) appear in a consistent position across pages of the same form. |
| **3.2.4 Consistent Identification** (Level AA) | Supports | Controls with the same function (e.g., Save, Submit, Previous, Next buttons) are identified consistently across forms and pages. |
| **3.3.3 Error Suggestion** (Level AA) | Supports | When a control fails validation, the associated alert message describes the error and, where applicable, suggests how to correct it. Form authors can configure custom error messages per constraint. |
| **3.3.4 Error Prevention (Legal, Financial, Data)** (Level AA) | Supports | Form Runner provides a review/confirmation step in wizard forms and a read-only "view" mode before final submission. Saved data can be edited before final submission. Whether a confirmation step is presented depends on the form design chosen by the form author. |
| **4.1.3 Status Messages** (Level AA) | Partially Supports | Inline validation alerts are surfaced in ARIA live regions. The Error Summary updates dynamically. |

---

## Revised Section 508 Report

### Chapter 3: Functional Performance Criteria (FPC)

| Criteria | Conformance Level | Remarks and Explanations |
|---|---|---|
| **302.1 Without Vision** | Supports | Form Runner is usable with screen readers (NVDA, JAWS, VoiceOver). All form controls, labels, hints, validation messages, and navigation buttons are exposed to assistive technology. Complex custom controls (date pickers, dynamic dropdowns, file upload) have been progressively improved. |
| **302.2 With Limited Vision** | Supports | Text can be resized up to 200%. Form Runner uses relative units. The responsive layout reflows at narrow widths. High contrast mode is supported. |
| **302.3 Without Perception of Color** | Supports | Required fields are indicated by both a visual marker and text. Validation states include icons in addition to color. |
| **302.4 Without Hearing** | Not Applicable | Form Runner does not produce audio output. |
| **302.5 With Limited Hearing** | Not Applicable | Form Runner does not produce audio output. |
| **302.6 Without Speech** | Not Applicable | Form Runner does not require speech input. |
| **302.7 With Limited Manipulation** | Supports | All functionality is keyboard-operable. No interaction requires fine motor control (e.g., simultaneous multi-touch gestures or precise mouse movements). |
| **302.8 With Limited Reach and Strength** | Supports | All functionality is achievable without simultaneous multi-key commands beyond standard keyboard shortcuts. |
| **302.9 With Limited Language, Cognitive, and Learning Abilities** | Supports | Forms can include descriptive labels, hints, examples, and help text authored by the form designer. The Error Summary provides a consolidated list of errors with links to the relevant controls. Wizard-mode forms can break complex processes into smaller, labeled steps. The level of cognitive support depends significantly on the form design, which is the responsibility of the form author. |

### Chapter 5: Software

*Note: Form Runner is a Web application delivered in a browser. Under the Revised Section 508 Standards, Web content must conform to WCAG 2.0 Level AA (§1194.22), which is addressed in the WCAG 2.x tables above. The criteria below address platform software behaviors.*

| Criteria | Conformance Level | Remarks and Explanations |
|---|---|---|
| **501.1 Scope** | Heading | |
| **502 Interoperability with Assistive Technology** | Supports | Form Runner renders standard HTML and exposes information to platform accessibility APIs via WAI-ARIA. It does not require or install any platform-level software components outside the browser. |
| **503.4 User Controls for Captions and Audio Description** | Not Applicable | No media player is included. |
| **503.4.1 Caption Controls** | Not Applicable | |
| **503.4.2 Audio Description Controls** | Not Applicable | |

### Chapter 6: Support Documentation and Services

| Criteria | Conformance Level | Remarks and Explanations |
|---|---|---|
| **601.1 Scope** | Heading | |
| **602.2 Accessibility and Compatibility Features** | Supports | Orbeon Forms documentation at https://doc.orbeon.com/ describes accessibility-related properties and features. A dedicated accessibility page exists. The documentation itself is published as HTML and covers ARIA properties, keyboard navigation, error summary, focus management, and PDF/A. |
| **602.3 Electronic Support Documentation** | Supports | Support documentation is provided in HTML format on https://doc.orbeon.com/. |
| **602.4 Alternate Formats for Non-Electronic Support Documentation** | Not Applicable | All support documentation is provided electronically. |
| **603.2 Information on Accessibility and Compatibility Features** | Supports | Accessibility-related features are described in the product documentation. This VPAT serves as the primary consolidated accessibility statement. |
| **603.3 Accommodation of Communication Needs** | Supports | Orbeon, Inc. can provide support via email and written communication to accommodate users who require alternative communication methods. |

---

## Legal Disclaimer

This Voluntary Product Accessibility Template (VPAT) is provided for informational purposes only. The information in this document reflects the current understanding of Orbeon, Inc. regarding the accessibility of Orbeon Forms at the time of publication. Orbeon, Inc. makes no warranty, express or implied, with respect to this document. Conformance claims are based on self-evaluation by the product team; an independent third-party accessibility audit has not been conducted. Orbeon, Inc. is committed to improving accessibility and welcomes feedback from users with disabilities.

---

*This VPAT was prepared using the [VPAT® 2.5 template](https://www.itic.org/policy/accessibility/vpat) provided by the Information Technology Industry Council (ITI).*
