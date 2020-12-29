module Day15

type State =
    { Value: int
      Prev: int
      Seen: Map<int, int> }

let day15 =
    printfn "Running Day 15 - a"

    let input =
        (readLines 15).[0].Split ',' |> Array.map (int)

    let seen =
        input
        |> Seq.indexed
        |> Seq.map (fun (i, s) -> s, i + 1)
        |> Map.ofSeq

    let genNext (state: State) round =
        let next =
            if state.Value = state.Prev then
                1
            else
                match Map.tryFind state.Value state.Seen with
                | Some (r) -> round - 1 - r
                | None -> 0

        { Value = next
          Prev = state.Value
          Seen = Map.add state.Prev (round - 2) state.Seen }

    let genState target =
        [ input.Length + 1 .. target ]
        |> Seq.fold
            genNext
            { Value = Array.last input |> int
              Prev = -1
              Seen = seen }

    printfn "2020th Number = %d" (genState 2020).Value

    printfn "Running Day 15 - b"

    printfn "30 Millionth Number = %d" (genState 30_000_000).Value

    printfn "Day 15 Complete"
