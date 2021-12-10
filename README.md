# Snip
*Simple cut & paste CLI*

[![badge](https://img.shields.io/github/v/tag/andtechstudios/snip?label=nuget)](https://gitlab.com/andtech/pkg/-/packages)

Now you can enjoy the ease of cut & paste functionality in the shell. No more needing to remember obnoxiously long `mv` operands.

```bash
$ cd ~/downloads
$ snip file.txt # cut
$ cd ~/really/long/path/
$ snip # paste
```

# Usage
## Cut
```
$ snip <target>
```

This is like pressing <kbd>Ctrl</kbd> + <kbd>X</kbd> (or <kbd>⌘</kbd> + <kbd>X</kbd>)

## Paste
```
$ snip
```

This is like pressing <kbd>Ctrl</kbd> + <kbd>V</kbd> (or <kbd>⌘</kbd> + <kbd>V</kbd>)
