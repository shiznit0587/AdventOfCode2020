// Learn more about F# at http://fsharp.org

let time f d =
    let timer = System.Diagnostics.Stopwatch()
    timer.Start()
    f()
    printfn "Elapsed Time for Day %d: %i ms" d timer.ElapsedMilliseconds

[<EntryPoint>]
let main argv =
    printfn "\n🎅🎅🎅🎅🎅 ADVENT OF CODE 2020 🎅🎅🎅🎅🎅\n"

    let timer = System.Diagnostics.Stopwatch()
    timer.Start()

    time (fun () -> Day1.day1) 1
    time (fun () -> Day2.day2) 2
    time (fun () -> Day3.day3) 3
    time (fun () -> Day4.day4) 4
    time (fun () -> Day5.day5) 5
    time (fun () -> Day6.day6) 6
    time (fun () -> Day7.day7) 7
    time (fun () -> Day8.day8) 8
    time (fun () -> Day9.day9) 9
    time (fun () -> Day10.day10) 10
    time (fun () -> Day11.day11) 11
    time (fun () -> Day12.day12) 12
    time (fun () -> Day13.day13) 13
    time (fun () -> Day14.day14) 14
    time (fun () -> Day15.day15) 15

    printfn "Total Elapsed Time: %i ms" timer.ElapsedMilliseconds

    0 // return an integer exit code
