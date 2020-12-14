module Day7

open System.Text.RegularExpressions

type InnerBagRule = { Name: string; Count: int }

type BagRule =
    { Name: string
      InnerBags: list<InnerBagRule> }

let day7 =
    printfn "Running Day 7 - a"

    let lines = readLines 7

    let rec parseExtraInnerRules m =
        match m with
        | [] -> []
        | [ _; count; name ] -> [ { Name = name; Count = int count } ]
        | list ->
            parseExtraInnerRules list.[0..2]
            @ parseExtraInnerRules list.[3..]

    let rec parseRule m =
        match m with
        | [ outer; count; inner ] ->
            { Name = outer
              InnerBags = [ { Name = inner; Count = int count } ] }
        | list ->
            let rule = parseRule list.[0..2]
            { rule with
                  InnerBags = rule.InnerBags @ parseExtraInnerRules list.[3..] }

    let parseLine line =
        let m =
            Regex.Match(line, "(\w+ \w+) bags contain (\d+) (\w+ \w+) bags?(, (\d+) (\w+ \w+) bags?)*\.")

        parseRule (List.tail [ for g in m.Groups -> g.Value ])

    let rules = Seq.map (parseLine) lines

    // After testing, I've determined that the regex capture for the extra inner rules will only resolve to the last.
    // A rule with three inner bags will only capture the first and third when run through the regex.
    // Better to split remaining string on comma and regex from there.

    printfn "Running Day 7 - b"

    printfn "Day 7 Complete"
