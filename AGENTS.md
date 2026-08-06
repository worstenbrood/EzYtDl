# AGENTS.md

## Repo

C# 7.3, .NET Framework 4.8 WinForms. Single-project solution. No test framework, no linter, no formatter, no CI.

## Build

```
msbuild YtEzDL.sln /p:Configuration=Release-external /p:Platform=AnyCPU
```

**Configurations:** Debug | Release | Release-external | Debug-x64 | Release-x64 | Release-external-x64. **Release-external** auto-downloads yt-dlp and FFmpeg into `Tools/` at build time.

**PreBuild:** `Build\PreBuild.cmd` injects Git commit hash into `Properties\AssemblyInfo.cs`.

**PostBuild:** `Build\PostBuild.cmd` copies native DLLs from `External/`. Release-external additionally downloads yt-dlp.exe and FFmpeg zip from GitHub, extracts, and places them in `Tools/`.

**NuGet:** `packages.config` (classic, not PackageReference).

## Key quirks

- **Single instance:** `Program.cs` uses a named `Mutex`. Second instance shows a MessageBox and exits.
- **Clipboard monitoring:** `ClipboardMonitor` uses Win32 API `AddClipboardFormatListener`. 5-second debounce via `TimedVariable`.
- **WebP native DLLs:** `External/` has x86 and x64 variants of `libwebp` and `libsharpyuv`. Copied to output in PostBuild.
- **Configuration path:** `%USERPROFILE%\YtEzDL\ezytdl.json` (and `History.json`).
- **Process management:** Console processes (yt-dlp, ffmpeg) use `ConsoleProcess<T>` with `KillProcessTree()` for cleanup.
- **Auto-start:** Registry key `HKCU\SOFTWARE\Microsoft\Windows\CurrentVersion\Run`.

## Architecture (read for context)

- `Program.cs` → `Utils.ApplicationContext` (tray icon + clipboard)
- `Tools/YoutubeDownload.cs` — core yt-dlp wrapper
- `Tools/FfMpeg.cs` — ffmpeg wrapper
- `Audio/AudioPlayer.cs` — NAudio WASAPI playback
- `Streams/FfMpegStream.cs` — yt-dlp → ffmpeg → stdout stream
- `Config/` — JSON persistence, `LockedProperty<T>` for thread safety
- `DownLoad/DownLoadParameters.cs` — fluent CLI argument builder for yt-dlp

## File structure

```
YtEzDL.sln
├── README.MD                  # Solution-level docs
├── YtEzDL/                    # Project root
│   ├── YtEzDL.csproj
│   ├── packages.config
│   ├── Program.cs             # Entry point
│   ├── Audio/                 # Audio playback (AudioPlayer)
│   ├── Build/                 # PreBuild.cmd, PostBuild.cmd
│   ├── Config/                # Settings, JSON persistence
│   ├── Console/               # ConsoleProcess<T>
│   ├── DownLoad/              # DTOs, yt-dlp CLI params
│   ├── External/              # Native WebP DLLs
│   ├── Forms/                 # WinForms UI windows
│   ├── Interfaces/            # IProgress, ITool
│   ├── Properties/            # AssemblyInfo, Resources
│   ├── Resources/             # Icons
│   ├── Streams/               # Stream classes
│   ├── Tools/                 # yt-dlp/ffmpeg wrappers
│   ├── UserControls/          # Track, Player, ClipboardMonitor
│   └── Utils/                 # ApplicationContext, WebP, Win32
```
