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

    // It's the Chinese Remainder Theorem, just like 2016-12-15

    let buses =
        input.[1].Split ','
        |> Seq.indexed
        |> Seq.choose (fun (i, s) -> if s = "x" then None else Some(int64 i, int64 s))
        |> Seq.map (fun (i, bus) -> bus - (i % bus), bus)

    let modInverse x mod' =
        [ 1L .. mod' ]
        |> Seq.find (fun m -> (x * m) % mod' = 1L)

    let N = buses |> Seq.map (snd) |> Seq.reduce (*)

    let x =
        Seq.sumBy (fun (bi, ni) -> bi * (N / ni) * modInverse (N / ni) ni) buses

    printfn "Timestamp = %d" (x % N)

    printfn "Day 13 Complete"
