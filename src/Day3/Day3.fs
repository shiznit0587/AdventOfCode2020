module Day3

let day3 =
    printfn "Running Day 3 - a"

    let lines = readLines 3

    let forestWidth = lines.[0].Length
    let forestHeight = lines.Length

    let forest =
        Array2D.init forestWidth forestHeight (fun x y -> lines.[y].[x] = '#')

    let rec countOfTrees (x, y) slope count =
        let (x', y') = (x + fst slope, y + snd slope)
        match (x, y) with
        | (_, y) when y >= forestHeight -> count
        | (x, y) when forest.[x % forestWidth, y] -> 1 + countOfTrees (x', y') slope count
        | _ -> countOfTrees (x', y') slope count

    printfn "Trees Encountered = %d" (countOfTrees (0, 0) (3, 1) 0)

    printfn "Running Day 3 - b"

    let slopes =
        [| (1, 1)
           (3, 1)
           (5, 1)
           (7, 1)
           (1, 2) |]

    let product =
        Array.map (fun s -> countOfTrees (0, 0) s 0) slopes
        |> Array.fold (fun x y -> x * (int64 y)) 1L

    printfn "Product = %d" product

    printfn "Day 3 Complete"
