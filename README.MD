# YtEzDL

A Windows desktop application providing a user-friendly graphical interface for downloading and streaming audio/video content from YouTube and other yt-dlp supported websites.

**YtEzDL** = "Yt-easy-download" — the play on the `yt-dlp` command-line tool, making it effortless to download media with a modern Metro-style interface.

---

## Table of Contents

- [Features](#features)
- [Architecture](#architecture)
- [Getting Started](#getting-started)
- [Building](#building)
- [Configuration](#configuration)
- [Usage](#usage)
- [Dependencies](#dependencies)
- [External Tools](#external-tools)
- [Advanced](#advanced)
- [File Structure](#file-structure)
- [Troubleshooting](#troubleshooting)

---

## Features

### Core Features
- **Clipboard Monitoring** — Automatically detects YouTube and other yt-dlp supported URLs pasted to the clipboard, prompting the download dialog
- **Tray Icon** — System tray notification icon with quick access menu (settings, history, about, update, exit)
- **Audio Extraction** — Extract high-quality audio from videos in multiple formats (MP3, M4A, FLAC, Opus, AAC, Vorbis, WAV)
- **Video Download** — Download full videos in various formats (MP4, FLV, Ogg, WebM, MKV, AVI)
- **Real-time Preview** — Stream and preview audio before downloading using FFmpeg's ffplay
- **Playlist Support** — Download individual tracks or entire playlists with fine-grained selection control
- **Metadata Embedding** — Embed video metadata (title, artist, etc.) into audio files
- **Thumbnail Embedding** — Embed video thumbnails into audio files
- **Download History** — Keep track of previous downloads
- **Per-Track Settings** — Configure audio format and quality on a per-track basis
- **Multi-threaded Downloads** — Configurable download thread count for faster batch downloads

### Advanced Features
- **Auto-update yt-dlp** — Automatic updates for the yt-dlp engine
- **Custom Progress Tracking** — Real-time progress feedback during download and conversion phases
- **Window State Persistence** — Remembers window size and position
- **Color Theme Customization** — MetroModernUI theme support with multiple color styles
- **Fast Thumbnail Fetching** — Optimized thumbnail loading for faster UI rendering
- **Automatic Startup** — Optional auto-start with Windows
- **Mutex Protection** — Prevents multiple instances of the application from running simultaneously

---

## Architecture

### Solution Overview

```
YtEzDL/
├── YtEzDL.sln              # Solution file
├── README.md               # Solution-level documentation
└── YtEzDL/                 # Main project
    ├── YtEzDL.csproj       # Project file
    ├── packages.config     # NuGet packages
    ├── Program.cs          # Entry point
    ├── Audio/              # Audio playback engine
    ├── Build/              # Build scripts
    ├── Config/             # Configuration layer
    ├── Console/            # Console process management
    ├── DownLoad/           # Download parameters & DTOs
    ├── External/           # Native WebP DLLs
    ├── Forms/              # WinForms UI windows
    ├── Interfaces/         # Shared interfaces
    ├── Properties/         # Assembly info, resources
    ├── Resources/          # Embedded resources
    ├── Streams/            # Stream handling
    ├── Tools/              # External tool wrappers (yt-dlp, FFmpeg)
    ├── UserControls/       # Reusable UI controls
    └── Utils/              # Utility classes
```

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
└─────────────────────────────────────────────┘
```

### Data Flow

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

- **Singleton** — ApplicationContext, YoutubeDownload, ConsoleProcess, AudioPlayer
- **Observer** — IProgress interface for real-time progress notifications
- **Factory/Builder** — DownLoadParameters for fluent CLI argument construction
- **Decorator** — EventStream for stream instrumentation
- **Repository** — Configuration and History for data persistence

---

## Getting Started

### Prerequisites

- **Windows 10 or later**
- **.NET Framework 4.8** — Required runtime (usually pre-installed)
- **Visual Studio 2022** — For building the project
- **Git** — For PreBuild.cmd git revision embedding

### Quick Start (Pre-built)

1. Download the latest release from the GitHub releases page
2. Extract the zip archive to any location
3. Run `YtEzDL.exe`
4. Copy a YouTube URL to the clipboard
5. The download dialog will appear automatically
6. Select tracks and click Download

### Building from Source

See the [Building](#building) section below for detailed instructions.

---

## Building

### Build Configurations

| Configuration | Description |
|---|---|
| **Debug** | Full debug symbols, no optimizations, for development |
| **Release** | Optimized, requires yt-dlp and FFmpeg to already exist in the `Tools/` folder |
| **Release-external** | Optimized, auto-downloads the latest yt-dlp and FFmpeg into the `Tools/` folder (recommended for distribution) |
| **x64 Debug** | x64 build with debug symbols |
| **x64 Release** | x64 build, optimized, requires external tools |
| **x64 Release-external** | x64 build, auto-downloads tools |

### Build Process

The build process consists of two phases:

#### Pre-build

1. `Build\PreBuild.cmd` reads the current Git commit hash (both full and short)
2. The `Properties\AssemblyInfo.Template.cs` file is processed, replacing `$revision$` and `$revshort$` placeholders with actual values
3. The result is written to `Properties\AssemblyInfo.cs`
4. This allows each build to include the Git commit hash in the assembly's informational version

#### Post-build

1. `Build\PostBuild.cmd` copies native DLLs from the `External/` folder to the output directory
2. XML documentation files are cleaned up
3. **If Release-external**: Downloads the latest yt-dlp and FFmpeg binaries, extracts FFmpeg, and places everything in the `Tools/` folder:
   - yt-dlp.exe from GitHub Releases
   - FFmpeg-Builds zip from GitHub Releases
   - Extracts ffmpeg.exe and related files
   - Removes ffplay.exe (not needed)

### Build Commands

Open a **Developer Command Prompt for VS 2022** or use **Visual Studio**:

```powershell
# Using MSBuild via Developer Command Prompt
msbuild YtEzDL.sln /p:Configuration=Release-external /p:Platform=AnyCPU

# Using dotnet CLI
dotnet build YtEzDL.sln --configuration Release-external
```

### Build Output

- **Debug**: `YtEzDL\bin\Debug\`
- **Release**: `YtEzDL\bin\Release\`
- **Release-external**: `YtEzDL\bin\Release-external\`

The `Release-external` build will contain:
- `YtEzDL.exe`
- `YtEzDL.exe.config`
- All dependency DLLs
- `Tools/` folder with `yt-dlp.exe`, `ffmpeg.exe`, and related files
- Native WebP DLLs (`libwebp_x64.dll`, `libwebp_x86.dll`, etc.)

---

## Configuration

### Configuration File

The application stores its settings in a JSON file located at:

```
%USERPROFILE%\YtEzDL\ezytdl.json
```

### Thread-Safe Configuration

Configuration values use the `LockedProperty<T>` wrapper for thread safety. The `Configuration` class uses `Lazy<Configuration>` for thread-safe singleton initialization.

### Configuration Categories

#### FileSettings

| Setting | Description |
|---|---|
| `Path` | Default download path (defaults to Music folder) |
| `CreatePlaylistFolder` | Create a subfolder per playlist |

#### DownloadSettings

| Setting | Description |
|---|---|
| `DownloadThreads` | Number of concurrent download threads (min: 2) |
| `ExtractAudio` | Extract audio from video |
| `AddMetadata` | Add video metadata to audio |
| `EmbedThumbnail` | Embed video thumbnail into audio |
| `AudioFormat` | Audio output format (enum, see below) |
| `AudioQuality` | Audio quality/preset (enum, see below) |
| `VideoFormat` | Video output format (enum, see below) |

#### LayoutSettings

| Setting | Description |
|---|---|
| `AutoSelect` | Auto-select all tracks in playlists |
| `SelectionWidth` | Width of the selection border (1-10) |
| `PerTrackSettings` | Enable per-track audio settings |
| `FetchThumbnail` | Fetch thumbnails for tracks |
| `FetchBestThumbnail` | Fetch highest quality thumbnail |
| `YoutubeFastFetch` | Use optimized YouTube metadata fetch |
| `ColorStyle` | MetroModernUI color style (enum) |
| `WindowState` | Window state (Normal/Maximized/Minimized) |
| `WindowSize` | Window size (Size) |

#### ApplicationSettings

| Setting | Description |
|---|---|
| `Autostart` | Auto-start with Windows |
| `AdvancedSettings` | Show advanced settings tab |
| `CaptureClipboard` | Monitor clipboard for URLs |
| `EnableHistory` | Enable download history |

#### AdvancedSettings

| Setting | Description |
|---|---|
| `UpdateChannel` | yt-dlp update channel: Stable / Master / Nightly |

### History

Download history is stored in:

```
%USERPROFILE%\YtEzDL\History.json
```

History items include `Title`, `Url`, and `Id`. The history is deduplicated by ID and respects the `EnableHistory` setting.

---

## Usage

### Basic Workflow

1. **Start the Application** — Run `YtEzDL.exe`. A tray icon appears in the system tray.
2. **Copy a URL** — Copy a YouTube (or yt-dlp supported site) URL to the clipboard.
3. **Download Dialog Appears** — The application detects the URL and opens the DownloadForm.
4. **Select Tracks** — For playlists, select which tracks to download.
5. **Download** — Click the download button. Progress is shown for each track.
6. **Preview** — Click the play button on a track thumbnail to preview the audio before downloading.

### System Tray Menu

- **Capture Clipboard** — Enable/disable clipboard monitoring
- **History** — View download history
- **About** — Show about dialog with version information
- **Settings** — Open the settings dialog
- **Clear Cache** — Clear yt-dlp cache data
- **Update** — Update yt-dlp to the latest version
- **Exit** — Close the application

### Download Form

- **Track Selection** — Select individual tracks or use Select All/Select None
- **Per-Track Settings** — When enabled, each track has its own audio format/quality settings
- **Search/Filter** — Filter tracks by title
- **Sort** — Sort tracks by title or length
- **Progress** — Each track shows real-time progress
- **Cancel** — Cancel downloads for specific tracks or all
- **Thumbnail Preview** — Click play button on a thumbnail to preview audio
- **Window State** — Window remembers size and position

### Settings Dialog

| Tab | Settings |
|---|---|
| **File** | Download path, create playlist folder |
| **Download** | Extract audio, add metadata, embed thumbnail, audio format/quality, video format, threads count |
| **Layout** | Auto-select, fetch thumbnail, fetch best thumbnail, color style, selection width, per-track settings |
| **Advanced** | Update channel, autostart, advanced settings toggle, history |

---

## Dependencies

### .NET Framework

- **.NET Framework 4.8** — Core runtime

### NuGet Packages

| Package | Version | Purpose |
|---|---|---|
| **MetroModernUI** | 1.4.0.0 | Modern flat-design UI framework (Metro/Fluent style) |
| **NAudio** | 2.2.1 | Audio playback (WASAPI output, wave stream handling) |
| **NAudio.Core** | 2.2.1 | NAudio core abstractions |
| **NAudio.Wasapi** | 2.2.1 | WASAPI audio output for low-latency playback |
| **Newtonsoft.Json** | 13.0.3 | JSON serialization for configuration and yt-dlp data parsing |

### Native Dependencies (External/)

| DLL | Purpose | Architecture |
|---|---|---|
| **libsharpyuv_x64.dll** | SharpYUV color space conversion (x64) | x64 |
| **libsharpyuv_x86.dll** | SharpYUV color space conversion (x86) | x86 |
| **libwebp_x64.dll** | libwebp native library (x64) | x64 |
| **libwebp_x86.dll** | libwebp native library (x86) | x86 |

These are used by `WebPWrapper` for decoding WebP images (YouTube thumbnails are often in WebP format).

---

## External Tools

### yt-dlp

- **Purpose**: Core download engine — handles URL parsing, metadata extraction, and file downloading
- **Location**: `Tools\yt-dlp.exe` (Downloaded automatically by Release-external build)
- **Update Channel**: Configurable via Settings → Advanced → Update Channel
  - **Stable** — Stable releases
  - **Master** — Latest master branch
  - **Nightly** — Nightly builds
- **Update Command**: `yt-dlp -U`

### FFmpeg

- **Purpose**: Audio/video conversion, extraction, and streaming
- **Location**: `Tools\ffmpeg.exe` (Downloaded automatically by Release-external build)
- **Key Uses**:
  - Audio format conversion (e.g., converting m4a to mp3)
  - Audio extraction from video files
  - Real-time audio streaming for preview (via ffplay)
  - Metadata embedding into audio files
  - Thumbnail extraction and embedding

### ffplay

- **Purpose**: Audio visualization for preview (disabled in the application, removed during build)
- **Note**: The `ffplay.exe` is deleted from the `Tools/` folder during the Release-external build as it is not needed.

---

## Advanced

### Preventing Multiple Instances

The application uses a named `Mutex` based on the assembly full name to prevent multiple instances. If a second instance is started, it shows a MessageBox and exits immediately.

```csharp
var mutexName = Assembly.GetExecutingAssembly().FullName;
if (Mutex.TryOpenExisting(mutexName, out _))
{
    // Another instance is running
    return;
}
```

### Clipboard Monitoring

The application uses the Win32 API `AddClipboardFormatListener` to register as a clipboard format listener. When the clipboard changes, it checks for YouTube URLs and opens the download dialog if found.

Clipboard checks are debounced with a 5-second cooldown (via `TimedVariable`) to prevent excessive processing.

### Concurrent Downloads

The `LimitedConcurrencyLevelTaskScheduler` limits the degree of parallelism for downloads based on the `DownloadThreads` setting. This prevents overwhelming the system or network with too many concurrent connections.

### Process Management

The application uses the `ConsoleProcess<T>` base class to manage yt-dlp and FFmpeg processes:

- Redirected stdout/stderr/stdin
- Asynchronous stream reading with `BeginOutputReadLine`
- Process tree killing via `KillProcessTree()` (suspends all threads, then kills the process and its children)
- Progress parsing via regex patterns:
  - `[download]` — Download progress
  - `[ffmpeg]` — FFmpeg conversion progress
  - `[download] 100%` — Download complete
  - `[metadata]` — Metadata embedding
  - `[thumbnail]` — Thumbnail processing

### Audio Preview Pipeline

```
yt-dlp → FFmpeg → WasapiOut → Audio Renderer
    ↓
-ss (seek position)
AudioFormat (e.g., WAV, FLAC)
-af "volume=1.0"
```

The audio preview pipeline:
1. yt-dlp streams the audio from the URL
2. FFmpeg converts it in real-time (e.g., m4a to WAV, adjusting audio quality)
3. The converted stream is fed to NAudio's `WasapiOut` for playback

### Thread Safety

- `Configuration` uses `lock` on a private `_lock` object for JSON serialization
- `History` uses `lock` on a private `_lock` object for list modifications
- `TimedVariable` uses `System.Timers.Timer` with thread-safe callbacks
- `ConsoleProcess.IsRunning` uses `Interlocked` for concurrent process count

---

## File Structure

### Solution Level

```
YtEzDL/
├── .gitignore                          # Git ignore rules
├── YtEzDL.sln                          # Visual Studio solution file
├── README.md                           # Solution-level README (this file)
├── YtEzDL.sln.DotSettings              # ReSharper/StyleCop settings
└── YtEzDL/                             # Main project
```

### Project Level (`YtEzDL/`)

#### Entry Point

| File | Purpose |
|---|---|
| `Program.cs` | Application entry point, mutex check, ApplicationContext |

#### UI Layer (`Forms/`, `UserControls/`)

| File | Purpose |
|---|---|
| `Forms/About.cs` | About dialog (version info for all components) |
| `Forms/Settings.cs` | Settings dialog with tabbed interface |
| `Forms/DownloadForm.cs` | Main download management form |
| `UserControls/ClipboardMonitor.cs` | Clipboard monitoring via Win32 API |
| `UserControls/Track.cs` | Single track/playlist item control |
| `UserControls/Player.cs` | Audio playback control with track bar |
| `UserControls/ScrollTextBox.cs` | Custom TextBox with scroll redirection |
| `UserControls/CustomToolStrip.cs` | Custom ToolStrip with border display |
| `UserControls/CustomLayoutPanel.cs` | Flicker-free FlowLayoutPanel |
| `UserControls/MetroToolStripProgressBar.cs` | Metro-themed progress bar for ToolStrip |

#### Business Logic (`Audio/`, `Streams/`)

| File | Purpose |
|---|---|
| `Audio/AudioPlayer.cs` | NAudio WASAPI-based audio playback engine |
| `Streams/ConsoleStream.cs` | Console process stream with cancellation support |
| `Streams/EventStream.cs` | Decorator stream for read/write event firing |
| `Streams/FfMpegStream.cs` | yt-dlp → FFmpeg → stdout stream for audio |
| `Streams/FfPlayStream.cs` | Stream to ffplay for visualization |
| `Streams/YtDlStream.cs` | Raw yt-dlp output stream with time offset |

#### Tool Integration (`Tools/`, `Console/`)

| File | Purpose |
|---|---|
| `Tools/YoutubeDownload.cs` | Core yt-dlp wrapper (download, metadata, streaming) |
| `Tools/FfMpeg.cs` | FFmpeg wrapper, implements ITool |
| `Tools/FfPlay.cs` | ffplay wrapper, implements ITool |
| `Console/ConsoleProcess.cs` | Generic console process manager |
| `Console/ConsoleProcessException.cs` | Exception with exit code |

#### Data Model (`DownLoad/`)

| File | Purpose |
|---|---|
| `DownLoad/DownLoadParameters.cs` | Fluent builder for yt-dlp CLI arguments |
| `DownLoad/Parameters.cs` | Generic dictionary-based CLI parameter builder |
| `DownLoad/TrackData.cs` | DTO for yt-dlp JSON metadata |
| `DownLoad/Thumbnail.cs` | DTO for thumbnail info |

#### Configuration (`Config/`)

| File | Purpose |
|---|---|
| `Config/Configuration.cs` | Main configuration singleton |
| `Config/JsonFile.cs` | Base class for JSON persistence |
| `Config/History.cs` | Download history management |
| `Config/LockedProperty.cs` | Thread-safe generic property wrapper |
| `Config/Settings/` | Settings classes |

#### Utilities (`Utils/`)

| File | Purpose |
|---|---|
| `Utils/ApplicationContext.cs` | System tray icon, clipboard monitoring |
| `Utils/AppStyle.cs` | MetroModernUI style management |
| `Utils/CommonTools.cs` | Static utilities (path handling, auto-start) |
| `Utils/FormTools.cs` | WinForms binding helpers, color mapping |
| `Utils/ImageTools.cs` | Image manipulation (resize, download, WebP) |
| `Utils/LimitedConcurrencyLevelTaskScheduler.cs` | Custom TaskScheduler for download limiting |
| `Utils/ProcessTools.cs` | Process management (kill tree, suspend) |
| `Utils/TimedVariable.cs` | Auto-resetting variable with timer |
| `Utils/WebPWrapper.cs` | Complete libwebp C# wrapper |
| `Utils/Win32.cs` | Win32 API P/Invoke declarations |

#### Build Scripts (`Build/`)

| File | Purpose |
|---|---|
| `Build/PreBuild.cmd` | Git revision embedding in AssemblyInfo |
| `Build/PostBuild.cmd` | Tool copying (yt-dlp, FFmpeg for Release-external) |

#### Resources

| File | Purpose |
|---|---|
| `YTIcon.ico` | Application icon |
| `Properties/AssemblyInfo.cs` | Auto-generated from template with Git revision |
| `Properties/AssemblyInfo.Template.cs` | Template for PreBuild.cmd |
| `Properties/Resources.resx` | Embedded resources (Play/Pause/Stop icons) |
| `External/*.dll` | Native WebP library DLLs |
| `Tools/` | Downloaded yt-dlp.exe, ffmpeg.exe (Release-external) |

---

## Troubleshooting

### yt-dlp / FFmpeg Not Found

If you see errors about yt-dlp or FFmpeg not being found:

1. Run the **Release-external** build configuration — this auto-downloads both tools
2. Or manually download them and place them in the `Tools/` folder:
   - [yt-dlp.exe](https://github.com/yt-dlp/yt-dlp/releases/latest/download/yt-dlp.exe)
   - [FFmpeg](https://github.com/yt-dlp/FFmpeg-Builds/releases/latest/download/ffmpeg-master-latest-win64-gpl-shared.zip)

### Clipboard Monitoring Not Working

- Right-click the tray icon → **Capture Clipboard** to toggle
- Check that clipboard permissions are granted to the application
- The clipboard check has a 5-second cooldown to prevent excessive processing

### Audio Preview Not Working

- Ensure FFmpeg is properly installed in the `Tools/` folder
- Check that the `Tools/` folder contains `ffmpeg.exe`
- Ensure the audio format is compatible (e.g., WAV for preview)

### Download Fails with Permission Error

- Ensure the download path has write permissions
- Check that the path exists and is accessible
- Try a different download path (e.g., Documents folder)

### Multiple Instances Running

This should not happen — the mutex should prevent it. If it does, close the application via Task Manager and restart.

### Configuration Corrupted

The configuration file is located at `%USERPROFILE%\YtEzDL\ezytdl.json`. If corrupted, delete it to reset all settings to defaults. The application will recreate it on next startup.

### Thumbnail Display Issues

- WebP thumbnails require the native DLLs in the `External/` folder
- Ensure the DLLs are copied to the output directory (the build process handles this)
- If thumbnails don't display, check that `WebPWrapper` is properly initialized

### Update Issues

- Check the update channel setting (Stable/Master/Nightly)
- Manually update: Right-click tray icon → **Update**
- Or run `yt-dlp -U` from the command line

---

## License

MIT License

Copyright (c) 2020 Jose M. Piñeiro

See the [LICENSE](LICENSE) file for details.
