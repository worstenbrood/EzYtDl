# YtEzDL

**Yt-easy-download** — A Windows desktop application providing a user-friendly graphical interface for downloading and streaming audio/video content from YouTube and other yt-dlp supported websites.

**Version:** 2.4.0.0  
**Framework:** .NET Framework 4.8  
**Language:** C# 7.3  
**License:** MIT

---

## Table of Contents

- [Overview](#overview)
- [Features](#features)
- [Architecture](#architecture)
- [Project Structure](#project-structure)
- [Dependencies](#dependencies)
- [Building](#building)
- [Configuration](#configuration)
- [Usage](#usage)
- [Components](#components)
- [Classes Reference](#classes-reference)
- [API Reference](#api-reference)
- [Troubleshooting](#troubleshooting)
- [Changelog](#changelog)

---

## Overview

YtEzDL is a Windows Forms desktop application that serves as a GUI wrapper for the powerful [yt-dlp](https://github.com/yt-dlp/yt-dlp) command-line tool. The application provides:

- **Automatic clipboard monitoring** for YouTube and other supported URLs
- **Audio extraction** with customizable formats and quality settings
- **Video download** in multiple formats
- **Real-time audio preview** before downloading
- **Metadata embedding** for audio files
- **Thumbnail embedding** into audio files
- **Playlist support** with per-track selection and settings

The application is designed to be lightweight, with a minimal system tray presence and automatic startup with Windows.

---

## Features

### Core Features

| Feature | Description |
|---|---|
| **Clipboard Monitoring** | Automatically detects YouTube and yt-dlp supported URLs pasted to the clipboard |
| **Tray Icon** | System tray notification with context menu (settings, history, update, exit) |
| **Audio Extraction** | Extract audio from videos in MP3, M4A, FLAC, Opus, AAC, Vorbis, WAV formats |
| **Video Download** | Download full videos in MP4, FLV, Ogg, WebM, MKV, AVI formats |
| **Real-time Preview** | Stream and preview audio before downloading |
| **Playlist Support** | Download individual tracks or entire playlists |
| **Metadata Embedding** | Embed video metadata into audio files |
| **Thumbnail Embedding** | Embed video thumbnails into audio files |
| **Download History** | Track previous downloads |
| **Per-Track Settings** | Configure audio format and quality per track |
| **Multi-threaded Downloads** | Configurable thread count for concurrent downloads |
| **Auto-update yt-dlp** | Automatic updates for the yt-dlp engine |

### Advanced Features

| Feature | Description |
|---|---|
| **Auto-start** | Optional auto-start with Windows (registry-based) |
| **Mutex Protection** | Prevents multiple instances from running |
| **Progress Tracking** | Real-time progress for download and conversion phases |
| **Window State Persistence** | Remembers window size and position |
| **Color Themes** | MetroModernUI theme support with 15+ color styles |
| **Fast Thumbnail Fetching** | Optimized YouTube metadata fetch for playlists |
| **Debounced Clipboard** | 5-second cooldown to prevent excessive checks |

---

## Architecture

### Layered Architecture

```
┌─────────────────────────────────────────────┐
│              UI Layer                        │
│  Forms/ + UserControls/                      │
├─────────────────────────────────────────────┤
│           Business Logic Layer               │
│  Audio/ + Streams/                           │
├─────────────────────────────────────────────┤
│        Tool Integration Layer                │
│  Tools/ + Console/                           │
├─────────────────────────────────────────────┤
│           Data Model Layer                   │
│  DownLoad/                                   │
├─────────────────────────────────────────────┤
│           Persistence Layer                  │
│  Config/                                     │
├─────────────────────────────────────────────┤
│            Utilities                         │
│  Utils/ + Interfaces/                        │
└─────────────────────────────────────────────┘
```

### Data Flow

```
1. User copies URL to clipboard
       │
2. ApplicationContext detects URL via ClipboardMonitor
       │
3. DownloadForm opened with Track controls for each track
       │
4. User selects tracks and settings
       │
5. YoutubeDownload.DownloadAsync() called
       │
6. yt-dlp process runs with parameters from DownLoadParameters
       │
7. Progress parsed from stdout via regex:
   - [download] → Download(pct)
   - [ffmpeg] → FfMpeg(action, pct)
       │
8. Files cleaned up on process exit
       │
9. User can preview audio via AudioPlayer
```

### Process Flow

```
Application Start
    └─> Mutex check (prevent multiple instances)
        └─> ApplicationContext (system tray + clipboard monitoring)
            └─> Clipboard detects YouTube URL
                └─> DownloadForm shown
                    └─> YoutubeDownload.GetJsonAsync()
                        └─> yt-dlp -j → TrackData[]
                            └─> Track controls created
                                └─> User selects tracks
                                    └─> YoutubeDownload.DownloadAsync()
                                        └─> yt-dlp + ffmpeg
                                            └─> Progress via IProgress callbacks
```

### Key Design Patterns

| Pattern | Usage |
|---|---|
| **Singleton** | ApplicationContext, YoutubeDownload, ConsoleProcess, AudioPlayer, Configuration |
| **Observer** | IProgress interface for real-time progress notifications |
| **Builder/Fluent API** | DownLoadParameters for yt-dlp CLI argument construction |
| **Decorator** | EventStream for stream instrumentation |
| **Strategy** | AudioFormat and AudioQuality enums for different encoding strategies |
| **Repository** | Configuration and History for data persistence |

---

## Project Structure

### File Index

```
YtEzDL/
├── Program.cs                           # Application entry point, mutex check
├── YtEzDL.csproj                        # Project file
├── packages.config                      # NuGet packages
│
├── Audio/
│   └── AudioPlayer.cs                   # Audio playback engine
│
├── Build/
│   ├── PreBuild.cmd                     # Git revision embedding in AssemblyInfo
│   └── PostBuild.cmd                    # Tool copying (yt-dlp, FFmpeg)
│
├── Config/
│   ├── Configuration.cs                 # Main configuration singleton
│   ├── JsonFile.cs                      # Base class for JSON persistence
│   ├── History.cs                       # Download history management
│   ├── LockedProperty.cs                # Thread-safe generic property wrapper
│   └── Settings/
│       ├── AdvancedSettings.cs          # UpdateChannel enum
│       ├── ApplicationSettings.cs       # Autostart, clipboard, history
│       ├── DownloadSettings.cs          # Audio/video format, quality
│       ├── FileSettings.cs              # Download path settings
│       └── LayoutSettings.cs            # UI layout settings
│
├── Console/
│   ├── ConsoleProcess.cs                # Generic console process manager
│   └── ConsoleProcessException.cs       # Exception with exit code
│
├── DownLoad/
│   ├── DownLoadParameters.cs            # Fluent builder for yt-dlp CLI args
│   ├── Parameters.cs                    # Generic dictionary-based CLI builder
│   ├── Thumbnail.cs                     # DTO for thumbnail info
│   └── TrackData.cs                     # DTO for yt-dlp JSON metadata
│
├── External/
│   ├── libsharpyuv_x64.dll              # SharpYUV x64
│   ├── libsharpyuv_x86.dll              # SharpYUV x86
│   ├── libwebp_x64.dll                  # libwebp x64
│   └── libwebp_x86.dll                  # libwebp x86
│
├── Forms/
│   ├── About.cs                         # About dialog
│   ├── DownloadForm.cs                  # Main download management form
│   └── Settings.cs                      # Settings dialog (tabbed)
│
├── Interfaces/
│   ├── IProgress.cs                     # Progress reporting interface
│   └── ITool.cs                         # Tool interface for console executables
│
├── Properties/
│   ├── AssemblyInfo.cs                  # Auto-generated from template
│   ├── AssemblyInfo.Template.cs         # Template for PreBuild.cmd
│   └── Resources.resx                   # Embedded resources
│
├── Resources/
│   └── YTIcon.ico                       # Application icon
│
├── Streams/
│   ├── ConsoleStream.cs                 # Console process stream
│   ├── EventStream.cs                   # Decorator stream for events
│   ├── FfMpegStream.cs                  # yt-dlp → ffmpeg → stdout stream
│   ├── FfPlayStream.cs                  # Stream to ffplay
│   └── YtDlStream.cs                    # Raw yt-dlp output stream
│
├── Tools/
│   ├── FfMpeg.cs                        # FFmpeg wrapper
│   ├── FfPlay.cs                        # ffplay wrapper
│   └── YoutubeDownload.cs               # Core yt-dlp wrapper
│
├── UserControls/
│   ├── ClipboardMonitor.cs              # Clipboard monitoring via Win32 API
│   ├── CustomLayoutPanel.cs             # Flicker-free FlowLayoutPanel
│   ├── CustomToolStrip.cs               # Custom ToolStrip with border
│   ├── MetroToolStripProgressBar.cs     # Metro progress bar wrapper
│   ├── Player.cs                        # Audio playback control
│   ├── ScrollTextBox.cs                 # Custom TextBox with scroll redirection
│   └── Track.cs                         # Single track/playlist item control
│
└── Utils/
    ├── ApplicationContext.cs            # System tray icon, clipboard monitoring
    ├── AppStyle.cs                      # MetroModernUI style management
    ├── CommonTools.cs                   # Static utilities
    ├── FormTools.cs                     # WinForms binding helpers
    ├── ImageTools.cs                    # Image manipulation utilities
    ├── LimitedConcurrencyLevelTaskScheduler.cs  # Custom TaskScheduler
    ├── ProcessTools.cs                  # Process management utilities
    ├── TimedVariable.cs                 # Auto-resetting variable with timer
    ├── WebPWrapper.cs                   # Complete libwebp C# wrapper
    └── Win32.cs                         # Win32 API P/Invoke declarations
```

---

## Dependencies

### .NET Framework

- **.NET Framework 4.8** — Core runtime

### NuGet Packages

| Package | Version | Purpose |
|---|---|---|
| **MetroModernUI** | 1.4.0.0 | Modern flat-design UI framework |
| **NAudio** | 2.2.1 | Audio playback (WASAPI, wave streams) |
| **NAudio.Core** | 2.2.1 | NAudio core abstractions |
| **NAudio.Wasapi** | 2.2.1 | WASAPI audio output |
| **Newtonsoft.Json** | 13.0.3 | JSON serialization |

### Native Dependencies (External/)

| DLL | Purpose | Arch |
|---|---|---|
| **libsharpyuv_x64.dll** | SharpYUV color space conversion | x64 |
| **libsharpyuv_x86.dll** | SharpYUV color space conversion | x86 |
| **libwebp_x64.dll** | libwebp native library | x64 |
| **libwebp_x86.dll** | libwebp native library | x86 |

### External Tools

| Tool | Purpose | Location |
|---|---|---|
| **yt-dlp** | Core download engine | `Tools\yt-dlp.exe` |
| **FFmpeg** | Audio/video conversion, streaming | `Tools\ffmpeg.exe` |
| **ffplay** | Audio visualization (removed from tools) | — |

---

## Building

### Build Configurations

| Configuration | Description |
|---|---|
| **Debug** | Full debug symbols, no optimizations |
| **Release** | Optimized, requires yt-dlp and FFmpeg in `Tools/` |
| **Release-external** | Auto-downloads yt-dlp and FFmpeg (recommended) |

### Build Scripts

#### PreBuild.cmd

1. Reads Git commit hash (full and short)
2. Processes `AssemblyInfo.Template.cs` replacing `$revision$` and `$revshort$`
3. Writes `AssemblyInfo.cs`

#### PostBuild.cmd

1. Copies native DLLs from `External/` to output
2. Deletes `.xml` files
3. **If Release-external**: Downloads yt-dlp and FFmpeg

### Build Commands

```powershell
# MSBuild
msbuild YtEzDL.sln /p:Configuration=Release-external

# dotnet CLI
dotnet build YtEzDL.sln --configuration Release-external
```

### Build Output

| Config | Output Path |
|---|---|
| Debug | `YtEzDL\bin\Debug\` |
| Release | `YtEzDL\bin\Release\` |
| Release-external | `YtEzDL\bin\Release-external\` |

---

## Configuration

### Configuration File

| Setting | Path |
|---|---|
| Configuration | `%USERPROFILE%\YtEzDL\ezytdl.json` |
| History | `%USERPROFILE%\YtEzDL\History.json` |
| Tools | `<app-dir>\Tools\` |

### Settings Categories

#### FileSettings

| Property | Type | Default | Description |
|---|---|---|---|
| Path | string | Music folder | Default download path |
| CreatePlaylistFolder | bool | false | Create subfolder per playlist |

#### DownloadSettings

| Property | Type | Default | Description |
|---|---|---|---|
| DownloadThreads | int | 2 | Concurrent download threads (min: 2) |
| ExtractAudio | bool | false | Extract audio from video |
| AddMetadata | bool | false | Add metadata to audio files |
| EmbedThumbnail | bool | false | Embed thumbnail into audio |
| AudioFormat | AudioFormat | Best | Audio output format |
| AudioQuality | AudioQuality | Best | Audio quality preset |
| VideoFormat | VideoFormat | Mp4 | Video output format |

#### LayoutSettings

| Property | Type | Default | Description |
|---|---|---|---|
| AutoSelect | bool | true | Auto-select all tracks in playlists |
| SelectionWidth | int | 1 | Selection border width (1-10) |
| PerTrackSettings | bool | false | Per-track audio settings |
| FetchThumbnail | bool | true | Fetch thumbnails for tracks |
| FetchBestThumbnail | bool | false | Fetch highest quality thumbnail |
| YoutubeFastFetch | bool | false | Optimized YouTube metadata fetch |
| ColorStyle | MetroColorStyle | Default | Color theme |
| WindowState | FormWindowState | Normal | Window state |
| WindowSize | Size | — | Window size |

#### ApplicationSettings

| Property | Type | Default | Description |
|---|---|---|---|
| Autostart | bool | false | Auto-start with Windows |
| AdvancedSettings | bool | false | Show advanced settings tab |
| CaptureClipboard | bool | true | Monitor clipboard for URLs |
| EnableHistory | bool | true | Enable download history |

#### AdvancedSettings

| Property | Type | Default | Description |
|---|---|---|---|
| UpdateChannel | UpdateChannel | Stable | yt-dlp update channel |

### Enumerations

#### AudioFormat

| Value | Description |
|---|---|
| Best | Best available format |
| Aac | AAC |
| Flac | FLAC |
| Mp3 | MP3 |
| M4A | M4A |
| Mp4 | MP4 |
| Opus | Opus |
| Vorbis | Vorbis |
| Wav | WAV |
| S16Le | 16-bit little-endian PCM |
| Nut | NUT format |

#### AudioQuality

| Value | Bitrate | Description |
|---|---|---|
| Best | — | Best quality |
| Medium | 128k | Medium quality |
| Worst | 64k | Low quality |
| Cbr128 | 128k | Constant bitrate 128 kbps |
| Cbr192 | 192k | Constant bitrate 192 kbps |
| Cbr256 | 256k | Constant bitrate 256 kbps |
| Cbr320 | 320k | Constant bitrate 320 kbps |

#### VideoFormat

| Value | Description |
|---|---|
| Mp4 | MP4 |
| Flv | FLV |
| Ogg | Ogg |
| Webm | WebM |
| Mkv | MKV |
| Avi | AVI |

#### UpdateChannel

| Value | Description |
|---|---|
| Stable | Stable releases |
| Master | Latest master branch |
| Nightly | Nightly builds |

#### MetroColorStyle

| Value | Color |
|---|---|
| Default | Blue |
| Black | Black |
| White | White |
| Silver | Silver |
| Blue | Blue |
| Green | Green |
| Lime | Lime |
| Teal | Teal |
| Orange | Orange |
| Brown | Brown |
| Pink | Pink |
| Magenta | Magenta |
| Purple | Purple |
| Red | Red |
| Yellow | Yellow |

---

## Usage

### Basic Workflow

1. **Start the Application** — Run `YtEzDL.exe`
2. **Copy a URL** — Copy a YouTube URL to the clipboard
3. **Download Dialog** — The application detects the URL and opens the download dialog
4. **Select Tracks** — For playlists, select which tracks to download
5. **Download** — Click the download button
6. **Preview** — Click the play button on a track thumbnail to preview

### System Tray Menu

| Menu Item | Description |
|---|---|
| **Capture Clipboard** | Toggle clipboard monitoring |
| **History** | View download history |
| **About** | Show version information |
| **Settings** | Open settings dialog |
| **Clear Cache** | Clear yt-dlp cache |
| **Update** | Update yt-dlp |
| **Exit** | Close the application |

---

## Components

### Audio Preview Pipeline

```
yt-dlp → FFmpeg → WasapiOut → Audio Renderer
    ↓
-ss (seek position)
AudioFormat (e.g., WAV, FLAC)
-af "volume=1.0"
```

1. yt-dlp streams the audio from the URL
2. FFmpeg converts it in real-time
3. NAudio's WasapiOut plays the audio

### Stream Architecture

```
┌─────────────────────────────────────────────────────────────────┐
│                       Stream Hierarchy                          │
├─────────────────────────────────────────────────────────────────┤
│  ConsoleStream                                                   │
│  └── YtDlStream (yt-dlp output)                                 │
│      └── FfMpegStream (yt-dlp → ffmpeg → stdout)                │
│          └── FfPlayStream (yt-dlp → ffplay)                     │
├─────────────────────────────────────────────────────────────────┤
│  AudioPlayer                                                     │
│  └── RawSourceWaveStream (from FfMpegStream)                    │
│      └── WasapiOut (WASAPI audio output)                        │
└─────────────────────────────────────────────────────────────────┘
```

---

## Classes Reference

### UI Layer

#### Forms

| Class | Purpose |
|---|---|
| `About` | About dialog showing version info for all components |
| `DownloadForm` | Main download management form |
| `Settings` | Settings dialog with tabbed interface |

#### UserControls

| Class | Purpose |
|---|---|
| `ClipboardMonitor` | Clipboard monitoring via Win32 API |
| `Track` | Single track/playlist item control |
| `Player` | Audio playback control with track bar |
| `ScrollTextBox` | Custom TextBox with scroll redirection |
| `CustomLayoutPanel` | Flicker-free FlowLayoutPanel |
| `CustomToolStrip` | Custom ToolStrip with border display |
| `MetroToolStripProgressBar` | Metro-themed progress bar wrapper |

### Business Logic Layer

#### Audio

| Class | Purpose |
|---|---|
| `AudioPlayer` | NAudio WASAPI-based audio playback engine |

#### Streams

| Class | Purpose |
|---|---|
| `ConsoleStream` | Console process stream with cancellation support |
| `EventStream` | Decorator stream for read/write event firing |
| `FfMpegStream` | yt-dlp → ffmpeg → stdout stream for audio |
| `FfPlayStream` | Stream to ffplay for visualization |
| `YtDlStream` | Raw yt-dlp output stream with time offset |

### Tool Integration Layer

#### Tools

| Class | Purpose |
|---|---|
| `YoutubeDownload` | Core yt-dlp wrapper (download, metadata, streaming) |
| `FfMpeg` | FFmpeg wrapper, implements ITool |
| `FfPlay` | ffplay wrapper, implements ITool |

#### Console

| Class | Purpose |
|---|---|
| `ConsoleProcess<T>` | Generic console process manager |
| `ConsoleProcessException` | Exception carrying exit code |

### Data Model Layer

#### DownLoad

| Class | Purpose |
|---|---|
| `DownLoadParameters` | Fluent builder for yt-dlp CLI arguments |
| `Parameters<T>` | Generic dictionary-based CLI parameter builder |
| `TrackData` | DTO for yt-dlp JSON metadata |
| `Thumbnail` | DTO for thumbnail info |

### Persistence Layer

#### Config

| Class | Purpose |
|---|---|
| `Configuration` | Main configuration singleton |
| `JsonFile` | Base class for JSON persistence |
| `History` | Download history management |
| `LockedProperty` | Thread-safe generic property wrapper |
| `AdvancedSettings` | UpdateChannel enum |
| `ApplicationSettings` | Autostart, clipboard, history settings |
| `DownloadSettings` | Audio/video format, quality settings |
| `FileSettings` | Download path settings |
| `LayoutSettings` | UI layout settings |

### Utilities

#### Utils

| Class | Purpose |
|---|---|
| `ApplicationContext` | System tray icon, clipboard monitoring |
| `AppStyle` | MetroModernUI style management |
| `CommonTools` | Static utilities (path handling, auto-start) |
| `FormTools` | WinForms binding helpers, color mapping |
| `ImageTools` | Image manipulation utilities |
| `LimitedConcurrencyLevelTaskScheduler` | Custom TaskScheduler for download limiting |
| `ProcessTools` | Process management utilities |
| `TimedVariable` | Auto-resetting variable with timer |
| `WebPWrapper` | Complete libwebp C# wrapper |
| `Win32` | Win32 API P/Invoke declarations |

### Interfaces

#### Interfaces

| Interface | Purpose |
|---|---|
| `ITool` | Interface for console executables |
| `IProgress` | Progress reporting interface |

---

## API Reference

### AudioPlayer

| Property/Method | Type | Description |
|---|---|---|
| `Rate` | const int | 44100 Hz |
| `Bits` | const int | 16 bits |
| `Channels` | const int | 2 (stereo) |
| `Format` | static WaveFormat | 44100Hz, 16-bit, stereo |
| `PlaybackState` | PlaybackState | Current playback state |
| `Position` | long | Current playback position |
| `Volume` | float | Volume (0.0–1.0) |
| `Play(string url, TimeSpan position)` | void | Play from URL with start position |
| `Play(string url)` | void | Play from URL (start at 0) |
| `Play(TimeSpan position)` | void | Resume from position |
| `Play()` | void | Resume from 0 |
| `Pause()` | void | Pause playback |
| `Resume()` | void | Resume playback |
| `Reset(bool triggerEvent)` | void | Stop and clean up |
| `PlaybackStopped` | event | Fired when playback stops |
| `StreamRead` | event | Fired on stream read |

### AudioFormat

| Member | Value |
|---|---|
| `Best` | Best available |
| `Aac` | AAC |
| `Flac` | FLAC |
| `Mp3` | MP3 |
| `M4A` | M4A |
| `Mp4` | MP4 |
| `Opus` | Opus |
| `Vorbis` | Vorbis |
| `Wav` | WAV |
| `S16Le` | 16-bit little-endian PCM |
| `Nut` | NUT format |

### AudioQuality

| Member | Bitrate |
|---|---|
| `Best` | — |
| `Medium` | 128k |
| `Worst` | 64k |
| `Cbr128` | 128k |
| `Cbr192` | 192k |
| `Cbr256` | 256k |
| `Cbr320` | 320k |

### YoutubeDownload (ITool)

| Property/Method | Type | Description |
|---|---|---|
| `Instance` | static | Singleton instance |
| `Path` | static | Path to yt-dlp.exe |
| `GetPath()` | string | Implementation of ITool.GetPath() |
| `GetVersion()` | string | Implementation of ITool.GetVersion() |
| `DownloadAsync(...)` | Task | Download with progress tracking |
| `GetJsonAsync(...)` | Task | Fetch metadata as JSON |
| `Update(...)` | void | Update yt-dlp |
| `CreateStreamProcess(...)` | Process | Create stream process |
| `StreamAsync(...)` | Task | Stream audio to output |
| `DownloadAction` | enum | Download phase enum |
| `RunAsync(...)` | Task | Run yt-dlp and capture output |
| `Run(...)` | int | Run synchronously |

### FfMpeg (ITool)

| Property/Method | Type | Description |
|---|---|---|
| `Instance` | static | Singleton instance |
| `GetPath()` | string | Implementation of ITool.GetPath() |
| `GetVersion()` | string | Implementation of ITool.GetVersion() |
| `CreateStreamProcess(...)` | Process | Create ffmpeg stream process |
| `UrlToAudioStreamAsync(...)` | static Task | Stream URL to audio format |

### WebP (WebPWrapper)

| Method | Return | Description |
|---|---|---|
| `Load(string pathFileName)` | Bitmap | Load WebP file |
| `Decode(byte[] rawWebP)` | Bitmap | Decode WebP data |
| `Decode(byte[] rawWebP, WebPDecoderOptions options)` | Bitmap | Decode with options |
| `GetThumbnailFast(byte[] rawWebP, int width, int height)` | Bitmap | Fast thumbnail |
| `GetThumbnailQuality(byte[] rawWebP, int width, int height)` | Bitmap | Quality thumbnail |
| `Save(Bitmap bmp, string pathFileName, int quality)` | void | Save bitmap to WebP |
| `EncodeLossy(Bitmap bmp, int quality)` | byte[] | Lossy encode |
| `EncodeLossy(Bitmap bmp, int quality, int speed)` | byte[] | Lossy with speed |
| `EncodeLossless(Bitmap bmp)` | byte[] | Lossless encode |
| `EncodeLossless(Bitmap bmp, int speed)` | byte[] | Lossless with speed |
| `EncodeNearLossless(Bitmap bmp, int quality)` | byte[] | Near lossless encode |
| `GetVersion()` | string | Library version |
| `GetInfo(byte[] rawWebP, ...)` | void | Get WebP image info |
| `GetPictureDistortion(Bitmap, Bitmap, int)` | float[] | Compute distortion metric |

### Configuration

| Property/Method | Type | Description |
|---|---|---|
| `Default` | static | Singleton instance |
| `FileSettings` | FileSettings | File-related settings |
| `DownloadSettings` | DownloadSettings | Download settings |
| `LayoutSettings` | LayoutSettings | UI layout settings |
| `ApplicationSettings` | ApplicationSettings | Application settings |
| `AdvancedSettings` | AdvancedSettings | Advanced settings |
| `Save()` | void | Save configuration to file |
| `Load()` | void | Load configuration from file |

### History

| Property/Method | Type | Description |
|---|---|---|
| `Default` | static | Singleton instance |
| `Count` | int | Number of history items |
| `Items` | List<HistoryItem> | History items list |
| `Clear()` | void | Clear all history |
| `Save()` | void | Save history to file |
| `Load()` | void | Load history from file |

### FormTools

| Method | Return | Description |
|---|---|---|
| `AddCheckedBinding<T>(CheckBox, T, Expression, ...)` | void | CheckBox data binding |
| `AddEnumBinding<TEnum, T>(ComboBox, T, Expression, ...)` | void | ComboBox enum binding |
| `AddTextBinding<T>(Control, T, Expression, ...)` | void | Control text binding |
| `AddRangeBinding<T>(ComboBox, T, Expression, int, int)` | void | ComboBox numeric range binding |
| `AddRangeBinding<T>(ComboBox, T, Expression, float, int)` | void | ComboBox float range binding |
| `GetTextHeight(Control)` | int | Measure text height |
| `ColorMapping` | static Dictionary | MetroColorStyle to Color mapping |
| `ShowActiveForm<T>()` | bool | Bring existing form to front |
| `ShowFormDialog<T>()` | void | Show form as dialog |

### CommonTools

| Property/Method | Type | Description |
|---|---|---|
| `ApplicationName` | static string | "YtEzDL" |
| `ApplicationPath` | static string | Assembly location |
| `ApplicationProductVersion` | static string | Product version |
| `EzYtDlProfilePath` | static string | User profile path |
| `ToolsPath` | static string | Tools directory path |
| `GetFileVersionInfo(string)` | static FileVersionInfo | Get file version info |
| `GetVersion<T>()` | static Version | Get assembly version |
| `ProfileFolderCombine(params string[])` | static string | Combine profile folder path |
| `SetAutoStart(bool)` | static void | Set/remove from Windows startup |
| `RemoveInvalidChars(string, char[], char)` | string | Remove invalid characters |
| `RemoveInvalidPathChars(string)` | string | Remove invalid path characters |
| `GetValidPath(string)` | string | Get valid path string |

### TimedVariable<T>

| Property/Method | Type | Description |
|---|---|---|
| `Value` | property | Current value |
| `TimedVariable(T defaultValue, int timeout, T resetValue)` | constructor | Initialize with timeout |

### Win32 (P/Invoke)

| Method | Description |
|---|---|
| `PostMessage(hwnd, msg, wParam, lParam)` | Post message to window |
| `HideCaret(hwnd)` | Hide caret |
| `SetParent(hwndChild, hwndNewParent)` | Set parent window |
| `AddClipboardFormatListener(hwnd)` | Register clipboard listener |
| `RemoveClipboardFormatListener(hwnd)` | Unregister clipboard listener |
| `EnumWindows(...)` | Enumerate windows |
| `EnumChildWindows(...)` | Enumerate child windows |
| `GetWindowThreadProcessId(hwnd, out int)` | Get thread/process ID |
| `GetClipboardSequenceNumber()` | Get clipboard sequence number |
| `CloseHandle(handle)` | Close handle |
| `OpenThread(desiredAccess, inherit, threadId)` | Open thread |
| `SuspendThread(handle)` | Suspend thread |
| `CreateToolhelp32Snapshot(flags, processId)` | Create process snapshot |
| `Process32First(snapshot, entry)` | First process entry |
| `Process32Next(snapshot, entry)` | Next process entry |

### IProgress

| Method | Description |
|---|---|
| `Download(double progress)` | Download progress |
| `FfMpeg(DownloadAction action, double progress)` | FFmpeg action progress |

### ITool

| Method | Description |
|---|---|
| `GetPath()` | Get path to executable |
| `GetVersion()` | Get version string |

---

## Troubleshooting

### yt-dlp / FFmpeg Not Found

**Problem:** Application shows errors about missing tools.

**Solution:**
1. Run the Release-external build — this auto-downloads both tools
2. Or manually download them and place in `Tools/`:
   - [yt-dlp.exe](https://github.com/yt-dlp/yt-dlp/releases/latest/download/yt-dlp.exe)
   - [FFmpeg](https://github.com/yt-dlp/FFmpeg-Builds/releases/latest/download/ffmpeg-master-latest-win64-gpl-shared.zip)

### Clipboard Monitoring Not Working

**Problem:** URLs pasted to clipboard don't trigger download dialog.

**Solution:**
1. Right-click tray icon → **Capture Clipboard** to toggle
2. Check that clipboard permissions are granted
3. The clipboard check has a 5-second cooldown

### Audio Preview Not Working

**Problem:** Cannot preview audio via play button.

**Solution:**
1. Ensure FFmpeg is properly installed in `Tools/` folder
2. Ensure `ffmpeg.exe` exists in the `Tools/` directory
3. Verify the audio format is compatible (e.g., WAV for preview)

### Download Fails with Permission Error

**Problem:** Download fails because of permission issues.

**Solution:**
1. Ensure the download path has write permissions
2. Verify the path exists and is accessible
3. Try a different download path

### Multiple Instances Running

**Problem:** Application allows multiple instances.

**Solution:** This should not happen. The mutex (`Mutex.TryOpenExisting`) prevents this. If it occurs, close via Task Manager and restart.

### Configuration Corrupted

**Problem:** Application won't start or settings don't load.

**Solution:** Delete the configuration file at `%USERPROFILE%\YtEzDL\ezytdl.json`. The application will recreate it on next startup.

### Thumbnail Display Issues

**Problem:** Thumbnails don't display in download form.

**Solution:**
1. Ensure native DLLs are in `External/` folder
2. Verify DLLs are copied to output directory (build process handles this)
3. Check `WebPWrapper` is properly initialized

### Update Issues

**Problem:** yt-dlp update fails or doesn't work.

**Solution:**
1. Check update channel setting (Stable/Master/Nightly)
2. Update via tray icon → **Update**
3. Or manually: run `yt-dlp -U` from command line

---

## Changelog

### Version 2.4.0

- Current version with Git revision tracking in assembly metadata
- Auto-download of yt-dlp and FFmpeg in Release-external build
- Complete WebP native library integration for thumbnail support
- Per-track audio format and quality settings
- Real-time audio preview via FFmpeg streaming
- Clipboard monitoring with 5-second debounce
- MetroModernUI theme support with 15+ color styles
- Download history management
- Process tree killing for proper cleanup
- Thread-safe configuration and history persistence

---

## License

MIT License

Copyright (c) 2020 Jose M. Piñeiro

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
