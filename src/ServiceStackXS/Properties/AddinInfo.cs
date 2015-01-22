using System;
using Mono.Addins;
using Mono.Addins.Description;

[assembly: Addin("ServiceStackXS",
    Namespace = "ServiceStackXS",
	Version = "0.2",
	Category = "IDE extensions")]

[assembly: AddinName("ServiceStackXS")]
[assembly: AddinDescription("An extension that enhances the development experience when working with the ServiceStack framework.")]

[assembly: AddinDependency ("::MonoDevelop.Core", MonoDevelop.BuildInfo.CompatVersion)]
[assembly: AddinDependency("::MonoDevelop.Ide", MonoDevelop.BuildInfo.CompatVersion)]