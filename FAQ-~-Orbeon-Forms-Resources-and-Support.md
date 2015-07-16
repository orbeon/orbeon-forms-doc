> [[Home]] ▸ [[FAQ|FAQ]]

### How can I get support for Orbeon Forms?

- You can get support from the Orbeon Forms community on the [Orbeon Forms forum][1] which is the hub of the Orbeon Forms community, and this is the best place to ask questions, share your experience, or post issues and requests for enhancements.
- Orbeon can provide professional support for your company. Orbeon has two types of plans: [Orbeon Forms PE subscriptions][2] are the most appropriate when your application is in production, while [Dev Support plans][3] also covers issues during development, feature enhancements, and training. For more information contact Orbeon at [info@orbeon.com][4].

### How can I subscribe to the Orbeon Forms forum?

See the [Community][5] page. If you are having any problem subscribing to the forum, please contact Orbeon at [info@orbeon.com][4].

### Where do I find the source code?

The source code for the CE version is on GitHub. See: [Orbeon Source Code Repository](http://wiki.orbeon.com/forms/doc/contributor-guide/development-environment/orbeon-source-code-repository).

The source code for the PE version is made available to PE customers on demand.

### Where do I find the source code for a particular release of Orbeon Forms?

See: [Orbeon Source Code Repository](http://wiki.orbeon.com/forms/doc/contributor-guide/development-environment/orbeon-source-code-repository).

### When are you going to fix/implement my favorite issue/feature?

Usually an issue gets fixed or a feature gets implemented in Orbeon Forms when:

- The bug has been reported by a support customer and has been evaluated as high priority.
- It is very easy to implement, so we just implement it along the way to make everybody happy.
- We have put it on our [roadmap](https://github.com/orbeon/orbeon-forms/wiki/Orbeon-Forms-Roadmap) and scheduled it because we think it is a crucial feature for Orbeon Forms. 
- We need it for a project we are currently working on.
- Somebody sponsors it.
- Somebody sends a patch for it.

For a list of open issues/features, see the [issue tracker](https://github.com/orbeon/orbeon-forms/issues).

We need help on Orbeon Forms by the way, and we definitely appreciate outside contributions. So check out the [source code][6] and start hacking away. You can ask your questions on the [Orbeon Forms forum][1].

### How do I report an issue?

The key to effectively reporting an issue is to post in the Orbeon Forms forum a message with:

- A reproducible test case, attached to your message
- How to run this test case
- What is the behavior you observe when you run the test case
- What is the behavior you expect

But why a reproducible test case? The alternative is to describe the issue in plain English with maybe the help of code snippets. The problem with this approach is that it requires of people who will be helping you on the Orbeon Forms forum to understand the context of your problem, which might be trivial to you, but can be hard to grasp quickly for someone else.

You can provide two types of test cases:

- __An XHTML+XForms__ file that runs in Form Runner or in the XForms sandbox - This is the preferred way of submitting a test case. Most problems with XForms can be shown in one single XHTML+XForms file. If you need the XForms to call a service with an `<xf:submission>`, you can simulate this by placing an XML file that contains the response from that service on a public web site, and changing the `<xf:submission>` to point to that file. If you don't have a server of your own that you can use for this, a free hosting service will work. With Form Builder, you can open the form source with "Edit Source" and copy the form definition from the editor.
- __An application__ - An Orbeon Forms application is stored under `resources/apps`. Assuming you have your application under `resources/apps/foo`, it can be accessed by going to`http://localhost:8080/ops/foo/`. If you can't create a test case that runs in the XForms sandbox, create a simple standalone application under directory foo, zip the content of that directory, and attach it to your email in the Orbeon Forms forum as `foo.zip`.

In both cases:

- Make your test case as simple as possible. Remember that people who will be helping you in the forum often don't have much time, and you are more likely to receive feedback that is useful if your test case only contains the minimum amount of code required to reproduce the issue.
- Keep your post focused on a single issue. If there are more issues you would like to report, post them in separate threads, or post the first one and wait to get that one resolved before you post the next one in another thread.

### Where are the Orbeon Forms forum archives?

[Right here](http://discuss.orbeon.com/).

### How can I support Orbeon Forms?

You can help by:

- Subscribing to the [discussion forum][1], and helping other people in community.
- Contributing to the open source effort. If you have a feature in mind and think it would benefit Orbeon Forms, you can implement it and contribute it. The best place to get started is to discuss your idea on the Orbeon Forms forum.
- Get a [PE subscription](http://www.orbeon.com/pricing) or a [Dev Support plan](http://www.orbeon.com/services) with Orbeon. If Orbeon Forms is what it is today, it is in great part thanks to companies who have financially supported Orbeon, allowing Orbeon developers to be paid to work on Orbeon Forms and make it a better product. For more, contact Orbeon at [info@orbeon.com][4].

Your support is greatly appreciated!

### Is there a list of known issues?

Yes, [on GitHub][7].

In general, we recommend you do not add bugs or RFEs in the tracker yourself, as we may miss them, and you may introduce duplicates. Please discuss bugs or issues on the Orbeon Forms [forum][1] first.

### Is there a wiki?

Yes, in fact we have two (which we plan to unify):

- on [Google Sites](http://wiki.orbeon.com/forms/)
- on [GitHub](https://github.com/orbeon/orbeon-forms/wiki)

Feel free to add or make changes to the contents there. You can do so after creating a user (anonymous changes are not allowed). But please be considerate. You may want to discuss changes on the forum first.

### What's going on with development?

We regularly publish a [status message like this one](http://discuss.orbeon.com/Activity-in-Orbeon-land-td4658518.html) in the forum.

See also our [roadmap](https://github.com/orbeon/orbeon-forms/wiki/Orbeon-Forms-Roadmap).

[1]: http://discuss.orbeon.com/
[2]: http://www.orbeon.com/pricing
[3]: http://www.orbeon.com/services
[4]: mailto:info%40orbeon.com
[5]: http://www.orbeon.com/community
[6]: http://wiki.orbeon.com/forms/doc/contributor-guide/development-environment/orbeon-source-code-repository
[7]: https://github.com/orbeon/orbeon-forms/issues