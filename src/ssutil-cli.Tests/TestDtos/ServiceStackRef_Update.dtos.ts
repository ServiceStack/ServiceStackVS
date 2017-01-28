/* Options:
Date: 2017-01-28 22:51:27
Version: 4.55
Tip: To override a DTO option, remove "//" prefix before updating
BaseUrl: http://techstacks.io

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


export interface IReturnVoid
{
}

export interface IReturn<T>
{
}

export type TechnologyTier = "ProgrammingLanguage" | "Client" | "Http" | "Server" | "Data" | "SoftwareInfrastructure" | "OperatingSystem" | "HardwareInfrastructure" | "ThirdPartyServices";

export class TechnologyBase
{
    Id: number;
    Name: string;
    VendorName: string;
    VendorUrl: string;
    ProductUrl: string;
    LogoUrl: string;
    Description: string;
    Created: string;
    CreatedBy: string;
    LastModified: string;
    LastModifiedBy: string;
    OwnerId: string;
    Slug: string;
    LogoApproved: boolean;
    IsLocked: boolean;
    Tier: TechnologyTier;
    LastStatusUpdate: string;
}

export class Technology extends TechnologyBase
{
}

// @DataContract
export class ResponseError
{
    // @DataMember(Order=1, EmitDefaultValue=false)
    ErrorCode: string;

    // @DataMember(Order=2, EmitDefaultValue=false)
    FieldName: string;

    // @DataMember(Order=3, EmitDefaultValue=false)
    Message: string;

    // @DataMember(Order=4, EmitDefaultValue=false)
    Meta: { [index:string]: string; };
}

// @DataContract
export class ResponseStatus
{
    // @DataMember(Order=1)
    ErrorCode: string;

    // @DataMember(Order=2)
    Message: string;

    // @DataMember(Order=3)
    StackTrace: string;

    // @DataMember(Order=4)
    Errors: ResponseError[];

    // @DataMember(Order=5)
    Meta: { [index:string]: string; };
}

export class TechnologyStackBase
{
    Id: number;
    Name: string;
    VendorName: string;
    Description: string;
    AppUrl: string;
    ScreenshotUrl: string;
    Created: string;
    CreatedBy: string;
    LastModified: string;
    LastModifiedBy: string;
    IsLocked: boolean;
    OwnerId: string;
    Slug: string;
    Details: string;
    LastStatusUpdate: string;
}

export class TechnologyInStack extends TechnologyBase
{
    TechnologyId: number;
    TechnologyStackId: number;
    Justification: string;
}

export class TechStackDetails extends TechnologyStackBase
{
    DetailsHtml: string;
    TechnologyChoices: TechnologyInStack[];
}

export class TechnologyHistory extends TechnologyBase
{
    TechnologyId: number;
    Operation: string;
}

export class QueryBase
{
    // @DataMember(Order=1)
    Skip: number;

    // @DataMember(Order=2)
    Take: number;

    // @DataMember(Order=3)
    OrderBy: string;

    // @DataMember(Order=4)
    OrderByDesc: string;

    // @DataMember(Order=5)
    Include: string;

    // @DataMember(Order=6)
    Fields: string;

    // @DataMember(Order=7)
    Meta: { [index:string]: string; };
}

export class QueryDb<T> extends QueryBase
{
}

export class TechnologyStack extends TechnologyStackBase
{
}

export class TechnologyStackHistory extends TechnologyStackBase
{
    TechnologyStackId: number;
    Operation: string;
    TechnologyIds: number[];
}

export class UserInfo
{
    UserName: string;
    AvatarUrl: string;
    StacksCount: number;
}

export class TechnologyInfo
{
    Tier: TechnologyTier;
    Slug: string;
    Name: string;
    LogoUrl: string;
    StacksCount: number;
}

// @DataContract
export class Option
{
    // @DataMember(Name="name")
    Name: string;

    // @DataMember(Name="title")
    Title: string;

    // @DataMember(Name="value")
    Value: TechnologyTier;
}

export class LogoUrlApprovalResponse
{
    Result: Technology;
}

export class LockStackResponse
{
}

export class CreateTechnologyResponse
{
    Result: Technology;
    ResponseStatus: ResponseStatus;
}

export class UpdateTechnologyResponse
{
    Result: Technology;
    ResponseStatus: ResponseStatus;
}

export class DeleteTechnologyResponse
{
    Result: Technology;
    ResponseStatus: ResponseStatus;
}

export class CreateTechnologyStackResponse
{
    Result: TechStackDetails;
    ResponseStatus: ResponseStatus;
}

export class UpdateTechnologyStackResponse
{
    Result: TechStackDetails;
    ResponseStatus: ResponseStatus;
}

export class DeleteTechnologyStackResponse
{
    Result: TechStackDetails;
    ResponseStatus: ResponseStatus;
}

export class GetTechnologyPreviousVersionsResponse
{
    Results: TechnologyHistory[];
}

export class GetAllTechnologiesResponse
{
    Results: Technology[];
}

// @DataContract
export class QueryResponse<T>
{
    // @DataMember(Order=1)
    Offset: number;

    // @DataMember(Order=2)
    Total: number;

    // @DataMember(Order=3)
    Results: T[];

    // @DataMember(Order=4)
    Meta: { [index:string]: string; };

    // @DataMember(Order=5)
    ResponseStatus: ResponseStatus;
}

export class GetTechnologyResponse
{
    Created: string;
    Technology: Technology;
    TechnologyStacks: TechnologyStack[];
    ResponseStatus: ResponseStatus;
}

export class GetTechnologyFavoriteDetailsResponse
{
    Users: string[];
    FavoriteCount: number;
}

export class GetTechnologyStackPreviousVersionsResponse
{
    Results: TechnologyStackHistory[];
}

export class GetPageStatsResponse
{
    Type: string;
    Slug: string;
    ViewCount: number;
    FavCount: number;
}

export class OverviewResponse
{
    Created: string;
    TopUsers: UserInfo[];
    TopTechnologies: TechnologyInfo[];
    LatestTechStacks: TechStackDetails[];
    PopularTechStacks: TechnologyStack[];
    TopTechnologiesByTier: { [index:string]: TechnologyInfo[]; };
    ResponseStatus: ResponseStatus;
}

export class AppOverviewResponse
{
    Created: string;
    AllTiers: Option[];
    TopTechnologies: TechnologyInfo[];
    ResponseStatus: ResponseStatus;
}

export class GetAllTechnologyStacksResponse
{
    Results: TechnologyStack[];
}

export class GetTechnologyStackResponse
{
    Created: string;
    Result: TechStackDetails;
    ResponseStatus: ResponseStatus;
}

export class GetTechnologyStackFavoriteDetailsResponse
{
    Users: string[];
    FavoriteCount: number;
}

export class GetConfigResponse
{
    AllTiers: Option[];
}

export class GetFavoriteTechStackResponse
{
    Results: TechnologyStack[];
}

export class FavoriteTechStackResponse
{
    Result: TechnologyStack;
}

export class GetFavoriteTechnologiesResponse
{
    Results: Technology[];
}

export class FavoriteTechnologyResponse
{
    Result: Technology;
}

export class GetUserFeedResponse
{
    Results: TechStackDetails[];
}

export class GetUserInfoResponse
{
    UserName: string;
    Created: string;
    AvatarUrl: string;
    TechStacks: TechnologyStack[];
    FavoriteTechStacks: TechnologyStack[];
    FavoriteTechnologies: Technology[];
}

// @DataContract
export class AuthenticateResponse
{
    // @DataMember(Order=1)
    UserId: string;

    // @DataMember(Order=2)
    SessionId: string;

    // @DataMember(Order=3)
    UserName: string;

    // @DataMember(Order=4)
    DisplayName: string;

    // @DataMember(Order=5)
    ReferrerUrl: string;

    // @DataMember(Order=6)
    BearerToken: string;

    // @DataMember(Order=7)
    ResponseStatus: ResponseStatus;

    // @DataMember(Order=8)
    Meta: { [index:string]: string; };
}

// @DataContract
export class AssignRolesResponse
{
    // @DataMember(Order=1)
    AllRoles: string[];

    // @DataMember(Order=2)
    AllPermissions: string[];

    // @DataMember(Order=3)
    ResponseStatus: ResponseStatus;
}

// @DataContract
export class UnAssignRolesResponse
{
    // @DataMember(Order=1)
    AllRoles: string[];

    // @DataMember(Order=2)
    AllPermissions: string[];

    // @DataMember(Order=3)
    ResponseStatus: ResponseStatus;
}

// @DataContract
export class ConvertSessionToTokenResponse
{
    // @DataMember(Order=1)
    Meta: { [index:string]: string; };

    // @DataMember(Order=2)
    ResponseStatus: ResponseStatus;
}

// @Route("/admin/technology/{TechnologyId}/logo")
export class LogoUrlApproval implements IReturn<LogoUrlApprovalResponse>
{
    TechnologyId: number;
    Approved: boolean;
    createResponse() { return new LogoUrlApprovalResponse(); }
    getTypeName() { return "LogoUrlApproval"; }
}

// @Route("/admin/techstacks/{TechnologyStackId}/lock")
export class LockTechStack implements IReturn<LockStackResponse>
{
    TechnologyStackId: number;
    IsLocked: boolean;
    createResponse() { return new LockStackResponse(); }
    getTypeName() { return "LockTechStack"; }
}

// @Route("/admin/technology/{TechnologyId}/lock")
export class LockTech implements IReturn<LockStackResponse>
{
    TechnologyId: number;
    IsLocked: boolean;
    createResponse() { return new LockStackResponse(); }
    getTypeName() { return "LockTech"; }
}

// @Route("/ping")
export class Ping
{
}

// @Route("/{PathInfo*}")
export class FallbackForClientRoutes
{
    PathInfo: string;
}

// @Route("/stacks")
export class ClientAllTechnologyStacks
{
}

// @Route("/tech")
export class ClientAllTechnologies
{
}

// @Route("/tech/{Slug}")
export class ClientTechnology
{
    Slug: string;
}

// @Route("/users/{UserName}")
export class ClientUser
{
    UserName: string;
}

// @Route("/technology", "POST")
export class CreateTechnology implements IReturn<CreateTechnologyResponse>
{
    Name: string;
    VendorName: string;
    VendorUrl: string;
    ProductUrl: string;
    LogoUrl: string;
    Description: string;
    IsLocked: boolean;
    Tier: TechnologyTier;
    createResponse() { return new CreateTechnologyResponse(); }
    getTypeName() { return "CreateTechnology"; }
}

// @Route("/technology/{Id}", "PUT")
export class UpdateTechnology implements IReturn<UpdateTechnologyResponse>
{
    Id: number;
    Name: string;
    VendorName: string;
    VendorUrl: string;
    ProductUrl: string;
    LogoUrl: string;
    Description: string;
    IsLocked: boolean;
    Tier: TechnologyTier;
    createResponse() { return new UpdateTechnologyResponse(); }
    getTypeName() { return "UpdateTechnology"; }
}

// @Route("/technology/{Id}", "DELETE")
export class DeleteTechnology implements IReturn<DeleteTechnologyResponse>
{
    Id: number;
    createResponse() { return new DeleteTechnologyResponse(); }
    getTypeName() { return "DeleteTechnology"; }
}

// @Route("/techstacks", "POST")
export class CreateTechnologyStack implements IReturn<CreateTechnologyStackResponse>
{
    Name: string;
    VendorName: string;
    AppUrl: string;
    ScreenshotUrl: string;
    Description: string;
    Details: string;
    IsLocked: boolean;
    TechnologyIds: number[];
    createResponse() { return new CreateTechnologyStackResponse(); }
    getTypeName() { return "CreateTechnologyStack"; }
}

// @Route("/techstacks/{Id}", "PUT")
export class UpdateTechnologyStack implements IReturn<UpdateTechnologyStackResponse>
{
    Id: number;
    Name: string;
    VendorName: string;
    AppUrl: string;
    ScreenshotUrl: string;
    Description: string;
    Details: string;
    IsLocked: boolean;
    TechnologyIds: number[];
    createResponse() { return new UpdateTechnologyStackResponse(); }
    getTypeName() { return "UpdateTechnologyStack"; }
}

// @Route("/techstacks/{Id}", "DELETE")
export class DeleteTechnologyStack implements IReturn<DeleteTechnologyStackResponse>
{
    Id: number;
    createResponse() { return new DeleteTechnologyStackResponse(); }
    getTypeName() { return "DeleteTechnologyStack"; }
}

// @Route("/my-session")
export class SessionInfo
{
}

// @Route("/technology/{Slug}/previous-versions", "GET")
export class GetTechnologyPreviousVersions implements IReturn<GetTechnologyPreviousVersionsResponse>
{
    Slug: string;
    createResponse() { return new GetTechnologyPreviousVersionsResponse(); }
    getTypeName() { return "GetTechnologyPreviousVersions"; }
}

// @Route("/technology", "GET")
export class GetAllTechnologies implements IReturn<GetAllTechnologiesResponse>
{
    createResponse() { return new GetAllTechnologiesResponse(); }
    getTypeName() { return "GetAllTechnologies"; }
}

// @Route("/technology/search")
// @AutoQueryViewer(Title="Find Technologies", Description="Explore different Technologies", IconUrl="octicon:database", DefaultSearchField="Tier", DefaultSearchType="=", DefaultSearchText="Data")
export class FindTechnologies extends QueryDb<Technology> implements IReturn<QueryResponse<Technology>>
{
    Name: string;
    NameContains: string;
    createResponse() { return new QueryResponse<Technology>(); }
    getTypeName() { return "FindTechnologies"; }
}

// @Route("/technology/{Slug}")
export class GetTechnology implements IReturn<GetTechnologyResponse>
{
    Slug: string;
    createResponse() { return new GetTechnologyResponse(); }
    getTypeName() { return "GetTechnology"; }
}

// @Route("/technology/{Slug}/favorites")
export class GetTechnologyFavoriteDetails implements IReturn<GetTechnologyFavoriteDetailsResponse>
{
    Slug: string;
    createResponse() { return new GetTechnologyFavoriteDetailsResponse(); }
    getTypeName() { return "GetTechnologyFavoriteDetails"; }
}

// @Route("/techstacks/{Slug}/previous-versions", "GET")
export class GetTechnologyStackPreviousVersions implements IReturn<GetTechnologyStackPreviousVersionsResponse>
{
    Slug: string;
    createResponse() { return new GetTechnologyStackPreviousVersionsResponse(); }
    getTypeName() { return "GetTechnologyStackPreviousVersions"; }
}

// @Route("/pagestats/{Type}/{Slug}")
export class GetPageStats implements IReturn<GetPageStatsResponse>
{
    Type: string;
    Slug: string;
    createResponse() { return new GetPageStatsResponse(); }
    getTypeName() { return "GetPageStats"; }
}

// @Route("/techstacks/search")
// @AutoQueryViewer(Title="Find Technology Stacks", Description="Explore different Technology Stacks", IconUrl="material-icons:cloud", DefaultSearchField="Description", DefaultSearchType="Contains", DefaultSearchText="ServiceStack")
export class FindTechStacks extends QueryDb<TechnologyStack> implements IReturn<QueryResponse<TechnologyStack>>
{
    NameContains: string;
    createResponse() { return new QueryResponse<TechnologyStack>(); }
    getTypeName() { return "FindTechStacks"; }
}

// @Route("/overview")
export class Overview implements IReturn<OverviewResponse>
{
    Reload: boolean;
    createResponse() { return new OverviewResponse(); }
    getTypeName() { return "Overview"; }
}

// @Route("/app-overview")
export class AppOverview implements IReturn<AppOverviewResponse>
{
    Reload: boolean;
    createResponse() { return new AppOverviewResponse(); }
    getTypeName() { return "AppOverview"; }
}

// @Route("/techstacks", "GET")
export class GetAllTechnologyStacks implements IReturn<GetAllTechnologyStacksResponse>
{
    createResponse() { return new GetAllTechnologyStacksResponse(); }
    getTypeName() { return "GetAllTechnologyStacks"; }
}

// @Route("/techstacks/{Slug}", "GET")
export class GetTechnologyStack implements IReturn<GetTechnologyStackResponse>
{
    Slug: string;
    createResponse() { return new GetTechnologyStackResponse(); }
    getTypeName() { return "GetTechnologyStack"; }
}

// @Route("/techstacks/{Slug}/favorites")
export class GetTechnologyStackFavoriteDetails implements IReturn<GetTechnologyStackFavoriteDetailsResponse>
{
    Slug: string;
    createResponse() { return new GetTechnologyStackFavoriteDetailsResponse(); }
    getTypeName() { return "GetTechnologyStackFavoriteDetails"; }
}

// @Route("/config")
export class GetConfig implements IReturn<GetConfigResponse>
{
    createResponse() { return new GetConfigResponse(); }
    getTypeName() { return "GetConfig"; }
}

// @Route("/favorites/techtacks", "GET")
export class GetFavoriteTechStack implements IReturn<GetFavoriteTechStackResponse>
{
    TechnologyStackId: number;
    createResponse() { return new GetFavoriteTechStackResponse(); }
    getTypeName() { return "GetFavoriteTechStack"; }
}

// @Route("/favorites/techtacks/{TechnologyStackId}", "PUT")
export class AddFavoriteTechStack implements IReturn<FavoriteTechStackResponse>
{
    TechnologyStackId: number;
    createResponse() { return new FavoriteTechStackResponse(); }
    getTypeName() { return "AddFavoriteTechStack"; }
}

// @Route("/favorites/techtacks/{TechnologyStackId}", "DELETE")
export class RemoveFavoriteTechStack implements IReturn<FavoriteTechStackResponse>
{
    TechnologyStackId: number;
    createResponse() { return new FavoriteTechStackResponse(); }
    getTypeName() { return "RemoveFavoriteTechStack"; }
}

// @Route("/favorites/technology", "GET")
export class GetFavoriteTechnologies implements IReturn<GetFavoriteTechnologiesResponse>
{
    TechnologyId: number;
    createResponse() { return new GetFavoriteTechnologiesResponse(); }
    getTypeName() { return "GetFavoriteTechnologies"; }
}

// @Route("/favorites/technology/{TechnologyId}", "PUT")
export class AddFavoriteTechnology implements IReturn<FavoriteTechnologyResponse>
{
    TechnologyId: number;
    createResponse() { return new FavoriteTechnologyResponse(); }
    getTypeName() { return "AddFavoriteTechnology"; }
}

// @Route("/favorites/technology/{TechnologyId}", "DELETE")
export class RemoveFavoriteTechnology implements IReturn<FavoriteTechnologyResponse>
{
    TechnologyId: number;
    createResponse() { return new FavoriteTechnologyResponse(); }
    getTypeName() { return "RemoveFavoriteTechnology"; }
}

// @Route("/my-feed")
export class GetUserFeed implements IReturn<GetUserFeedResponse>
{
    createResponse() { return new GetUserFeedResponse(); }
    getTypeName() { return "GetUserFeed"; }
}

// @Route("/userinfo/{UserName}")
export class GetUserInfo implements IReturn<GetUserInfoResponse>
{
    UserName: string;
    createResponse() { return new GetUserInfoResponse(); }
    getTypeName() { return "GetUserInfo"; }
}

// @Route("/auth")
// @Route("/auth/{provider}")
// @Route("/authenticate")
// @Route("/authenticate/{provider}")
// @DataContract
export class Authenticate implements IReturn<AuthenticateResponse>
{
    // @DataMember(Order=1)
    provider: string;

    // @DataMember(Order=2)
    State: string;

    // @DataMember(Order=3)
    oauth_token: string;

    // @DataMember(Order=4)
    oauth_verifier: string;

    // @DataMember(Order=5)
    UserName: string;

    // @DataMember(Order=6)
    Password: string;

    // @DataMember(Order=7)
    RememberMe: boolean;

    // @DataMember(Order=8)
    Continue: string;

    // @DataMember(Order=9)
    nonce: string;

    // @DataMember(Order=10)
    uri: string;

    // @DataMember(Order=11)
    response: string;

    // @DataMember(Order=12)
    qop: string;

    // @DataMember(Order=13)
    nc: string;

    // @DataMember(Order=14)
    cnonce: string;

    // @DataMember(Order=15)
    UseTokenCookie: boolean;

    // @DataMember(Order=16)
    Meta: { [index:string]: string; };
    createResponse() { return new AuthenticateResponse(); }
    getTypeName() { return "Authenticate"; }
}

// @Route("/assignroles")
// @DataContract
export class AssignRoles implements IReturn<AssignRolesResponse>
{
    // @DataMember(Order=1)
    UserName: string;

    // @DataMember(Order=2)
    Permissions: string[];

    // @DataMember(Order=3)
    Roles: string[];
    createResponse() { return new AssignRolesResponse(); }
    getTypeName() { return "AssignRoles"; }
}

// @Route("/unassignroles")
// @DataContract
export class UnAssignRoles implements IReturn<UnAssignRolesResponse>
{
    // @DataMember(Order=1)
    UserName: string;

    // @DataMember(Order=2)
    Permissions: string[];

    // @DataMember(Order=3)
    Roles: string[];
    createResponse() { return new UnAssignRolesResponse(); }
    getTypeName() { return "UnAssignRoles"; }
}

// @Route("/session-to-token")
// @DataContract
export class ConvertSessionToToken implements IReturn<ConvertSessionToTokenResponse>
{
    // @DataMember(Order=1)
    PreserveSession: boolean;
    createResponse() { return new ConvertSessionToTokenResponse(); }
    getTypeName() { return "ConvertSessionToToken"; }
}

// @Route("/admin/technology/search")
// @AutoQueryViewer(Title="Find Technologies Admin", Description="Explore different Technologies", IconUrl="octicon:database", DefaultSearchField="Tier", DefaultSearchType="=", DefaultSearchText="Data")
export class FindTechnologiesAdmin extends QueryDb<Technology> implements IReturn<QueryResponse<Technology>>
{
    Name: string;
    createResponse() { return new QueryResponse<Technology>(); }
    getTypeName() { return "FindTechnologiesAdmin"; }
}
