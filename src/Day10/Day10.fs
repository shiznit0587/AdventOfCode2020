module Day10

let day10 =
    printfn "Running Day 10 - a"

    let input =
        readLines 10
        |> Array.map (int64)
        |> Array.sort
        |> Array.toList

    let input =
        [ 0L ] @ input @ [ List.last input + 3L ]

    let rec countDiffs idx ones threes =
        if idx = input.Length then
            (ones, threes)
        else
            match input.[idx] - input.[idx - 1] with
            | 1L -> countDiffs (idx + 1) (ones + 1) threes
            | 3L -> countDiffs (idx + 1) ones (threes + 1)
            | _ -> countDiffs (idx + 1) ones threes

    let (ones, threes) = countDiffs 1 0 0

    printfn "1-Jolt Differences = %d, 3-Jolt Differences = %d, Product = %d" ones threes (ones * threes)

    printfn "Running Day 10 - b"

    let pathCounts: int64 array = Array.zeroCreate input.Length
    pathCounts.[pathCounts.Length - 1] <- 1L

    let getPathCounts idx offset =
        if input.Length <= idx + offset then 0L
        else if input.[idx + offset] - input.[idx] > 3L then 0L
        else pathCounts.[idx + offset]

    let countPaths i =
        (getPathCounts i 1)
        + (getPathCounts i 2)
        + (getPathCounts i 3)

    for i in input.Length - 2 .. -1 .. 0 do
        pathCounts.[i] <- countPaths i

    printfn "Distinct Arrangements = %d" pathCounts.[0]

    printfn "Day 10 Complete"
