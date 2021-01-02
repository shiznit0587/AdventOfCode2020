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

    if m.Success then
        Some(List.tail [ for g in m.Groups -> g.Value ])
    else
        None

let charCount cs c = cs |> Seq.filter ((=) c) |> Seq.length

let flatten s =
    seq {
        for a in s do
            yield! a
    }

let flatten3d (arr: 'a [,,]) =
    seq {
        for z = 0 to Array3D.length3 arr - 1 do
            for y = 0 to Array3D.length2 arr - 1 do
                for x = 0 to Array3D.length1 arr - 1 do
                    yield arr.[x, y, z]
    }

let flatten4d (arr: 'a [,,,]) =
    seq {
        for w = 0 to Array4D.length4 arr - 1 do
            yield! flatten3d arr.[*, *, *, w]
    }

let flatMap f s =
    seq {
        for a in s do
            yield! Seq.map (f) a
    }
