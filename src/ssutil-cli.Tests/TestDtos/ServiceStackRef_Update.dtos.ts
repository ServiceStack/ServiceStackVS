/* Options:
Date: 2015-12-23 23:52:33
Version: 4.051
Tip: To override a DTO option, remove "//" prefix before updating
BaseUrl: http://techstacks.io

//GlobalNamespace: 
//ExportAsTypes: True
//MakePropertiesOptional: True
//AddServiceStackTypes: True
//AddResponseStatus: False
//AddImplicitVersion: 
//IncludeTypes: 
//ExcludeTypes: 
//DefaultImports: 
*/


module TechStacks.ServiceModel
{

    export interface IReturnVoid
    {
    }

    export interface IReturn<T>
    {
    }

    export class Technology extends TechnologyBase
    {
    }

    export const enum TechnologyTier
    {
        ProgrammingLanguage,
        Client,
        Http,
        Server,
        Data,
        SoftwareInfrastructure,
        OperatingSystem,
        HardwareInfrastructure,
        ThirdPartyServices,
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

    export class TechnologyStack extends TechnologyStackBase
    {
    }

    export class TechnologyHistory extends TechnologyBase
    {
        TechnologyId: number;
        Operation: string;
    }

    export class QueryBase_1<T> extends QueryBase
    {
    }

    export class TechStackDetails extends TechnologyStackBase
    {
        DetailsHtml: string;
        TechnologyChoices: TechnologyInStack[];
    }

    export class TechnologyStackHistory extends TechnologyStackBase
    {
        TechnologyStackId: number;
        Operation: string;
        TechnologyIds: number[];
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
        Meta: { [index:string]: string; };
    }

    export class TechnologyInStack extends TechnologyBase
    {
        TechnologyId: number;
        TechnologyStackId: number;
        Justification: string;
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

    export class GetTechnologyResponse
    {
        Created: string;
        Technology: Technology;
        TechnologyStacks: TechnologyStack[];
        ResponseStatus: ResponseStatus;
    }

    export class GetTechnologyPreviousVersionsResponse
    {
        Results: TechnologyHistory[];
    }

    export class GetTechnologyFavoriteDetailsResponse
    {
        Users: string[];
        FavoriteCount: number;
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

    export class GetTechnologyStackPreviousVersionsResponse
    {
        Results: TechnologyStackHistory[];
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

    export class OverviewResponse
    {
        Created: string;
        TopUsers: UserInfo[];
        TopTechnologies: TechnologyInfo[];
        LatestTechStacks: TechStackDetails[];
        PopularTechStacks: TechnologyStack[];
        TopTechnologiesByTier: { [index:TechnologyTier]: TechnologyInfo[]; };
        ResponseStatus: ResponseStatus;
    }

    export class AppOverviewResponse
    {
        Created: string;
        AllTiers: Option[];
        TopTechnologies: TechnologyInfo[];
        ResponseStatus: ResponseStatus;
    }

    export class GetPageStatsResponse
    {
        Type: string;
        Slug: string;
        ViewCount: number;
        FavCount: number;
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
        ResponseStatus: ResponseStatus;

        // @DataMember(Order=7)
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

    // @Route("/admin/technology/{TechnologyId}/logo")
    export class LogoUrlApproval implements IReturn<LogoUrlApprovalResponse>
    {
        TechnologyId: number;
        Approved: boolean;
    }

    // @Route("/admin/techstacks/{TechnologyStackId}/lock")
    export class LockTechStack implements IReturn<LockStackResponse>
    {
        TechnologyStackId: number;
        IsLocked: boolean;
    }

    // @Route("/admin/technology/{TechnologyId}/lock")
    export class LockTech implements IReturn<LockStackResponse>
    {
        TechnologyId: number;
        IsLocked: boolean;
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

    // @Route("/my-session")
    export class SessionInfo
    {
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
    }

    // @Route("/technology/{Id}", "DELETE")
    export class DeleteTechnology implements IReturn<DeleteTechnologyResponse>
    {
        Id: number;
    }

    // @Route("/technology/{Slug}")
    export class GetTechnology implements IReturn<GetTechnologyResponse>
    {
        Reload: boolean;
        Slug: string;
    }

    // @Route("/technology/{Slug}/previous-versions", "GET")
    export class GetTechnologyPreviousVersions implements IReturn<GetTechnologyPreviousVersionsResponse>
    {
        Slug: string;
    }

    // @Route("/technology/{Slug}/favorites")
    export class GetTechnologyFavoriteDetails implements IReturn<GetTechnologyFavoriteDetailsResponse>
    {
        Slug: string;
        Reload: boolean;
    }

    // @Route("/technology", "GET")
    export class GetAllTechnologies implements IReturn<GetAllTechnologiesResponse>
    {
    }

    // @Route("/technology/search")
    // @AutoQueryViewer(Title="Find Technologies", Description="Explore different Technologies", IconUrl="/img/app/tech-white-75.png", DefaultSearchField="Tier", DefaultSearchType="=", DefaultSearchText="Data")
    export class FindTechnologies extends QueryBase_1<Technology> implements IReturn<QueryResponse<Technology>>
    {
        Name: string;
        Reload: boolean;
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
    }

    // @Route("/techstacks/{Id}", "DELETE")
    export class DeleteTechnologyStack implements IReturn<DeleteTechnologyStackResponse>
    {
        Id: number;
    }

    // @Route("/techstacks", "GET")
    export class GetAllTechnologyStacks implements IReturn<GetAllTechnologyStacksResponse>
    {
    }

    // @Route("/techstacks/{Slug}", "GET")
    export class GetTechnologyStack implements IReturn<GetTechnologyStackResponse>
    {
        Reload: boolean;
        Slug: string;
    }

    // @Route("/techstacks/{Slug}/previous-versions", "GET")
    export class GetTechnologyStackPreviousVersions implements IReturn<GetTechnologyStackPreviousVersionsResponse>
    {
        Slug: string;
    }

    // @Route("/techstacks/{Slug}/favorites")
    export class GetTechnologyStackFavoriteDetails implements IReturn<GetTechnologyStackFavoriteDetailsResponse>
    {
        Slug: string;
        Reload: boolean;
    }

    // @Route("/config")
    export class GetConfig implements IReturn<GetConfigResponse>
    {
    }

    // @Route("/overview")
    export class Overview implements IReturn<OverviewResponse>
    {
        Reload: boolean;
    }

    // @Route("/app-overview")
    export class AppOverview implements IReturn<AppOverviewResponse>
    {
        Reload: boolean;
    }

    // @Route("/pagestats/{Type}/{Slug}")
    export class GetPageStats implements IReturn<GetPageStatsResponse>
    {
        Type: string;
        Slug: string;
    }

    // @Route("/techstacks/search")
    // @AutoQueryViewer(Title="Find Technology Stacks", Description="Explore different Technology Stacks", IconUrl="/img/app/stacks-white-75.png", DefaultSearchField="Description", DefaultSearchType="Contains", DefaultSearchText="ServiceStack")
    export class FindTechStacks extends QueryBase_1<TechnologyStack> implements IReturn<QueryResponse<TechnologyStack>>
    {
        Reload: boolean;
    }

    // @Route("/favorites/techtacks", "GET")
    export class GetFavoriteTechStack implements IReturn<GetFavoriteTechStackResponse>
    {
        TechnologyStackId: number;
    }

    // @Route("/favorites/techtacks/{TechnologyStackId}", "PUT")
    export class AddFavoriteTechStack implements IReturn<FavoriteTechStackResponse>
    {
        TechnologyStackId: number;
    }

    // @Route("/favorites/techtacks/{TechnologyStackId}", "DELETE")
    export class RemoveFavoriteTechStack implements IReturn<FavoriteTechStackResponse>
    {
        TechnologyStackId: number;
    }

    // @Route("/favorites/technology", "GET")
    export class GetFavoriteTechnologies implements IReturn<GetFavoriteTechnologiesResponse>
    {
        TechnologyId: number;
    }

    // @Route("/favorites/technology/{TechnologyId}", "PUT")
    export class AddFavoriteTechnology implements IReturn<FavoriteTechnologyResponse>
    {
        TechnologyId: number;
    }

    // @Route("/favorites/technology/{TechnologyId}", "DELETE")
    export class RemoveFavoriteTechnology implements IReturn<FavoriteTechnologyResponse>
    {
        TechnologyId: number;
    }

    // @Route("/my-feed")
    export class GetUserFeed implements IReturn<GetUserFeedResponse>
    {
    }

    // @Route("/userinfo/{UserName}")
    export class GetUserInfo implements IReturn<GetUserInfoResponse>
    {
        Reload: boolean;
        UserName: string;
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
        Meta: { [index:string]: string; };
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
    }

}
