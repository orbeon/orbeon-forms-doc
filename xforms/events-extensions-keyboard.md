# Keyboard events

## Availability

- UNTIL Orbeon Forms 2018.2
    - `keypress`
- SINCE Orbeon Forms 2019.1
    - `keypress`, `keydown` and `keyup`
    
## Rationale 

The main idea behind supporting keyboard events is to allow creating keyboard shortcuts.

## Basic usage

You can, by listening to the `keypress`, `keydown` and `keyup` events, run actions as users type a certain key combination. Your listener can be registered on:

- __The whole document__, in which case it will run whenever users press the key combination you specified. You can register a listener on the whole document either by declaring you listener directly under the `xh:body` as in:

    ```xml
    <xh:body>
        <xf:action 
            event="keydown" 
            xxf:modifiers="ctrl" 
            xxf:text="y">
            ...
        </xf:action>
        ...
    </xh:body>
    ```
    
    Or you can declare it anywhere in your form with an observer set to `#document`, as in:
    
    ```xml
    <xf:action 
        event="keydown" 
        observer="#document" 
        xxf:modifiers="ctrl"
        xxf:text="y">
        ...
    </xf:action>
    ```
- __Part of the document__, in which case you set your actions to listen on a XForms control such as a `xf:group` or an `xf:input`. Note that in this case, your listener will be called only if a form control (either the one you have specified, or form control inside the one you have specified for container form controls) has the focus when users press the key combination.
- __A dialog__, in which case your listener will be called only when users press the key combination while the dialog is open. In this case, the only requirement for the listener to be called is for the dialog to be open; the focus does not necessarily need to be on a form control inside the dialog.

You specify what key stroke(s) you want to listen to with the following two attributes:

- `xxf:text`:
    - mandatory for `keypress`, `keydown` and `keyup` handlers
    - the key you want to listen to
    - SINCE Orbeon Forms 2019.1
        - special keys are supported: `backspace`, `tab`, `enter`, `return`, `capslock`, `esc`, `escape`, `space`, `pageup`, `pagedown`, `end`, `home`, `left`, `up`, `right`, `down`, `ins`, `del`, `plus`
- `xxf:modifier`
    - optional
    - space-separated list of key modifiers that need to be pressed in addition to the key
    - UNTIL Orbeon Forms 2018.2
        - values can be `Control`, `Shift`, and `Alt`
    - SINCE Orbeon Forms 2019.1
        - values are no longer case-sensitive, but lowercase is recommended
        - `shift`, `ctrl` (`control` is supported for backward compatibility), `alt`/`option`, `meta`/`command`
        
*NOTE: When using modifiers, the event name should be `keydown`. For backward compatibility, `keypress` is still supported but translated to `keydown`.* 

## See also

- [Standard support](events-standard.md)
- [UI refresh events](events-refresh.md)
- [Extension events](events-extensions-events.md)
- [Extension context information](events-extensions-context.md)
- [Other extensions](events-extensions-other.md)
