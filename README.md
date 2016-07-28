# Colours

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

## Structure

* `Colours.Core` - The shared backend for any frontend.
    * Application objects - classes only really useful for building a frontend.
    	* `AppArgParser.cs` - A primitive getopt-like parser, used to pass parameters for init on platforms that support passing arguments to Main.
    	* `AppController.cs`  - The application's logic, the "C" in MVC. A frontend will have this.
    		* Events are fired whenever the result changes. However, for your initializing in your view, initialize the object, hook up the event, and fire your handler manually, as the first event was fired before could handle it.
            * You'll also need to modify `GetSchemeResults` if you do need to add a scheme type.
        * `AppPaletteController.cs` - An application controller for the palette management half.
    	* `HtmlProofGenerator.cs` - Makes an HTML page with a colour listing on it.
    		* This has room for improvement - maybe multiple listings? 
    * Mixing objects - Contains functions for blending and mixing operations on colours.
        * `Blend.cs` - Gets colours between two colours by blending them together..
	    * `Schemer.cs`
		    * *Schemer* - Generates colour schemes.
		    * *SchemeType* - An enum to correspond with `Schemer`. Note that applications depend on the ordering of it, at least for now.
    * Model objects - classes that handle the core data types and utility libraries for them. These are usable even if you aren't interested in developing a frontend.
        * `AppState.cs`
		    * *AppState* - Encapsulates the app's state, mostly for undo/redo.
            * *InitialAppState* - AppState, but with extra info for palette file names to initialize a more fully-featured frontend.
	    * `ColorUtils.cs` - Shared functions useful for converting colours.
	    * `HsvColor.cs` - Represents a colour in Hue/Saturation/Value forms.
        * `RgbColor.cs` - Represents a colour in Red/Green/Blue forms. This object intended for wrapping into your framework's native color type.
    * Palette objects - classes that handle palettes and conversions between palette formats.
        * `AcoConverter.cs` - Converts to and from Photoshop color swatches and Palette objects.
        * `Palette.cs` - Represents a colour palette, using the GIMP format.
        * `PaletteColor.cs` - Represents a colour in a palette.
* `Colours.Gtk` - A Unix/Linux frontend using GTK#.
	* `ConfigParser.cs` - Parses a config file. Extremely basic.
	* `GdkWrapper.cs` - Converts to and from GDK and Core abstract objects.
	* `MainWindow.cs` - The view itself.
	* `Program.cs` - Init.
* `Colours.Windows` - A Windows frontend using `System.Windows.Forms`.
    * `AboutForm.cs` - An about form.
	* `ColorButton.cs` - A button that takes on a Color/HsvColor.
		* There's room for improvement on this. It really depends on how much logic you want to move from the Controller-View sync, and if you want to use a button as the primary colour picker.
	* `ColorList.cs` - A hack of a class for serializing a ColorDialog's custom pallette for settings.
    * `EyedropperForm.cs` - Picks colours from off the screen.
    * `GdiWrapper.cs` - Converts to and from GDI+ and Core abstract objects.
	* `MainForm.cs` - The view itself.
    * `PalettePropertiesForm.cs` - A form to manipulate the metadata of palettes.
	* `Program.cs` - Init.

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
* [X] Colour pallettes - both either readymades and/or custom "bookmark" like ones.
 * Should favourites be implemented the same way as a pallette? This is flexible, though not as simple to implement UI or code wise.
* [ ] Rebrand? - Colours is a generic name.
 * [ ] To go with this, maybe an icon.
* [ ] Icons for actions. There are generic sets, and the GTK version uses stock icons, but SWF could use, say, the VS image library for well-known functions like save. We still need a icons for commands like saturate though.
* [ ] A colour wheel would be impressive. Kuler does this.
* [ ] Picking colors from images would be neat.
* [ ] Finish GTK parity
 * [ ] Genercize file dialog handling