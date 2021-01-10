module Day19

type RuleChoice =
    | Text of string
    | Rules of int array

type Rule = { Id: int; Choices: RuleChoice array }

let day19 =
    printfn "Running Day 19 - a"

    let parseChoice s =
        match s with
        | Regex @"""(.)""" [ text ] -> Text(text)
        | _ -> Rules(s.Split ' ' |> Array.map int)

    let parseRule s =
        match s with
        | Regex @"(\d+): (.+)" [ id; choices ] ->
            Some(
                { Id = int id
                  Choices = choices.Split " | " |> Array.map parseChoice }
            )
        | _ -> None

    let input =
        (readText 19)
            .Split("\n\n", System.StringSplitOptions.TrimEntries)

    let rules =
        input.[0].Split '\n' |> Array.choose parseRule

    let messages = input.[1].Split '\n'

    printfn "Running Day 19 - b"

    printfn "Day 19 Complete"
