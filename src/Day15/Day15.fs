module Day15

type State =
    { Value: int
      Prev: int
      Seen: Map<int, int> }

open System.Collections.Generic

// Re-implemented Day 15 Part 2 with a mutable dictionary instead of allocating new Maps.
// Reduced the runtime from 60s to 6s.
let day15Part2 =

    printfn "Running Day 15 - b"

    let input =
        (readLines 15).[0].Split ',' |> Array.map (int)

    let dict = new Dictionary<int, int>()

    for (i, n) in input |> Array.indexed do
        dict.[n] <- i + 1

    let mutable value = Array.last input |> int
    let mutable prev = input.[input.Length - 2] |> int

    dict.Remove value |> ignore

    let genNext round =
        let round = round + input.Length + 1

        let next =
            if value = prev then
                1
            else if dict.ContainsKey value then
                round - 1 - dict.[value]
            else
                0

        dict.[prev] <- round - 2
        prev <- value
        value <- next
        next

    Seq.initInfinite (genNext)
    |> Seq.take (30_000_000 - input.Length)
    |> Seq.last

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

    printfn "30 Millionth Number = %d" day15Part2

    printfn "Day 15 Complete"
