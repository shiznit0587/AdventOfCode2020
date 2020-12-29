module Day15

type State =
    { Value: int
      Seen: Map<int, int option * int option> }

let day15 =
    printfn "Running Day 15 - a"

    let input =
        (readLines 15).[0].Split ',' |> Array.map (int)

    let seen: Map<int, int option * int option> =
        input
        |> Seq.indexed
        |> Seq.map (fun (i, s) -> s, (None, Some(i + 1)))
        |> Map.ofSeq

    let genNext (state: State) round =
        let next =
            match state.Seen.[state.Value] with
            | Some (r1), Some (r2) -> r2 - r1
            | _ -> 0

        let seen =
            match Map.tryFind next state.Seen with
            | Some (_, n) -> Map.add next (n, Some(round)) state.Seen
            | None -> Map.add next (None, Some(round)) state.Seen

        { Value = next; Seen = seen }

    let genState target =
        [ input.Length + 1 .. target ]
        |> Seq.fold
            genNext
            { Value = Array.last input |> int
              Seen = seen }

    printfn "2020th Number = %d" (genState 2020).Value

    printfn "Running Day 15 - b"

    printfn "30 Millionth Number = %d" (genState 30_000_000).Value

    printfn "Day 15 Complete"
