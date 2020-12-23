module Day8

type Status =
    | Running = 0
    | Halted = 1
    | Crashed = 2
    | Finished = 3

type Instruction = { Name: string; Number: int }

type ProgramState =
    { Pointer: int
      Accumulator: int
      Status: Status
      Visited: Set<int> }

let day8 =
    printfn "Running Day 8 - a"

    let lines = readLines 8

    let parseInstruction (line: string) =
        { Name = line.[0..2]
          Number = int line.[4..] }

    let instructions = Array.map (parseInstruction) lines

    let tick (instructions: array<Instruction>) state =
        match state.Pointer with
        | p when p < 0 -> { state with Status = Status.Crashed }
        | p when p > instructions.Length -> { state with Status = Status.Crashed }
        | p when p = instructions.Length -> { state with Status = Status.Finished }
        | p when state.Visited.Contains p -> { state with Status = Status.Halted }
        | _ ->
            let instr = instructions.[state.Pointer]

            match instr.Name with
            | "acc" ->
                { state with
                      Pointer = state.Pointer + 1
                      Accumulator = state.Accumulator + instr.Number
                      Visited = state.Visited.Add state.Pointer }
            | "jmp" ->
                { state with
                      Pointer = state.Pointer + instr.Number
                      Visited = state.Visited.Add state.Pointer }
            | "nop" ->
                { state with
                      Pointer = state.Pointer + 1
                      Visited = state.Visited.Add state.Pointer }
            | _ -> state

    let rec tickProgram instructions state =
        if state.Status <> Status.Running then
            state
        else
            tickProgram instructions
            <| tick instructions state

    let runProgram instructions =
        tickProgram
            instructions
            { Pointer = 0
              Accumulator = 0
              Status = Status.Running
              Visited = Set.empty }

    let state = runProgram instructions

    printfn "Accumulator = %d" state.Accumulator

    printfn "Running Day 8 - b"

    let getProgramVariants (instructions: array<Instruction>) =
        let flipInstruction idx =
            let instr = instructions.[idx]

            match instr.Name with
            | "nop" -> Some({ instr with Name = "jmp" })
            | "jmp" -> Some({ instr with Name = "nop" })
            | _ -> None

        seq {
            for i in 0 .. instructions.Length - 1 do
                match flipInstruction i with
                | Some (instr) ->
                    let prevInstr = instructions.[i]
                    instructions.[i] <- instr
                    yield instructions
                    instructions.[i] <- prevInstr
                | None -> ()
        }

    let state =
        getProgramVariants instructions
        |> Seq.map (runProgram)
        |> Seq.find (fun s -> s.Status = Status.Finished)

    printfn "Accumulator = %d" state.Accumulator

    printfn "Day 8 Complete"
