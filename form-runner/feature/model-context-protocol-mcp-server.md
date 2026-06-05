# Model Context Protocol (MCP) Server

## Availability

[\[SINCE Orbeon Forms 2025.1.2\]](/release-notes/orbeon-forms-2025.1.2.md)

This is an experimental feature, which means that it is not yet fully documented and may change in the future. If you are interested in using or contributing to this feature, please contact us.

## What it does

The Orbeon Form Builder MCP server exposes Form Builder functionality to AI assistants, such as ChatGPT, via the Model Context Protocol (MCP). This allows AI assistants to interact with Form Builder, for example to create forms based on user instructions.

## Configuration

### Orbeon Forms configuration

By default, the MCP server is disabled. To enable it, set the following property:

```xml
<property 
    as="xs:boolean" 
    name="oxf.fb.mcp.enable" 
    value="true"/>
```

The MCP server can support an authorization token. By default, a token is required. To disable token support, set the following property:

```xml
<property 
    as="xs:boolean" 
    name="oxf.fb.mcp.token.enable" 
    value="false"/>
```

__WARNING: This will allow anyone to access the MCP server, which may have security implications. Use with caution, for testing, ideally only in restricted local environments.__

You can generate a token from Form Builder, using the "Create Model Context Protocol (MCP) token" dialog.

![Creating an MCP token in Form Builder](../images/mcp-token-dialog.webp)

By default, the token validity is one day. You can change this by setting the following property:

```xml
<property 
    as="xs:integer" 
    name="oxf.fb.mcp.token.validity" 
    value="1440"/>
```

The duration is in minutes, so:

- `1440` means 24 hours (1 day)
- `10080` means 7 days (1 week)
- `44640` means 31 days (1 month)

In order to use the token, you must also set a token password, which is used to sign the token. You can set the token password with the following property:

```xml
<property 
    as="xs:string"  
    name="oxf.fb.mcp.token.password" 
    value="This is not a very safe password!"/>
```

In order to revoke all tokens issued, simply change the token password.

### AI assistant configuration

Command-line AI assistants usually have a configuration file:

- Copilot CLI: `~/.copilot/mcp-config.json`
- Antigravity CLI: `~/.gemini/antigravity-cli/mcp_config.json`

Here is a Copilot example:

```json
{
  "mcpServers": {
    "orbeon": {
      "type": "http",
      "url": "URL_TO_FORM_BUILDER_MCP_SERVER",
        "headers": {
            "Authorization": "AUTHORIZATION_TOKEN"
      }
    }
  }
}
```

Here is an Antigravity example:

```json
{
  "mcpServers": {
    "orbeon": {
        "serverUrl": "URL_TO_FORM_BUILDER_MCP_SERVER",
        "headers": {
          "Authorization": "AUTHORIZATION_TOKEN"
        }
    }
  }
}


```

Where:

- `URL_TO_FORM_BUILDER_MCP_SERVER` is the URL of the Form Builder MCP server, for example `https://example.org/orbeon/fr/mcp/builder`
    - An important part is `/fr/mcp/builder`, which is the path to the MCP server in Form Builder.
    - The domain, port, and prefix (here `/orbeon`) should be those of your Orbeon Forms instance.
- `AUTHORIZATION` is the value of the `Authorization` header to use when making requests to the MCP server.
    - If token support is enabled, this should be in the format `Bearer 12345678`, where `12345678` is the token generated from Form Builder.

You can also add to your AI assistant a skill file. The latest version of the skill file can be found [in the Orbeon Forms GitHub repository here](https://github.com/orbeon/orbeon-forms/blob/master/.agents/skills/orbeon/SKILL.md). You place such as file in the appropriate location for your AI assistant, for example:

```
.agents/skills/orbeon/SKILL.md
```

Once the MCP server and skill configuration is done, you can start using the AI assistant to interact with Form Builder.

## Usage patterns

With MCP support, you can use your AI assistant to interact with Form Builder in various ways, for example:

> Using Orbeon, create a new demo form for a personal collection of widgets. Split the form into sections, and use appropriate form controls. Then save and close the form.

> Using Orbeon, edit form 33e71949140e1282b9428770176994bdb24c702a and modify the size of control widget-quantity to half its current width. 

> Using Orbeon, update form 33e71949140e1282b9428770176994bdb24c702a to make the type of the widget-quantity field an non-negative integer.

## Using WebMCP

You can also use Orbeon Forms's AI assistant support using WebMCP, an interface directly available in Form Builder.

As of Summer 2026, you can use it with:

- [WebMCP - Model Context Tool Inspector](https://chromewebstore.google.com/detail/webmcp-model-context-tool/gbpdfapgefenggkahomfgkhfehlcenpd)
    - This is a Chrome extension which allows you to interact with the MCP server directly from the browser.
    - In order to run prompts, you need a Google Gemini API key.
    - You need to enable this with a flag in Chrome. 
- [Chrome DevTools for agents](https://github.com/ChromeDevTools/chrome-devtools-mcp)
    - This is a tool which allows you to use MCP to interact with Chrome DevTools.
    - This includes WebMCP support, which allows AI assistants to use this extension to access WebMCP.
    - You need to enable this with a flag in Chrome.

It is expected that support for WebMCP will grow quickly, and that more tools will become available directly in the browser in the future.
