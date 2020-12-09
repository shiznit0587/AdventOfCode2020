# AdventOfCode2020

[Advent of Code 2020](https://adventofcode.com/2020)

For this year, I decided to try my hand at a functional programming language.

## Resources

- [Tour of F#](https://docs.microsoft.com/en-us/dotnet/fsharp/tour)
- [F# syntax in 60 seconds](https://fsharpforfunandprofit.com/posts/fsharp-in-60-seconds/)
- [F# Cheatsheet](http://dungpa.github.io/fsharp-cheatsheet/)
- [F# Collection Types](https://docs.microsoft.com/en-us/dotnet/fsharp/language-reference/fsharp-collection-types)
- [fsharp-tutorial.fs](https://gist.github.com/odytrice/667bc1d8d7c872fe8c5b1baa58898c32)
- [Active Patterns](https://fsharpforfunandprofit.com/posts/convenience-active-patterns/)
- [Regular expression active pattern](http://www.fssnip.net/29)
- [F# Programming WikiBook](https://en.wikibooks.org/wiki/F_Sharp_Programming)
- [F# documentation](https://docs.microsoft.com/en-us/dotnet/fsharp/)
- [FSharp.Core API Reference](https://fsharp.github.io/fsharp-core-docs/reference/)

## Journal

A chronicle of what I've tried and learned each day.

### Day 5

- I could not find a standard way to iterate an array with both the index and value, so I wrote it.
- For loop ranges are end-inclusive, good to know.

### Day 4

- It took me three hours to figure out how to parse the input into a collection of key-value lookup tables, or `seq<Map<string,string>>`.
- I learned how to use the F# interactive REPL environment to execute individual lines of code and see the results.

### Day 3

- Investigating 2d-arrays, tuple decomposition, and more recursion.
- First puzzle that caused an integer overflow!

### Day 2

- Found the `__SOURCE_DIRECTORY__` define, so I could fix file i/o when running via either `dotnet run` or `F# Solution Explorer` (for breakpoint debugging).
- Figured out how to reference nuget assemblies in my project.
  - `dotnet add package <packagename>`
- Tried using the [FSharp.Text.RegexProvider](http://fsprojects.github.io/FSharp.Text.RegexProvider/) library. Ended up not being able to due to an incompatibility with my dev env. The compilation server wasn't aware of provided types. It was fine, since the provided type would still have had each field typed as string, regardless of the pattern contained in the capture, so authoring my own type was better.
- Learned a bit about defining custom active patterns for matching.
- Getting more comfortable with `Seq` methods.

### Day 1

- Setup my dev environment:
  - MacBook Pro (15-inch, 2018)
  - [Visual Studio Code](https://code.visualstudio.com/)
  - [.NET SDK 5.0](https://dotnet.microsoft.com/download)
    - Upgraded from 2.2.101 so I could use string interpolation
  - [Ionide-fsharp](https://marketplace.visualstudio.com/items?itemName=Ionide.Ionide-fsharp) plugin
- Set up base modules and compilation ordering
- Figured out file i/o and some basic syntax
- git refresher