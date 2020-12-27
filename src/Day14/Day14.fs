module Day14

type Instruction =
    | Mask of int64 * int64
    | MemSet of int * int64

type ProgramState =
    { Addresses: Map<int, int64>
      Mask: int64 * int64 }

let day14 =
    printfn "Running Day 14 - a"

    let parseMask mask =
        let maskOn =
            mask
            |> Seq.rev
            |> Seq.indexed
            |> Seq.choose (fun (i, c) -> if c = '1' then Some(i) else None)
            |> Seq.fold (fun m b -> m ||| (1L <<< b)) 0L

        let maskOff =
            mask
            |> Seq.rev
            |> Seq.indexed
            |> Seq.choose (fun (i, c) -> if c = '0' then Some(i) else None)
            |> Seq.fold (fun m b -> m &&& ~~~(1L <<< b)) 0xFFFFFFFFFFFFFFFFL

        Mask(maskOn, maskOff)

    let parseInstruction line =
        match line with
        | Regex @"mask = (\w+)" [ mask ] -> Some(parseMask mask)
        | Regex @"mem\[(\d+)\] = (\d+)" [ address; value ] -> Some(MemSet(int address, int64 value))
        | _ -> None

    let instructions =
        readLines 14 |> Array.choose (parseInstruction)

    let processInstruction state instruction =
        match instruction with
        | Mask (on, off) -> { state with Mask = (on, off) }
        | MemSet (address, value) ->
            let value =
                ((value ||| fst state.Mask) &&& snd state.Mask)
                &&& 0xFFFFFFFFFL

            { state with
                  Addresses = (Map.add address value state.Addresses) }

    let state =
        Seq.fold
            (processInstruction)
            { Addresses = Map.empty
              Mask = (0L, 0L) }
            instructions

    let sum =
        Map.fold (fun s _ v -> s + v) 0L state.Addresses

    printfn "Sum = %d" sum

    printfn "Running Day 14 - b"

    // Day 14 processes the mask string differently - It actually generates multiple results based on the Xs.
    // I'm thinking a recursive sequence generator to handle traversing all the options.

    printfn "Day 14 Complete"
