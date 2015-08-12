> [[Home]] ▸ Contributors ▸ [[Test Plan|Contributors ~ Test Plan]]

Check that all PE features are available in PE, but not in CE:

- features which are checked [4.9 done]
    - distribution: `orbeon-embedding.war` and `proxy-portlet.war` are not present
    - FB: no "Add Language" button
    - FB: check with CE that a PE dialog shows for
        - Services
        - Actions
        - Attach PDF
        - Attach Schema
        - Permissions
    - FR: PDF Template button doesn't show for DMV-14 and W-9
    - FR: TIFF button doesn't show even if configured
    - FR: Import page returns 404
    - FR: No remote server support in Form Runner home page
        - in `form-builder-permissions.xml` add `<role name="orbeon-user" app="*" form="*"/>`
        - in `properties-local.xml`
            - add `<property as="xs:string" name="oxf.fr.authentication.container.roles" value="orbeon-user"/>`
            - add remove servers, e.g.:
            
              ```xml
              <property as="xs:string"  name="oxf.fr.home.remote-servers">
                  [
                      { "label": "Public Demo Server", "url": "http://demo.orbeon.com/orbeon" },
                      { "label": "Local Liferay", "url": "http://localhost:9090/orbeon" }
                  ]
              </property>
              ```
        - in `web.xml` uncomment authentication section
        - access [http://localhost:8080/orbeon/fr/](http://localhost:8080/orbeon/fr/)
        - login with user with the `orbeon-user` role
        - check doesn't ask user for remote servers and only loads local form definitions
- features which are not checked
    - captcha
        - in `properties-local.xml` add
            - `<property as="xs:string" name="oxf.fr.detail.captcha.*.*" value="reCAPTCHA"/>`
            - the properties for the private/public key
        - access [http://localhost:8080/orbeon/fr/orbeon/bookshelf/new](http://localhost:8080/orbeon/fr/orbeon/bookshelf/new)
        - check the captcha isn't shown
    - other features listed on the [web site][1]
    - known issues
        - [#1043](https://github.com/orbeon/orbeon-forms/issues/1043) Disable noscript mode in CE version
        - [#1407][2] Accessible link changes the toolbar with CE builds
        - [#1927](https://github.com/orbeon/orbeon-forms/issues/1927) PE check not in place for captcha feature

[1]: http://www.orbeon.com/download
[2]: https://github.com/orbeon/orbeon-forms/issues/1407