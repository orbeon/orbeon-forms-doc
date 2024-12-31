# Angular component

## Availability

- [SINCE Orbeon Forms 2022.1.6, 2023.1.1]
- This is an [Orbeon Forms PE](https://www.orbeon.com/download) feature.
- The Angular component is available from [the npm package registry](https://www.npmjs.com/package/@orbeon/angular).

## Usage

The Orbeon Forms Angular component can be used in any Angular application. To use it, add `@orbeon/angular` to your dependencies, typically in your `package.json` file, like this:

```json
{
  "dependencies": {
    "@orbeon/angular": "^1.0.1"
  }
}
```

Usually, this is done via the command line:

```sh
npm install @orbeon/angular
```

The Orbeon Forms Angular component is based on the [JavaScript embedding API](/form-runner/link-embed/javascript-api.md), but you don't need to include it explicitly using a `<script>` tag. The Angular component will include it automatically.

The component is used as follows:

```tsx
<fr-form app="acme" form="order" mode="new" orbeonContext="http://localhost:8080/orbeon"></fr-form>
```

The properties have the same meaning as in the [JavaScript embedding API](/form-runner/link-embed/javascript-api.md):

| Parameter     | Optional                             | Type         | Example                          | Description                                             |
|---------------|--------------------------------------|--------------|----------------------------------|---------------------------------------------------------|
| orbeonContext | No                                   | String       | `'http://localhost:8080/orbeon'` | Context where Orbeon Forms is deployed                  |
| app           | No                                   | String       | `'human-resources'`              | App name                                                |
| form          | No                                   | String       | `'job-application'`              | Form name                                               |
| mode          | No                                   | String       | `'new'`                          | Either `'new'`, `'edit'`, or `'view'`                   |
| documentId    | Mandatory for modes other than `new` | String       |                                  | For modes other than `'new'`, the document to be loaded |
| queryString   | Yes                                  | String       | `"job=clerk"`                    | Additional query parameters                             |
| headers       | Yes                                  | [Headers][h] | `new Headers({ 'Foo': 'bar' })`  | Additional HTTP headers; see point 2 below              |

## Example

Here's a simple example in TypeScript where a form is selected by clicking on buttons and then embedded using the Orbeon Forms Angular component:

`app.component.ts`:

```tsx
import { Component } from '@angular/core';
import { FrFormComponent } from '@orbeon/angular';

@Component({
    selector: 'app-root',
    standalone: true,
    imports: [ FrFormComponent ],
    template: `
      <button (click)="loadForm('acme', 'form-1')">Form 1</button>
      <button (click)="loadForm('acme', 'form-2')">Form 2</button>
      <button (click)="loadForm('acme', 'form-3')">Form 3</button>
      <div style="margin-top: 10px;">
        <div>App: {{ app }}</div>
        <div>Form: {{ form }}</div>
      </div>
      <fr-form app="{{app}}" form="{{form}}" mode="new" orbeonContext="http://localhost:8080/orbeon"></fr-form>
    `
})

export class AppComponent {
    title = 'Orbeon Angular component test';

    app  : string = 'acme';
    form : string = 'form-1';

    loadForm(app: string, form: string): void {
        this.app  = app;
        this.form = form;
    }
}
```

`app.module.ts`:

```tsx
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';

@NgModule({
  imports: [
    BrowserModule,
    HttpClientModule
  ]
})

export class AppModule { }
```

[h]: https://developer.mozilla.org/en-US/docs/Web/API/Headers
