# Building Orbeon Forms

## Introduction

This page explains how to build Orbeon Forms.

Please note that the build system has changed over time. Building a full distribution with `ant orbeon-dist` has remained a constant though.

If something is broken, please [let us know](https://twitter.com/intent/tweet?in_reply_to=orbeon&in_reply_to_status_id=261900968369729536&source=webclient&text=%40orbeon+)!

## Prerequisites

You need to have already installed:

- [git](https://git-scm.com/)
- [ant](https://ant.apache.org/)
- [sbt](https://www.scala-sbt.org/)
- [Tomcat 8](https://tomcat.apache.org/download-80.cgi) or [Tomcat 9](https://tomcat.apache.org/download-90.cgi)
    - ([Tomcat 7](http://tomcat.apache.org/download-70.cgi) still works as of 2018-10-01 but is deprecated)
- [Node.js](https://nodejs.org/en/)
- [jsdom](https://github.com/jsdom/jsdom)
- Java 11 or newer

On macOS, you can install the following easily if you have [Homebrew](http://brew.sh/):

```bash
brew install git
brew install ant
brew install sbt
brew install nvm
nvm install node
npm install jsdom
```

If you encounter any problem with Node.js and/or Uglify, try using an older version of Node.js, e.g. `nvm use 18` or `nvm use 20`. 

## Getting the source

If you have never obtained the Orbeon Forms source code, you need to get it [from github](https://github.com/orbeon/orbeon-forms). To get the latest code from the `master` branch, run the following command line:

```bash
git clone git@github.com:orbeon/orbeon-forms.git ~/my/projects/orbeon-forms
```

where `~/my/projects` is an existing directory on your system where you want to place the Orbeon Forms source code.

This clones the git repository into a child directory called `orbeon-forms`.

*NOTE: There is no guarantee that the master branch is stable, as it contains some of the latest changes to Orbeon Forms!*

## Current instructions

### What the Orbeon Forms developers use

You don't have to use the following, but in case you care, the Orbeon Forms developers use:

- the latest version of MacOS (Monterey as of 2022)  
- the latest version of IntelliJ IDEA (2022.2 as of 2022-08) and the Scala plugin

### GitHub token

Starting with Orbeon Forms 2021.1, some packages are hosted by [GitHub Packages](https://github.com/features/packages). Unfortunately, as of May 2022, there is no way to enable downloading these packages fully anonymously via tools like Maven or sbt. There is a now [multi-year thread on GitHub about this](https://github.community/t/download-from-github-package-registry-without-authentication/14407/146).

This means that until that is addressed, or until we move to another host for packages, a GitHub token is needed to build Orbeon Forms.

If you have a GitHub account:

- Create a [PAT (Personal Access Token)](https://docs.github.com/en/authentication/keeping-your-account-and-data-secure/creating-a-personal-access-token) with the Read Packages ability
- Export that token before launching sbt:

```bash
export GITHUB_TOKEN=ghp_...
```

This can also be stored in your shell profile file.

If you don't have a GitHub account, either create one or ask us for a token.

### Building

Most of the build is now done with [sbt](https://www.scala-sbt.org/).

To build files for development:

- `sbt`: launch sbt
- `project /`: this is the default (with older versions of sbt: `project root`)
- `package`: compile Scala, Java and assets, create JARs, and create the exploded WAR 

The exploded WAR is available under:

    orbeon-war/jvm/target/webapp

To run all the tests:

- `sbt`: launch sbt
    - `project /`: this is the default (with older versions of sbt: `project root`)
    - `Test/test`: run unit tests (with older versions of sbt: `test:test`)
    - `Db/test`: run database tests (with older versions of sbt: `db:test`)

To create a distribution:

- `ant orbeon-dist`

### Using IntelliJ

You need:

- [IntelliJ IDEA](https://www.jetbrains.com/idea/)
- the Scala plugin for Scala source code support

Currently, compiling and packaging is done via `sbt` and not from IntelliJ. You use IntelliJ for: 

- editing
- debugging

To open the Orbeon Forms project:

- Go to the "File" → "Open Project" menu and  select the `orbeon-forms` directory.
- IntelliJ then indexes the project, which can take a minute the first time you do it.
- Open the "sbt" tab which should appear on the right side of IntelliJ.
    - If this doesn't show, search for "sbt" in the list of commands (Ctrl-Shift-A).
- Do "Refresh all sbt projects".
- ~~Open `build.sbt` and click "Refresh" to let IntelliJ create modules based on the sbt build.~~

![IntelliJ sbt module import](images/building-intellij-sbt.png)

### Running

Create a new context in Tomcat's `conf/server.xml`:

```xml
<Context
    path="/orbeon"
    docBase="/path/to/orbeon-forms/orbeon-war/jvm/target/webapp"
    reloadable="false"
    override="true"
    crossContext="true"
    allowLinking="true">
        
    <Parameter
        override="false"
        name="oxf.resources.priority.0"
        value="org.orbeon.oxf.resources.FilesystemResourceManagerFactory"/>
    <Parameter
        override="false"
        name="oxf.resources.priority.0.oxf.resources.filesystem.sandbox-directory"
        value="/path/to/orbeon-forms/form-runner/jvm/src/main/resources"/>
    
    <Parameter
        override="false"
        name="oxf.resources.priority.1"
        value="org.orbeon.oxf.resources.FilesystemResourceManagerFactory"/>
    <Parameter
        override="false"
        name="oxf.resources.priority.1.oxf.resources.filesystem.sandbox-directory"
        value="/path/to/orbeon-forms/form-builder/jvm/src/main/resources"/>
    
    <Parameter
        override="false"
        name="oxf.resources.priority.2"
        value="org.orbeon.oxf.resources.FilesystemResourceManagerFactory"/>
    <Parameter
        override="false"
        name="oxf.resources.priority.2.oxf.resources.filesystem.sandbox-directory"
        value="/path/to/orbeon-forms/resources-local"/>
    
    <Parameter
        override="false"
        name="oxf.resources.priority.3"
        value="org.orbeon.oxf.resources.WebAppResourceManagerFactory"/>
    <Parameter
        override="false"
        name="oxf.resources.priority.3.oxf.resources.webapp.rootdir"
        value="/WEB-INF/resources"/>
    
</Context>
```

Tomcat has a file called `bin/setenv.sh` where you can set environment variables. In it, set the `JAVA_OPTS` for Tomcat. Create the file if it doesn't exist yet. For Java 11:

```bash
ORBEON_MEMORY_OPTS="-Xms300m -Xmx3g -verbosegc -XX:+PrintGCDetails"
ORBEON_DEBUG_OPTS="-agentlib:jdwp=transport=dt_socket,server=y,suspend=n,address=61155"

JAVA_OPTS="-ea $ORBEON_MEMORY_OPTS -Dapple.awt.UIElement=true $ORBEON_DEBUG_OPTS"

export JAVA_OPTS
```

*NOTE: You don't have to set `-verbosegc -XX:+PrintGCDetails` if you are not interested in garbage collector output.*

*NOTE: The `-Djava.net.preferIPv4Stack=true` can help in some cases.*

*NOTE: Older options for Java 8 included: `-XX:MaxPermSize=256m `, `-XX:+PrintGCDateStamps -XX:+PrintGCTimeStamps`.*

Finally, you can start Tomcat with:

```bash
 ./apache-tomcat-9.0.56/bin/catalina.sh run
```

And test by going to:

`http://localhost:8080/orbeon/`

This will show the Orbeon Forms landing page.

### Debugging

The settings above start Tomcat in debug mode. This means that you can debug the Java and Scala code from IntelliJ. Select the "Tomcat" configuration in IntelliJ, then "Run" → "Debug". IntelliJ opens the debugging window and connects to Tomcat. You can then set breakpoints and do the usual things one does with a debugger!

### Running without debugging or profiling

Alternatively, for running without debugging enabled, set instead:

```bash
JAVA_OPTS="-ea $ORBEON_MEMORY_OPTS -Dapple.awt.UIElement=true"
```

### Making changes

If you modify Java or Scala files, you need to recompile. You do this from the command-line with sbt, which can let run the `compile` task incrementally with:

```
~compile
```

TODO: document making changes to resources or assets.

### Sbt tips

The following runs only tests within `FormBuilderFunctionsTest` which contain the string `my test` and show full stack traces in case of exception: 

```
testOnly *FormBuilderFunctionsTest -- -z "my test" -oF
```

## About building Orbeon Forms PE

Orbeon does not provide public instructions or code to build Orbeon Forms PE, which is a commercial, supported build of the product. If you are a PE customer, contact Orbeon at info@orbeon.com.

The idea is just that we want to ensure that something called "Orbeon Forms PE" in fact comes from Orbeon.

----

## Historical instructions

### With sbt from the command-line

As of 2016-06-02:

To compile the Scala and Java files from the command-line the first time, run:

- `ant orbeon-war`

To incrementally compile Scala and Java files during further development, run:

- `sbt`
- `project root`
- one of
    - `~copyJarToExplodedWar`: to only update server-side classes
    - `~fastOptJSToExplodedWar`: to only update the Form Builder Scala.js artifacts
    - `~ ;copyJarToExplodedWar;fastOptJSToExplodedWar`: to update both

The `copyJarToExplodedWar` command in the `root` project incrementally:
 
- builds all the sub-projects depended by `root`
    - `orbeon-common`
    - `orbeon-dom`
    - `orbeon-form-builder-shared` (empty currently on `master`)
    - `orbeon-xupdate`
    - `orbeon-core`
    - `orbeon-form-runner`    
    - `orbeon-form-builder`
    - `orbeon-form-builder-client`
- copies the following resulting JAR files to `build/orbeon-war/WEB-INF/lib`
    - `orbeon-common.jar`
    - `orbeon-dom.jar`
    - `orbeon-form-builder-shared.jar`
    - `orbeon-xupdate.jar`
    - `orbeon-core.jar`    
    - `orbeon-form-runner.jar`
    - `orbeon-form-builder.jar`
    
To run tests from IntelliJ:

- from sbt, first run `test:compile`
- run "Unit Tests" from IntelliJ
    - there are issues, with the following tests failing:
        - `formRunnerStaticCache`
            - "Some(false) did not equal None"
        - `formRunnerItemsetActions`
            - "None.get"
        - `ResourcesPatcherTest.testResourcesConsistency`
            - "Cannot find resource: /apps/fr/i18n/resources.xml"
        - `MemoryCacheTest`
            - tries to connect to Selenium?
        - "Form Runner and Form Builder - Oracle view column names"
            - "Cannot find resource: /apps/fr/persistence/relational/sql-utils.xsl"
        - `VersionTest`
            because the version used is "2016.2-SNAPSHOT"

NOTES:

- compiling from IntelliJ doesn't work properly yet: files compile, but won't be copied to the exploded WAR

### With IntelliJ

#### Prerequisites

You need to have already installed:

- [git](http://git-scm.com/)
- [sbt](http://www.scala-sbt.org/) [SINCE 2015-08]
- [IntelliJ IDEA](http://www.jetbrains.com/idea/index.html) 14 (previous versions may or may not work)
- [Tomcat 7](http://tomcat.apache.org/download-70.cgi)
- Java 1.7

On macOS, you can install git and sbt easily if you have [Homebrew](http://brew.sh/):

```bash
brew install git
brew install ant
brew install sbt
```

With IntelliJ, you need the following plugins, which you can download and enable from IntelliJ directly:

- Scala: to compile Scala source code
- File Watchers: to compile `.less` files to CSS (if you make changes to those)

#### Opening the project

To open Orbeon Forms in IntelliJ:

- Go to the "File" → "Open Project" menu and  select the `orbeon-forms` directory.
- IntelliJ then indexes the project, which can take a minute the first time you do it.

#### Building the project

- Go to the "Build" → "Make Project" (this takes about 1 mn 10 seconds on a recent laptop).
- Go to the "Ant Build" pane and run the `orbeon-war` target.
- Manually create the directory `orbeon-forms/src/resources-local` if it is missing.

This builds Orbeon Forms in development mode, where the Java/Scala class files are not placed into JARs. This way you can quickly recompile incrementally.

In this mode, running the `orbeon-war` ant target skips compilation but processes resources and creates an "exploded" WAR file which Tomcat can point to.

#### Running Orbeon Forms

Create a new context in Tomcat's `server.xml`:

```xml
<Context
  path="/orbeon"
  docBase="/your/path/to/orbeon-forms/build/orbeon-war"
  reloadable="false"
  override="true"
  crossContext="true"
  allowLinking="true"/>
```

Then set `JAVA_OPTS` for Tomcat:

```bash
ORBEON_MEMORY_OPTS="-Xms300m -Xmx1000m -XX:MaxPermSize=256m -verbosegc -XX:+PrintGCDateStamps -XX:+PrintGCTimeStamps -XX:+PrintGCDetails"
ORBEON_DEBUG_OPTS="-Xdebug -Xnoagent -Djava.compiler=NONE -Xrunjdwp:transport=dt_socket,address=61155,suspend=n,server=y"
export JAVA_OPTS="-ea $ORBEON_MEMORY_OPTS $ORBEON_DEBUG_OPTS -Dapple.awt.UIElement=true"
```

*NOTE: You don't have to set `-verbosegc -XX:+PrintGCDateStamps -XX:+PrintGCTimeStamps -XX:+PrintGCDetails` if you are not interested in garbage collector output.*

Finally, you can start Tomcat with:

```bash
 ./apache-tomcat-7.0.53/bin/catalina.sh run
```

And test by going to:

`http://localhost:8080/orbeon/`

This will show the Orbeon Forms landing page.

#### Profiling

Alternatively, for running with the YourKit profiler, you need to set `DYLD_LIBRARY_PATH` and use a different `JAVA_OPTS` variable:

```bash
export DYLD_LIBRARY_PATH=/Applications/YourKit\ Java\ Profiler\ 12.0.6.app/bin/mac/
export JAVA_OPTS="$ORBEON_MEMORY_OPTS -agentlib:yjpagent"
```

Note that the debugging and profiling settings above are different: you either run in debug mode, or in profiling mode.

#### Making changes

If you modify Java or Scala files, you need to recompile. Go to menu "Build" → "Make Module 'Orbeon Forms'", or use the keyboard shortcut (`F7`).

If you are connected to Tomcat via the debugger AND you are lucky, changed classes will reload in the JVM via HotSwap. See also the IntelliJ doc on [Reloading Classes](https://www.jetbrains.com/help/idea/2017.3/reloading-classes.html). Otherwise, you need to restart Tomcat to see your changes.

*NOTE: HotSwap has limitations, especially with Scala code which produces and modifies more class files. It is not a silver bullet and there will be cases where you will need to redeploy the web application or restart the application server. But you will spare yourself a redeployment or restart in the cases where your modification to the Java or Scala code does not significantly change the structure of classes.*

If you modify resource files, re-run the ant `orbeon-war` target from IntelliJ.

If you make changes to `.less` files and want those recompiled automatically, you need the "File Watchers" IntelliJ plugin, as well as the less compiler. You can install it with:

```bash
brew install lessc
```

This installs the less compiler to `/usr/local/bin/lessc`, which is where the included IntelliJ "File Watchers" configuration points to. If it's in a different location, you'll need to adjust the path.

#### Running the tests

Select the "Unit Tests" configuration in IntelliJ, and run it. This should take about a minute.

IntelliJ then shows : "Done: 723 of 731  Failed: 8"

The tests that fail are the following:

- `CombinedClientTest`: this requires Selenium to be setup.
- `DDLTest` and `RestApiTest`: these require a database setup.

We hope to provide instructions to run these in the future.

### From the command line

#### Initial build

From the command line, you need [ant](http://ant.apache.org/) installed. Building should work with ant 1.8.x or 1.9.x.

You can then run, from the `orbeon-forms` directory:

```bash
ant orbeon-war
```

Currently, there is no incremental compilation when running from the command line. We hope to move to [sbt](http://www.scala-sbt.org/) in the future, see [#1585](https://github.com/orbeon/orbeon-forms/issues/1585). Because of this, Orbeon developers tend to build from IntelliJ, and build with ant on continuous integration build servers only.

A related known issue, from the command-line, is that running `ant classes` twice in a row fails with errors. You must remove classes under `build/classes` before running `ant classes` again.

*NOTE: With ant, class files are produced under `build/classes`, but with IntelliJ they are produced under `build/orbeon-war/WEB-INF/classes`. You should be aware of this is you switch between building from IntelliJ and building with ant.*

#### Building a distribution

*WARNING: `ant clean` deletes everything under the `build` directory. This includes the data for the embedded eXist database. If you have some test data, including form definitions and data in there, backup `build/orbeon-war/WEB-INF/exist-data` first!*

- run `ant clean`
- run `ant orbeon-dist-war` to build the WAR files

Alternatively:

- run `ant teamcity-release` to clean, test, and build the entire release

[//]: # (xxx GITHUB_TOKEN)
[//]: # (xxx IntelliJ and GITHUB_TOKEN)

## See also

- [Contributor License Agreements (CLA)](cla.md) 
- [Localizing Orbeon Forms](localizing-orbeon-forms.md)
