[<AutoOpen>]
module Utils

open System.IO

let readLines (day: int) =
    File.ReadAllLines($"{__SOURCE_DIRECTORY__}/../inputs/Day{day}.txt")
