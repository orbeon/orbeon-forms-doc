# Security checklist

## Introduction

This page provides a practical security checklist for deploying Orbeon Forms. Depending on your architecture, you may deploy Orbeon Forms as a single instance or split authoring and runtime across separate instances:

1. **[Baseline checklist (all deployments)](#baseline-checklist-all-deployments):** Essential security measures that apply to every Orbeon Forms instance.
2. **[Deployed as Form Runner only](#deployed-as-form-runner-only):** Hardening steps for a runtime-only instance (internal or public-facing) where end users fill out and review published forms, and where Form Builder is not used.
3. **[Deployed as internal Form Builder](#deployed-as-internal-form-builder):** Hardening steps for an internal authoring instance where form authors design, test, and publish forms (and optionally push them to a remote Form Runner server).
4. **[Secure defaults to keep](#secure-defaults-to-keep):** Built-in security settings that are enabled out of the box—no changes are needed on a fresh installation, but verify they have not been weakened when auditing an existing deployment.

---

## Baseline checklist (all deployments)

Apply the following measures to both Form Runner and Form Builder deployments:

### Software and runtime environment

- [ ] **Use an up-to-date Orbeon Forms release:** Run the latest maintenance release for your Orbeon Forms version (for example, `2025.1.x` or `2024.1.x`) to benefit from security fixes and third-party library updates. See [Upgrading](../../upgrading.md).
- [ ] **Use a patched JVM and servlet container:** Run a supported LTS Java version and apply security updates regularly to your JVM and application server (Tomcat, WildFly, etc.). See [Installation](../../installation/README.md).

### WAR cleanup and database setup

- [ ] **Remove demo files from the WAR:** Remove `xforms-jsp`, `WEB-INF/resources/apps`, `WEB-INF/orbeon-demo.sqlite`, and `WEB-INF/lib/sqlite-jdbc-*.jar`. See [Creating a production WAR](production-war.md).
- [ ] **Disable the embedded SQLite database:** Set `oxf.fr.persistence.sqlite.active` to `false` in `properties-local.xml` and configure an external [relational database](../../form-runner/persistence/relational-db.md).
- [ ] **Apply database least privilege:** Ensure the database user configured in your container's JDBC datasource only has `SELECT`, `INSERT`, `UPDATE`, and `DELETE` privileges on the Orbeon Forms schema (no DDL or database administrator privileges at runtime).
- [ ] **Verify the configuration banner is absent:** Confirm that the [Configuration banner](../../installation/configuration-banner.md) does not appear and no incomplete configuration warnings are logged in `orbeon.log` at startup.

### Passwords and secrets

- [ ] **Set a strong main encryption password:** Set [`oxf.crypto.password`](../properties/general.md#oxf.crypto.password) in `properties-local.xml` to a strong, randomly-generated secret.
- [ ] **Set dedicated passwords for optional features:**
  - If using [Field-level encryption](../../form-builder/field-level-encryption.md), set a strong, separate password in `oxf.fr.field-encryption.password` and back it up securely.
  - If using [Token-based permissions](../../form-runner/access-control/tokens.md), set a strong, separate password in `oxf.fr.access-token.password`.
- [ ] **Restrict configuration file permissions:** Restrict OS filesystem permissions on `properties-local.xml`, `web.xml`, `license.xml`, container datasource configurations (such as Tomcat's `server.xml` or `orbeon.xml`), and keystores so that only the OS account running the servlet container can read them.

### HTTPS, cookies, and HTTP headers

- [ ] **Enforce HTTPS (TLS) everywhere:** Serve all traffic over HTTPS (terminated at a reverse proxy, load balancer, or the servlet container).
- [ ] **Harden session cookies:** Configure your servlet container and/or reverse proxy so session cookies (`JSESSIONID` or custom session cookie) include:
  - `Secure` (sent over HTTPS only)
  - `HttpOnly` (inaccessible to client-side JavaScript)
  - `SameSite=Strict` or `SameSite=Lax` (for example, `<CookieProcessor sameSiteCookies="strict"/>` in Tomcat's `<Context>`)
- [ ] **Configure session timeout:** Set an appropriate `<session-timeout>` in `WEB-INF/web.xml`. See [Session management](session.md).
- [ ] **Enable `Content-Security-Policy` (CSP):** Set a strict `Content-Security-Policy` header (such as `default-src 'self'; img-src 'self' data:`) via your reverse proxy or in `web.xml`. See [Content-Security-Policy header](content-security-policy.md).
- [ ] **Set standard HTTP security headers:** Configure your reverse proxy to return `Strict-Transport-Security` (HSTS), `X-Content-Type-Options: nosniff`, and `X-Frame-Options: DENY` (or `SAMEORIGIN` / CSP `frame-ancestors` if framing is required).

### Authentication and services

- [ ] **Prevent header spoofing when using header-driven authentication:** If `oxf.fr.authentication.method` is set to `header`:
  - Isolate the Orbeon Forms server (via firewall rules, network segmentation, localhost binding, or mutual TLS) so it **only** accepts requests from the authenticating reverse proxy.
  - Configure the reverse proxy to strip any incoming client-supplied authentication headers (including `Orbeon-Username`, `Orbeon-Group`, `Orbeon-Roles`, `Orbeon-Credentials`, and your custom header names) before setting verified headers on every request. See [Users](../../form-runner/access-control/users.md#prevent-header-spoofing).
- [ ] **Protect server-side service APIs (`/fr/service/*`):** External access to `/fr/service/*` is blocked by default. If external applications call Form Runner service APIs, configure the [authorizer service](../../xml-platform/controller/authorization-of-pages-and-services.md#authorization-service) using a dedicated service role, and ensure human users cannot access `/fr/service/*` directly. See [Authentication of server-side service APIs](../../form-runner/api/authentication.md).

---

## Deployed as Form Runner only

Use this checklist when deploying an Orbeon Forms instance dedicated to running published forms for end users (whether internal users or anonymous public users), without Form Builder on the server.

### Remove Form Builder

- [ ] **Remove the Form Builder JAR:** Delete `WEB-INF/lib/orbeon-form-builder.jar` from the WAR file. See [Creating a production WAR](production-war.md#removing-form-builder).
- [ ] **Block Form Builder paths at the reverse proxy:** As defense in depth, block `/fr/orbeon/builder/*` and `/fr/mcp/builder` at your reverse proxy or web server.

### Restrict administrative and discovery pages

- [ ] **Restrict or disable the Landing, Published Forms, and Admin pages:**
  - If end users access forms only through direct links (for example, `/fr/$app/$form/new`), block access to `/fr/` ([Landing page](../../form-runner/feature/landing-page.md)), `/fr/forms` ([Published Forms page](../../form-runner/feature/published-forms-page.md)), and `/fr/admin` ([Forms Admin page](../../form-runner/feature/forms-admin-page.md)) at the reverse proxy or via `<security-constraint>` in `web.xml`.
  - Hide the Landing page link in the navigation bar by setting `oxf.fr.navbar.home-link.enable` to `false`. See [Form Runner navigation bar](../../form-runner/feature/navbar.md#landing-page-link).
  - If authenticated users use the Landing page (`/fr/`), configure `oxf.fr.landing.cards` to exclude `form-builder-forms`, `demo-forms`, and (unless needed) `quick-links`.
- [ ] **Lock down `form-builder-permissions.xml`:**
  - If no administrative UI operations are needed on this server, leave `WEB-INF/resources/config/form-builder-permissions.xml` without `<role>` elements (which disables unpublishing/publishing and hides unavailable forms on the Admin page) and block `/fr/admin`.
  - If administrators use `/fr/admin` on this instance (for example, to toggle form availability, reindex, or re-encrypt), map only a dedicated administrator role in `form-builder-permissions.xml` (never `name="*"`) and require authentication for `/fr/admin`. See [Editing forms](../../form-runner/access-control/editing-forms.md#form-runner-home-page).

### Configure access control and permissions for deployed forms

- [ ] **Set a default global permission policy:** By default, a form created in Form Builder without permissions enabled allows anyone to `create`, `read`, `update`, `delete`, and `list` its data. Define a restrictive global fallback in `properties-local.xml` using [`oxf.fr.permissions.*.*`](../../form-runner/access-control/deployed-forms.md#per-app-and-global-permissions) so that forms without explicit permissions defined in Form Builder are protected by default.
- [ ] **Verify permissions on every published form:** Review form-level permissions in Form Builder (or per-app properties) before publishing. In particular, ensure the `list` permission (which grants access to the Summary page) and `read`/`update`/`delete` permissions are granted only to authorized roles, owners, or group members. See [Deployed forms](../../form-runner/access-control/deployed-forms.md).
- [ ] **Harden anonymous / public-facing forms:** If anonymous users fill out forms:
  - Grant `anyone` **only** the `create` permission (never `read`, `update`, `delete`, or `list`). See [Scenarios](../../form-runner/access-control/scenarios.md).
  - Configure Detail page buttons (`oxf.fr.detail.buttons.$app.$form`) to show only submission actions (such as `send` or `submit`), omitting `save` and `summary`.
  - If unauthenticated users need to view or update their submission via a shared link, use [Token-based permissions](../../form-runner/access-control/tokens.md) (`anyone-with-token`) with a strong `oxf.fr.access-token.password` and a bounded token validity (`oxf.fr.access-token.validity.*.*`).
  - Add a [Captcha](../../form-runner/component/captcha.md) component and configure rate limiting on your reverse proxy or WAF to mitigate automated abuse.

### Constrain and scan file uploads

- [ ] **Restrict allowed file types:** Configure [`oxf.fr.detail.attachment.mediatypes.*.*`](../properties/form-runner-attachments.md#allowed-file-types) (and/or form- and control-level settings) to allow only expected media types (for example, `application/pdf image/jpeg image/png`) rather than `*/*`.
- [ ] **Enforce upload size and count limits:** Set sensible limits for:
  - [`oxf.fr.detail.attachment.max-size-per-file.*.*`](../properties/form-runner-attachments.md#maximum-attachment-size)
  - [`oxf.fr.detail.attachment.max-size-aggregate-per-control.*.*`](../properties/form-runner-attachments.md#maximum-aggregate-attachment-size-controls)
  - [`oxf.fr.detail.attachment.max-size-aggregate-per-form.*.*`](../properties/form-runner-attachments.md#maximum-aggregate-attachment-size-forms)
  - [`oxf.fr.detail.attachment.max-files-per-control.*.*`](../properties/form-runner-attachments.md#maximum-number-of-files-controls)
- [ ] **Scan uploaded files for malware:** Implement a [File scan API](../../form-runner/api/other/file-scan-api.md) (`FileScanProvider2`) provider to scan uploaded attachments with an antivirus solution before they are attached to form data.
- [ ] **Secure attachment storage:** If storing attachments on the filesystem (`oxf.fr.persistence.filesystem.directory`) or S3 (`oxf.fr.persistence.s3.s3-config`), ensure the storage location is outside the web server's document root, not directly accessible via HTTP, and encrypted at rest. See [Persistence configuration properties](../properties/persistence.md#storing-attachments-in-the-filesystem-or-on-s3).

### Protect sensitive data at rest

- [ ] **Encrypt sensitive fields at rest:** Mark fields capturing PII/SPI for [Field-level encryption](../../form-builder/field-level-encryption.md) and configure `oxf.fr.field-encryption.password`.
- [ ] **Review revision history and data retention:** If regulations or privacy policies prohibit retaining edit history or require periodic deletion of old records, configure [`oxf.fr.persistence.proxy.disable-version-history.*.*`](../properties/persistence.md#disabling-version-history) and/or schedule [Purging historical data](../../form-runner/feature/purging-historical-data.md).

### Secure embedding and remote publishing (if applicable)

- [ ] **Secure embedded Form Runner access:** When embedding forms via the [Java Embedding API](../../form-runner/link-embed/java-api.md) or [Liferay Proxy Portlet](../../form-runner/link-embed/liferay-proxy-portlet.md), ensure end users cannot access the Form Runner server directly (use network/IP filtering, HTTPS with HTTP Basic authentication, or client certificates). See [Securing Form Runner access](../../form-runner/link-embed/securing.md).
- [ ] **Secure remote form publishing endpoints:** If an internal Form Builder server pushes published forms to this Form Runner instance, protect the `/fr/service/*` publishing and CRUD endpoints with the [authorizer service](../../xml-platform/controller/authorization-of-pages-and-services.md#authorization-service) over HTTPS, and restrict network access to those endpoints to the internal Form Builder server.

---

## Deployed as internal Form Builder

Use this checklist when deploying an internal Orbeon Forms instance where form authors design, test, and publish forms. Because Form Builder allows authors to define services, queries, formulas, and form structure, it requires specific controls depending on how trusted your form authors are.

### Network isolation and environment separation

- [ ] **Restrict network access to internal users:** Deploy the Form Builder instance on an internal network (or behind a VPN or zero-trust access proxy). Never expose Form Builder (`orbeon/builder`) to the public internet.
- [ ] **Separate authoring from production data:** Use a separate database (or separate database schema/instance) for the internal Form Builder environment so that in-progress forms and test submissions are isolated from production Form Runner data.

### Authentication and authoring permissions

- [ ] **Require authentication for Form Builder and admin paths:** Configure your reverse proxy (header-driven method) or `<security-constraint>` in `WEB-INF/web.xml` (container-driven method) to require authentication for `/fr/orbeon/builder/*`, `/fr/admin`, and `/fr/`. See [Users](../../form-runner/access-control/users.md).
- [ ] **Configure `form-builder-permissions.xml` with least privilege:**
  - Define explicit `<role name="..." app="..." form="*"/>` entries in `WEB-INF/resources/config/form-builder-permissions.xml` so each group of form authors can only create, edit, and administer forms in their designated application(s).
  - Do **not** leave `form-builder-permissions.xml` empty or use `<role name="*" app="*" form="*"/>`, as this allows any user who can reach Form Builder, authenticated or not, to create or modify forms in any application.
  - Note that roles listed in `form-builder-permissions.xml` also grant access to the [Forms Admin page](../../form-runner/feature/forms-admin-page.md) (`/fr/admin`) for the matching applications. See [Editing forms](../../form-runner/access-control/editing-forms.md).

### Restrict high-risk Form Builder toolbox capabilities

Form Builder includes advanced features in its toolbox that allow forms to interact with external systems or edit underlying XML markup. Review and disable capabilities that form authors do not need in `properties-local.xml` (see [Form Builder configuration properties](../properties/form-builder.md#other-toolbox-features)):

```xml
<property as="xs:boolean" name="oxf.fb.menu.services.database" value="false"/>
<property as="xs:boolean" name="oxf.fb.menu.services.http"     value="false"/>
<property as="xs:boolean" name="oxf.fb.menu.edit-source"       value="false"/>
<property as="xs:boolean" name="oxf.fb.menu.schema"            value="false"/>
<property as="xs:boolean" name="oxf.fb.menu.actions"           value="false"/>
```

- [ ] **Database services (`oxf.fb.menu.services.database`):** When enabled, form authors can write arbitrary SQL queries against any JNDI datasource configured in the servlet container. Set to `false` unless required and datasources are locked down (see below).
- [ ] **HTTP services (`oxf.fb.menu.services.http`):** When enabled, form authors can configure server-side HTTP requests to arbitrary URLs, which can introduce Server-Side Request Forgery (SSRF) risks against internal network services or cloud metadata endpoints (such as `169.254.169.254`). Set to `false` if not needed, or restrict outbound HTTP connections from the Form Builder server using firewall rules or an outbound HTTP proxy (`oxf.http.proxy.*`).
- [ ] **Edit Source (`oxf.fb.menu.edit-source`):** When enabled, form authors can directly edit the raw XHTML+XForms XML of a form definition. While Form Runner sanitizes HTML output and enforces the allowed XPath functions policy at runtime, set `oxf.fb.menu.edit-source` to `false` if form authors should only use the visual editor.
- [ ] **XML Schema (`oxf.fb.menu.schema`) and Actions (`oxf.fb.menu.actions`):** Set to `false` if your form authors do not need to upload custom XML Schemas or define service actions.

### Isolate JNDI datasources for Database Services

- [ ] **Use read-only, least-privilege datasources for lookup queries:** If [Database services](../../form-builder/database-services.md) are enabled:
  - Configure a dedicated JNDI datasource specifically for lookup tables.
  - Ensure the database user for that datasource has **read-only (`SELECT`) privileges only** on the specific tables or views intended for form lookups.

### Safe publishing, versioning, and remote promotion

- [ ] **Prevent accidental overwriting of published forms:** Consider setting [`oxf.fb.publish.version`](../properties/form-builder.md#the-version-dropdown) to `default-to-next` or `force-next` so publishing a form creates a new version rather than overwriting an existing published version.
- [ ] **Pre-populate standard permission roles:** Configure [`oxf.fb.permissions.role.always-show`](../properties/form-builder.md#permissions-dialog) with your organization's standard role names to reduce the risk of form authors mistyping role names when configuring form permissions.
- [ ] **Secure remote server push/pull (`oxf.fr.home.remote-servers`):** If administrators push or pull form definitions between the internal Form Builder server and staging/production Form Runner servers:
  - Always use `https://` URLs in [`oxf.fr.home.remote-servers`](../properties/form-runner.md#remote-servers).
  - Do not set `"api-access": true` unless you explicitly use the programmatic [Remote server APIs](../../form-runner/api/other/remote.md) and protect them via the authorizer service.
- [ ] **Prevent accidental outbound emails during form testing:** Configure [`test-smtp-host`](../properties/general.md#test-smtp-host) and [`test-to`](../properties/general.md#test-recipient) for the `oxf:email` processor in `properties-local.xml` so that testing forms in Form Builder redirects all emails to a safe test address or sandbox SMTP server.

### Harden the Form Builder MCP server (if enabled)

- [ ] **Harden MCP tokens when enabled:** If you enable [Form Builder MCP](../../form-builder/mcp.md) (`oxf.fb.mcp.enable` set to `true`):
  - Set `oxf.fb.mcp.token.password` to a strong, unique secret.
  - Set a reasonable token expiration in `oxf.fb.mcp.token.validity` (in minutes).
  - Issue **Read-only** tokens when an AI agent only needs to inspect forms, and reserve **Read/Write** tokens for trusted authoring sessions.
  - Rotate `oxf.fb.mcp.token.password` immediately if you need to revoke issued MCP tokens.

---

## Secure defaults to keep

Orbeon Forms ships with the following security settings enabled out of the box. No configuration changes are needed on a fresh installation, but you should verify they have not been weakened or overridden when auditing an existing deployment:

- [ ] **Keep production run mode enabled:** Keep `oxf.run-mode` in `WEB-INF/web.xml` set to `prod` (the default in official WAR distributions). In `prod` mode, Orbeon Forms hides internal exception details (`oxf.http.exceptions` is `false`) and suppresses version numbers in HTML and asset URLs (`oxf.show-version` is `false`). See [Run modes](run-modes.md).
- [ ] **Keep password strength checks enabled:** Leave [`oxf.crypto.check-password-strength`](../properties/general.md#oxf.crypto.check-password-strength) set to `true` (the default).
- [ ] **Keep inline resources disabled:** Leave `oxf.xforms.inline-resources` set to `false` (the default) so strict `Content-Security-Policy` headers without `'unsafe-inline'` are supported. See [Content-Security-Policy header](content-security-policy.md).
- [ ] **Keep page-flow public methods restricted:** Do not widen the default `oxf:page-flow` `page-public-methods` (`GET HEAD`) or `service-public-methods` (`""`) properties. See [Authorization of Pages and Services](../../xml-platform/controller/authorization-of-pages-and-services.md).
- [ ] **Keep resource and DoS limits active:**
  - Keep the [Limiter filter](limiter-filter.md) enabled in `WEB-INF/web.xml` to cap concurrent CPU- and memory-intensive requests.
  - Keep [`oxf.xml-parsing.entity-expansion-limit`](../properties/general.md#xml-entity-expansion) set to `0` (the default) to prevent XML entity expansion attacks.
  - Keep [`oxf.xforms.ajax.max-request-size`](../properties/xforms.md#maximum-request-size) set to an appropriate limit (default `10000000` bytes / 10 MB).
- [ ] **Keep outbound HTTP client and environment variable defaults:**
  - Keep [`oxf.http.ssl.hostname-verifier`](../properties/properties-general-http-client.md#ssl-hostname-verifier) set to `strict` (the default).
  - Keep `oxf.http.forward-headers` and `oxf.http.forward-cookies` empty (the default), forwarding headers or cookies only when required by trusted backend services. (If your container uses a non-default session cookie name and you forward cookies, explicitly set [`oxf.http.forward-cookies.session-cookie-name`](../properties/properties-general-http-client.md#session-cookie-name).)
  - Keep [`oxf.xpath.environment-variable.enabled`](../properties/general.md#oxf.xpath.environment-variable.enabled) set to `false` (the default).
- [ ] **Keep the XPath function restrictions enabled:** Keep the default [Allowed XPath functions](../properties/form-runner-allowed-functions.md) policy active so disallowed functions (such as `fn:doc`, `xxf:instance`, `xxf:call-xpl`, session/request attribute accessors, and `java:*` reflection calls) are blocked in Form Builder at design time and in Form Runner at runtime. Do not disable restrictions globally with `-* *`; if a legacy form requires a restricted function, allow only that specific function for that specific form via `oxf.fr.detail.formula.allowed-functions.<app>.<form>`.
- [ ] **Keep built-in HTML sanitization enabled:** Leave HTML sanitization at its default configuration so HTML in control labels, hints, help messages, Rich Text controls, and Explanatory Text controls is sanitized automatically. See [Security](security.md#html-sanitization).
- [ ] **Keep selection control value encoding enabled:** Leave [`oxf.xforms.encrypt-item-values`](../properties/xforms.md#xforms-items-encoding) set to `true` (the default) so item values in dropdowns, radio buttons, and checkboxes are encoded before being sent to the browser.
- [ ] **Keep the Form Builder MCP server disabled unless needed:** Leave `oxf.fb.mcp.enable` set to `false` (the default) if you do not connect AI agents to [Form Builder MCP](../../form-builder/mcp.md).

---

## See also

- [Security](security.md)
- [Content-Security-Policy header](content-security-policy.md)
- [Creating a production WAR](production-war.md)
- [Run modes](run-modes.md)
- [Allowed XPath functions in formulas and expressions](../properties/form-runner-allowed-functions.md)
- [Access control and permissions](../../form-runner/access-control/README.md)
- [Authentication of server-side service APIs](../../form-runner/api/authentication.md)
- [Authorization of Pages and Services](../../xml-platform/controller/authorization-of-pages-and-services.md)
- [Securing Form Runner access](../../form-runner/link-embed/securing.md)
