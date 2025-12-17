# Captcha

## Introduction

If you are creating a public form, you might want to add a [captcha](https://en.wikipedia.org/wiki/CAPTCHA) to avoid spam. You can do so by enabling the Orbeon Forms _captcha_ feature. Under this umbrella, Orbeon Forms supports several captcha implementations.

## Which captcha is right for you

[reCAPTCHA](https://en.wikipedia.org/wiki/ReCAPTCHA) is almost a de facto standard on the web: more than a hundred million reCAPTCHA are solved every day, it is used by a large number of mainstream sites and is constantly updated providing a high level of security. This service is owned by Google.

Because of the high level of safety provided by reCAPTCHA, we recommend you use it, unless doing so isn't possible in your situation. Maybe, for instance, you don't want the server on which Orbeon Forms runs to connect to any external service, which the reCAPTCHA component does to verify the answer provided. If you can't use the reCAPTCHA, Orbeon Forms provides an alternate component which runs entirely within Orbeon Forms, without the need to connect to any external server.

[\[SINCE Orbeon Forms 2023.1.4\]](../../release-notes/orbeon-forms-2023.1.4.md)

Using [Friendly Captcha](https://friendlycaptcha.com/) is another solid option. Friendly Captcha is an alternative to Google's reCAPTCHA and, in their own words, "GDPR-Compliant Bot Protection".

## Enabling and choosing a component

You enable a captcha by adding the following property to your `properties-local.xml`. For example:

\[SINCE Orbeon Forms 2020.1]

```xml
<property
    as="xs:string"
    name="oxf.fr.detail.captcha.component.*.*"  
    value="fr:recaptcha"
    xmlns:fr="http://orbeon.org/oxf/xml/form-runner"/>
```

\[UNTIL Orbeon Forms 2019.2]

```xml
<property
    as="xs:string"
    name="oxf.fr.detail.captcha.*.*"
    value="reCAPTCHA"/>
```

The property name changed in Orbeon Forms 2020.1 with the introduction of new captcha-related properties (more on this below). The old property name, without the `component` part, is still supported, but deprecated. Allowed values are:

* Blank or empty string: no captcha is added to your form (default)
* `fr:recaptcha`
  * This enables [reCAPTCHA](https://en.wikipedia.org/wiki/ReCAPTCHA).
  * You can also use the deprecated token `reCAPTCHA` for backward compatibility.
  * Make sure that, at the top of your properties-local.xml file, you have `xmlns:fr="http://orbeon.org/oxf/xml/form-runner"` defined (this should be present by default).
* `fr:friendly-captcha`
  * This enables [Friendly Captcha](https://friendlycaptcha.com/).
  * [\[SINCE Orbeon Forms 2023.1.4\]](../../release-notes/orbeon-forms-2023.1.4.md)
  * make sure that, at the top of your properties-local.xml file, you have `xmlns:fr="http://orbeon.org/oxf/xml/form-runner"` defined (this should be present by default)
* `fr:on-premise-captcha`
  * [\[SINCE Orbeon Forms 2023.1.1\]](../../release-notes/orbeon-forms-2023.1.1.md)
  * This enables the [Katpcha](https://github.com/youkol/kaptcha) implementation.
  * You can also use the deprecated tokens `OnPremiseCaptcha` or `SimpleCaptcha` for backward compatibility.
  * Make sure that, at the top of your properties-local.xml file, you have `xmlns:fr="http://orbeon.org/oxf/xml/form-runner"` defined (this should be present by default).
* `SimpleCaptcha`
  * [\[DEPRECATED SINCE Orbeon Forms 2023.1.1\]](../../release-notes/orbeon-forms-2023.1.1.md)
  * [\[UNTIL Orbeon Forms 2023.1\]](../../release-notes/orbeon-forms-2023.1.md)
    * This uses the SimpleCaptcha implementation.
  * [\[SINCE Orbeon Forms 2023.1.1\]](../../release-notes/orbeon-forms-2023.1.1.md)
    * This uses to the [Katpcha](https://github.com/youkol/kaptcha) implementation.
    * If you use Orbeon Forms 2023.1.1 or newer, use `fr:on-premise-captcha` instead.
* Qualified name of an XBL component that implements a captcha
  * \[SINCE Orbeon Forms 2017.2]
  * Example: `acme:custom-captcha`
  * When doing so, you need to have a namespace defined in your property file for the component prefix you're using, say `xmlns:acme="http://acme.org/"`.

If using the reCAPTCHA, you'll also need to add properties to specify your reCAPTCHA public and private keys.

## Captcha implementations

### reCAPTCHA

#### Supported versions

Orbeon Forms supports the reCAPTCHA v2 since Orbeon Forms 2018.1 and 2017.2.2, and reCAPTCHA v3 since Orbeon Forms 2024.1. Google [stopped supporting reCAPTCHA v1](https://developers.google.com/recaptcha/docs/faq#what-happens-to-recaptcha-v1) used by earlier versions of Orbeon Forms after March 31, 2018. Hence, if you're using the reCAPTCHA, and are using an older version of Orbeon Forms, you'll want to upgrade to a newer version which supports reCAPTCHA v2 or v3.

#### Usage

You can use this component to show users a captcha, like the one shown in the following screenshot:

<figure><img src="../../.gitbook/assets/xbl-recaptcha.gif" alt="" width="308"><figcaption></figcaption></figure>

1. First, you need to [sign up with reCAPTCHA](https://www.google.com/recaptcha/admin#list) to get your own public/private key pair.
2. Store your public keys (now called "site keys") and private keys in your properties, as follows:
   *   [\[SINCE Orbeon Forms 2024.1\]](../../release-notes/orbeon-forms-2024.1.md)

       ```xml
       <property
           as="xs:string"
           name="oxf.xforms.xbl.fr.recaptcha.v2.public-key"
           value="..."/>
       <property
           as="xs:string"
           name="oxf.xforms.xbl.fr.recaptcha.v2.private-key"
           value="..."/>
       <property
           as="xs:string"
           name="oxf.xforms.xbl.fr.recaptcha.v3.public-key"
           value="..."/>
       <property
           as="xs:string"
           name="oxf.xforms.xbl.fr.recaptcha.v3.private-key"
           value="..."/>
       ```
   *   \[UNTIL Orbeon Forms 2023.1]

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
   *   For forms you're creating in Form Builder, enable the reCAPTCHA by setting the following property:

       ```xml
       <property
           as="xs:string" 
           name="oxf.fr.detail.captcha.*.*" 
           value="reCAPTCHA"/>
       ```
   *   For forms you're creating by writing XForms "by hand", add the component to your form as follows. You'll also want to add handlers for the `fr-verify-done` and maybe `fr-verify-error` events.

       ```xml
       <fr:recaptcha id="my-recaptcha"/>
       ```

#### Configuration

You can configure:

* The theme using properties, either:
  *   For forms you're creating with Form Builder, with:

      ```xml
      <property 
          as="xs:string" 
          name="oxf.xforms.xbl.fr.recaptcha.theme.*.*" 
          value="light"/>
      ```
  *   For forms you're creating by writing XForms "by hand", with:

      ```xml
      <property 
          as="xs:string" 
          name="oxf.xforms.xbl.fr.recaptcha.theme"     
          value="light"/>
      ```
* The language with the `lang` attribute on the `<html>` element.
*   When a control is invalid, an alert message appears directly below it. This applies to the reCAPTCHA as well. However, the error summary typically appears just below the reCAPTCHA, causing the alert to be shown twice: once below the reCAPTCHA and again in the summary. To address this, the reCAPTCHA alert is hidden by default using CSS. If you want the alert to be visible below the reCAPTCHA, perhaps because you're not showing the error summary, you can use the following CSS:

    ```css
    .orbeon .fr-captcha .xbl-fr-recaptcha.xforms-visited .xforms-alert.xforms-active {
    	display: block;
    }
    ```
*   The score threshold (for reCAPTCHA v3 only). When verifying the response, the reCAPTCHA API returns a score between 0 and 1. If the score is higher than the threshold, the verification is considered successful. If it is lower, the component will switch to reCAPTCHA v2 (visible reCAPTCHA) for further verification. The default score threshold is 0.5. You can customize this threshold with the following property:

    ```xml
    <property
        as="xs:decimal"
        name="oxf.xforms.xbl.fr.recaptcha.v3.score-threshold"
        value="0.5"/>
    ```

See the [reCAPTCHA documentation](https://developers.google.com/recaptcha/docs/display), under _Look & Feel Customizations_ for more information on the possible values for the `theme` and `lang` properties.

#### Resetting the captcha

When users submit a form with a captcha and subsequently navigate away from the page containing that form, they can use their browser's back functionality to return to the form. By default, the captcha will appear as already resolved. In some cases, you might prefer to require users to complete a new captcha for each submission they make. If so, you'll want to _reset_ the captcha.

[\[SINCE Orbeon Forms 2024.1\]](../../release-notes/orbeon-forms-2024.1.md)

Add the following to the process you are running, before the action that navigates away from the page.

```xml
then captcha-reset
then navigate(uri = "https://www.orbeon.com")
```

\[SINCE Orbeon Forms 2022.1.8, 2023.1.3, 2024.1]

Add the following to the process you are running, before the action that navigates away from the page.

```xml
then xf:dispatch(
	name     = 'fr-reload',
	targetid = 'fr-captcha'
)
```

### On-premise captcha

#### Usage

You can use this component to show users a simple captcha like the one shown in the following screenshot:

![](../../.gitbook/assets/xbl-simple-captcha.png)

Note that this doesn't offer as much security as reCAPTCHA, for example, but it is entirely handled by Orbeon Forms and does not call external services.

#### Configuration

You enable the simple on-premise captcha by adding the following property to your `properties-local.xml`:

[\[SINCE Orbeon Forms 2023.1.1\]](../../release-notes/orbeon-forms-2023.1.1.md)

```xml
<property
    as="xs:string"
    name="oxf.fr.detail.captcha.component.*.*"
    value="OnPremiseCaptcha"/>
```

**NOTE: The older `SimpleCaptcha` value is still accepted but deprecated.**

[\[SINCE Orbeon Forms 2020.1\]](../../release-notes/orbeon-forms-2020.1.md) [\[UNTIL Orbeon Forms 2023.1\]](../../release-notes/orbeon-forms-2023.1.md)

```xml
<property
    as="xs:string"
    name="oxf.fr.detail.captcha.component.*.*"
    value="SimpleCaptcha"/>
```

[\[UNTIL Orbeon Forms 2019.2\]](../../release-notes/orbeon-forms-2019.2.md)

```xml
<property
    as="xs:string"
    name="oxf.fr.detail.captcha.*.*"
    value="SimpleCaptcha"/>
```

#### XForms usage

To use this component with plain XForms, where you want the captcha to show in your form, add:

```xml
<fr:simple-captcha id="my-simple-captcha"/>
```

Most likely, you'll want to add code dispatching an `fr-verify` event to your component to trigger a verification, and listeners on the `fr-verify-done` and `fr-verify-error` events. For more information on this, see the section _Events_ below.

### Friendly Captcha

[\[SINCE Orbeon Forms 2023.1.4\]](../../release-notes/orbeon-forms-2023.1.4.md)

Similar to reCAPTCHA, you must configure properties in `properties-local.xml`. You start by enabling Friendly Captcha by setting the following property:

```xml
<property
    as="xs:string"
    name="oxf.fr.detail.captcha.component.*.*"
    value="fr:friendly-captcha"/>
```

You must configure keys, which you obtain by signing up with [Friendly Captcha](https://friendlycaptcha.com/) and following their [documentation](https://docs.friendlycaptcha.com/).

The public key (or application, or "site key") is set with:

```xml
<property
	as="xs:string"
	name="oxf.xforms.xbl.fr.friendly-captcha.public-key"
	value="..."/>
```

The private key is set with:

```xml
<property
	as="xs:string"
	name="oxf.xforms.xbl.fr.friendly-captcha.private-key"
	value="..."/>
```

The private key will remain on the Orbeon Forms server and be sent server-side to the Friendly Captcha API endpoint to verify the captcha. It will never be shown to the end-user.

As of Orbeon Forms 2023.1.4, Orbeon Forms uses version 0.9.16 of the Friendly Captcha widget. You can change the URL to the widget script with the following property:

```xml
<property
    as="xs:string"
    name="oxf.xforms.xbl.fr.friendly-captcha.script-url"
    value="https://cdn.jsdelivr.net/npm/friendly-challenge@0.9.16/widget.min.js"/>
```

You can configure the Friendly Captcha start mode with the following property:

```xml
<property 
    as="xs:string" 
    name="oxf.xforms.xbl.fr.friendly-captcha.start-mode"                
    value="auto"/>
```

Allowed values:

* `auto`: the solver starts as soon as possible after the page loads (the default)
* `focus`: the solver starts when the user focuses in the form or presses the start button in the widget
* `none`: the solver only starts when the user presses the start button

## Advanced configuration

### Captcha location

#### Configuration

\[SINCE Orbeon Forms 2020.1]

The following property allows you to configure where the captcha is shown, if enabled. The possible values are:

* `form-bottom`: displays the captcha at the bottom of the form (default).
* `inside-wizard`: displays the captcha on every page of the wizard.
* `inside-wizard-first-page`: [\[SINCE Orbeon Forms 2024.1\]](../../release-notes/orbeon-forms-2024.1.md) displays the captcha on the first visible page of the wizard.
* `inside-wizard-last-page`: [\[SINCE Orbeon Forms 2024.1\]](../../release-notes/orbeon-forms-2024.1.md) displays the captcha on the last visible page of the wizard.

Only use `inside-wizard`, `inside-wizard-first-page`, or `inside-wizard-last-page` if your form is using the [wizard view](../feature/wizard-view.md). If you use one of these tokens without the wizard view, the captcha will not be displayed.

```xml
<property 
    as="xs:string"  
    name="oxf.fr.detail.captcha.location.*.*"                         
    value="inside-wizard"/>
```

#### Limitations

When all the following conditions are met:

* The captcha is shown inside the wizard.
* You have set the `oxf.fr.detail.captcha.visible.*.*` property (see below) to have the captcha show only on certain pages.
* Users attempt to save or submit the form, or otherwise perform an action that requires data to be valid.
* The captcha hasn't been solved yet.

Then:

* While an error will show in the error summary to inform users that the captcha needs to be solved, when users click on the error message, Form Runner will not switch to the page where the captcha appears. (The situation here is somewhat different relative to what happens with normal fields, as depending on how you set the `oxf.fr.detail.captcha.visible.*.*` property, the captcha could appear on multiple pages.)
* The page or pages in which the captcha appear won't be highlighted, as is the case for other invalid fields.

### Captcha visibility

\[SINCE Orbeon Forms 2020.1]

When the captcha is enabled, you can control its visibility with the following property. The default value is `true` (shown below), and you can use a [value template](../../xforms/attribute-value-templates.md), to make it dynamic.

Even when not visible according this property (i.e. your value template returns `false`), as long as the captcha is enabled, solving it is required; so this property isn't intended to be used to dynamically decide whether to have a captcha on a page or not, but it is to decide at what point during the form filling process you want the captcha to show.

```xml
<property 
    as="xs:string"  
    name="oxf.fr.detail.captcha.visible.*.*"                          
    value="true"/>
```

### Events

The `fr:recaptcha`, `fr:on-premise-captcha` (formerly:`fr:simple-captcha`), and `fr:friendly-captcha` components support the same events:

1. **Verifying the answer entered by users** — Some implementations don't include a _Verify_ button that triggers the value entered by users to be checked. This is to give more control to you, the form author, as to when the verification is done. For instance, you might want to verify the captcha when users click on a _Save_ button on your form. To trigger the value to be verified, dispatch a `fr-verify` event to the captcha.
2. **Verification succeeded** — When the verification succeeds, the component dispatches a `fr-verify-done` event. The example below, using the reCAPTCHA, listens to that event to run a submission.
3. **Verification failed** — When the verification fails, you get the `fr-verify-error` event. The example below, using the reCAPTCHA, listens to that event to show a case id `failure-case` (which might tell users the verification failed).
4.  **Loading another captcha** — Specifically for the reCAPTCHA, as part of the context information for the fr-verify-error event, you get an error code, which you can access with `event('fr-error-code')`. This is the error code returned by the reCAPTCHA API, which is a string. Its value can either be:

    * `empty` — this tells you users didn't provide any answer. When that happens, you could notify users and keep the same challenge.
    * One of the values listed in the [reCAPTCHA API documentation](https://developers.google.com/recaptcha/docs/verify?csw=1) (look for the table under _Error Code Reference_). When this happens, you could notify users, and _need_ to change the challenge by dispatching `fr-reload` to the reCAPTCHA. (For added safety, the reCAPTCHA won't let users try to solve the same captcha multiple times.)

    ```xml
    <fr:recaptcha id="my-captcha">
        <xf:send ev:event="fr-verify-done" submission="save-submission"/>
        <xf:action ev:event="fr-verify-error">
            <xf:toggle case="failure-case"/>
            <xf:dispatch target="my-captcha" name="fr-reload"/>
        </xf:action>
    </fr:recaptcha>
    ```

## See also

* [Stop spammers by adding a CAPTCHA to your forms](https://blog.orbeon.com/2011/12/stop-spammer-by-adding-captcha-to-your.html)
