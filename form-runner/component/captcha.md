# Captcha components



## Which captcha is right for you

[reCAPTCHA][1] is almost a de facto standard on the web: more than a million reCAPTCHA are solved every day, it is used by a large number of mainstream sites, like Facebook, and is constantly updated providing a high level of security. Since 2009, this service is owned by Google.

Because of the high level of safety provided by reCAPTCHA, we recommend you use it, unless doing so isn't possible in your situation. Maybe, for instance, you don't want the server on which Orbeon Forms runs to connect to any external service, which the reCAPTCHA component does to verify the answer provided. If you can't use the reCAPTCHA, Orbeon Forms provides an alternate component, SimpleCaptcha, which runs entirely within Orbeon Forms, without the need to connect to any external server.

## Events

Both the `fr:recaptcha` and `fr:simple-captcha` components support the same events:

1. **Verifying the answer entered by users** — Both components don't include a _Verify_ button that triggers the value entered by users to be checked. This is to give more control to you, the form author, as to when the verification is done. For instance, you might want to verify the captcha when users click on a _Save_ button on your form. To trigger the value to be verified, dispatch a `fr-verify` event to the captcha.
2. **Verification succeeded** — When the verification succeeds, the component dispatches a `fr-verify-done` event. The example below, using the reCAPTCHA, listens to that event to run a submission.
3. **Verification failed** — When the verification fails, you get the `fr-verify-error` event. The example below, using the reCAPTCHA, listens to that event to show a case id `failure-case` (which might tell users the verification failed).
4. **Loading another captcha** — Specifically for the reCAPTCHA, as part of the context information for the fr-verify-error event, you get an error code, which you can access with `event('fr-error-code')`. This is the error code returned by the reCAPTCHA API, which is a string. Its value can either be:
    * `empty` — this tells you users didn't provide any answer. When that happens, you could notify users and keep the same challenge.
    * One of the values listed in the [reCAPTCHA API documentation][2] (look for the table under _Error Code Reference_). When this happens, you could notify users, and _need_ to change the challenge by dispatching `fr-reload` to the reCAPTCHA. (For added safety, the reCAPTCHA won't let users try to solve the same captcha multiple times.)

    ```xml
    <fr:recaptcha id="my-captcha">
        <xf:send ev:event="fr-verify-done" submission="save-submission"/>
        <xf:action ev:event="fr-verify-error">
            <xf:toggle case="failure-case"/>
            <xf:dispatch target="my-captcha" name="fr-reload"/>
        </xf:action>
    </fr:recaptcha>
    ```

## reCAPTCHA

### Supported version

Orbeon Forms supports the reCAPTCHA v2 since Orbeon Forms 2018.1 and 2017.2.2. Google [stopped supporting the reCAPTCHA v1](https://developers.google.com/recaptcha/docs/faq#what-happens-to-recaptcha-v1) used by earlier versions of Orbeon Forms after March 31, 2018. Hence, if you're using the reCAPTCHA, and are using an older version of Orbeon Forms, you'll want to upgrade to a newer version which supports reCAPTCHA v2.

### Usage

You can use this component to show users a captcha, like the one shown in the following screen shot:

<img alt="" src="images/xbl-recaptcha.gif" width="308">

1. First, you need to [sign up with reCAPTCHA][4] to get your own public/private key pair.
2. Store your public and private keys in your properties, as follows:

    ```xml
    <property
        as="xs:string"
        name="oxf.xforms.xbl.fr.recaptcha.public-key"
        value="..."/>
    <property
        as="xs:string"
        name="oxf.xforms.xbl.fr.recaptcha.private-key"
        value="..."/>
    ```

3. Add the reCAPTCHA component to your form:

	- For forms you're creating in Form Builder, enable the reCAPTCHA by setting the following property:

		```xml
		<property
		    as="xs:string" 
		    name="oxf.fr.detail.captcha.*.*" 
		    value="reCAPTCHA"/>
		```

	- For forms you're creating by writing XForms "by hand", add the component to your form as follows. You'll also want to add handlers for the `fr-verify-done` and maybe `fr-verify-error` events.

    	```xml
    	<fr:recaptcha id="my-recaptcha"/>
    	```

### Configuration

You can configure:

- The theme using properties, either:
	- For forms you're creating with Form Builder, with:
		```xml
		<property 
		    as="xs:string" 
		    name="oxf.xforms.xbl.fr.recaptcha.theme.*.*" 
		    value="light"/>
		```
	- For forms you're creating by writing XForms "by hand", with:
		```xml
		<property 
		    as="xs:string" 
		    name="oxf.xforms.xbl.fr.recaptcha.theme"     
		    value="light"/>
		```
- The language with the `lang` attribute on the `<html>` element.

See the [reCAPTCHA documentation][5], under _Look &amp; Feel Customizations_ for more information on the possible values for the `theme` and `lang` properties.

### SimpleCaptcha

You can use this component to show users a captcha, like the one shown in the following screen shot:

![](images/xbl-simple-captcha.png)

To use this component, where you want the captcha to show in your form, add:

```xml
<fr:simple-captcha id="my-simple-captcha">
```

Most likely, you'll want to add code dispatching an `fr-verify` event to your component to trigger a verification, and listeners on the `fr-verify-done` and `fr-verify-error` events. For more information on this, see the section _Events_ above.

## See also

- [Stop spammers by adding a CAPTCHA to your forms](http://blog.orbeon.com/2011/12/stop-spammer-by-adding-captcha-to-your.html)

[1]: http://en.wikipedia.org/wiki/ReCAPTCHA
[2]: https://developers.google.com/recaptcha/docs/verify?csw=1
[4]: https://www.google.com/recaptcha/admin#list
[5]: https://developers.google.com/recaptcha/docs/display
