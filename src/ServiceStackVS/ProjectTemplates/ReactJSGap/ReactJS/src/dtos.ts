/* Options:
Date: 2016-07-22 12:50:06
Version: 4
Tip: To override a DTO option, remove "//" prefix before updating
BaseUrl: http://localhost:$iisexpressport$

//GlobalNamespace: 
ExportAsTypes: True
//MakePropertiesOptional: True
//AddServiceStackTypes: True
//AddResponseStatus: False
//AddImplicitVersion: 
//AddDescriptionAsComments: True
//IncludeTypes: 
//ExcludeTypes: 
//DefaultImports: 
*/


export interface IReturnVoid {
}

export interface IReturn<T> {
}

export class HelloResponse {
    Result: string;
}

// @Route("/hello")
// @Route("/hello/{Name}")
export class Hello implements IReturn<HelloResponse>
{
    Name: string;
    createResponse() { return new HelloResponse(); }
    getTypeName() { return "Hello"; }
}
