module Day12

type Map =
    { Ship: int * int
      Waypoint: int * int }

let day12 =
    printfn "Running Day 12 - a"

    let input =
        readLines 12
        |> Array.map (fun l -> (l.[0], int l.[1..]))

    let rotateRight times (x, y) =
        [ 1 .. times ]
        |> Seq.fold (fun (x, y) _ -> y, -x) (x, y)

    let rotateLeft times (x, y) =
        [ 1 .. times ]
        |> Seq.fold (fun (x, y) _ -> -y, x) (x, y)

    let moveCoords (x, y) action units =
        match action with
        | 'N' -> (x, y + units)
        | 'S' -> (x, y - units)
        | 'E' -> (x + units, y)
        | 'W' -> (x - units, y)
        | _ -> (x, y)

    let moveShip (map: Map) action units =
        { map with
              Ship = moveCoords map.Ship action units }

    let moveWaypoint (map: Map) action units =
        { map with
              Waypoint = moveCoords map.Waypoint action units }

    let doNav movement (map: Map) (action, units) =
        match action with
        | 'L' ->
            { map with
                  Waypoint = rotateLeft (units / 90) map.Waypoint }
        | 'R' ->
            { map with
                  Waypoint = rotateRight (units / 90) map.Waypoint }
        | 'F' ->
            { map with
                  Ship = fst map.Ship + fst map.Waypoint * units, snd map.Ship + snd map.Waypoint * units }
        | _ -> movement map action units

    let map =
        Seq.fold (doNav moveShip) { Ship = 0, 0; Waypoint = 1, 0 } input

    printfn "Manhattan Distance = %d" (abs (fst map.Ship) + abs (snd map.Ship))

    printfn "Running Day 12 - b"

    let map =
        Seq.fold (doNav moveWaypoint) { Ship = 0, 0; Waypoint = 10, 1 } input

    printfn "Manhattan Distance = %d" (abs (fst map.Ship) + abs (snd map.Ship))

    printfn "Day 12 Complete"
