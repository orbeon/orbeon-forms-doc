> [Wiki](Home) ▸ [FAQ](./FAQ)

### You sent a license for orbeon Forms 4.6. Can I use Orbeon Forms 4.0 with that license?

Yes, a license generated for a given version will work with previous versions of the software as well.

### You sent a license for orbeon Forms 4.4. Can I use Orbeon Forms 4.6 with that license?

It depends:

- If your license file has a non-blank `subscription-end` date, then you can upgrade to any Orbeon Forms version published before that date. In other words, you can upgrade to any version of Orbeon Forms published while your subscription is active and your license file reflects that.
- If your license file has a blank `subscription-end` but has a non-blank `version`, then you can upgrade to any version up to and including the version specified. *NOTE: Only the first two version numbers are checked. If your license file says `4.4`, then you can use `4.4.1`, for example. In other words, minor updates are always allowed.*
- If your license file has neither a non-blank `subscription-end` nor a non-blank `version`, then there are no restrictions to which version of Orbeon Forms you can use.

The above is valid as long as the license hasn't expired, if it has an `expiration` date specified.

In practice, the Orbeon Forms licenses we produce typically have the following features:

- PE Basic license
  - have an `expiration` date
  - don't have a `version` specified
  - don't have a `subscription-end` date specified
- PE Silver and PE Gold licenses
  - don't have an `expiration` date
  - have a `version` specified
  - have a `subscription-end` date specified

### Will my license expire and cause the software to stop working?

- Production licenses don't expire.
- Non-production Basic licenses (as well as the older Dev licenses) do expire.

You can check whether there is an actual expiration by checking the `expiration` field of the license file.

### What is the subscription-end field in the license file?

The `subscription-end` field is informative and indicates the end of the support subscription, when applicable.

### What am I paying for when I acquire an Orbeon Forms PE Production subscription?

The first year, both:

- a license to install and use the software
- one year of support

The second and subsequent years:

- additional years of support
