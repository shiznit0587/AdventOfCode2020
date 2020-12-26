module Day12

type Ship = { X: int; Y: int; Waypoint: int * int }

let day12 =
    printfn "Running Day 12 - a"

    let input =
        readLines 12
        |> Array.map (fun l -> (l.[0], int l.[1..]))

    let rec rotateRight times (x, y) =
        if times = 1 then (y, -x) else rotateRight (times - 1) (y, -x)

    let rec rotateLeft times (x, y) =
        if times = 1 then (-y, x) else rotateLeft (times - 1) (-y, x)

    let moveShip (ship: Ship) (action, units) =
        match action with
        | 'N' -> { ship with Y = ship.Y + units }
        | 'S' -> { ship with Y = ship.Y - units }
        | 'E' -> { ship with X = ship.X + units }
        | 'W' -> { ship with X = ship.X - units }
        | _ -> ship

    let moveWaypoint (ship: Ship) (action, units) =
        match action with
        | 'N' ->
            { ship with
                  Waypoint = (fst ship.Waypoint, snd ship.Waypoint + units) }
        | 'S' ->
            { ship with
                  Waypoint = (fst ship.Waypoint, snd ship.Waypoint - units) }
        | 'E' ->
            { ship with
                  Waypoint = (fst ship.Waypoint + units, snd ship.Waypoint) }
        | 'W' ->
            { ship with
                  Waypoint = (fst ship.Waypoint - units, snd ship.Waypoint) }
        | _ -> ship

    let doNav movement (ship: Ship) (action, units) =
        match action with
        | 'L' ->
            { ship with
                  Waypoint = rotateLeft (units / 90) ship.Waypoint }
        | 'R' ->
            { ship with
                  Waypoint = rotateRight (units / 90) ship.Waypoint }
        | 'F' ->
            { ship with
                  X = ship.X + fst ship.Waypoint * units
                  Y = ship.Y + snd ship.Waypoint * units }
        | _ -> movement ship (action, units)

    let ship =
        Seq.fold (doNav moveShip) { X = 0; Y = 0; Waypoint = (1, 0) } input

    printfn "Manhattan Distance = %d" (abs ship.X + abs ship.Y)

    printfn "Running Day 12 - b"

    let ship =
        Seq.fold (doNav moveWaypoint) { X = 0; Y = 0; Waypoint = (10, 1) } input

    printfn "Manhattan Distance = %d" (abs ship.X + abs ship.Y)

    printfn "Day 12 Complete"
