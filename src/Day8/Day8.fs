module Day8

type Instruction =
    { name: string
      number: int }

let day8 =
    printfn "Running Day 8 - a"

    let lines = readLines 8

    let parseInstruction (line:string) =
        let name = line.[0..2]
        let sign = line.[4]
        let number = int line.[5..]
        match sign with
        | '+' -> Some({name = name; number = number })
        | '-' -> Some({name = name; number = -number })
        | _ -> None

    let instructions = Seq.choose(parseInstruction) lines

    printfn "Running Day 8 - b"

    printfn "Day 8 Complete"
