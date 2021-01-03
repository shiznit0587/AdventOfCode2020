module Day18

type Operation =
    | Plus
    | Times

type Token =
    | OpenParen
    | CloseParen
    | Operation of Operation
    | Operand of int64

let day18 =
    printfn "Running Day 18 - a"

    let parseToken s =
        match s with
        | "(" -> OpenParen
        | ")" -> CloseParen
        | "+" -> Operation(Plus)
        | "*" -> Operation(Times)
        | _ -> Operand(int64 s)

    let parseLine (line: string) =
        line
            .Replace("(", "( ")
            .Replace(")", " )")
            .Split(' ')
        |> Array.map (parseToken)
        |> Array.toList

    let homework = readLines 18 |> Array.map (parseLine)

    let scanParenDepth depth token =
        match token with
        | OpenParen -> depth + 1
        | CloseParen -> depth - 1
        | _ -> depth

    let findMatchingCloseParen tokens =
        tokens
        |> List.scan (scanParenDepth) 0
        |> List.tail
        |> List.findIndex (fun d -> d = 0)

    let rec compute (tokens: List<Token>): int64 =
        match tokens with
        | OpenParen :: _ ->
            let closeParenIdx = findMatchingCloseParen tokens
            let subTokens = tokens.[1..closeParenIdx - 1]
            let token = Operand(compute subTokens)
            compute (token :: tokens.[closeParenIdx + 1..])
        | [ Operand (x) ] -> x
        | [ Operand (x); Operation (op); Operand (y) ] ->
            match op with
            | Plus -> x + y
            | Times -> x * y
        | Operand (x) :: Operation (op) :: Operand (y) :: tl ->
            match op with
            | Plus -> compute (Operand(x + y) :: tl)
            | Times -> compute (Operand(x * y) :: tl)
        | Operand (x) :: Operation (op) :: OpenParen :: _ ->
            let closeParenIdx = 2 + findMatchingCloseParen tokens.[2..]
            let y = compute tokens.[3..closeParenIdx - 1]

            let token =
                match op with
                | Plus -> Operand(x + y)
                | Times -> Operand(x * y)

            compute (token :: List.skip (closeParenIdx + 1) tokens)
        | _ -> failwith "Invalid token order"

    let computeAndPrint tokens =
        printfn "Tokens = %A" tokens
        let solution = compute tokens
        printfn "Solution = %d" solution
        solution

    printfn "Homework Sum = %d" (Array.sumBy compute homework)

    printfn "Running Day 18 - b"

    printfn "Day 18 Complete"
