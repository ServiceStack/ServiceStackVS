/* Options:
Date: 2018-07-30 03:35:45
Version: 5.11
Tip: To override a DTO option, remove "//" prefix before updating
BaseUrl: http://localhost:$iisexpressport$

//GlobalNamespace: 
//MakePropertiesOptional: True
//AddServiceStackTypes: True
//AddResponseStatus: False
//AddImplicitVersion: 
//AddDescriptionAsComments: True
//IncludeTypes: 
//ExcludeTypes: 
//DefaultImports: 
*/


interface IReturn<T>
{
}

interface IReturnVoid
{
}

interface HelloResponse
{
    result?: string;
}

// @Route("/hello")
// @Route("/hello/{Name}")
interface Hello extends IReturn<HelloResponse>
{
    name?: string;
}

