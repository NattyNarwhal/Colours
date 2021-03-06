﻿// Learn more about F# at http://fsharp.org
// See the 'F# Tutorial' project for more help.

open Colours
open Colours.App

let trueColorEscape (s: string, r, g, b) = 
    sprintf "\e[38;2;%d;%d;%dm%s\e[0m" r g b s

[<EntryPoint>]
let main argv = 
    let parsed =
        AppArgParser.ParseArgs(argv, new HsvColor(0.0, 1.0, 1.0), SchemeType.Complement)
    let controller = new AppController(parsed.MixerState)

    let visual =
        if not (isNull(parsed.UnparsedArgs)) then
            parsed.UnparsedArgs.Contains("-v")
        else false

    let results = controller.Results

    for hsv in results do
        let rgb = hsv.ToRgb();
        let html = rgb.ToHtml()
        if visual then
            let escaped = trueColorEscape(html, rgb.R8, rgb.G8, rgb.B8)
            printfn "%s" escaped
        else
            printfn "%s" html

#if DEBUG
    ignore(System.Console.ReadLine())
#endif

    0 // return an integer exit code
