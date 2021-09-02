# AvalonSS2

![v1](https://img.shields.io/badge/version-0.0.1-blue)
![MIT](https://img.shields.io/badge/License-GNU%20GPLv3-red)

> A .NET implementation of the **hacking mini game** from **ss2** using **.NET 5**/**Avalonia UI** and **WPF/WinForms**.  
> The mini game from the classic System Shock 2 itself is like *tic-tac-toc/connect-four*

![Icon](Assets/Icon_96x96.png)

## Project Structure

[SS2.Core](SS2.Core) *Essential game logic and models*  
[SS2.Avalonia](SS2.SS2.AvaloniaUI) *Cross-platform implementation using [Avalonia UI](https://avaloniaui.net/)*   
[SS2.WPF](SS2.WPF) *Experimental WPF/WinForms implementation*  

## Screenshots

### Start

![Icon](Assets/Screenshots/Start.png)


### Ingame

![Icon](Assets/Screenshots/Ingame.png)

### Win

![Icon](Assets/Screenshots/Win.png)

### Lose

![Icon](Assets/Screenshots/Lose.png)

## Download

* [Windows x64]()
* [Linux x64]()
* [macOS x64]()

## Build

### SS2.Avalonia

```bash
dotnet build
```

### SS2.Core

```bash
dotnet build
```

### SS2.WPF

```bash
MSBuild ...
```

## Debug

### Visual Studio

Press `F12` for a developer console similar to brower devtools

## Docs

* [Avalonia Documentation](https://docs.avaloniaui.net)  
* [Avalonia Reference](http://reference.avaloniaui.net/api)
* [SS2 Rules](https://shodan.fandom.com/wiki/Hacking)

## License

[GNU GPLv3](LICENSE)