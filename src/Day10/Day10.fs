module Day10

let day10 =
    printfn "Running Day 10 - a"

    let input =
        readLines 10 |> Array.map (int) |> Array.sort

    let rec countDiffs idx ones threes =
        if idx = 0 then
            match input.[idx] with
            | 1 -> countDiffs 1 1 0
            | 3 -> countDiffs 1 0 1
            | _ -> countDiffs 1 0 0
        else if idx = input.Length then
            (ones, threes + 1)
        else
            match input.[idx] - input.[idx - 1] with
            | 1 -> countDiffs (idx + 1) (ones + 1) threes
            | 3 -> countDiffs (idx + 1) ones (threes + 1)
            | _ -> countDiffs (idx + 1) ones threes

    let (ones, threes) = countDiffs 0 0 0

    printfn "1-Jolt Differences = %d, 3-Jolt Differences = %d, Product = %d" ones threes (ones * threes)

    printfn "Running Day 10 - b"

    printfn "Day 10 Complete"
