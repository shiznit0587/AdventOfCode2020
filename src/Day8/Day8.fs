module Day8

type Status =
    | Running
    | Halted
    | Crashed
    | Finished

type Instruction = { Name: string; Number: int }

type ProgramState =
    { Pointer: int
      Accumulator: int
      Status: Status
      Visited: array<bool> }

let day8 =
    printfn "Running Day 8 - a"

    let lines = readLines 8

    let parseInstruction (line: string) =
        let name = line.[0..2]
        let sign = line.[4]
        let number = int line.[5..]
        match sign with
        | '+' -> Some({ Name = name; Number = number })
        | '-' -> Some({ Name = name; Number = -number })
        | _ -> None

    let instructions = Array.choose (parseInstruction) lines

    let visitInstruction visited pointer =
        visited
        |> iteri
        |> Seq.map (fun p -> if fst p = pointer then true else snd p)
        |> Seq.toArray

    let tick (instructions: array<Instruction>) state =
        if state.Pointer < 0 then
            { state with Status = Crashed }
        else if instructions.Length < state.Pointer then
            { state with Status = Crashed }
        else if instructions.Length = state.Pointer then
            { state with Status = Finished }
        else if state.Visited.[state.Pointer] then
            { state with Status = Halted }
        else
            let instr = instructions.[state.Pointer]
            match instr.Name with
            | "acc" ->
                { state with
                      Pointer = state.Pointer + 1
                      Accumulator = state.Accumulator + instr.Number
                      Visited = visitInstruction state.Visited state.Pointer }
            | "jmp" ->
                { state with
                      Pointer = state.Pointer + instr.Number
                      Visited = visitInstruction state.Visited state.Pointer }
            | "nop" ->
                { state with
                      Pointer = state.Pointer + 1
                      Visited = visitInstruction state.Visited state.Pointer }
            | _ -> state

    let rec tickProgram instructions state =
        match state.Status with
        | Running ->
            tickProgram instructions
            <| tick instructions state
        | _ -> state

    let runProgram instructions =
        tickProgram
            instructions
            { Pointer = 0
              Accumulator = 0
              Status = Running
              Visited = Array.zeroCreate instructions.Length }

    let state = runProgram instructions

    printfn "Accumulator = %d" state.Accumulator

    printfn "Running Day 8 - b"

    let getProgramVariants (instructions: array<Instruction>) =
        let flipInstruction idx =
            let inst = instructions.[idx]
            match inst.Name with
            | "nop" -> Some({ inst with Name = "jmp" })
            | "jmp" -> Some({ inst with Name = "nop" })
            | _ -> None

        seq {
            for i in 0 .. instructions.Length do
                match flipInstruction i with
                | Some (inst) ->
                    let prevInst = instructions.[i]
                    instructions.[i] <- inst
                    yield instructions
                    instructions.[i] <- prevInst
                | None -> ()
        }

    let state =
        getProgramVariants instructions
        |> Seq.map (runProgram)
        |> Seq.find (fun s -> s.Status = Finished)

    printfn "Accumulator = %d" state.Accumulator

    printfn "Day 8 Complete"
