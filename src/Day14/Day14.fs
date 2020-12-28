module Day14

type Instruction =
    | Mask of int64 * int64 * string
    | MemSet of int64 * int64

type ProgramState =
    { Addresses: Map<int64, int64>
      Mask: int64 * int64
      MaskStr: string }

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

        Mask(maskOn, maskOff, mask)

    let parseInstruction line =
        match line with
        | Regex @"mask = (\w+)" [ mask ] -> Some(parseMask mask)
        | Regex @"mem\[(\d+)\] = (\d+)" [ address; value ] -> Some(MemSet(int64 address, int64 value))
        | _ -> None

    let instructions =
        readLines 14 |> Seq.choose parseInstruction

    let memSet1 address value state =
        let value =
            ((value ||| fst state.Mask) &&& snd state.Mask)
            &&& 0xFFFFFFFFFL

        { state with
              Addresses = (Map.add address value state.Addresses) }

    let processInstruction memSet state instruction =
        match instruction with
        | Mask (on, off, mask) ->
            { state with
                  Mask = on, off
                  MaskStr = mask }
        | MemSet (address, value) -> memSet address value state

    let calcSum memSet =
        let state =
            Seq.fold
                (processInstruction memSet)
                { Addresses = Map.empty
                  Mask = 0L, 0L
                  MaskStr = "" }
                instructions

        Map.fold (fun s _ v -> s + v) 0L state.Addresses

    printfn "Sum = %d" (calcSum memSet1)

    printfn "Running Day 14 - b"

    let getAddresses address mask =
        let mask = mask |> Seq.rev |> Seq.toArray

        let rec blitAddress bit value =
            seq {
                if bit = mask.Length then
                    yield value
                else
                    match mask.[bit] with
                    | '0' -> yield! blitAddress (bit + 1) value
                    | '1' -> yield! blitAddress (bit + 1) (value ||| (1L <<< bit))
                    | _ ->
                        yield! blitAddress (bit + 1) (value ||| (1L <<< bit))
                        yield! blitAddress (bit + 1) (value &&& ~~~(1L <<< bit))
            }

        blitAddress 0 address

    let memSet2 address value state =
        let addresses = getAddresses address state.MaskStr

        let addresses =
            Seq.fold (fun s a -> Map.add a value s) state.Addresses addresses

        { state with Addresses = addresses }

    printfn "Sum = %d" (calcSum memSet2)

    printfn "Day 14 Complete"
