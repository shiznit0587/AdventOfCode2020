module Day13

let day13 =
    printfn "Running Day 13 - a"

    let input = readLines 13
    let earliestDeparture = int input.[0]

    let buses =
        input.[1].Split ','
        |> Array.filter (fun s -> s <> "x")
        |> Array.map (int)

    let calcBusDeparture bus =
        let div = earliestDeparture / bus
        if earliestDeparture % bus = 0 then bus * div else bus * (div + 1)

    let bus =
        buses
        |> Array.map (fun b -> (b, calcBusDeparture b))
        |> Array.minBy (snd)

    printfn
        "Bus = %d, Wait Time = %d, Product = %d"
        (fst bus)
        (snd bus - earliestDeparture)
        (fst bus * (snd bus - earliestDeparture))

    printfn "Running Day 13 - b"

    printfn "Day 13 Complete"
