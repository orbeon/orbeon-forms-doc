# FAQ - Licensing

## Is there any cost associated with using Orbeon Forms?

[Professional Edition (PE) builds](https://www.orbeon.com/download) are available through [PE Subscription plans](https://www.orbeon.com/pricing). Further commercial support is available with [Dev Support plans](https://www.orbeon.com/services).

[Community Edition (CE) builds](https://www.orbeon.com/download) are available free of charge whether your use it to build open source or commercial applications.

The complete [source code](http://github.com/orbeon/orbeon-forms/) to Orbeon Forms CE is available free of charge and under *real* open source terms. The source code to Orbeon Forms PE is available to subscription customers on demand.

With the open source code, you are free as you please to:

- extend the platform
- build applications on top of the platform

Note however that if you make changes to the existing Orbeon Forms code, you are bound by the terms of the LGPL license, which requires you to redistribute changes to the open source community when you distribute your application.

## Can I use an older version of Orbeon Forms with a newer license file?

Yes, a license generated for a given version will work with previous versions of the software as well.

For example if you have a license file for orbeon Forms 2017.2, you can use it with Orbeon Forms 2017.1, and so on.

## Can I use a newer version of Orbeon Forms with an older license file?

It depends:

- If your license file has a non-blank `subscription-end` date, then you can upgrade to any Orbeon Forms version published before that date. In other words, you can upgrade to any version of Orbeon Forms published while your subscription is active and your license file reflects that.
- If your license file has a blank `subscription-end` but has a non-blank `version`, then you can upgrade to any version up to and including the version specified. *NOTE: Only the first two version numbers are checked. If your license file says `4.4`, then you can use `4.4.1`, for example. In other words, minor updates are always allowed.*
- If your license file has neither a non-blank `subscription-end` nor a non-blank `version`, then there are no restrictions on the version of Orbeon Forms you can use.

The above is valid as long as the license hasn't expired, if it has an `expiration` date specified.

In practice, the Orbeon Forms licenses we produce typically have the following features:

Starting February 2018:

- PE Basic licenses
    - have an `expiration` date with a grace period
    - have an empty `version` field
    - have `subscription-start` and `subscription-end` dates
- PE Silver and PE Gold licenses
    - have an `expiration` date with a grace period (except for grandfathered license renewals)
    - have an empty `version` field
    - have `subscription-start` and `subscription-end` dates
    
Until February 2018:

- PE Basic licenses
    - have an `expiration` date
    - have a blank `version` field
    - don't have `subscription-start` and `subscription-end` dates
- PE Silver and PE Gold licenses
    - don't have an `expiration` date
    - have a specific `version` field
    - have `subscription-start` and `subscription-end` dates

## Will my license expire and cause the software to stop working?

Starting February 2018:

- Production licenses expire after a grace period (except for grandfathered license renewals).
- Non-production Basic licenses (as well as the older Dev licenses) expire after a grace period (except for grandfathered license renewals).

For details, see the [New PE Gold Benefits and Perpetual Licenses Grandfathered
](https://blog.orbeon.com/2018/02/pe-gold-benefits-perpetual-licenses-grandfathered.html).

Until February 2018:

- Production licenses don't expire.
- Non-production Basic licenses (as well as the older Dev licenses) do expire.

You can check whether there is an actual expiration by checking the `expiration` field of the license file.

## What is the subscription-end field in the license file?

The `subscription-end` field is informative and indicates the end of the support subscription, when applicable.

## What am I paying for when I acquire an Orbeon Forms PE Production subscription?

The first year, both:

- a license to install and use the software
- one year of support

The second and subsequent years:

- additional years of support

## See also

- [Pricing](https://www.orbeon.com/pricing)
- [Services](https://www.orbeon.com/services)
- [New PE Gold Benefits and Perpetual Licenses Grandfathered
](https://blog.orbeon.com/2018/02/pe-gold-benefits-perpetual-licenses-grandfathered.html)