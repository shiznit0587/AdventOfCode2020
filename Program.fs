// Learn more about F# at http://fsharp.org

open System

let time f d =
    let timer = Diagnostics.Stopwatch()
    timer.Start()
    f ()
    printfn "Elapsed Time for Day %d: %i ms" d timer.ElapsedMilliseconds

let runDay day =
    match day with
    | Some (1) -> time (fun () -> Day1.day1) 1
    | Some (2) -> time (fun () -> Day2.day2) 2
    | Some (3) -> time (fun () -> Day3.day3) 3
    | Some (4) -> time (fun () -> Day4.day4) 4
    | Some (5) -> time (fun () -> Day5.day5) 5
    | Some (6) -> time (fun () -> Day6.day6) 6
    | Some (7) -> time (fun () -> Day7.day7) 7
    | Some (8) -> time (fun () -> Day8.day8) 8
    | Some (9) -> time (fun () -> Day9.day9) 9
    | Some (10) -> time (fun () -> Day10.day10) 10
    | Some (11) -> time (fun () -> Day11.day11) 11
    | Some (12) -> time (fun () -> Day12.day12) 12
    | Some (13) -> time (fun () -> Day13.day13) 13
    | Some (14) -> time (fun () -> Day14.day14) 14
    | Some (15) -> time (fun () -> Day15.day15) 15
    | Some (16) -> time (fun () -> Day16.day16) 16
    | Some (17) -> time (fun () -> Day17.day17) 17
    | Some (18) -> time (fun () -> Day18.day18) 18
    | Some (_)
    | None -> printfn "Please provide a Day\n"

[<EntryPoint>]
let main argv =
    printfn "\n🎅🎅🎅🎅🎅 ADVENT OF CODE 2020 🎅🎅🎅🎅🎅\n"

    let tryParseInt s =
        try
            s |> int |> Some
        with :? FormatException -> None

    let day =
        if argv.Length = 0 then
            None
        else
            tryParseInt argv.[0]

    let all = argv.Length = 0

    let timer = Diagnostics.Stopwatch()
    timer.Start()

    if all then
        [ 1 .. 18 ]
        |> Seq.map (fun d -> Some(d))
        |> Seq.iter (runDay)
    else
        runDay day

    printfn "Total Elapsed Time: %i ms" timer.ElapsedMilliseconds

    0 // return an integer exit code
