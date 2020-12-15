module Day7

open System.Text.RegularExpressions

let day7 =
    printfn "Running Day 7 - a"

    let lines = readLines 7

    let parseLine line =
        match line with
        | Regex @"(\w+ \w+) bags contain (.*)\." [ name; rest ] ->
            let rules =
                [ for m in Regex.Matches(rest, "(\d+) (\w+ \w+)") -> m ]
                |> Seq.map (fun m -> List.tail [ for g in m.Groups -> g.Value ])
                |> Seq.map (fun vl -> (int vl.[0], vl.[1]))

            Some((name, rules))
        | _ -> None

    let rules = Seq.choose (parseLine) lines |> Map

    let reversedRules =
        rules
        |> Seq.map (fun s -> seq { for i in s.Value -> (s.Key, snd i) })
        |> flatten
        |> Seq.map (fun p -> (snd p, fst p))
        |> Seq.groupBy (fst)
        |> Seq.map (fun p -> (fst p, Seq.map (snd) (snd p)))
        |> Map

    let wrappingBags bag =
        let mutable visited = Set.empty

        let rec traverseBags bag =
            seq {
                if reversedRules.ContainsKey bag then
                    let bags = Set(reversedRules.Item bag)
                    let newBags = Set.difference bags visited
                    yield! newBags
                    visited <- Set.union visited bags
                    yield! flatten (seq { for b in newBags -> traverseBags b })
                else
                    yield! Seq.empty
            }

        traverseBags bag

    let wrappers = wrappingBags "shiny gold"

    printfn "Outer Bag Colors = %d" (Seq.length wrappers)

    printfn "Running Day 7 - b"

    printfn "Day 7 Complete"
