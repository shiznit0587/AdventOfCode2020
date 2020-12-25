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

### Day 11

- I used a bunch of new `Array2D` library functions.
- I learned that most collections support direct equality comparison.
- I was able to commonize some code between the parts by passing a function as an argument to another function, which I hadn't done yet as part of a day solution.

### Day 10

- I was able to simplify some code and handle edge case issues by appending `0` and `max + 3` to the head and tail of the input, using the `@` operator for list append.
- I used memoization to optimize the evaluation of possible paths from an adapter to the device.

### Day 9

- I tried to commonize the `findSummingPair` method so I could use it for both Day 9 and Day 1, but Day 1 operates on `int` and Day 9 on `int64`, and I couldn't figure out how to provide the correct operator-based type constraints (such as `(=)`).

### Day 8

- This time, I'm actually sticking with records as my state storage instead of using tuples. It's easier to read, and doesn't require re-passing every unchanged part.
- Modifying a single value in an immutable array seems harder than it should be. The answers on this [SO question](https://stackoverflow.com/questions/29966294/how-to-edit-an-item-in-a-mutable-list-in-f-and-allow-the-other-items-in-the-lis) showcase that well, and also led me to learn about [ref cells](https://stackoverflow.com/questions/3221200/f-let-mutable-vs-ref), which may allow me to solve future problems more imperatively.
- Part 1 runs really fast, only 13ms! I think it's because everything is on the stack and `runProgram` is tail-recursive.
- When working on Part 2, I realized that arrays are actually mutable. I used that to write a sequence generator of program variants that always returns the same instruction list, but modified.
- I figured out how to have a match case do nothing, with `()`. It saved me from an extra `|> Seq.choose(id)` and additional boxing/unboxing.
- I know that part 2 is iterating the sequence generator lazily, but it still runs slow. I wonder if my use of array mutability is causing performance issues.
- I figured out that the performance issue was the constant creation of the `visited` array. Converted it to a `Set` and cut the runtime from 850ms to 35ms.

### Day 7

- I learned that regex capture patterns within a repeated capture pattern will greedily match only within the last instance of the repeated pattern.
- I had an entire iteration of the input parsing that used list deconstructing matching patterns, ranges, and records. While it's not in my final solution, at least now I know it.
- I got more comfortable with recursion and yielding sequences.
- I learned how to work with a mutable reference. It feels cludgy for a functional language, but I needed it to solve an edge case.

### Day 6

- Worked with sets and sequence comprehension for the first time. This gives me an idea to optimize Day 1-b.
- Learned about [Function Currying](https://fsharpforfunandprofit.com/posts/currying/), which is why most functions can be piped to.
  - Rewrote my `charCount` method to allow for currying correctly with `Seq.map`.
- Added timings for execution time per day.
- Optimized Day 1 solution to use a yielding sequence instead of recursive list deconstruction. Down from 685ms to 56ms!

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