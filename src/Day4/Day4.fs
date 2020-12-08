module Day4

open System.Text.RegularExpressions

type Passport = Map<string, string>

let day4 =
    printfn "Running Day 4 - a"

    let text = readText 4

    let inputs =
        text.Split([| "\n\n" |], System.StringSplitOptions.TrimEntries)

    let parsePassport input =
        [ for m in Regex.Matches(input, "(\S+):(\S+)") -> m ]
        |> Seq.map (fun m -> List.tail [ for g in m.Groups -> g.Value ])
        |> Seq.map (fun kvl -> (kvl.[0], kvl.[1]))
        |> Map

    let passports = inputs |> Seq.map (parsePassport)

    let checkPassport (passport: Passport) =
        match passport.Count with
        | 8 -> true
        | 7 -> not (passport.ContainsKey "cid")
        | _ -> false

    let validPassports = Seq.filter (checkPassport) passports

    printfn "Valid Passports = %d" (Seq.length validPassports)

    printfn "Running Day 4 - b"

    let checkBirthYear (passport: Passport) =
        let byr = int (passport.Item "byr")
        1920 <= byr && byr <= 2002

    let checkIssueYear (passport: Passport) =
        let iyr = int (passport.Item "iyr")
        2010 <= iyr && iyr <= 2020

    let checkExpirationYear (passport: Passport) =
        let eyr = int (passport.Item "eyr")
        2020 <= eyr && eyr <= 2030

    let checkHeight (passport: Passport) =
        match passport.Item "hgt" with
        | Regex @"(\d+)(in|cm)" [ value; unit ] ->
            let hgt = int value
            match unit with
            | "cm" -> 150 <= hgt && hgt <= 193
            | "in" -> 59 <= hgt && hgt <= 76
            | _ -> false
        | _ -> false

    let checkHairColor (passport: Passport) =
        Regex.Match(passport.Item "hcl", @"#[0-9a-f]{6}").Success

    let checkEyeColor (passport: Passport) =
        match passport.Item "ecl" with
        | "amb"
        | "blu"
        | "brn"
        | "gry"
        | "grn"
        | "hzl"
        | "oth" -> true
        | _ -> false

    let checkPassportId (passport: Passport) =
        Regex.Match(passport.Item "pid", @"^[0-9]{9}$").Success

    let checkPassportValues (passport: Passport) =
        checkBirthYear passport
        && checkIssueYear passport
        && checkExpirationYear passport
        && checkHeight passport
        && checkHairColor passport
        && checkEyeColor passport
        && checkPassportId passport

    let validPassports =
        Seq.filter (checkPassportValues) validPassports

    printfn "Valid Passports = %d" (Seq.length validPassports)

    printfn "Day 4 Complete"
