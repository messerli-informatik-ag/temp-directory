# TempDirectory
[![Build](https://github.com/messerli-informatik-ag/temp-directory/actions/workflows/build.yml/badge.svg)](https://github.com/messerli-informatik-ag/temp-directory/actions/workflows/build.yml)
[![NuGet](https://img.shields.io/nuget/v/Messerli.TempDirectory.svg)](https://www.nuget.org/packages/Messerli.TempDirectory/)

This library provides a `TempSubdirectory` class that provides a unique subdirectory in the user's temp directory
that is automatically cleaned up when the class is disposed.

### Usage
```cs
using var directory = TempSubdirectory.Create("prefix");
File.WriteAllText(Path.Combine(directory.FullName, "example.txt"), contents: "...");
```
