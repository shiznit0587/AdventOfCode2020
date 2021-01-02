[<AutoOpen>]
module Utils

open Microsoft.FSharp.Core.OptimizedClosures
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

let array4dmapi (mapping: int -> int -> int -> int -> 'a -> 'b) (arr: 'a [,,,]) =
    let len1 = Array4D.length1 arr
    let len2 = Array4D.length2 arr
    let len3 = Array4D.length3 arr
    let len4 = Array4D.length4 arr

    let result: 'b [,,,] = Array4D.zeroCreate len1 len2 len3 len4

    let f =
        FSharpFunc<_, _, _, _, _, _>.Adapt(mapping)

    for x = 0 to len1 - 1 do
        for y = 0 to len2 - 1 do
            for z = 0 to len3 - 1 do
                for w = 0 to len4 - 1 do
                    result.[x, y, z, w] <- f.Invoke(x, y, z, w, arr.[x, y, z, w])

    result
