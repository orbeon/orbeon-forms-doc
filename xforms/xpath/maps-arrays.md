# Maps and arrays functions



## Introduction

XPath 3.1 introduces [maps](https://www.w3.org/TR/xpath-31/#id-maps) and [arrays](https://www.w3.org/TR/xpath-31/#id-arrays). Orbeon Forms does not support XPath 3.1 yet, however it implements a subset of operations on maps and arrays. 

## Availability

[SINCE Orbeon Forms 2017.2]

### Maps

#### Introduction

Orbeon Forms does not support the native XPath 3.1 syntax to create maps. But you can create a new map as follows using the `map:merge()` and `map:entry()` functions:

```xpath
map:merge(
    (
        map:entry('number',   42),
        map:entry('string',   'forty-two'),
        map:entry('node',     instance()),
        map:entry('sequence', 1 to 10)
    )
)
```

#### map:entry()

```xpath
map:entry(
    $key as xs:anyAtomicType,
    $value as item()*
) as map(*)
```

[XPath 3.1 documentation](https://www.w3.org/TR/xpath-functions-31/#func-map-entry).

#### map:merge()

```xpath
map:merge($maps as map(*)*) as map(*)
```

[XPath 3.1 documentation](https://www.w3.org/TR/xpath-functions-31/#func-map-merge).

#### map:get()

```xpath
map:get(
    $map as map(*),
    $key as xs:anyAtomicType
) as item()*
```

[XPath 3.1 documentation](https://www.w3.org/TR/xpath-functions-31/#func-map-get).

### Arrays

#### Introduction

Orbeon Forms does not support the native XPath 3.1 syntax to create arrays. But you can create a new array as follows using the `array:join()` and `array:append()` functions:

```xpath
array:append(
    array:append(
        array:append(
            array:append(
                array:join(()),
                42
            ),
            'forty-two'
        ),
        instance()
    ),
    1 to 10
)
```

#### array:size()

```xpath
array:size($array as array(*)) as xs:integer
```

[XPath 3.1 documentation](https://www.w3.org/TR/xpath-functions-31/#func-array-size).

#### array:get()

```xpath
array:get($array as array(*), $position as xs:integer) as item()*
```

[XPath 3.1 documentation](https://www.w3.org/TR/xpath-functions-31/#func-array-get).

#### array:put()

```xpath
array:put(
    $array    as array(*),
    $position as xs:integer,
    $member   as item()*
) as array(*)
```

[XPath 3.1 documentation](https://www.w3.org/TR/xpath-functions-31/#func-array-put).

#### array:append()

```xpath
array:append(
    $array     as array(*),
    $appendage as item()*
) as array(*)
```

[XPath 3.1 documentation](https://www.w3.org/TR/xpath-functions-31/#func-array-append).

#### array:join()

```xpath
array:join($arrays as array(*)*) as array(*)
```

[XPath 3.1 documentation](https://www.w3.org/TR/xpath-functions-31/#func-array-join).
