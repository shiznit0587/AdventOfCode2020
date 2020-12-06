[<AutoOpen>]
module Utils

open System.IO

let readLines (day: int) =
    // TODO: Depending on if this program is being run via `dotnet run` or the ionide-fsharp plugin, the working directory is different.
    // Walk up the directory tree until Program.fs is found, then start from there.
    File.ReadAllLines($"inputs/Day{day}.txt")
