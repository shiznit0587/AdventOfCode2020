module Day4

open System.Text.RegularExpressions

let day4 =
    printfn "Running Day 4 - a"

    let text = readText 4

    let inputs =
        text.Split([| "\n\n" |], System.StringSplitOptions.TrimEntries)

    let parsePassport input =
        [ for m in Regex.Matches(input, "(\S+):(\S+)") -> m ]
        |> Seq.map (fun m -> List.tail [ for g in m.Groups -> g.Value ])
        |> Seq.map (fun kvl -> (kvl.[0], kvl.[1]))
        |> Map

    let passports = inputs |> Seq.map (parsePassport)

    printfn "Running Day 4 - b"

    printfn "Day 4 Complete"
