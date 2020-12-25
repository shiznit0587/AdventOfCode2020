module Day11

let day11 =
    printfn "Running Day 11 - a"

    let seats = readLines 11 |> array2D
    let len1 = Array2D.length1 seats
    let len2 = Array2D.length2 seats

    let directions =
        seq {
            for i in -1 .. 1 do
                for j in -1 .. 1 do
                    if i <> 0 || j <> 0 then yield (i, j)
        }

    let getAdjacentCoords x y =
        seq {
            for (x', y') in directions do
                let i = x + x'
                let j = y + y'

                if 0 <= i && i < len1 && 0 <= j && j < len2
                then yield (i, j)
        }

    // Optimization - build adjacency matrix once
    let adjacencyMatrix = Array2D.init len1 len2 getAdjacentCoords

    let countAdjacentOccupancies x y (seats: char [,]) =
        Seq.sumBy (fun (i, j) -> if seats.[i, j] = '#' then 1 else 0) adjacencyMatrix.[x, y]

    let evalSeat1 x y (seats: char [,]) =
        match seats.[x, y] with
        | 'L' -> if countAdjacentOccupancies x y seats = 0 then '#' else 'L'
        | '#' -> if countAdjacentOccupancies x y seats >= 4 then 'L' else '#'
        | _ -> '.'

    let rec stabilizeSeats seats eval =
        let newSeats =
            Array2D.mapi (fun x y _ -> eval x y seats) seats

        if newSeats = seats then seats else stabilizeSeats newSeats eval

    let countSeats (seats: char [,]) =
        seats
        |> Seq.cast<char>
        |> Seq.sumBy (fun c -> if c = '#' then 1 else 0)

    printfn "Occupied Seats = %d" (countSeats (stabilizeSeats seats evalSeat1))

    printfn "Running Day 11 - b"

    let rec checkDirection x y x' y' (seats: char [,]) =
        let (i, j) = (x + x', y + y')

        if i < 0 || j < 0 then
            0
        else if i >= len1 || j >= len2 then
            0
        else
            match seats.[i, j] with
            | '#' -> 1
            | 'L' -> 0
            | _ -> checkDirection i j x' y' seats

    let countVisibleOccupancies x y (seats: char [,]) =
        Seq.sumBy (fun (x', y') -> checkDirection x y x' y' seats) directions

    let evalSeat2 x y (seats: char [,]) =
        match seats.[x, y] with
        | 'L' -> if countVisibleOccupancies x y seats = 0 then '#' else 'L'
        | '#' -> if countVisibleOccupancies x y seats >= 5 then 'L' else '#'
        | _ -> '.'

    printfn "Occupied Seats = %d" (countSeats (stabilizeSeats seats evalSeat2))

    printfn "Day 11 Complete"
