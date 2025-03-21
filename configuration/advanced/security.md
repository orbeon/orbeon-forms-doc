# Security
  
## Rationale

Orbeon Forms is used by health care, financial companies, government entities, and other organizations for which security is paramount. So we take security very seriously and believe Orbeon Forms provides a solid foundation in terms of security. What follows goes through common attacks and what Orbeon Forms is doing to counter those attacks.  

## Type of security flaws

### Buffer overflows

Until recently, the most publicly reported security flaws were [buffer overflows][1]. The server-side code in Orbeon Forms is entirely written in Java and Scala, languages which performs bounds checking and other measures to shield programs from similar issues.

### Cross site scripting

Cross-site scripting ([XSS][2]) attacks come from the application taking some users' input, through form elements, request parameters, or otherwise, and displaying it on the page without proper escaping, thus allowing a malicious user to inject code into the page. To prevent this, Orbeon Forms:  

* always encodes content provided by the user
* the content is stored in a safe container: XML
* uses standard XML parsers and serializers (which do proper escaping)  to read/write the data   
* when users can enter rich content (HTML), Orbeon Forms automatically performs [HTML cleanup][3] on the data provided by users even before it reaches your application, so only HTML known to be safe is kept
* control values sent to the server are never inserted literally into queries

### HTML sanitization

[\[SINCE Orbeon Forms 2024.1.1\]](/release-notes/orbeon-forms-2024.1.1.md)

Form Builder automatically sanitizes HTML content entered by form authors, including HTML labels, hints, and help messages.

This helps security by preventing a malicious or inattentive form author to inject script elements into a form, for example.  

In addition, all Orbeon Forms HTML output goes through HTML sanitization as well.

### Authentication and cookie security

Orbeon Forms doesn't handle aspects of the infrastructure that can be handled in a better, more versatile, and more secure way by your application server. For instance, Orbeon Forms doesn't do user authentication itself, but relies on your application server. In a similar manner, it doesn't keep track of users' sessions, but leaves that to the application server, which, say, you could be setup to tie cookies to IP addresses to prevent cookie stealing.  

### Modification of the internal state

#### Rationale

Your XForms manipulates on a number of XML documents called _instances_ in XForms. You capture the data users enter in instances, but also use instances to store the internal state of your XForms pages. Some XForms implementations expose those instances to the client (the browser, a plug-in running in the browser, or JavaScript running in the browser). When this happens, malicious users can access and modify your application internal state, potentially leading to security flaws.  

#### Direct access to XForms instance data

XForms state is kept on the server, so it is just not exposed to users. Only the values that are displayed to users are sent the browsers, and only values the user is authorized to change will be taken into account when received from the browser.

_NOTE: There is a deprecated option to keep state on the client. In this case, it is encrypted with a password you set. The password doesn't leave the server, and even if the encrypted value is visible to the client, it cannot be decrypted by users and can only be decrypted on the server with a valid key._

#### Indirect access to XForms instance data via the Fetch API

The Orbeon Forms XForms engine typically interacts with the client using Fetch API requests (also known as XHR or Ajax requests) sent from the client browser. These requests are protected as follows:

* Fetch requests are only allowed to modify controls, not XML data directly.
* Fetch requests are only allowed to modify visible, read-write controls.
* Each page has a unique UUID generated each time the page is produced. The page has a finite lifetime, typically expiring with the user's session. Once the session has expired, any attempt to use that UUID fails.
* Each Fetch request has a unique sequence number. The server rejects incorrect sequence numbers. This prevents simply replaying incoming requests.
* Only requests via `POST` can have a side-effect on Orbeon Forms internal state. This excludes attacks via simply loading a URL via an image or a link.

### Cross site request forgery

Cross-site request forgery (XSRF) can be understood in 2 ways.

The first way is the risk of using Orbeon to perform CSRF attacks on another site. This relies on the ability for users to inject content into an Orbeon Forms page. Orbeon takes steps to protect pages against this, as documented above.

The second way is the risk of using another site to trick Orbeon into doing something bad, such as modifying instance data or calling services.

The key trick with CSRF is that the third-party site contains URLs, or is able to control a form submission, that can target Orbeon Forms into performing actions that otherwise would only be possible by the user of Orbeon Forms.

Based on our understanding of CSRF, the measures above appear to make an XSRF attack difficult. The user would need to be able to `POST` via Fetch or an HTML form, and to guess a valid UUID and sequence number. Even so, in this case only actions that the user of the form could perform would be possible. Note that such some actions could be dangerous: for example a "delete" button on the page could do harm. However again this would require the attacker to guess a lot about the possible requests first.

The [Wikipedia page about CSRF][4] confirms that some of the measures above are effective:

* "Requiring a secret, user-specific token in all form submissions and side-effect URLs prevents CSRF; the attacker's site cannot put the right token in its submissions"  This is handled by the unique UUID and request number used by Orbeon in all requests.
* "`GET` requests never have a permanent effect" is implemented by Orbeon Forms.

### Communication with services

XForms pages communicate with the "outside world", say to load data initially shown in your page or to save data entered by users, by calling _services_. Services are usually HTTP services, such as REST or web services. These services typically implement your application backend logic: they provide data to your form and receive data from your form. With some XForms implementations, the calls to those services are made from the browser. This can potentially pose a significant security risk: it means you can't keep those services behind your firewall, and that the user can doctor the data sent to the services.

With Orbeon Forms, call to services are made from the Orbeon Forms server. You can keep the services running behind your firewall, and users won't be able to doctor the data sent to the services, or even see what that data is.  

### Constraints on selection controls

Forms often constraint the values that can be entered by users with drop-downs, lists, radio buttons, or checkboxes. Say users need to rate a service and you provide a radio buttons they can select to choose a grade from 1 to 5. The values for the radio buttons will be 1, 2, 3, 4, and 5. In most web applications, users can easily doctor the value they send back to the server, and send, say 100. If you don't perform in your server-side code an additional check on the value received from the browser, you will take the 100 at face value, and users will be able to game an average of all the ratings you later compute based on that value.

With Orbeon Forms, the values you choose to use to represent the different items in drop-downs, lists, radio buttons, and checkboxes are encoded (either encrypted or represented as an opaque number) before they are sent to the browser. So not only they can't be doctored by users, but they won't even be sent to the browser, and you don't incur the risk of malicious users injecting an out-of-range value.

When the option of not encoding such values is chosen, the server still checks that the incoming value is allowed and rejects it if that is not the case. 

[1]: https://en.wikipedia.org/wiki/Buffer_overflow
[2]: https://en.wikipedia.org/wiki/Cross-site_scripting
[3]: /xforms/controls/textarea.md#html-cleanup
[4]: https://en.wikipedia.org/wiki/Cross-site_request_forgery

## See also 

- [Content-Security-Policy header](content-security-policy.md)
