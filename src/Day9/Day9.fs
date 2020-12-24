module Day9

let PREAMBLESIZE = 25

let day9 =
    printfn "Running Day 9 - a"

    let input =
        readLines 9 |> Array.map (int64) |> Seq.toList

    // Given a sorted list, find a pair whose sum equals the target
    let rec findSummingPair target (l: list<int64>) =
        if l.Length < 2 then
            None
        else
            let sum = l.[0] + l.[l.Length - 1]

            match sum with
            | sum when sum > target -> findSummingPair target l.[0..l.Length - 2]
            | sum when sum < target -> findSummingPair target l.[1..l.Length - 1]
            | _ -> Some(l.[0], l.[l.Length - 1])

    let rec findWeakness idx =
        let range =
            input.[idx - PREAMBLESIZE..idx - 1] |> List.sort

        match findSummingPair input.[idx] range with
        | Some (_) -> findWeakness (idx + 1)
        | None -> input.[idx]

    let weaknessKey = findWeakness PREAMBLESIZE

    printfn "Weakness Key = %d" weaknessKey

    printfn "Running Day 9 - b"

    let findRange target =
        let rec findRangeRec x y sum =
            let sum = sum + input.[y]

            match sum with
            | s when s > target -> findRangeRec (x + 1) (x + 2) input.[x + 1]
            | s when s < target -> findRangeRec x (y + 1) sum
            | _ -> (x, y)

        findRangeRec 0 1 input.[0]

    let (x, y) = findRange weaknessKey

    let weakness =
        List.min input.[x..y] + List.max input.[x..y]

    printfn "Weakness = %d" weakness

    printfn "Day 9 Complete"
