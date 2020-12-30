module Day16

type Rule =
    { Name: string
      Ranges: List<int * int> }

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
                { Name = name
                  Ranges =
                      [ int min1, int max1
                        int min2, int max2 ] }
            )
        | _ -> None

    let rules =
        input.[0].Split('\n') |> Seq.choose (parseRule)

    let allAllowedValues =
        rules
        |> Seq.map (fun r -> r.Ranges)
        |> flatten
        |> Seq.map (fun r -> [ fst r .. snd r ])
        |> flatten
        |> Set

    let nearbyTickets =
        Seq.map ((fun (s: string) -> s.Split ',') >> (Seq.map int)) (input.[2].Split('\n'))

    let errorRate =
        nearbyTickets
        |> Seq.map (fun t -> Set.difference (t |> Set) allAllowedValues)
        |> flatten
        |> Seq.sum

    printfn "Ticket Scanning Error Rate = %d" errorRate

    let validTickets =
        nearbyTickets
        |> Seq.filter
            (fun t ->
                (Set.difference (t |> Set) allAllowedValues)
                    .IsEmpty)

    // I need to group the values from all of the tickets

    let valueGroups =
        validTickets
        |> Seq.map (Seq.indexed)
        |> flatten
        |> Seq.groupBy (fst)
        |> Seq.map (fun (i, s) -> (i, Seq.map (snd) s))
        |> Map

    let myTicket = input.[1]


    // Each rule is <name>: <min1>-<max1> or <min2>-<max2>

    printfn "Running Day 16 - b"

    printfn "Day 16 Complete"
