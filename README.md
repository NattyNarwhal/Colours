# Colours

![Screenshot](http://i.imgur.com/Al3Dykh.png)

This is a small program to create colour schemes and palette. There's a `System.Windows.Forms` version as well as a GTK version, intended for Linux/BSD systems. The backend is portable to create a frontend on any .NET platform.

## Features

* Simple UI
* Cleanish-MVC design allowing for creating new interfaces, easily
    * Windows frontend
    * GTK frontend
    * Console frontend
        * Invoke like any other unix tool - one shot with parameters
        * Written in F#
    * And any other UI framework supporting C# or another .NET language
* Easily create colour schemes
    * Variety of algorithms, easy to extend for adding more
* Organize colours into palettes
    * Natively operates on [GIMP](http://gimp.org) palettes, imports/exports Photoshop palettes

## Similar programs

I won't deny inspiration ;)

* [Gpick](https://github.com/thezbyg/gpick) - Does a lot of things, including palette management and scheme generation, (with a greater variety of options, and a wheel to see where they fit) but also previews of colours in sample usage, importing from images, and various ways to blend colours into new ones. Has a clumsy interface though. Written in C++ and Lua, GTK only.
* [Color Wheel Pro](http://www.color-wheel-pro.com/) - The inspiration for Agave. Multiple types of wheel options, preview templates, and Photoshop palette conversion. Seemingly unmaintained and proprietary.
* [Agave](http://home.gna.org/colorscheme/) - My main inspiration. Basic, but with a nice UI. Seemingly unmaintained? Written in C++, GTK only.

## Goals

* [ ] Android/iOS/UWP ports: The Core library has been converted to a PCL. This means you need to wrap RgbColor to your framework's native color type like `NSColor`, `System.Drawing.Color`, `Gdk.Color`, etc.
 * [ ] Android version: Should be possible. What likely needs to be implemented is a color picker, color well, and any glue.
 * [ ] Mac version: A port using MonoMac or `Xamarin.Mac` should be very feasible, due to the system having robust color GUI controls, but my only Mac is a MacMini1,1 running SL. I've taken a look in VMware and Xcode makes me want to puke.
 * [ ] iOS version: An iOS port for the same reasons as the Mac.
 * [ ] UWP/WinRT version: A Windows 10 version IMHO is of limited usefulness, as desktop Windows 10 runs the SWF version fine, and I lack a WM10 device. On the other hand, I do have a Lumia 520 and Surface RT running and stuck on 8.1.
* [X] Eyedropper in SWF version. The GTK version naturally has this because the native colour picker does. Portability might have been a concern, but the GTK version exists now.
 * [ ] I'm not entirely happy with the UI though.
* [ ] DPI awareness in SWF version: Does this even work?
* [ ] Rebrand? - Colours is a generic name.
 * [X] To go with this, maybe an icon.
* [ ] A colour wheel would be impressive. Kuler does this.
* [ ] Alternative colour wheels for different blending beyond RGB would be nice.
* [ ] Improved colour picker in Windows Forms.
* [ ] Picking colors from images would be neat.