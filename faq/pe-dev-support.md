# FAQ - PE and Dev Support

<!-- toc -->

## What is the difference between the Development and Production subscriptions?

1. __Intended use__ — _Development subscriptions_ are for laptops, workstations, or servers on which you install Orbeon Forms PE for development, that are not accessed by end users of the forms or application you are creating. _Production subscriptions_ are for the servers on which you install Orbeon Forms PE and that will be accessed by end users.
2. __Expiration__ — The license you get with Development subscriptions expires at the end of the subscription, while the license you get with a Production subscription doesn't and is sometime referred to as _perpetual_. Production licenses are perpetual, to give you a choice on whether you want to renew your subscription. If you decide to renew, you'll continue to get support, security patches, and free upgrades to major releases of the software. If you don't, you won't get those benefits, but your existing software will continue to run.

## Can I use a Development subscription for staging or test server?

You can use Development subscriptions for staging or test server, as long as those servers are not accessed by end users (see _intended use_on the previous question). If your staging or test servers is only used during development, we recommend you get a Development subscription. If it they run along your production servers, and you're planning to keep using them after going in production, we recommend you get Production subscription to have perpetual license on those server (see _expiration_ on the previous question).

## What do you mean by "server" and "computer"?

1. __Server vs. computer__ — Production subscriptions are said to be per server, while Development subscription are per computer. The term "computer" is used to convey the fact that Development subscriptions are often used on laptops or workstations, which people wouldn't describe as "servers".
2. __Physical hardware__ — In both cases, subscriptions are _per hardware machine_. It doesn't matter how many CPUs, cores, users, or forms you have on a given machine: you just need one subscription per machine.
3. __Virtualization__ — If you are using virtualization and run two or more VMs on the same server, you just need one subscription for that physical server. However, if you use virtualization across multiple physical servers, then you need one PE subscription per server.

## Do you provide free or discounted non-profit and/or educational licenses?

In general, we don't, and we couldn't explain why better than [this blog post at 37signals](https://signalvnoise.com/posts/2580-why-non-profit-pricing), which we recommend reading.

## Do you have an official list of patches?

We don't have a list of patches you can just download.

However, we do provide custom patches to PE customers on a case by case basis.

* Often such patches are backports of commits publicly available in the public source code repository.
* Sometimes, they are created specifically to address issues you report.

Some patches are also available in the latest stable release of Orbeon Forms PE, which you can find from the [downloads page](http://www.orbeon.com/download).

If you are interested to know what kind of issues and features are being worked on, please make sure to check the following:

- [Commits to the public source code repository](https://github.com/orbeon/orbeon-forms/commits/master)
- [Recently closed issues](https://github.com/orbeon/orbeon-forms/issues?q=is%3Aclosed+sort%3Aupdated-desc)
- [Orbeon on Twitter](https://twitter.com/orbeon)
- [Orbeon Forms roadmap](https://github.com/orbeon/orbeon-forms/wiki/Orbeon-Forms-Roadmap)
- [Orbeon Forms blog](http://blog.orbeon.com/)

## Does Orbeon Forms contact a license server over the internet?

No, Orbeon Forms doesn't contact a license server. The licensing system does a local check of the license when the application starts. For production licenses, the check is against the version of the software. The major version numbers are checked (for example 3.8, 3.9, 4.0 are all major versions, coming out roughly once a year). For development licenses, the check is against the expiration date.

## What versions of Orbeon Forms are covered by subscriptions?

* Development licenses cover all versions, however they expire at the end of one year (or the number of years purchased) and need to be renewed if still in use.
* Production licenses are produced for the latest version of Orbeon Forms at the time of purchase and cover that version and all earlier versions.

## Are new licence files required when upgrading to a new version of Orbeon Forms?

For development licenses, no upgrade is necessary. For production licenses, as long as you maintain production support, you get free license upgrades to any new major version upon request (minor versions updates, such as 4.8.0 to 4.8.1, do not require a license upgrade).
