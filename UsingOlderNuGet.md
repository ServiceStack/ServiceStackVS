Using NuGet 2.0 - 2.6
---------------------

If you are using the templates provided by ServiceStackVS and If you are running NuGet 2.6 or older, you should update NuGet by checking the 'Extensions and Updates' menu under Tools and then the Update tab on the left.

![](https://github.com/ServiceStack/ServiceStackVS/raw/master/Images/tools_update_nuget.png)

![](https://github.com/ServiceStack/ServiceStackVS/raw/master/Images/nuget_update.png)

If you are unable to update NuGet, you can also 'Enable NuGet Package Restore' on a per-solution basis by right clicking on the solution.

![](https://github.com/ServiceStack/ServiceStackVS/raw/master/enable_package_restore.png)

This will have to be done before the external NuGet references can be resolved otherwise the projects will not build.