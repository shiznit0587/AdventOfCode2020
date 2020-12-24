module Day10

let day10 =
    printfn "Running Day 10 - a"

    let input =
        readLines 10 |> Array.map (int64) |> Array.sort

    let rec countDiffs idx ones threes =
        if idx = 0 then
            match input.[idx] with
            | 1L -> countDiffs 1 1 0
            | 3L -> countDiffs 1 0 1
            | _ -> countDiffs 1 0 0
        else if idx = input.Length then
            (ones, threes + 1)
        else
            match input.[idx] - input.[idx - 1] with
            | 1L -> countDiffs (idx + 1) (ones + 1) threes
            | 3L -> countDiffs (idx + 1) ones (threes + 1)
            | _ -> countDiffs (idx + 1) ones threes

    let (ones, threes) = countDiffs 0 0 0

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

    let getPathCounts i =
        if input.[i] <= 3L then pathCounts.[i] else 0L

    let finalPathCounts =
        getPathCounts 0
        + getPathCounts 1
        + getPathCounts 2

    printfn "Distinct Arrangements = %d" finalPathCounts

    printfn "Day 10 Complete"
