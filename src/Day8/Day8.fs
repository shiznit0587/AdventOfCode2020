module Day8

type Instruction = { Name: string; Number: int }

type ProgramState =
    { Pointer: int
      Accumulator: int
      Halted: bool
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

    let tick state =
        let instr = instructions.[state.Pointer]

        // printfn "State : %d %d" state.Pointer state.Accumulator
        // printfn "Instr : %s %d" instr.Name instr.Number

        if state.Visited.[state.Pointer] then
            { state with Halted = true }
        else
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

    let rec runProgram state =
        if state.Halted then state else runProgram <| tick state

    let state =
        runProgram
            { Pointer = 0
              Accumulator = 0
              Halted = false
              Visited = Array.zeroCreate instructions.Length }

    printfn "Accumulator = %d" state.Accumulator

    printfn "Running Day 8 - b"

    printfn "Day 8 Complete"
