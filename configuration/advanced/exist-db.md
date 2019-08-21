# Configuring an eXist-db database

## Deprecation

[SINCE Orbeon Forms 2019.1]

Using the eXist database with Orbeon Forms is deprecated. We recommend using one of the supported [relational databases](/form-runner/persistence/relational-db.md) for production.

## Rationale

By default, Orbeon Forms ships with an embedded eXist-db database. For production, this is not an ideal setup and it is better to have a separate eXist-db database. After you download and install eXist-db, you should follow the instruction below to set it up, and let Orbeon Forms know how it should connect to eXist-db.

## Setup

### Configuring full-text indexing in eXist-db

Orbeon Forms uses the [Lucene-based full-text index](http://exist-db.org/exist/apps/doc/lucene.xml). Without this configuration, free-text search in the Form Runner summary page won't work. After you install eXist-db, store a `collection.xconf` file as follows in the eXist-db, under `system/config/db/orbeon/fr/`. You can also find the [latest version of this file on GitHub](https://github.com/orbeon/orbeon-forms/blob/master/data/system/config/db/orbeon/fr/collection.xconf).

```xml
<collection xmlns="http://exist-db.org/collection-config/1.0">
    <index>
        <!-- Disable the standard full text index -->
        <fulltext default="none" attributes="no"/>
        <!-- Lucene index is configured below -->
        <lucene>
            <analyzer class="org.apache.lucene.analysis.standard.StandardAnalyzer"/>
            <!-- We want to index the content of all form elements -->
            <text match="//*"/>
        </lucene>
    </index>
</collection>
```

### Configuring Form Runner

Set the `oxf.fr.persistence.exist.exist-uri` in your Orbeon Forms `properties-local.xml` to tell Form Runner how to connect to your eXist-db REST API, using the appropriate URL.

```xml
<property
    as="xs:anyURI"
    name="oxf.fr.persistence.exist.exist-uri"
    value="http://orbeon:secret@localhost:8090/exist/rest/db/orbeon/fr"/>
```
