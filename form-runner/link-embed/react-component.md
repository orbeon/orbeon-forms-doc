# React Component

## Availability

The React component is available from [the npm package registry](https://www.npmjs.com/package/@orbeon/react).

## Usage

The Orbeon Forms React component can be used in any React application. To use it, add `@orbeon/react` to your dependencies, typically in your `package.json` file, like this:

```json
{
  "dependencies": {
    "@orbeon/react": "^1.0.5"
  }
}
```

Usually, this is done via the command line:

```sh
npm install @orbeon/react
```

The Orbeon Forms React component is based on the [JavaScript embedding API](/form-runner/link-embed/javascript-api.md), but you don't need to include it explicitly using a `<script>` tag. The React component will include it automatically.

The component is used as follows:

```tsx
<FrForm app='acme' form='order' mode='new' orbeonContext='http://localhost:8080/orbeon'/>
```

The properties have the same meaning as in the [JavaScript embedding API](/form-runner/link-embed/javascript-api.md):

| Parameter     | Optional                             | Example                          | Description                                             |
|---------------|--------------------------------------|----------------------------------|---------------------------------------------------------|
| orbeonContext | No                                   | `'http://localhost:8080/orbeon'` | Context where Orbeon Forms is deployed                  |
| app           | No                                   | `'human-resources'`              | App name                                                |
| form          | No                                   | `'job-application'`              | Form name                                               |
| mode          | No                                   | `'new'`                          | Either `'new'`, `'edit'`, or `'view'`                   |
| documentId    | Mandatory for modes other than `new` |                                  | For modes other than `'new'`, the document to be loaded |

## Example

Here's a simple example in TypeScript where a form is selected from a dropdown and then embedded using the Orbeon Forms React component:

```tsx
import React from 'react';
import ReactDOM from 'react-dom';
import { FrForm } from "@orbeon/react";

interface MyComponentProps {
  selectedValue: string;
}

const MyComponent: React.FC<MyComponentProps> = ({ selectedValue }) => {
  const [selectedOption, setSelectedOption] = React.useState(selectedValue);
  
  const handleSelectChange = (event: React.ChangeEvent<HTMLSelectElement>) => {
    setSelectedOption(event.target.value);
  };

  return (
    <div>
      <select value={selectedOption} onChange={handleSelectChange}>
        <option value="Form 1">form-1</option>
        <option value="Form 2">form-2</option>
        <option value="Form 3">form-3</option>
      </select>
      <p>Form: {selectedOption}</p>
      <FrForm app='acme' form={selectedOption} mode='new' orbeonContext='http://localhost:8080/orbeon'/>
    </div>
  );
};

document.addEventListener('DOMContentLoaded', function() {
  ReactDOM.render(<MyComponent selectedValue='form-1'/>, document.getElementById('root'));
});
```
