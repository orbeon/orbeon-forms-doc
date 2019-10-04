# Static and dynamic dropdown

<!-- Diagrams: https://docs.google.com/drawings/d/1MbNMY-mfp05BIBHsZtCryr-10B7nuSmfn7cHhJtkz94/edit -->

[SINCE Orbeon Forms 2019.1]

## Possible combinations

![Combinations of static vs. dynamic, with vs. without search](images/dynamic-data-dropdown-combinations.png)

### Static vs. Dynamic

In the Form Builder Toolbox you'll find 2 dropdown components, as shown in the screenshot below. The *Static Dropdown* is for cases when you, as a form author, want to provide, ahead of time, in Form Builder, the list of all the different choices users will be able to choose from. This is in contrast with the *Dynamic Dropdown* where the list of choices will be loaded at runtime from a service you provide.

Typically, you'll want to use the Static Dropdown for cases where the number of choices is limited, and doesn't change much over time, and the Dynamic Dropdown for cases where you have a long list of choices and/or those choices can change over time.

<img alt="Static Dropdown and Dynamic Dropdown in the Toolbox" src="images/dynamic-data-dropdown-components.png" width="193">

### With vs. without search

You can choose to have each one of your Static or Dynamic Dropdown show as a regular dropdown native to the browser, or as a dropdown with search. You make that choice in the Control Settings dialog, as highlighted in the screenshot below.

<img alt="Choosing between a regular dropdown and a dropdown with search" src="images/dynamic-data-dropdown-with-without-search.png" width="987">

The dropdown "with search" doesn't use the native browser dropdown, but instead uses a dropdown implemented in JavaScript that allows users to search for the choice they want to select by typing part of the label, which is typically useful when the list of choices can be pretty long, and it is thus hard for users to visually just pick one of the choices offered when the dropdown opens.

<img alt="Doing a search to select a country" src="images/dynamic-data-dropdown-search-country.gif" width="478">

## Dynamic dropdowns

### Extracting choices from your service response

Your service must either return XML or JSON. (If it returns JSON the result will first be [converted to XML](/xforms/submission-json.md), so Orbeon Forms can run XPath expression on it.) Say your service returns a list of countries as follows, here including just the first 3 countries:

```xml
<countries>
    <country>
        <name>Afghanistan</name>
        <us-code>af</us-code>
    </country>
    <country>
        <name>Akrotiri</name>
        <us-code>ax</us-code>
    </country>
    <country>
        <name>Albania</name>
        <us-code>al</us-code>
    </country>
</countries>
```

You specify how to extract the necessary information to populate the dropdown through 3 XPath expressions, which you enter in the Control Settings dialog:

- The "Choices XPath expression" must return one item per choice in the dropdown, or in our example, per country, which is done with `/countries/country`.
- The "Label XPath expression" is relative to the a given choice, and must return the label shown to users in the dropdown. In our example, this will point to the `name` element.
- The "Value XPath expression" is similar to the "Label XPath expression" but points to the value stored in the data when users make a selection.

![Combinations of static vs. dynamic, with vs. without search](images/dynamic-data-dropdown-exact.png)

