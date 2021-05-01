# pkgscan
[![GitHub issues](https://img.shields.io/github/issues/abdllhcay/pkgscan)](https://github.com/abdllhcay/pkgscan/issues)
![Version](https://img.shields.io/badge/version-1.0.2-blue)

Dependency scanner and visualization tool for C# applications. 

## Usage
```
pkgscan 1.0.2

Usage: pkgscan [COMMAND] [OPTION]

  show       Print packages to standard output. Type -h for more options.
  export     Export package list. Type -h for more options.
  help       Display more information on a specific command.
  version    Display version information.
```
## Examples
To list the dependencies on a specific project, use `show` command.
```
pkgscan show --project path/to/project
```
**Output**
```
PACKAGE                                   AUTHOR     VERSION  LATEST  SIZE    PUBLISHED   LAST UPDATE 
------------------------------------------------------------------------------------------------------
Microsoft.AspNetCore.Mvc.Versioning       Microsoft  5.0.0    5.0.0   260 KB  02/09/2021  2 months ago
Microsoft.Extensions.SecretManager.Tools  Microsoft  2.0.2    2.0.2   48 KB   05/07/2018  2 years ago
```
You can use the `export` command to extract the dependencies in JSON or CSV format instead of printing them to the screen.
```
pkgscan export --project path/to/project --json
```
## Supported Platforms
pkgscan is currently only being tested on MacOs. In the future multi-platform support will be available. 
