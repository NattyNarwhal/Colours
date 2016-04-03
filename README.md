# Colours

This is a small program to create colour schemes. There's a `System.Windows.Forms` version as well as a GTK version, intended for Linux/BSD systems. Theoretically, the backend is portable enough to run on anything that has `System.Drawing`.

## Structure

* `Colours.Core` - The shared backend for any frontend.
    * Application objects - classes only really useful for building a frontend.
    	* `AppArgParser.cs` - A primitive getopt-like parser, used to pass parameters for init on platforms that support passing arguments to Main.
    	* `AppController.cs`
		    * *AppController* - The application's logic, the "C" in MVC. A frontend will have this.
    			* Events are fired whenever the result changes. However, for your initializing in your view, initialize the object, hook up the event, and fire your handler manually, as the first event was fired before could handle it.
		    * *AppState* - Encapsulates the app's state, mostly for undo/redo and init.
    	* `HtmlProofGenerator.cs` - Makes an HTML page with a colour listing on it.
    		* This has room for improvement - maybe multiple listings? 
	    * `Schemer.cs`
		    * *Schemer* - Generates colour schemes.
		    * *SchemeType* - An enum to correspond with `Schemer`. Note that applications depend on the ordering of it, at least for now.
    * Model objects - classes that handle the core data types and utility libraries for them. These are usable even if you aren't interested in developing a frontend.
	    * `ColorUtils.cs` - Shared functions useful for converting colours.
	    * `HsvColor.cs` - Represents a colour in Hue/Saturation/Value forms.
        * `RgbColor.cs` - Represents a color in Red/Green/Blue forms. This object intended for wrapping into your framework's native color type.

* `Colours.Gtk` - A Unix/Linux frontend using GTK#.
	* `ConfigParser.cs` - Parses a config file. Extremely basic.
	* `GdkWrapper.cs` - Converts to and from GDK and Core abstract objects.
	* `MainWindow.cs` - The view itself.
	* `Program.cs` - Init.
* `Colours.Windows` - A Windows frontend using `System.Windows.Forms`.
	* `ColorButton.cs` - A button that takes on a Color/HsvColor.
		* There's room for improvement on this. It really depends on how much logic you want to move from the Controller-View sync, and if you want to use a button as the primary colour picker.
	* `ColorList.cs` - A hack of a class for serializing a ColorDialog's custom pallette for settings.
    * `GdiWrapper.cs` - Converts to and from GDI+ and Core abstract objects.
	* `MainForm.cs` - The view itself.
	* `Program.cs` - Init.

## Goals

* [ ] Android/iOS/UWP ports: The Core library has been converted to a PCL. This means you need to wrap RgbColor to your framework's native color type like `NSColor`, `System.Drawing.Color`, `Gdk.Color`, etc.
 * [ ] Android version: Should be possible. What likely needs to be implemented is a color picker, color well, and any glue.
 * [ ] Mac version: A port using MonoMac or `Xamarin.Mac` should be very feasible, due to the system having robust color GUI controls, but my only Mac is a MacMini1,1 running SL. I've taken a look in VMware and Xcode makes me want to puke.
 * [ ] iOS version: An iOS port for the same reasons as the Mac.
 * [ ] UWP/WinRT version: A Windows 10 version IMHO is of limited usefulness, as desktop Windows 10 runs the SWF version fine, and I lack a WM10 device. On the other hand, I do have a Lumia 520 and Surface RT running and stuck on 8.1.
* [X] Eyedropper in SWF version. The GTK version naturally has this because the native colour picker does. Portability might have been a concern, but the GTK version exists now.
* [ ] Colour pallettes - both either readymades and/or custom "bookmark" like ones.
 * [ ] Perhaps parse GIMP or Photoshop pallettes if we do go with this.
 * Should favourites be implemented the same way as a pallette? This is flexible, though not as simple to implement UI or code wise.
* [ ] Rebrand? - Colours is a generic name.
 * [ ] To go with this, maybe an icon.
* [ ] Icons for actions. There are generic sets, and the GTK version uses stock icons, but SWF could use, say, the VS image library for well-known functions like save. We still need a icons for commands like saturate though.