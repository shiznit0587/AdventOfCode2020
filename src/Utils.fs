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

let iteri (arr: array<'a>): seq<(int * 'a)> =
    seq { for i in 0 .. arr.Length - 1 -> (i, arr.[i]) }

let siteri (s: seq<'a>): seq<(int * 'a)> =
    let mutable idx = 0

    seq {
        for a in s do
            yield (idx, a)
            idx <- idx + 1
    }

let flatten (s: seq<seq<'a>>) =
    seq {
        for a in s do
            yield! a
    }

let flatMap (f: 'a -> 'b) (s: seq<seq<'a>>) =
    seq {
        for a in s do
            yield! Seq.map (f) a
    }
