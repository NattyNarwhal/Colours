// Learn more about F# at http://fsharp.org
// See the 'F# Tutorial' project for more help.

open Colours

let hsvHtml (hsv: HsvColor) = hsv.ToRgb().ToHtml();

[<EntryPoint>]
let main argv = 
    let parsed =
        AppArgParser.ParseArgs(argv, new HsvColor(0.0, 1.0, 1.0), SchemeType.Complement)

    let results =
        match parsed.SchemeType with
            | SchemeType.Analogous -> ColorSchemer.Analogous(parsed.Color)
            | SchemeType.Complement -> ColorSchemer.Complement(parsed.Color)
            | SchemeType.Monochromatic -> ColorSchemer.Monochromatic(parsed.Color)
            | SchemeType.SplitComplements -> ColorSchemer.SplitComplement(parsed.Color)
            | SchemeType.Triads -> ColorSchemer.Triads(parsed.Color)
            | SchemeType.Tetrads -> ColorSchemer.Tetrads(parsed.Color)
            | _ -> System.Collections.Generic.List<HsvColor>() // this seems clunky

    for hsv in results do
        let html = hsvHtml hsv
        printfn "%s" html

    0 // return an integer exit code
