> [[Home]] ▸ Contributors ▸ [[Test Plan|Contributors ~ Test Plan]]

Do at least for eXist and DB2, as automated tests already test most of this, and the code running for DB2 is almost identical to the code running for other relational databases. But if possible do for the other relational databases as well.

## Setup

Setup: in `properties-local.xml`, add:

```xml
<property
    as="xs:string"
    name="oxf.fr.persistence.provider.exist.*.*"
    value="exist"/>
<property
    as="xs:string"
    name="oxf.fr.persistence.provider.db2.*.*"
    value="db2"/>
<property
    as="xs:string"
    processor-name="oxf:page-flow"
    name="service-public-methods"
    value="GET HEAD"/>
```

## Create forms

Create same form in all apps: `exist/a`, `db2/a`

- add static image
- use Duplicate button in FB Summary
- then change app name

## Pages

- FB: create form, publish
- FR: check it shows on http://localhost:8080/orbeon/fr/
- FR: create new form, review, back to edit ([#1643](https://github.com/orbeon/orbeon-forms/issues/1643))
- FR: enter data, save
- FR: check it shows in the summary page

## Attachments

- FB: attach static image to form
- FB: add file attachment field
- FB: save and publish
    - DB2: be aware of [#1409](https://github.com/orbeon/orbeon-forms/issues/1409)
- FR: deployed form loads image
- FR: attach file, save, edit

## Search

- FB: check summary/search field, save and deploy
- FR: create new form data, see in summary
- FR: search free-text and structured
- FR: delete data in summary page works

## Duplicate

- FR: Summary: Duplicate button works
    - data for latest form
    - older data

## Home page

With all persistence layers active

- go to /fr/
- check that form definitions from all persistence layers show
