# Snip
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
$ snip [-c|--copy]
```

This is like pressing <kbd>Ctrl</kbd> + <kbd>V</kbd> (or <kbd>⌘</kbd> + <kbd>V</kbd>)
