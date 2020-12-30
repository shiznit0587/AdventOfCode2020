module Day16

let day16 =
    printfn "Running Day 16 - a"

    let input =
        (readText 16)
            .Split("your ticket:", System.StringSplitOptions.TrimEntries)
        |> Seq.map (fun s -> s.Split("nearby tickets:", System.StringSplitOptions.TrimEntries))
        |> flatten
        |> Seq.toList

    let parseRule s =
        match s with
        | Regex @"(.+): (.+)-(.+) or (.+)-(.+)" [ name; min1; max1; min2; max2 ] ->
            Some(
                name,
                [ int min1 .. int max1 ]
                @ [ int min2 .. int max2 ]
                |> Set
            )
        | _ -> None

    let rules =
        input.[0].Split('\n') |> Seq.choose (parseRule)

    let allAllowedValues = rules |> Seq.map snd |> Set.unionMany

    let nearbyTickets =
        Seq.map ((fun (s: string) -> s.Split ',') >> (Seq.map int)) (input.[2].Split('\n'))

    let errorRate =
        nearbyTickets
        |> Seq.map (fun t -> Set.difference (t |> Set) allAllowedValues)
        |> flatten
        |> Seq.sum

    printfn "Ticket Scanning Error Rate = %d" errorRate

    printfn "Running Day 16 - b"

    let validTickets =
        nearbyTickets
        |> Seq.filter
            (fun t ->
                (Set.difference (t |> Set) allAllowedValues)
                    .IsEmpty)

    // Group the values from all valid tickets by their index.
    let valueGroups =
        validTickets |> Seq.transpose |> Seq.map Set

    // Get a set of valid rule names per value group
    let getRulesMatchingValues (values: Set<int>) =
        rules
        |> Seq.filter (fun (_, r) -> (Set.difference values r).IsEmpty)
        |> Seq.map fst
        |> Set

    let ruleSets =
        Seq.map getRulesMatchingValues valueGroups

    let resolveRules (ruleSets: seq<Set<string>>) =
        let rec resolveRulesRec (rss: seq<Set<string>>) =
            if rss |> Seq.forall (fun rs -> rs.Count = 1) then
                rss
            else
                // Build a set of all rules that appear alone.
                let resolvedRules =
                    rss
                    |> Seq.filter (fun rs -> rs.Count = 1)
                    |> flatten
                    |> Set

                // Remove all resolved rules from all rule sets with Count > 1.
                let rss =
                    rss
                    |> Seq.map
                        (fun rs ->
                            if rs.Count = 1 then
                                rs
                            else
                                Set.difference rs resolvedRules)

                resolveRulesRec rss

        resolveRulesRec ruleSets
        |> Seq.map (Set.minElement)

    let orderedRules = resolveRules ruleSets

    let myTicket = input.[1].Split ',' |> Seq.map int

    let departureProduct =
        Seq.zip orderedRules myTicket
        |> Seq.filter (fun (r, _) -> r.StartsWith "departure")
        |> Seq.map (fun (_, v) -> int64 v)
        |> Seq.reduce (*)

    printfn "Departure Product = %d" departureProduct

    printfn "Day 16 Complete"
