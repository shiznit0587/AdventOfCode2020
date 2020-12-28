[<AutoOpen>]
module Utils

open System.IO
open System.Text.RegularExpressions

let readLines (day: int) =
    File.ReadAllLines($"{__SOURCE_DIRECTORY__}/Day{day}/Input.txt")

let readText (day: int) =
    File.ReadAllText($"{__SOURCE_DIRECTORY__}/Day{day}/Input.txt")

// Regular Expression active pattern: http://www.fssnip.net/29
let (|Regex|_|) pattern input =
    let m = Regex.Match(input, pattern)

    if m.Success
    then Some(List.tail [ for g in m.Groups -> g.Value ])
    else None

let charCount cs c = cs |> Seq.filter ((=) c) |> Seq.length

let flatten s =
    seq {
        for a in s do
            yield! a
    }

let flatMap f s =
    seq {
        for a in s do
            yield! Seq.map (f) a
    }
