> [[Home]] ▸ [[Form Runner|Form Runner]]

- [Situation](#situation)
- [Techniques](#techniques)
    - [1. Send data on submit](#1-send-data-on-submit)
    - [2. Call the REST API](#2-call-the-rest-api)
    - [3. Accessing the database](#3-accessing-the-database)

## Situation

You've created forms with Form Builder, published those forms, and setup Orbeon Forms so it [stores data captured by the forms in your relational database](Installation-~-Relational-Database-Setup). Now, how can another app of yours access this data?

![Accessing data - How](https://orbeon.mybalsamiq.com/mockups/3495508.png?key=409bf6fda74861c325ab1cbb3f99d1ac269a20b6)

In what follows, you'll see  3 techniques for doing so:

1. Having Orbeon Forms send the data to your app when users click on the submit button in your form.
2. Your app calling a REST API provided by Orbeon Forms for this purpose.
3. Your app accessing the data directly in the database where it is saved by Orbeon Forms.

## Techniques

### 1. Send data on submit

In most cases, this is the best option, and the one we recommend. In essence, you setup Orbeon Forms so when users fill out a form and submit it, Orbeon Forms sends the data users entered to your app. Your app can do whatever it wants with this data, and if needed, in the response to Orbeon Forms, your app can tell Orbeon Forms which page the user should go to next.

![Doc - Accessing data - Process - Overview](https://orbeon.mybalsamiq.com/mockups/3496362.png?key=0de5fdf28d9bff939a0bef381754c6bf57a271a7)

Let's see in more details what this entails:

1. When users click the *submit* button on a form created in Form Builder (or for that matter any other button at the bottom of the form), a *process* runs. In essence, a *process* defines a sequence of actions to be performed, and one of them can be to *send* the data to your app. Currently, processes are defined in your [`properties-local.xml`](Installation-~-Configuration-Properties). To learn more about processes, see the documentation on [Buttons and Processes](Form-Runner-~-Buttons-and-Processes).
2. In your process, you'll be using the [`send()`](Form-Runner-~-Buttons-and-Processes#send) action to instruct Orbeon Forms to POST the data entered by users to a URL of your choice.
3. Your app can do what it wants with the data it receives: perform some operation in a database, call a service, etc.
4. If you passed the `replace = "all"` parameter to `send()`, then what your app sends back to Orbeon Forms in the HTTP response will be sent/proxied back to the browser by Orbeon Forms. This allows you to send a custom confirmation page, or issue a redirect to another page or form that users should go to next.

![Doc - Accessing data - Process - How](https://orbeon.mybalsamiq.com/mockups/3496409.png?key=8c133721c5ab53800f4a0ba422730f4f020dd695)

### 2. Call the REST API

![Doc - Accessing data - REST - Overview](https://orbeon.mybalsamiq.com/mockups/3496368.png?key=74ab13a5b0003ab944d0242d8f70f51c6293ce35)

### 3. Accessing the database

![Doc - Accessing data - DB - Overview](https://orbeon.mybalsamiq.com/mockups/3496415.png?key=78c6cf5202454498bc2560e8ea8bc7e593e5fce1)
![Doc - Accessing data - DB - How](https://orbeon.mybalsamiq.com/mockups/3496425.png?key=1865cc9145143beea62ed382102edddf24de1b03)