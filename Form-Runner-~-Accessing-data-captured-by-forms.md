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

Your second option is to have your app call the Orbeon Forms [persistence API](Form-Runner-~-APIs-~-Persistence) to retrieve the data saved by Orbeon Forms in the database. This is a simple REST API, and you'll want to make a first call to the [search API](Form-Runner-~-APIs-~-Persistence-~-Search) to *list* data submitted or saved with a specific form, and then a call to the [CRUD API](Form-Runner-~-APIs-~-Persistence-~-CRUD) to retrieve any piece of data you're interested in.

![Doc - Accessing data - REST - Overview](https://orbeon.mybalsamiq.com/mockups/3496368.png?key=74ab13a5b0003ab944d0242d8f70f51c6293ce35)

As mentioned, the API provided by Orbeon Forms is quite simple, but there are a few complications to keep in mind when calling such an API, which often make [option 1](#1-send-data-on-submit) above more desirable:
- You're deciding when to call the API. Most likely you'll want to do this at regular interval, like every hour or every day, to process any new data submitted to the system. This means that you need to have a cron-like infrastructure to perform that task on a regular basis, and that your app won't know about new data in real-time.
- Assuming your app is just interested in processing new data, it will need to somehow keep track of what data it has already processed.
- Out-of-the-box, for security reasons, access to the REST API is blocked. You can either completely open up access to the API at the Orbeon Forms level, and protect it through some other mean (e.g. filter), or setup some authentication between the caller of the API and Orbeon Forms, through an authorization service. You can find more about this in [Authorization of Pages and Services](Controller-~-Authorization-of-Pages-and-Services).

### 3. Accessing the database

![Doc - Accessing data - DB - Overview](https://orbeon.mybalsamiq.com/mockups/3496415.png?key=78c6cf5202454498bc2560e8ea8bc7e593e5fce1)
![Doc - Accessing data - DB - How](https://orbeon.mybalsamiq.com/mockups/3496425.png?key=1865cc9145143beea62ed382102edddf24de1b03)