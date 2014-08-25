> [Wiki](Home) ▸ Form Runner ▸ [APIs](./Form-Runner-:-APIs)

### Purpose

The purpose of the `duplicate` API is to duplicate form data with attachments.

### Interface

- URL: `/fr/service/$app/$form/duplicate`
- Method: POST

Request body:

```xml
<?xml version="1.0" encoding="UTF-8"?>
<document-id>51dfcf49bb4b7f994906a26911003e4a999f1e39</document-id>
```

Response body:

```xml
<?xml version="1.0" encoding="UTF-8"?>
<document-id>c0f6dd2e75e94f60b9493768843e3fdef2af6bc0</document-id>
```

### Permissions

The caller must either call the service internally or have proper credentials to access the data (username, group, roles).
