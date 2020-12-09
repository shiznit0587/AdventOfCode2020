module Day6

let day6 =
    printfn "Running Day 6 - a"

    let groups =
        (readText 6).Split([| "\n\n" |], System.StringSplitOptions.TrimEntries)

    let calcGroupCountAny group =
        group
        |> Set.ofSeq
        |> Set.intersect (set { 'a' .. 'z' })
        |> Set.count

    printfn "Sum of Group Counts = %d" (Seq.sumBy calcGroupCountAny groups)

    printfn "Running Day 6 - b"

    let charCount cs c = cs |> Seq.filter ((=) c) |> Seq.length

    let calcGroupCountAll group =
        let groupSize = (charCount group '\n') + 1

        seq { 'a' .. 'z' }
        |> Seq.map (charCount group)
        |> Seq.filter ((=) groupSize)
        |> Seq.length

    printfn "Sum of Group Counts = %d" (Seq.sumBy calcGroupCountAll groups)

    printfn "Day 6 Complete"
