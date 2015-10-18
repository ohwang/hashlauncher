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

In the last example ``/usr/bin/`` will be striped, i.e. using ``fsi`` from ``path``

### How to use

Create file association with ``HashLauncher.exe`` for the file types which you would like to be handled by it.
You can do so manually by right click the file type in explorer and select ``HashLauncher.exe`` as the default program.

Alternatively you can edit registry programmatically.

### Why?

It's probably not a big deal. Even I am suspicous that it can have a positive impact on my productivity.

Let's see what will happen then.

### TODO

+ Manage file associations programmatically

+ Ignore the shabang line and only interpret the rest of the files (to align with the behavior in linux)

+ Logging

+ Define different behaviors when executed from command line vs double clicked from file explorer (but HOW?)

+ Startup time optimization

