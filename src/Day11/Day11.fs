module Day11

let day11 =
    printfn "Running Day 11 - a"

    let seats = readLines 11 |> array2D
    let len1 = Array2D.length1 seats
    let len2 = Array2D.length2 seats

    let getAdjacentCoords x y =
        seq {
            for i in -1 .. 1 do
                for j in -1 .. 1 do
                    if i <> 0 || j <> 0 then
                        let i' = x + i
                        let j' = y + j

                        if 0 <= i' && i' < len1 && 0 <= j' && j' < len2
                        then yield (i', j')
        }

    // Optimization - build adjacency matrix once
    let adjacencyMatrix = Array2D.init len1 len2 getAdjacentCoords

    let countAdjacentOccupancies x y (seats: char [,]) =
        Seq.sumBy (fun (i, j) -> if seats.[i, j] = '#' then 1 else 0) adjacencyMatrix.[x, y]

    let evalSeat x y s seats =
        match s with
        | 'L' -> if countAdjacentOccupancies x y seats = 0 then '#' else 'L'
        | '#' -> if countAdjacentOccupancies x y seats >= 4 then 'L' else '#'
        | _ -> '.'

    let rec stabilizeSeats seats =
        let newSeats =
            Array2D.mapi (fun x y s -> evalSeat x y s seats) seats

        if newSeats = seats then seats else stabilizeSeats newSeats

    let seatCount =
        stabilizeSeats seats
        |> Seq.cast<char>
        |> Seq.sumBy (fun c -> if c = '#' then 1 else 0)

    printfn "Occupied Seats = %d" seatCount

    printfn "Running Day 11 - b"

    printfn "Day 11 Complete"
