# Configuring a Form Runner eXist database

## Rationale

By default, Orbeon Forms ships with an embedded eXist database. For production, this is not an ideal setup and it is better to have a separate eXist database. After you download and install eXist-db, you should follow the instruction below to set it up, and let Orbeon Forms know how it should connect to eXist-db.

## Setup

### Configuring full-text indexing in eXist-db

Orbeon Forms uses the [Lucene-based full-text index][4]. Without this configuration, free-text search in the Form Runner summary page won't work. After you install eXist-db, store a `collection.xconf` file as follows in the eXist-db, under `system/config/db/orbeon/fr/`. You can also find the [latest version of this file on GitHub](https://github.com/orbeon/orbeon-forms/blob/master/data/system/config/db/orbeon/fr/collection.xconf).

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

The step is to tell Form Runner how to connect to eXist. Set the following property in the Orbeon `properties-local.xml` file.

```xml
<property
    as="xs:anyURI"
    name="oxf.fr.persistence.exist.exist-uri"
    value="http://orbeon:secret@localhost:8080/exist/rest/db/orbeon/fr"/>
```

This assumes:

* The eXist user you picked above is `orbeon`
* The eXist password you picked above is `secret` (don't actually use "`secret`", use your own unique password!)
* Your servlet container is deployed on port 8080
* The eXist WAR is deployed under path `/exist`
* Your want to put your Form Runner documents under collection `/db/orbeon/fr`

[1]: http://wiki.orbeon.com/forms/doc/developer-guide/release-notes/40
[3]: http://exist-db.org/ftlegacy.html
[4]: http://exist-db.org/exist/apps/doc/lucene.xml
