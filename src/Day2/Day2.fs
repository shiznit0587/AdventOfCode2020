module Day2

type InputLine =
    { Min: int
      Max: int
      Letter: char
      Password: string }

let day2 =
    printfn "Running Day 2 - a"

    let parseLine line =
        match line with
        | Regex @"(?<min>\d+)-(?<max>\d+) (?<letter>\w): (?<password>\w+)" [ min; max; letter; password ] ->
            Some
                ({ Min = int min
                   Max = int max
                   Letter = char letter
                   Password = password })
        | _ -> None

    let lines = readLines 2 |> Seq.choose (parseLine)

    let validCount =
        Seq.map (fun line -> (line, (charCount line.Password line.Letter))) lines
        |> Seq.filter (fun (line, count) -> line.Min <= count && count <= line.Max)
        |> Seq.length

    printfn "Valid Passwords = %d" validCount

    printfn "Running Day 2 - b"

    let checkChars line =
        (line.Password.[line.Min - 1] = line.Letter)
        <> (line.Password.[line.Max - 1] = line.Letter)

    let validCount =
        Seq.filter (checkChars) lines |> Seq.length

    printfn "Valid Passwords = %d" validCount

    printfn "Day 2 Complete"
