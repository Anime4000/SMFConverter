# Company of Heroes Audio Converter
This will allow you to convert Company of Heroes sound files (.smf) This converter capable to convert .smf file to standard format either WAV or MP3, depending how source file structure.

SMFConverter support converting from archive, simply set .smf file to smfc.exe as default program.

To download click [here!](https://drive.google.com/file/d/0B_nmcRzQ-bL9bi1Ba0pQZDZwNDA/edit?usp=sharing)

## Info:
In order to use, you need extract the .sga file first, this can be done by using [GCFScape](https://nemstools.github.io/pages/GCFScape-Download.html)

A player that can play WAV (IMA ADPCM) or MP3, usually it installed by default, you may not to worries.


## Tweaking
By default, smfc will stay on screen after you open .smf file, to make auto close after opening, follow this step.

* Open "regedit" at Run or Start Menu search
* Find this key
```
HKEY_CURRENT_USER\Software\Classes\smf_auto_file\shell\open\command
```
* Double click (default) and add -c after "%1", for example:
```
"C:\Users\Anime4000\Desktop\smfc.exe" "%1" -c
```

## Uses:
```
Usage: smfc.exe [input|-h] [-c] [-p 0|1] [-o output]

Options:
    input       Company of Heroes sound file (*.smf)
    -h          Display this help screen.
    -c          Exit after converting.
    -p bool     Play converted audio. (default 1)
    -o name     Save converted file to specific path instead of save
                in working folder. It will ignore file extension.

Example: smfc.exe "C:\coh_combat.smf" -c -p 0 -o "D:\My Stuff\music"
```

## Compiling:
To compile the source code, you need Microsoft VisualStudio 2013.

This code was written in C#, thus you may need VisualStudio C#.