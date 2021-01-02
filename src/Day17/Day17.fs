module Day17

let day17 =
    printfn "Running Day 17 - a"

    let input = readLines 17

    let input =
        Array2D.init input.Length input.[0].Length (fun x y -> (input.[x].[y] = '#'))

    let state =
        Array3D.zeroCreate<bool> (Array2D.length1 input + 14) (Array2D.length2 input + 14) 15

    // ~ Array2D.blit input 0 0 state.[*, *, 7] 7 7 (Array2D.length1 input) (Array2D.length2 input) ~
    // Have to manually perform the 2d blit, since state is `Array3D` and any slice of it passed to `Array2D.blit` is a copy.
    for y = 0 to Array2D.length2 input - 1 do
        for x = 0 to Array2D.length1 input - 1 do
            state.[x + 7, y + 7, 7] <- input.[x, y]

    let countActiveCubes (state: bool [,,]) =
        state
        |> flatten3d
        |> Seq.sumBy (fun b -> if b then 1 else 0)

    let evalCubeState (state: bool [,,]) x y z =
        let activeCount =
            countActiveCubes state.[x - 1..x + 1, y - 1..y + 1, z - 1..z + 1]

        match state.[x, y, z], activeCount with
        // Match with 3 and 4 instead of 2 and 3 because the current cube was also counted.
        | true, 3
        | true, 4 -> true
        | true, _ -> false
        | false, 3 -> true
        | false, _ -> false

    let mapCubeState state x y z _ =
        // Skip the outer-most layer. It's only in the array to avoid index-out-of-bounds issues with the slice in `evalCubeState`
        match x, y, z with
        | x, _, _ when x = 0 || x = (Array3D.length1 state - 1) -> false
        | _, y, _ when y = 0 || y = (Array3D.length2 state - 1) -> false
        | _, _, z when z = 0 || z = (Array3D.length3 state - 1) -> false
        | _ -> evalCubeState state x y z

    let state =
        [ 1 .. 6 ]
        |> Seq.fold (fun s _ -> Array3D.mapi (mapCubeState s) s) state

    printfn "Active Cubes = %d" (countActiveCubes state)

    printfn "Running Day 17 - b"

    printfn "Day 17 Complete"
