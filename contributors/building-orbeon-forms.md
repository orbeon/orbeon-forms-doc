# Building Orbeon Forms

This page explains how to build Orbeon Forms:

- [With IntelliJ](#with-intellij)
- [From the command line](#from-the-command-line)

The following instructions are known to work as of 2014-05-20, which is between the Orbeon Forms 4.5 and 4.6 releases. If something is broken, please [let us know](https://twitter.com/intent/tweet?in_reply_to=orbeon&in_reply_to_status_id=261900968369729536&source=webclient&text=%40orbeon+)!

<!-- toc -->

## What the Orbeon Forms developers use

As of 2015-05-21:

- OS X Mavericks
- IntelliJ IDEA 14

*NOTE: There seems to be an issue compiling with IntelliJ 13. We recommend using IntelliJ 14 instead.*

## With IntelliJ

### Prerequisites

You need to have already installed:

- [git](http://git-scm.com/)
- [sbt](http://www.scala-sbt.org/) [SINCE 2015-08]
- [IntelliJ IDEA](http://www.jetbrains.com/idea/index.html) 14 (previous versions may or may not work)
- [Tomcat 7](http://tomcat.apache.org/download-70.cgi)
- Java 1.6

On OS X, you can install git and sbt easily if you have [Homebrew](http://brew.sh/):

```bash
brew install git
brew install sbt
```

With IntelliJ, you need the following plugins, which you can download and enable from IntelliJ directly:

- Scala: to compile Scala source code
- File Watchers: to compile `.less` files to CSS (if you make changes to those)

### Getting the source

If you have never obtained the Orbeon Forms source code, you need to get it [from github](https://github.com/orbeon/orbeon-forms). To get the latest code from the `master` branch, run the following command line:

```bash
git clone git@github.com:orbeon/orbeon-forms.git ~/my/projects/orbeon-forms
```

where `~/my/projects` is an existing directory on your system where you want to place the Orbeon Forms source code.

This clones the git repository into a child directory called `orbeon-forms`.

*NOTE: There is no guarantee that the master branch is stable, as it contains some of the latest changes to Orbeon Forms!*

### Opening the project

To open Orbeon Forms in IntelliJ:

- Go to the "File" → "Open Project" menu and  select the `orbeon-forms` directory.
- IntelliJ then indexes the project, which can take a minute the first time you do it.

### Building the project

- Go to the "Build" → "Make Project" (this takes about 1 mn 10 seconds on a recent laptop).
- Go to the "Ant Build" pane and run the `orbeon-war` target.
- Manually create the directory `orbeon-forms/src/resources-local` if it is missing.

This builds Orbeon Forms in development mode, where the Java/Scala class files are not placed into JARs. This way you can quickly recompile incrementally.

In this mode, running the `orbeon-war` ant target skips compilation but processes resources and creates an "exploded" WAR file which Tomcat can point to.

### Running Orbeon Forms

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

### Debugging

The settings above start Tomcat in debug mode. This means that you can debug the Java and Scala code from IntelliJ. Select the "Tomcat" configuration in IntelliJ, then "Run" → "Debug". IntelliJ opens the debugging window and connects to Tomcat. You can then set breakpoints and do the usual things one does with a debugger!

### Profiling

Alternatively, for running with the YourKit profiler, you need to set `DYLD_LIBRARY_PATH` and use a different `JAVA_OPTS` variable:

```bash
export DYLD_LIBRARY_PATH=/Applications/YourKit\ Java\ Profiler\ 12.0.6.app/bin/mac/
export JAVA_OPTS="$ORBEON_MEMORY_OPTS -agentlib:yjpagent"
```

Note that the debugging and profiling settings above are different: you either run in debug mode, or in profiling mode.

### Running without debugging or profiling

Alternatively, for running without debugging, set instead:

```bash
export JAVA_OPTS="$ORBEON_MEMORY_OPTS -server"
```

### Making changes

If you modify Java or Scala files, you need to recompile. Go to menu "Build" → "Make Module 'Orbeon Forms'", or use the keyboard shortcut (`F7`).

If you are connected to Tomcat via the debugger AND you are lucky, changed classes will reload in the JVM via HotSwap. See also the IntelliJ doc on [Reloading Classes](http://www.jetbrains.com/idea/webhelp/reloading-classes.html). Otherwise, you need to restart Tomcat to see your changes.

*NOTE: HotSwap has limitations, especially with Scala code which produces and modifies more class files. It is not a silver bullet and there will be cases where you will need to redeploy the web application or restart the application server. But you will spare yourself a redeployment or restart in the cases where your modification to the Java or Scala code does not significantly change the structure of classes.*

If you modify resource files, re-run the ant `orbeon-war` target from IntelliJ.

If you make changes to `.less` files and want those recompiled automatically, you need the "File Watchers" IntelliJ plugin, as well as the less compiler. You can install it with:

```bash
brew install lessc
```

This installs the less compiler to `/usr/local/bin/lessc`, which is where the included IntelliJ "File Watchers" configuration points to. If it's in a different location, you'll need to adjust the path.

### Running the tests

Select the "Unit Tests" configuration in IntelliJ, and run it. This should take about a minute.

IntelliJ then shows : "Done: 723 of 731  Failed: 8"

The tests that fail are the following:

- `CombinedClientTest`: this requires Selenium to be setup.
- `DDLTest` and `RestApiTest`: these require a database setup.

We hope to provide instructions to run these in the future.

## From the command line

### Initial build

From the command line, you need [ant](http://ant.apache.org/) installed. Building should work with ant 1.8.x or 1.9.x.

You can then run, from the `orbeon-forms` directory:

```bash
ant orbeon-war
```

Currently, there is no incremental compilation when running from the command line. We hope to move to [sbt](http://www.scala-sbt.org/) in the future, see [#1585](https://github.com/orbeon/orbeon-forms/issues/1585). Because of this, Orbeon developers tend to build from IntelliJ, and build with ant on continuous integration build servers only.

A related known issue, from the command-line, is that running `ant classes` twice in a row fails with errors. You must remove classes under `build/classes` before running `ant classes` again.

*NOTE: With ant, class files are produced under `build/classes`, but with IntelliJ they are produced under `build/orbeon-war/WEB-INF/classes`. You should be aware of this is you switch between building from IntelliJ and building with ant.*

### Building a distribution

*WARNING: `ant clean` deletes everything under the `build` directory. This includes the data for the embedded eXist database. If you have some test data, including form definitions and data in there, backup `build/orbeon-war/WEB-INF/exist-data` first!*

- run `ant clean`
- run `ant orbeon-dist-war` to build the WAR files

Alternatively:

- run `ant teamcity-release` to clean, test, and build the entire release

## About building Orbeon Forms PE

Orbeon does not provide public instructions or code to build Orbeon Forms PE, which is a commercial, supported build of the product. If you are a PE customer, contact Orbeon at info@orbeon.com.

The idea is just that we want to ensure that something called "Orbeon Forms PE" in fact comes from Orbeon.
