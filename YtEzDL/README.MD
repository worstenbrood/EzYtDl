EzYtDl is a windows (.net) frontend for yt-dlp (or youtube-dl). It monitors the clipboard for urls and passes those to
yt-dlp. yt-dlp's json output is used to fetch the data. Audio/video processing is done by ffmpeg.

Build
=====

1. Install a recent powershell version (https://github.com/PowerShell/PowerShell/releases)
2. Make sure git is installed and available in the path environment variable (https://gitforwindows.org/)
3. Build Release-external, it will automatically fetch the latest yt-dlp and ffmpeg to the tools folder (see Build\PostBuild.cmd)
4. Start YtEzDl.exe from the root folder
5. There will be tray icon, right click for settings and some other options.
6. Copy a supported link to the clipboard

Dependencies
============

- .NET Framework 4.8
- MetroFramework (http://thielj.github.io/MetroFramework/)
- yt-dlp (https://github.com/yt-dlp/yt-dlp)
- ffmpeg (https://www.ffmpeg.org/)
- libwepb (https://chromium.googlesource.com/webm/libwebp)
- WebP-wrapper (https://github.com/JosePineiro/WebP-wrapper)