## Windows Service Template

To make it easy to get started with hosting ServiceStack as a Windows Service, ServiceStackVS now includes a template for setting up a Windows Service running in a console for easy debugging and as a service when built in Release mode.

Your service name and description use the project name to make it easy to identify when managing your local services.

Also included are a few batch files, `install.bat`, `uninstall.bat`, `start.bat` and `stop.bat` to help with testing your Windows Service running locally.

![](https://github.com/ServiceStack/Assets/raw/69c62bfb26cfddcc99ceae70ba8b10ca62d4ce99/img/servicestackvs/windows-service-template.gif)

The batch files will automatically prompt for admin access if required and launch the default service URL in your default browser when starting your service from `start.bat`.

