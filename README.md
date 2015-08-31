## HashLauncher

A hashbang handler for Windows. The aim is to handle hashbangs in a way roughly compatible with *nix systems.

### Examples

The following kinds of directives are possible

```
#!ipython -i
```

```
#!C:\Cygin64\bin\bash.exe
```

```
#!/usr/bin/fsi
```

In the last example ``/usr/bin/`` will be striped.

### How to use

Create file associationwith ``hashlauncher.exe`` for the file types which you would like to be handled by this tool. You can do so manually by right click the file type in explorer and select ``hashlauncher.exe`` as the default program.

Alternatively you can edit registry programmatically.

__TODO__: add PS script for this.

