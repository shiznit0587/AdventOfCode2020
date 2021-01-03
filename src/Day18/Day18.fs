module Day18

type Operation =
    | Plus
    | Times

type MathType =
    | Basic
    | Advanced

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

    let compute mathType tokens =
        let rec computeRec tokens =
            match tokens with
            | OpenParen :: _ ->
                let closeParenIdx = findMatchingCloseParen tokens
                let subTokens = tokens.[1..closeParenIdx - 1]
                let token = Operand(computeRec subTokens)
                computeRec (token :: tokens.[closeParenIdx + 1..])
            | [ Operand (x) ] -> x
            | [ Operand (x); Operation (op); Operand (y) ] ->
                match op with
                | Plus -> x + y
                | Times -> x * y
            | Operand (x) :: Operation (op) :: Operand (y) :: tl ->
                match op, mathType with
                | Plus, _ -> computeRec (Operand(x + y) :: tl)
                | Times, Basic -> computeRec (Operand(x * y) :: tl)
                | Times, Advanced -> x * computeRec (Operand(y) :: tl)
            | Operand (x) :: Operation (op) :: OpenParen :: _ ->
                let closeParenIdx = 2 + findMatchingCloseParen tokens.[2..]
                let y = computeRec tokens.[3..closeParenIdx - 1]

                computeRec (
                    [ Operand(x)
                      Operation(op)
                      Operand(y) ]
                    @ List.skip (closeParenIdx + 1) tokens
                )
            | _ -> failwith "Invalid token order"

        computeRec tokens

    let homework = readLines 18 |> Array.map (parseLine)

    printfn "Homework Sum = %d" (Array.sumBy (compute Basic) homework)

    printfn "Running Day 18 - b"

    printfn "Homework Sum = %d" (Array.sumBy (compute Advanced) homework)

    printfn "Day 18 Complete"
