module Day1

let day1 =
    printfn "Running Day 1 - a"

    let lines =
        readLines 1 |> Array.map int |> Array.sort

    // Starting with pointers to the start and end of the sorted list,
    // recursively traverse our way inward until the sum of two numbers is 2020.
    let rec findPairSumming2020 x y =
        let sum = lines.[x] + lines.[y]
        match sum with
        | sum when sum > 2020 -> findPairSumming2020 x (y - 1)
        | sum when sum < 2020 -> findPairSumming2020 (x + 1) y
        | _ -> (lines.[x], lines.[y])

    let (x, y) = findPairSumming2020 0 (lines.Length - 1)

    printfn "x = %d, y = %d, product = %d" x y (x * y)

    printfn "Running Day 1 - b"

    // In functional programming, I can't figure out how to early-out when branches of a triple-nested loop sum > 2020
    // I'm going to instead just build all triples from the list and evaluate them all.

    // All this list splitting is expensive (685ms). Let's try something else.
    (*
    let rec makePairs list =
        match list with
        | [] -> []
        | x :: rest -> // deconstruct the list into two parts - the first item and the rest of the list
            let xPairs = rest |> List.map (fun y -> (x, y)) // make tuples
            xPairs @ (makePairs rest) // append this list of pairs with the pairs from the rest of the list

    let rec makeTriples list =
        match list with
        | [] -> []
        | x :: rest ->
            let pairs = makePairs rest

            let triples =
                pairs |> List.map (fun (y, z) -> (x, y, z))

            triples @ (makeTriples rest)
    *)

    // ~Now using for comprehension~ (was still slow-ish, at 196ms)
    // Now using a yielding sequence (now 56ms)
    let makeTriples (lines: array<'a>) =
        seq {
            for x in 0 .. lines.Length - 1 do
                for y in x + 1 .. lines.Length - 1 do
                    for z in y + 1 .. lines.Length - 1 do
                        yield (lines.[x], lines.[y], lines.[z])
        }

    let (x, y, z) =
        makeTriples lines
        |> Seq.find (fun (x, y, z) -> x + y + z = 2020)

    printfn "x = %d, y = %d, z = %d, product = %d" x y z (x * y * z)

    printfn "Day 1 Complete"
