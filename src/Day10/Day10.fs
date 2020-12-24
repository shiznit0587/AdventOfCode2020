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

    // okay, I have a plan
    // I think the number of permutations is strictly based on how many adapters can follow each adapter.
    // This may not be true, because for each +1, it means the chains for a particular adapter are skipped.

    // Will this work better working backwards?
    // 19 -> c(22) -> 1
    // 16 -> c(19) -> 1
    // 15 -> c(16) -> 1
    // 12 -> c(15) -> 1
    // 11 -> c(12) -> 1
    // 10 -> c(11) + c(12) -> 1 + 1 -> 2
    //  7 -> c(10) -> 2
    //  6 -> c( 7) -> 2
    //  5 -> c( 6) + c(7) -> 2 + 2 -> 4
    //  4 -> c( 5) + c(6) + c(7) -> 4 + 2 + 2 -> 8
    //  1 -> c( 4) -> 8

    let pathCounts = Array.zeroCreate input.Length
    pathCounts.[pathCounts.Length - 1] <- 1

    let getPathCounts idx offset =
        if input.Length <= idx + offset then 0
        else if input.[idx + offset] - input.[idx] > 3 then 0
        else pathCounts.[idx + offset]

    let countPaths i =
        (getPathCounts i 1)
        + (getPathCounts i 2)
        + (getPathCounts i 3)

    for i in input.Length - 2 .. -1 .. 0 do
        pathCounts.[i] <- countPaths i

    printfn "Distinct Arrangements = %d" pathCounts.[0]

    printfn "Day 10 Complete"
