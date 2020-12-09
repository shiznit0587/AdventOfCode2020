module Day5

let day5 =
    printfn "Running Day 5 - a"

    let lines = readLines 5

    let doOp (f, b, l, r) c =
        match c with
        | 'F' -> (f, f + (b - f) / 2, l, r)
        | 'B' -> (f + (b - f) / 2, b, l, r)
        | 'L' -> (f, b, l, l + (r - l) / 2)
        | 'R' -> (f, b, l + (r - l) / 2, r)
        | _ -> (f, b, l, r)

    let seatIds =
        Seq.map
            ((fun l -> Seq.fold doOp (0, 128, 0, 8) l)
             >> fun (r, _, c, _) -> r * 8 + c)
            lines
        |> Seq.sort
        |> Seq.toArray

    printfn "Highest Seat ID = %d" (Seq.last seatIds)

    printfn "Running Day 5 - b"

    let min = Seq.head seatIds

    let missingSeatId =
        fst (Seq.find (fun (index, seatId) -> index <> seatId - min) (iteri seatIds))
        + min

    printfn "My Seat ID = %d" missingSeatId

    printfn "Day 5 Complete"
