> [[Home]] ▸ Contributors ▸ [[Test Plan|Contributors ~ Test Plan]]

- README.md is up to date
  - links not broken (use Marked to save HTML, then check w/ Integrity)
  - latest release year
  - version number is correct
  - links to release notes (include link to new version even if blog post not up yet)
- file layout is correct in zip and war
- check WAR files have reasonable sizes (sizes as of 4.8)
  - orbeon-auth.war (< 10 KB)
  - orbeon-embedding.war (1-2 MB)
  - proxy-portlet.war (1-2 MB)
  - orbeon.war (65 MB)
- check CE zip doesn't have
  - orbeon-embedding.war
  - proxy-portlet.war
- dropping the WAR file (with license included or in ~/.orbeon/license.xml) into Tomcat and Liferay works - out of the box
- make sure the PE license is not included