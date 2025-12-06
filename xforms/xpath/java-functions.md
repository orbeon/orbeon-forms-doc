# Calling Java code from XPath

## Overview

Orbeon Forms allows you to call Java code directly from XPath expressions using Saxon's reflective extension function mechanism. This feature enables you to leverage existing Java libraries and write custom Java functions that can be invoked from your XForms applications.

## Basic syntax

To call Java code from XPath, you use the `java:` namespace prefix followed by the fully qualified class name and method name:

```xpath
java:fully.qualified.ClassName.methodName(arguments)
```

## Calling static methods

Static methods can be called directly using the class name:

```xpath
java:java.lang.Math.sqrt(16)
```

This example calls the `Math.sqrt()` static method and returns `4.0`.

### Examples of static method calls

**Getting the current time:**

```xpath
java:java.lang.System.currentTimeMillis()
```

**Working with strings:**

```xpath
java:java.lang.String.format('Hello, %s!', 'World')
```

**UUID generation:**

```xpath
java:java.util.UUID.randomUUID()
```

## Calling instance methods

To call instance methods, you first need to create an instance using a constructor, then call methods on that instance:

```xpath
java:java.lang.String.new('hello world').toUpperCase()
```

This creates a new String instance with "hello world" and calls the `toUpperCase()` method, returning "HELLO WORLD".

### Chaining method calls

You can chain multiple method calls together:

```xpath
java:java.lang.String.new('  hello  ').trim().toUpperCase()
```

This trims whitespace and converts to uppercase, returning "HELLO".

## Type conversion

Saxon automatically handles type conversion between XPath types and Java types:

| XPath Type | Java Type |
|------------|-----------|
| `xs:string` | `String` |
| `xs:integer` | `Integer`, `Long`, `BigInteger` |
| `xs:decimal` | `Double`, `Float`, `BigDecimal` |
| `xs:boolean` | `Boolean` |
| `xs:date` | `java.util.Date` |
| Node | `org.w3c.dom.Node` |
| Sequence | `java.util.List` |

## Namespace declaration

When using Java functions in your XForms code, you should declare the `java:` namespace prefix in your form, even though it's often not strictly required:

```xml
<xf:model xmlns:java="http://saxon.sf.net/java-type">
    <xf:instance id="example">
        <data>
            <value/>
        </data>
    </xf:instance>
    
    <xf:bind ref="value" 
             calculate="java:java.lang.System.currentTimeMillis()"/>
</xf:model>
```

## Practical examples

### Example 1: Using custom Java utilities

Suppose you have a custom utility class:

```java
package com.example.utils;

public class StringUtils {
    public static String capitalize(String input) {
        if (input == null || input.isEmpty()) {
            return input;
        }
        return input.substring(0, 1).toUpperCase() + 
               input.substring(1).toLowerCase();
    }
}
```

You can call it from XPath:

```xpath
java:com.example.utils.StringUtils.capitalize('hello')
```

This returns "Hello".

### Example 2: Working with dates

```xpath
java:java.text.SimpleDateFormat.new('yyyy-MM-dd')
    .format(java:java.util.Date.new())
```

This formats the current date in ISO format (e.g., "2025-12-06").

### Example 3: Using in calculated values

```xml
<xf:bind ref="timestamp" 
         calculate="string(java:java.lang.System.currentTimeMillis())"/>

<xf:bind ref="formatted-date" 
         calculate="java:java.text.SimpleDateFormat.new('dd/MM/yyyy HH:mm:ss')
                    .format(java:java.util.Date.new())"/>
```

### Example 4: Using in actions

```xml
<xf:action ev:event="DOMActivate">
    <xf:setvalue ref="uuid" 
                 value="string(java:java.util.UUID.randomUUID())"/>
</xf:action>
```

## Important considerations

### Performance impact

Java function calls from XPath can impact performance:

- Each call involves Java reflection overhead
- Calls are not optimized by XPath expression analysis (see [Expression Analysis](expression-analysis.md))
- For frequently executed expressions, consider caching results in instance data

### Security considerations

When allowing Java calls in XPath:

- Be cautious about what Java classes users can access
- Avoid exposing sensitive operations through XPath-callable methods
- Consider the security implications of reflection-based calls
- In production, limit access to trusted Java classes only

### Deployment requirements

To use custom Java classes:

1. Package your Java classes in a JAR file
2. Place the JAR in the application server's classpath or in `WEB-INF/lib`
3. Ensure the classes are available when the XForms engine evaluates expressions

### XPath analysis limitations

As noted in the [Expression Analysis](expression-analysis.md) documentation, Java function calls are not analyzed by the XPath dependency analysis system. This means:

- Expressions containing Java functions will be re-evaluated whenever needed
- Dependencies cannot be automatically determined
- This may reduce optimization opportunities

## Alternatives to consider

Before using Java functions, consider these alternatives:

1. **XPath 2.0 built-in functions**: Many operations can be accomplished with [standard XPath functions](standard-functions.md)

2. **Orbeon extension functions**: Check the [extension functions](extension-core.md) for existing utilities

3. **XForms actions**: For complex logic, use [XForms actions](../actions/extensions.md) instead of calculations

4. **Form Runner processes**: For Form Runner, use the [process syntax](../../form-runner/advanced/buttons-and-processes/README.md)

5. **Custom XPath functions**: Create native Orbeon XPath functions for better performance and integration

## Troubleshooting

### "Prefix has not been declared" error

If you see an error like "Prefix java has not been declared", ensure you've declared the namespace:

```xml
xmlns:java="http://saxon.sf.net/java-type"
```

### "No method matching" error

This typically means:

- The method name is incorrect
- The method doesn't exist on that class
- The argument types don't match any available method signature
- You're trying to call an instance method as static (or vice versa)

### ClassNotFoundException

This means the Java class isn't available in the classpath. Check that:

- The fully qualified class name is correct
- The JAR file is in `WEB-INF/lib` or the server's classpath
- The application has been restarted after adding new JARs

## See also

- [XPath Tips](tips.md)
- [Expression Analysis](expression-analysis.md)
- [Extension Functions](extension-core.md)
- [Saxon documentation on Java extension functions](https://www.saxonica.com/html/documentation/extensibility/)
