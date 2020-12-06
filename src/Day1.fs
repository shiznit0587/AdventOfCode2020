module Day1

let day1 =
    printfn "Running Day 1 - a"

    let lines =
        readLines 1 |> Array.map int |> Array.sort

    let rec findPairSumming2020 left right =
        let sum = lines.[left] + lines.[right]
        match sum with
        | sum when sum > 2020 -> findPairSumming2020 left (right - 1)
        | sum when sum < 2020 -> findPairSumming2020 (left + 1) right
        | _ -> (lines.[left], lines.[right])

    let (left, right) = findPairSumming2020 0 (lines.Length - 1)

    printfn "left = %d, right = %d, product = %d" left right (left * right)

    printfn "Running Day 1 - b"

    // TODO: This algorithm is nowhere near complete,
    let rec findTripleSumming2000 left center right =
        printfn "Testing left=%d, center=%d, right=%d" left center right
        if (lines.[left] + lines.[center] + lines.[right]) = 2020
        then (lines.[left], lines.[center], lines.[right])
        else if right = (lines.Length - 1)
        then findTripleSumming2000 (left + 1) 0 (left + 2)
        else findTripleSumming2000 left 0 (right + 1)

    let (left, center, right) = findTripleSumming2000 0 1 2

    printfn "left = %d, center = %d, right = %d, product = %d" left center right (left * center * right)
