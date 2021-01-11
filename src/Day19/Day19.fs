module Day19

type RuleChoice =
    | Text of char
    | Rules of int array

let day19 =
    printfn "Running Day 19 - a"

    let parseChoice s =
        match s with
        | Regex @"""(.)""" [ text ] -> Text(text.[0])
        | _ -> Rules(s.Split ' ' |> Array.map int)

    let parseRule s =
        match s with
        | Regex @"(\d+): (.+)" [ id; choices ] -> Some(int id, choices.Split " | " |> Array.map parseChoice)
        | _ -> None

    let input =
        (readText 19)
            .Split("\n\n", System.StringSplitOptions.TrimEntries)

    let rules =
        input.[0].Split '\n'
        |> Array.choose parseRule
        |> Map

    let messages = input.[1].Split '\n'

    let checkRules message rules =
        // The rules list must match the message in its entirety.
        // I have a list of rule IDs. Each one will have a list of rule choices.
        // I don't know if I should care about the choices here. I think I have to.
        // For each rule, for each choice, if that choice matches part of the message, continue with it, in case.
        // This is 2D then, huh? ...
        // Interestingly, I'm starting to wonder if this needs to return a list of matched message fragments
        Some(message)

    let checkRuleChoice (message:string) choice : Option<string> =
        match choice with
        | Text(c) -> if message.[0] = c then Some(message.[1..]) else None
        | Rules(r) -> checkRule message r

    // For each message, check if it matches rule 0.
    let checkMessage message =
        // I need to know how much of the string matches this rule.
        let rec checkMessageRec message choices: Option<string> =


        checkMessageRec message rules.[0]

    let matchingCount =
        messages |> Seq.choose checkMessage |> Seq.length

    printfn "Running Day 19 - b"

    printfn "Day 19 Complete"
