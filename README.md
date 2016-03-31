# Colours

This is a small program to create colour schemes. There's a `System.Windows.Forms` version as well as a GTK version, intended for Linux/BSD systems. Theoretically, the backend is portable enough to run on anything that has `System.Drawing`.

## Structure

* `Colours.Core` - The shared backend for any frontend.
 * `AppArgParser.cs` - A primitive getopt-like parser, used to pass parameters for init on platforms that support passing arguments to Main.
 * `AppController.cs`
  * *AppController* - The application's logic, the "C" in MVC. A frontend will have this.
  * *AppState* - Encapsulates the app's state, mostly for undo/redo and init.
 * `ColorUtils.cs` - Shared functions useful for converting colours.
 * `HsvColor.cs` - Represents a colour in Hue/Saturation/Value forms.
 * `HtmlProofGenerator.cs` - Makes an HTML page with a colour listing on it.
  * This has room for improvement - maybe multiple listings? 
 * `Schemer.cs`
  * *Schemer* - Generates colour schemes.
  * *SchemeType* - An enum to correspond with `Schemer`. Note that applications depend on the ordering of it, at least for now.
* `Colours.Gtk` - A Unix/Linux frontend using GTK#.
 * `ConfigParser.cs` - Parses a config file. Extremely basic.
 * `GdkWrapper.cs` - Converts to and from GDI+, GDK, and HsvColor objects.
 * `MainWindow.cs` - The view itself.
 * `Program.cs` - Init.
* `Colours.Windows` - A Windows frontend using `System.Windows.Forms`.
 * `ColorButton.cs` - A button that takes on a Color/HsvColor.
  * There's room for improvement on this. It really depends on how much logic you want to move from the Controller-View sync, and if you want to use a button as the primary colour picker.
 * `ColorList.cs` - A hack of a class for serializing a ColorDialog's custom pallette for settings.
 * `MainForm.cs` - The view itself.
 * `Program.cs` - Init.

## Goals

* Android/iOS/UWP ports: Do I need to convert the Core to a "portable class library?" Does this affect the desktop versions? Is a port feasible? `System.Drawing` would be nearly mandatory without major surgery - and even then, you need a wrapper (if you lack `System.Drawing` - the wrapper would be like `GdkWrapper` in the GTK version) you need to implement one for each new platform. Do I need to implement a colour picker for each platform too?
* Eyedropper in SWF version. The GTK version naturally has this because the native colour picker does. Portability might have been a concern, but the GTK version exists now.
* Colour pallettes - both either readymades or custom "bookmark" like ones.
* Rebrand? - Colours is a generic name.
 * To go with this, maybe an icon.
* Icons for actions. There are generic sets, and the GTK version uses stock icons, but SWF could use, say, the VS image library for well-known functions like save. We still need a icons for commands like saturate though.