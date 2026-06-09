# Form Builder MCP

## Availability

[\[SINCE Orbeon Forms 2025.1.2\]](/release-notes/orbeon-forms-2025.1.2.md)

This is an early access feature, which means that it is not yet fully documented and may change in the future. If you are interested in using or contributing to this feature, please contact us.

## What it does

The Form Builder MCP server exposes Form Builder functionality to AI agents via the MCP (Model Context Protocol), a standard protocol supported by most AI agents. This allows AI agents to interact with Form Builder, for example to:

- create forms based on user instructions
- modify existing forms based on user instructions
- retrieve information about forms, such as their structure and metadata

Orbeon Forms provides both:

- A Form Builder MCP Server; this is covered in the first section below.  
- Support for WebMCP in Form Builder loaded in your browser; this is covered in the second section below.

## MCP Server

<figure><img src="images/mcp-diagram.svg" alt="" width="760"><figcaption>AI Agent connected to Form Builder MCP</figcaption></figure>

### Orbeon Forms configuration

To use the MCE server, set the following property two properties. The first enables the MCP server (it is disabled by default). The second sets the password used to sign the token, which you need to set the value of this property to a secure password. 

```xml
<property 
    as="xs:boolean" 
    name="oxf.fb.mcp.enable" 
    value="true"/>
<property 
    as="xs:string"  
    name="oxf.fb.mcp.token.password" 
    value=""/>
```

In order to revoke all tokens issued, simply change the token password.

Once you have those two properties in place, you can generate a token. Open any form in Form Builder and click on the key icon that shows at the top right of the page to reveal the token dialog.

<figure><img src="images/mcp-token-dialog.webp" alt="" width="510"><figcaption>Creating an MCP token in Form Builder</figcaption></figure>

If choosing "Readonly" access, only read-only operations will be allowed, such as listing forms and retrieving form metadata. If choosing "Read/Write" access, all operations will be allowed, including creating and modifying forms.

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


### Agent configuration

In what follows:

- `your-form-builder-url` is the URL to your Orbeon Forms, for example `https://example.org/orbeon/fr/mcp/builder`
    - An important part is `/fr/mcp/builder`, which is the path to the MCP server in Form Builder.
    - The domain, port, and prefix (here `/orbeon`) should be those of your Orbeon Forms instance.
- `your-token` is the value of the token you generated in Form Builder.

#### Claude Code

Add the MCP server with:

```
claude mcp add orbeon your-form-builder-url \
    --scope user \
    --transport http \
    --header "Authorization: Bearer your-token"
```

You can then run `claude mcp list` to check it was correctly added and that Claude is able to connect. 

#### Codex CLI

Declare an `ORBEON_MCP_TOKEN` environment variable with the value of your token value, then run:

```
codex mcp add orbeon \
    --url your-form-builder-url \ 
    --bearer-token-env-var ORBEON_MCP_TOKEN
```

You can then run `codex mcp list` to check it was correctly properly added.

#### GitHub Copilot CLI

Add the MCP server with:

```
copilot mcp add orbeon \
    --url your-form-builder-url \
    --type http \
    --header "Authorization=Bearer your-token"
```

#### Antigravity CLI

Edit your `~/.gemini/antigravity-cli/mcp_config.json` to add the `orbeon` MCP server, for example:

```json
{
  "mcpServers": {
    "orbeon": {
      "serverUrl": "your-form-builder-url",
      "headers": {
        "Authorization": "Bearer your-token"
      }
    }
  }
}
```

#### Skill (optional)

You can also add to your AI agent a skill file. The latest version of the skill file can be found [in the Orbeon Forms GitHub repository here](https://github.com/orbeon/orbeon-forms/blob/master/.agents/skills/orbeon/SKILL.md). You place such as file in the appropriate location for your AI agent, for example:

```
.agents/skills/orbeon/SKILL.md
```

## WebMCP

As of Summer 2026, WebMCP is a nascent, but very primising standard. The idea is that users will load a WebMCP page in their browser, open an agent in a sidebar, either built in the browser or provided by a browser extension, and the agent in the sidebar will be able to use tools provided by that page. Gemini in Chrome would be a prime candidate to support this, and other vendors like Anthropic or OpenAI could provide similar support through a browser extensions. This could provide a very seemless experience for users.

Today, we get a taste for what using WebMCP will feel like by using a regular exteral agent, such as Claude or Codex. To set this up:   

1. In Chrome, open `chrome://flags/`, search for "MCP", enable both "WebMCP support in DevTools" and "WebMCP for testing", then restart the browser.
2. Set up your agent to add [Chrome DevTools for agents](https://github.com/ChromeDevTools/chrome-devtools-mcp). Make sure to use the `--autoConnect` parameter so your agent can connect to a live browser (for instance, with Claude Code, use `claude mcp add chrome-devtools -s user -- npx chrome-devtools-mcp@latest --autoConnect`).
3. In Chrome, open a form in Form Builder, and ask your agent "with Chrome DevTools, through WebMCP, give me a list of the control in the form I have open in Form Builder".

This set up quite convenient:

- It doesn't require users to generate a token in Form Builder and set up their agent with that token.
- Users can see changes made to the form in real time in the browser. They can also make their own changes, and use both the agent and the Form Builder UI in the same session to update a form.
- In the future, when agents built in the browser, such as Gemini in Chrome, support WebMCP, users will be able to do all of the above directly from their browser, without even having to install an external agent.

## Usage patterns

With MCP support, you can use your AI agent to interact with Form Builder in various ways using prompts such as:

> Using Orbeon, create a new demo form for a personal collection of widgets. Split the form into sections, and use appropriate form controls. Then save and close the form.

The result might look like this:

![Example of a form created by an AI agent](images/mcp-widgets-form.webp)

Further prompts can be used to update the form layout, for example:

> Using Orbeon, edit form 33e71949140e1282b9428770176994bdb24c702a and modify the size of control widget-quantity to half its current width. 

Or add validation rules:

> Using Orbeon, update form 33e71949140e1282b9428770176994bdb24c702a to make the type of the widget-quantity field a non-negative integer.
