> [[Home]] ▸ [[Form Runner|Form Runner]]

## Integration points

Form Builder and Form Runner integrate with other systems through the following means:

- [[__Plain URLs__|Form Runner ~ Integration ~ URLs]]
    - Through URLS you access Form Runner and Form Builder's pages
    - The URLs can be accessed simply by using hyperlinks or redirects from other applications.
- __A [[configurable persistence API|Form Runner ~ APIs ~ Persistence]]__
    - The API is based on REST (that is, through HTTP).
    - It provides CRUD, search, and metadata operations.
    - see also [[Accessing data captured by forms|Form Runner ~ Accessing data captured by forms]]
_ __Other APIS__
    - [[Generate XML Schema|Form-Runner ~ APIs ~ Generate XML Schema]]
    - [[Duplicate form data|Form-Runner ~ APIs ~ Duplicate Form Data]]
    - [[List form data attachments|Form-Runner-~-APIs-~-List-Form-Data-Attachments]]
- __HTTP services__
    - via the [[HTTP Service Editor|Form Builder ~ HTTP Services]]
    - via properties to load initial XML data
    - via processes to [[submit data|Form Runner ~ Buttons and Processes#send]]
    - see also [[Accessing data captured by forms|Form Runner ~ Accessing data captured by forms]]
- __[[Embedding|Form-Runner ~ Embedding]]__
    - with the [[Server-side embedding API|Form Runner ~ APIs ~ Server side Embedding]]
    - with the [[Form Runner proxy portlet|Form Runner ~ Portal ~ Liferay Proxy Portlet Guide]]
    - with the [[Form Runner full portlet|Form Runner ~ Portal ~ Full Portlet Guide]]
- __External user management__
    - This allows you to connect Orbeon Forms to a directory of users with associated roles.

The persistence API can be implemented either within Orbeon Forms (like for example the built-in eXist persistence layer), or within an external system.

### External user management

See [[Access Control|Form Runner ~ Access Control]].

### XML representation of form data

See [[Data Format|Form Runner ~ Data Format]]

_NOTE: Non-repeated grids do not create containing elements._

## See also

- [[Form Builder Integration|Form Builder ~ Integration]]
- [[Form Runner Embedding|Form-Runner ~ Embedding]]
- [[Form Runner persistence API|Form Runner ~ APIs ~ Persistence]]
- [[Access Control|Form Runner ~ Access Control]]
- [[Data Format|Form Runner ~ Data Format]]
- [[Accessing data captured by forms|Form Runner ~ Accessing data captured by forms]]
