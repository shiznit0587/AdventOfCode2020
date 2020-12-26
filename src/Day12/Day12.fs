module Day12

type Compass =
    | North
    | South
    | East
    | West

type Ship = { X: int; Y: int; Compass: Compass }

let day12 =
    printfn "Running Day 12 - a"

    let input =
        readLines 12
        |> Array.map (fun l -> (l.[0], int l.[1..]))

    let rec rotateLeft times compass =
        let compass =
            match compass with
            | North -> West
            | South -> East
            | East -> North
            | West -> South

        if times = 1 then compass else rotateLeft (times - 1) compass

    let rec rotateRight times compass =
        let compass =
            match compass with
            | North -> East
            | South -> West
            | East -> South
            | West -> North

        if times = 1 then compass else rotateRight (times - 1) compass

    let doNav (ship: Ship) (action, units) =
        let action =
            match action with
            | 'F' ->
                match ship.Compass with
                | North -> 'N'
                | South -> 'S'
                | East -> 'E'
                | West -> 'W'
            | _ -> action

        match action with
        | 'N' -> { ship with Y = ship.Y + units }
        | 'S' -> { ship with Y = ship.Y - units }
        | 'E' -> { ship with X = ship.X + units }
        | 'W' -> { ship with X = ship.X - units }
        | 'L' ->
            { ship with
                  Compass = (rotateLeft (units / 90) ship.Compass) }
        | 'R' ->
            { ship with
                  Compass = (rotateRight (units / 90) ship.Compass) }
        | _ -> ship

    let ship =
        Seq.fold (doNav) { X = 0; Y = 0; Compass = East } input

    printfn "Manhattan Distance = %d" (abs ship.X + abs ship.Y)

    printfn "Running Day 12 - b"

    let rec rotateWaypointRight times (x, y) =
        if times = 1 then (y, -x) else rotateWaypointRight (times - 1) (y, -x)

    let rec rotateWaypointLeft times (x, y) =
        if times = 1 then (-y, x) else rotateWaypointLeft (times - 1) (-y, x)

    let doNav2 (ship: Ship, (x, y)) (action, units) =
        match action with
        | 'N' -> (ship, (x, y + units))
        | 'S' -> (ship, (x, y - units))
        | 'E' -> (ship, (x + units, y))
        | 'W' -> (ship, (x - units, y))
        | 'L' -> (ship, rotateWaypointLeft (units / 90) (x, y))
        | 'R' -> (ship, rotateWaypointRight (units / 90) (x, y))
        | 'F' ->
            ({ ship with
                   X = ship.X + x * units
                   Y = ship.Y + y * units },
             (x, y))
        | _ -> (ship, (x, y))

    let (ship, _) =
        Seq.fold (doNav2) ({ X = 0; Y = 0; Compass = East }, (10, 1)) input

    printfn "Manhattan Distance = %d" (abs ship.X + abs ship.Y)

    printfn "Day 12 Complete"
