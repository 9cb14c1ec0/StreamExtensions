# Introduction

StreamExtensions is a C# library that uses extension methods to add two convenient methods to
`System.IO.Stream`

# Installing

```
git clone https://github.com/9cb14c1ec0/StreamExtensions
cd path/to/project/directory
dotnet add reference ../StreamExtensions
```

# Using extension methods

To use the ReadLine and ReadToEnd methods, add `using StreamExtensions` to the top of your .cs file.  Your auto-complete will then show ReadLine and ReadToEnd methods for all `System.IO.Stream` objects.  Both of these methods return strings.

