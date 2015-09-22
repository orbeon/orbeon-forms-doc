> [[Home]] ▸ [[Installation]]

## Rationale

By default, Orbeon Forms ships with an embedded eXist database. For production, this is not an ideal setup and it is better to have a separate eXist database, whether:

1. a separate WAR file running the same servlet container
2. or a completely separate eXist instance running in a separate container

Here we look at how to deploy a separate eXist WAR file (the first option).

_NOTE: Since [Orbeon Forms 4.0,][1]  the embedded eXist database is secured by a token mechanism, which only allows accesses internal to Orbeon Forms. Follow the instructions on this page if you need to setup a secure, separate eXist database which can be accessed  from outside Orbeon Forms, or setup a completely separate eXist instance._

## Creating a separate eXist WAR

eXist as of version 1.4 no longer ships with a separate WAR file, but it is reasonably easy to create one.

Assume the following:

* you are running running on a Unix system
* you are running as user `acme` with home in `/home/acme`

Steps:

* download eXist 1.4
* run the eXist installer in headless mode
    * `java -jar eXist-setup-1.4.0-rev10440.jar -p /home/acme/exist-tmp`
    * supply an administrator password when asked to do so
* repackage eXist (see <http: exist.sourceforge.net=building.html#war=>)
    * if you have limited memory, change `~/exist-tmp/build.sh` to set `-Xms=256m -Xmx=256m`
    * build the WAR file
        * `~/exist-tmp/build.sh dist-war`
    * this produces a packed WAR file
        * `~/exist-tmp/dist/exist-1.4.0-rev10440.war`
    * unzip the contents of eXist WAR under
        * `~/exist-war-1.4.0-rev10440`
    * create a symlink to that WAR if you want
        * `ln -s exist-war-1.4.0-rev10440 exist-war`

## Modifying the eXist configuration

### Location of eXist data files

Instead of storing the eXist data files under the eXist WAR, specify a separate location, for example:

```
/home/acme/exist-data
```

To do this, modify the `files` and `journal-dir` attributes in `~/exist-war/WEB-INF/conf.xml`:

```xml
<db-connection files="/home/acme/exist-data" ...>
<recovery journal-dir="/home/acme/exist-data" ...>
```

The benefit of this is this is that upgrading the eXist WAR file will be easier: you can:

* leave the data in place
* build/install a new eXist WAR
* update the `exist-war` symbolic link
* update again the eXist configuration to point to your data
* and voilà!

### Setting Unix permissions

When deploying eXist as a WAR file, it will run using the Unix user of the servlet container. Often, this is a user called e.g. `tomcat`. You need to make sure that user can write:

* the eXist data: `chown -R tomcat:tomcat /home/acme/exist-data`
* logs: `chown -R tomcat:tomcat /home/acme/exist-war/WEB-INF/logs/`

### Creating a separate eXist user for Orbeon Forms

To do so, you can use the eXist database administration tool:

![](images/fr-exist-users.png)

Steps:

* Username: `orbeon` (you can pick your own user name)
* Password: pick a proper password for the `orbeon` user
* Home-Collection: `/db/orbeon` is a reasonable choice
* Create a group, for example `orbeon`, and assign it to the `orbeon` user

### Default resource permissions

By default, eXist allows everybody to read newly created collections and resources. This might not be desirable. To protect Form Runner resources from other users, uncomment and set the following configuration in `~/exist-war/WEB-INF/conf.xml`:

```xml
<default-permissions collection="0740" resource="0740" />
```

This will cause new collections and resources to be only fully accessible through user `orbeon`.

_NOTE: With this setting, other users in the group `orbeon` can read Form Runner resources and collections too, although this is not currently useful._

## Configuring full-text indexing

With Orbeon Forms 3.9 and 4.x Form Runner uses the [Lucene-based full-text index][4]. Here too you need to store a `collection.xconf` file, but it contains a different configuration:

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

See also the [latest version of this file on GitHub][5].

_NOTE: Without this configuration, free-text search in the Form Runner summary page won't work._

## Configuring Form Runner

The last step is to tell Form Runner how to connect to eXist. Set the following property in the Orbeon `properties-local.xml` file. With Orbeon Forms 4.0:

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

_NOTE: the password is available in clear in properties-local.xml. You must make sure that there is no unauthorized access to that file._

## Resulting setup

After this setup, your file hierarchy looks like this:

```
/home/acme/       -> home where eXist is installed
  exist-war/      -> eXist WAR file
    WEB-INF/logs/ -> eXist logs writable by tomcat user
  exist-data/     -> where your eXist data is stored, writable by tomcat user
```

In addition:

* only the eXist administrator can modify the eXist configuration
* data created by Form Runner is not readable except by
    * the eXist administrator
    * the `orbeon` user

[1]: http://wiki.orbeon.com/forms/doc/developer-guide/release-notes/40
[3]: http://exist-db.org/ftlegacy.html
[4]: http://exist-db.org/exist/apps/doc/lucene.xml
[5]: https://github.com/orbeon/orbeon-forms/tree/master/src/resources/apps/fr/persistence/exist/collection.xconf