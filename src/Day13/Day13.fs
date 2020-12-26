module Day13

let day13 =
    printfn "Running Day 13 - a"

    let input = readLines 13
    let earliestDeparture = int input.[0]

    let buses =
        input.[1].Split ','
        |> Array.filter (fun s -> s <> "x")
        |> Array.map (int)

    let calcNextBusDeparture time bus =
        let div = time / bus
        if time % bus = 0 then bus * div else bus * (div + 1)

    let bus =
        buses
        |> Array.map (fun b -> (b, calcNextBusDeparture earliestDeparture b))
        |> Array.minBy (snd)

    printfn
        "Bus = %d, Wait Time = %d, Product = %d"
        (fst bus)
        (snd bus - earliestDeparture)
        (fst bus * (snd bus - earliestDeparture))

    printfn "Running Day 13 - b"

    let getBusMins (parts: string array) =
        seq {
            let mutable x = 1

            for i in 0 .. parts.Length - 1 do
                if parts.[i] = "x" then
                    x <- x + 1
                else
                    yield (int parts.[i], x)
                    x <- 1
        }

    let findTime time (bus, mins) =
        let rec findTimeRec time interval bus mins =
            if calcNextBusDeparture time bus = time + mins
            then time + mins
            else findTimeRec (time + interval) interval bus mins

        findTimeRec time time bus mins

    findTime 7 (13, 1)
    findTime 78 (59, 3) // -> 2655, which is incorrect! 2651 % 7 != 0 :: interval must be wrong?
    findTime 2655 (31, 2)
    findTime 7967 (19, 1)
    // 23902 - Incorrect!

    let time =
        input.[1].Split ','
        |> getBusMins
        |> Seq.fold (findTime) 0

    printfn "Time = %d" time

    printfn "Day 13 Complete"
