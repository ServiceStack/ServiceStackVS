# ServiceStackXS - An Addin for Xamarin Studio
This addin provides functionality around Add/Update ServiceStack Reference to match that of ServiceStackVS and other IDE integration.

## Build process
The build process for this Addin is provided by MonoDevelop/Xamarin teams accessible via the [Addins Management site](http://addins.monodevelop.com/).

These build servers can be a bit flaky and rules around which libs the build server provides and which are referenced by the addin seems to change as versions of MD/XS update.

Once it is building successfully the addin can be published to the Alpha, Beta or Stable channels which developers can opt in from the IDE when looking for Addins.

## Running locally
Testing locally can be done by running `package.bat`. This uses the `vstool` CLI to package the addin from the Debug folder to install locally and test. Alternatively, building and running the package from Visual Studio for Mac should launch a new instance of the IDE and allow debugging, this requres the `Addin Maker` addin to be installed (from the IDE).
