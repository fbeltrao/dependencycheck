# Dependency Check dotnet utility

## Purpose

Use this tool to view assembly references in a folder. This way you can find out which different versions have been binded by each reference in a project. Helps you troubleshoot runtime errors due to bindings

## Installation for .Net Core

With .Net Core 2.1 it is possible to install this tool globally this way it is always available on your terminal/console. 

TODO: Add instructions on how to use it

## Using with .Net Framework
Clone the repository and build the project DependencyCheck. Copy the result DependencyCheck.exe somewhere available in your path.

## Usage

Get all conflicts in a folder c:\myapplication\bin

**.Net Core**
```dotnet dependency-check -f c:\myapplication\bin```

