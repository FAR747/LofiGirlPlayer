# Lofi Girl Player
[![Current Release](https://img.shields.io/github/release/FAR747/LofiGirlPlayer.svg)](https://github.com/FAR747/LofiGirlPlayer/releases) [![GitHub license](https://img.shields.io/github/license/FAR747/LofiGirlPlayer)](https://github.com/FAR747/LofiGirlPlayer/blob/main/LICENSE)

### A simple Lofi Girl streaming application for Windows
![Screen](https://github.com/FAR747/LofiGirlPlayer/raw/main/DOCS/Screen_1.png)  

This player allows you to play YouTube streams from the [Lofi Girl](https://www.youtube.com/channel/UCSJ4gkVC6NrvII8umztf0Ow) channel (and not only).

**Lofi Girl Player** is suitable for those who like to listen to Lofi on speakers, but so that the rest of the sounds are played in headphones. All you need to do is start the player and in the system settings (`ms-settings:apps-volume`) specify the output device for `Microsoft Edge WebView2`

## System requirements
 - Windows 10, 11
 - .NET Framework 4.7.2+
 - Installed [Microsoft Edge](https://www.microsoft.com/edge)

## How to add stream to player
To add a stream to the player, you need to edit `Sources.json` by adding a new element to the `sources` array:  
```json
{
    "Name": "Top lofi",
    "YTID": "GtL1huin9EE"
}
```
- In `Name` specify the name that will be displayed in the interface;
- In `YTID` specify the stream ID. The ID is contained in the stream link after `watch?v=`. For example, the ID of this stream `https://www.youtube.com/watch?v=rUxyKA_-grg` is: `rUxyKA_-grg`  

An example file after adding the stream from the example above:
```json
{
  "version": 1,
  "sources": [
    {
      "Name": "lofi hip hop radio - beats to relax/study to",
      "YTID": "jfKfPfyJRdk"
    },
    {
      "Name": "lofi hip hop radio - beats to sleep/chill to",
      "YTID": "rUxyKA_-grg"
    },
	{
      "Name": "Top lofi",
      "YTID": "GtL1huin9EE"
	}
  ]
}
```

## Support
Got a problem? Create an [Issue](https://github.com/FAR747/LofiGirlPlayer/issues/new)

## Used libraries
 - [Newtonsoft.Json](https://www.newtonsoft.com/json)
 - [Microsoft.Web.WebView2](https://www.nuget.org/packages/Microsoft.Web.WebView2)
 - [Material Design In XAML](https://github.com/MaterialDesignInXAML/MaterialDesignInXamlToolkit)