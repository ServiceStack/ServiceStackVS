/* Options:
Date: 2017-01-28 22:45:49
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


interface IReturnVoid
{
}

interface IReturn<T>
{
}

type TechnologyTier = "ProgrammingLanguage" | "Client" | "Http" | "Server" | "Data" | "SoftwareInfrastructure" | "OperatingSystem" | "HardwareInfrastructure" | "ThirdPartyServices";

interface TechnologyBase
{
    Id?: number;
    Name?: string;
    VendorName?: string;
    VendorUrl?: string;
    ProductUrl?: string;
    LogoUrl?: string;
    Description?: string;
    Created?: string;
    CreatedBy?: string;
    LastModified?: string;
    LastModifiedBy?: string;
    OwnerId?: string;
    Slug?: string;
    LogoApproved?: boolean;
    IsLocked?: boolean;
    Tier?: TechnologyTier;
    LastStatusUpdate?: string;
}

interface Technology extends TechnologyBase
{
}

// @DataContract
interface ResponseError
{
    // @DataMember(Order=1, EmitDefaultValue=false)
    ErrorCode?: string;

    // @DataMember(Order=2, EmitDefaultValue=false)
    FieldName?: string;

    // @DataMember(Order=3, EmitDefaultValue=false)
    Message?: string;

    // @DataMember(Order=4, EmitDefaultValue=false)
    Meta?: { [index:string]: string; };
}

// @DataContract
interface ResponseStatus
{
    // @DataMember(Order=1)
    ErrorCode?: string;

    // @DataMember(Order=2)
    Message?: string;

    // @DataMember(Order=3)
    StackTrace?: string;

    // @DataMember(Order=4)
    Errors?: ResponseError[];

    // @DataMember(Order=5)
    Meta?: { [index:string]: string; };
}

interface TechnologyStackBase
{
    Id?: number;
    Name?: string;
    VendorName?: string;
    Description?: string;
    AppUrl?: string;
    ScreenshotUrl?: string;
    Created?: string;
    CreatedBy?: string;
    LastModified?: string;
    LastModifiedBy?: string;
    IsLocked?: boolean;
    OwnerId?: string;
    Slug?: string;
    Details?: string;
    LastStatusUpdate?: string;
}

interface TechnologyInStack extends TechnologyBase
{
    TechnologyId?: number;
    TechnologyStackId?: number;
    Justification?: string;
}

interface TechStackDetails extends TechnologyStackBase
{
    DetailsHtml?: string;
    TechnologyChoices?: TechnologyInStack[];
}

interface TechnologyHistory extends TechnologyBase
{
    TechnologyId?: number;
    Operation?: string;
}

interface QueryBase
{
    // @DataMember(Order=1)
    Skip?: number;

    // @DataMember(Order=2)
    Take?: number;

    // @DataMember(Order=3)
    OrderBy?: string;

    // @DataMember(Order=4)
    OrderByDesc?: string;

    // @DataMember(Order=5)
    Include?: string;

    // @DataMember(Order=6)
    Fields?: string;

    // @DataMember(Order=7)
    Meta?: { [index:string]: string; };
}

interface QueryDb<T> extends QueryBase
{
}

interface TechnologyStack extends TechnologyStackBase
{
}

interface TechnologyStackHistory extends TechnologyStackBase
{
    TechnologyStackId?: number;
    Operation?: string;
    TechnologyIds?: number[];
}

interface UserInfo
{
    UserName?: string;
    AvatarUrl?: string;
    StacksCount?: number;
}

interface TechnologyInfo
{
    Tier?: TechnologyTier;
    Slug?: string;
    Name?: string;
    LogoUrl?: string;
    StacksCount?: number;
}

// @DataContract
interface Option
{
    // @DataMember(Name="name")
    Name?: string;

    // @DataMember(Name="title")
    Title?: string;

    // @DataMember(Name="value")
    Value?: TechnologyTier;
}

interface LogoUrlApprovalResponse
{
    Result?: Technology;
}

interface LockStackResponse
{
}

interface CreateTechnologyResponse
{
    Result?: Technology;
    ResponseStatus?: ResponseStatus;
}

interface UpdateTechnologyResponse
{
    Result?: Technology;
    ResponseStatus?: ResponseStatus;
}

interface DeleteTechnologyResponse
{
    Result?: Technology;
    ResponseStatus?: ResponseStatus;
}

interface CreateTechnologyStackResponse
{
    Result?: TechStackDetails;
    ResponseStatus?: ResponseStatus;
}

interface UpdateTechnologyStackResponse
{
    Result?: TechStackDetails;
    ResponseStatus?: ResponseStatus;
}

interface DeleteTechnologyStackResponse
{
    Result?: TechStackDetails;
    ResponseStatus?: ResponseStatus;
}

interface GetTechnologyPreviousVersionsResponse
{
    Results?: TechnologyHistory[];
}

interface GetAllTechnologiesResponse
{
    Results?: Technology[];
}

// @DataContract
interface QueryResponse<T>
{
    // @DataMember(Order=1)
    Offset?: number;

    // @DataMember(Order=2)
    Total?: number;

    // @DataMember(Order=3)
    Results?: T[];

    // @DataMember(Order=4)
    Meta?: { [index:string]: string; };

    // @DataMember(Order=5)
    ResponseStatus?: ResponseStatus;
}

interface GetTechnologyResponse
{
    Created?: string;
    Technology?: Technology;
    TechnologyStacks?: TechnologyStack[];
    ResponseStatus?: ResponseStatus;
}

interface GetTechnologyFavoriteDetailsResponse
{
    Users?: string[];
    FavoriteCount?: number;
}

interface GetTechnologyStackPreviousVersionsResponse
{
    Results?: TechnologyStackHistory[];
}

interface GetPageStatsResponse
{
    Type?: string;
    Slug?: string;
    ViewCount?: number;
    FavCount?: number;
}

interface OverviewResponse
{
    Created?: string;
    TopUsers?: UserInfo[];
    TopTechnologies?: TechnologyInfo[];
    LatestTechStacks?: TechStackDetails[];
    PopularTechStacks?: TechnologyStack[];
    TopTechnologiesByTier?: { [index:string]: TechnologyInfo[]; };
    ResponseStatus?: ResponseStatus;
}

interface AppOverviewResponse
{
    Created?: string;
    AllTiers?: Option[];
    TopTechnologies?: TechnologyInfo[];
    ResponseStatus?: ResponseStatus;
}

interface GetAllTechnologyStacksResponse
{
    Results?: TechnologyStack[];
}

interface GetTechnologyStackResponse
{
    Created?: string;
    Result?: TechStackDetails;
    ResponseStatus?: ResponseStatus;
}

interface GetTechnologyStackFavoriteDetailsResponse
{
    Users?: string[];
    FavoriteCount?: number;
}

interface GetConfigResponse
{
    AllTiers?: Option[];
}

interface GetFavoriteTechStackResponse
{
    Results?: TechnologyStack[];
}

interface FavoriteTechStackResponse
{
    Result?: TechnologyStack;
}

interface GetFavoriteTechnologiesResponse
{
    Results?: Technology[];
}

interface FavoriteTechnologyResponse
{
    Result?: Technology;
}

interface GetUserFeedResponse
{
    Results?: TechStackDetails[];
}

interface GetUserInfoResponse
{
    UserName?: string;
    Created?: string;
    AvatarUrl?: string;
    TechStacks?: TechnologyStack[];
    FavoriteTechStacks?: TechnologyStack[];
    FavoriteTechnologies?: Technology[];
}

// @DataContract
interface AuthenticateResponse
{
    // @DataMember(Order=1)
    UserId?: string;

    // @DataMember(Order=2)
    SessionId?: string;

    // @DataMember(Order=3)
    UserName?: string;

    // @DataMember(Order=4)
    DisplayName?: string;

    // @DataMember(Order=5)
    ReferrerUrl?: string;

    // @DataMember(Order=6)
    BearerToken?: string;

    // @DataMember(Order=7)
    ResponseStatus?: ResponseStatus;

    // @DataMember(Order=8)
    Meta?: { [index:string]: string; };
}

// @DataContract
interface AssignRolesResponse
{
    // @DataMember(Order=1)
    AllRoles?: string[];

    // @DataMember(Order=2)
    AllPermissions?: string[];

    // @DataMember(Order=3)
    ResponseStatus?: ResponseStatus;
}

// @DataContract
interface UnAssignRolesResponse
{
    // @DataMember(Order=1)
    AllRoles?: string[];

    // @DataMember(Order=2)
    AllPermissions?: string[];

    // @DataMember(Order=3)
    ResponseStatus?: ResponseStatus;
}

// @DataContract
interface ConvertSessionToTokenResponse
{
    // @DataMember(Order=1)
    Meta?: { [index:string]: string; };

    // @DataMember(Order=2)
    ResponseStatus?: ResponseStatus;
}

// @Route("/admin/technology/{TechnologyId}/logo")
interface LogoUrlApproval extends IReturn<LogoUrlApprovalResponse>
{
    TechnologyId?: number;
    Approved?: boolean;
}

// @Route("/admin/techstacks/{TechnologyStackId}/lock")
interface LockTechStack extends IReturn<LockStackResponse>
{
    TechnologyStackId?: number;
    IsLocked?: boolean;
}

// @Route("/admin/technology/{TechnologyId}/lock")
interface LockTech extends IReturn<LockStackResponse>
{
    TechnologyId?: number;
    IsLocked?: boolean;
}

// @Route("/ping")
interface Ping
{
}

// @Route("/{PathInfo*}")
interface FallbackForClientRoutes
{
    PathInfo?: string;
}

// @Route("/stacks")
interface ClientAllTechnologyStacks
{
}

// @Route("/tech")
interface ClientAllTechnologies
{
}

// @Route("/tech/{Slug}")
interface ClientTechnology
{
    Slug?: string;
}

// @Route("/users/{UserName}")
interface ClientUser
{
    UserName?: string;
}

// @Route("/technology", "POST")
interface CreateTechnology extends IReturn<CreateTechnologyResponse>
{
    Name?: string;
    VendorName?: string;
    VendorUrl?: string;
    ProductUrl?: string;
    LogoUrl?: string;
    Description?: string;
    IsLocked?: boolean;
    Tier?: TechnologyTier;
}

// @Route("/technology/{Id}", "PUT")
interface UpdateTechnology extends IReturn<UpdateTechnologyResponse>
{
    Id?: number;
    Name?: string;
    VendorName?: string;
    VendorUrl?: string;
    ProductUrl?: string;
    LogoUrl?: string;
    Description?: string;
    IsLocked?: boolean;
    Tier?: TechnologyTier;
}

// @Route("/technology/{Id}", "DELETE")
interface DeleteTechnology extends IReturn<DeleteTechnologyResponse>
{
    Id?: number;
}

// @Route("/techstacks", "POST")
interface CreateTechnologyStack extends IReturn<CreateTechnologyStackResponse>
{
    Name?: string;
    VendorName?: string;
    AppUrl?: string;
    ScreenshotUrl?: string;
    Description?: string;
    Details?: string;
    IsLocked?: boolean;
    TechnologyIds?: number[];
}

// @Route("/techstacks/{Id}", "PUT")
interface UpdateTechnologyStack extends IReturn<UpdateTechnologyStackResponse>
{
    Id?: number;
    Name?: string;
    VendorName?: string;
    AppUrl?: string;
    ScreenshotUrl?: string;
    Description?: string;
    Details?: string;
    IsLocked?: boolean;
    TechnologyIds?: number[];
}

// @Route("/techstacks/{Id}", "DELETE")
interface DeleteTechnologyStack extends IReturn<DeleteTechnologyStackResponse>
{
    Id?: number;
}

// @Route("/my-session")
interface SessionInfo
{
}

// @Route("/technology/{Slug}/previous-versions", "GET")
interface GetTechnologyPreviousVersions extends IReturn<GetTechnologyPreviousVersionsResponse>
{
    Slug?: string;
}

// @Route("/technology", "GET")
interface GetAllTechnologies extends IReturn<GetAllTechnologiesResponse>
{
}

// @Route("/technology/search")
// @AutoQueryViewer(Title="Find Technologies", Description="Explore different Technologies", IconUrl="octicon:database", DefaultSearchField="Tier", DefaultSearchType="=", DefaultSearchText="Data")
interface FindTechnologies extends QueryDb<Technology>, IReturn<QueryResponse<Technology>>
{
    Name?: string;
    NameContains?: string;
}

// @Route("/technology/{Slug}")
interface GetTechnology extends IReturn<GetTechnologyResponse>
{
    Slug?: string;
}

// @Route("/technology/{Slug}/favorites")
interface GetTechnologyFavoriteDetails extends IReturn<GetTechnologyFavoriteDetailsResponse>
{
    Slug?: string;
}

// @Route("/techstacks/{Slug}/previous-versions", "GET")
interface GetTechnologyStackPreviousVersions extends IReturn<GetTechnologyStackPreviousVersionsResponse>
{
    Slug?: string;
}

// @Route("/pagestats/{Type}/{Slug}")
interface GetPageStats extends IReturn<GetPageStatsResponse>
{
    Type?: string;
    Slug?: string;
}

// @Route("/techstacks/search")
// @AutoQueryViewer(Title="Find Technology Stacks", Description="Explore different Technology Stacks", IconUrl="material-icons:cloud", DefaultSearchField="Description", DefaultSearchType="Contains", DefaultSearchText="ServiceStack")
interface FindTechStacks extends QueryDb<TechnologyStack>, IReturn<QueryResponse<TechnologyStack>>
{
    NameContains?: string;
}

// @Route("/overview")
interface Overview extends IReturn<OverviewResponse>
{
    Reload?: boolean;
}

// @Route("/app-overview")
interface AppOverview extends IReturn<AppOverviewResponse>
{
    Reload?: boolean;
}

// @Route("/techstacks", "GET")
interface GetAllTechnologyStacks extends IReturn<GetAllTechnologyStacksResponse>
{
}

// @Route("/techstacks/{Slug}", "GET")
interface GetTechnologyStack extends IReturn<GetTechnologyStackResponse>
{
    Slug?: string;
}

// @Route("/techstacks/{Slug}/favorites")
interface GetTechnologyStackFavoriteDetails extends IReturn<GetTechnologyStackFavoriteDetailsResponse>
{
    Slug?: string;
}

// @Route("/config")
interface GetConfig extends IReturn<GetConfigResponse>
{
}

// @Route("/favorites/techtacks", "GET")
interface GetFavoriteTechStack extends IReturn<GetFavoriteTechStackResponse>
{
    TechnologyStackId?: number;
}

// @Route("/favorites/techtacks/{TechnologyStackId}", "PUT")
interface AddFavoriteTechStack extends IReturn<FavoriteTechStackResponse>
{
    TechnologyStackId?: number;
}

// @Route("/favorites/techtacks/{TechnologyStackId}", "DELETE")
interface RemoveFavoriteTechStack extends IReturn<FavoriteTechStackResponse>
{
    TechnologyStackId?: number;
}

// @Route("/favorites/technology", "GET")
interface GetFavoriteTechnologies extends IReturn<GetFavoriteTechnologiesResponse>
{
    TechnologyId?: number;
}

// @Route("/favorites/technology/{TechnologyId}", "PUT")
interface AddFavoriteTechnology extends IReturn<FavoriteTechnologyResponse>
{
    TechnologyId?: number;
}

// @Route("/favorites/technology/{TechnologyId}", "DELETE")
interface RemoveFavoriteTechnology extends IReturn<FavoriteTechnologyResponse>
{
    TechnologyId?: number;
}

// @Route("/my-feed")
interface GetUserFeed extends IReturn<GetUserFeedResponse>
{
}

// @Route("/userinfo/{UserName}")
interface GetUserInfo extends IReturn<GetUserInfoResponse>
{
    UserName?: string;
}

// @Route("/auth")
// @Route("/auth/{provider}")
// @Route("/authenticate")
// @Route("/authenticate/{provider}")
// @DataContract
interface Authenticate extends IReturn<AuthenticateResponse>
{
    // @DataMember(Order=1)
    provider?: string;

    // @DataMember(Order=2)
    State?: string;

    // @DataMember(Order=3)
    oauth_token?: string;

    // @DataMember(Order=4)
    oauth_verifier?: string;

    // @DataMember(Order=5)
    UserName?: string;

    // @DataMember(Order=6)
    Password?: string;

    // @DataMember(Order=7)
    RememberMe?: boolean;

    // @DataMember(Order=8)
    Continue?: string;

    // @DataMember(Order=9)
    nonce?: string;

    // @DataMember(Order=10)
    uri?: string;

    // @DataMember(Order=11)
    response?: string;

    // @DataMember(Order=12)
    qop?: string;

    // @DataMember(Order=13)
    nc?: string;

    // @DataMember(Order=14)
    cnonce?: string;

    // @DataMember(Order=15)
    UseTokenCookie?: boolean;

    // @DataMember(Order=16)
    Meta?: { [index:string]: string; };
}

// @Route("/assignroles")
// @DataContract
interface AssignRoles extends IReturn<AssignRolesResponse>
{
    // @DataMember(Order=1)
    UserName?: string;

    // @DataMember(Order=2)
    Permissions?: string[];

    // @DataMember(Order=3)
    Roles?: string[];
}

// @Route("/unassignroles")
// @DataContract
interface UnAssignRoles extends IReturn<UnAssignRolesResponse>
{
    // @DataMember(Order=1)
    UserName?: string;

    // @DataMember(Order=2)
    Permissions?: string[];

    // @DataMember(Order=3)
    Roles?: string[];
}

// @Route("/session-to-token")
// @DataContract
interface ConvertSessionToToken extends IReturn<ConvertSessionToTokenResponse>
{
    // @DataMember(Order=1)
    PreserveSession?: boolean;
}

// @Route("/admin/technology/search")
// @AutoQueryViewer(Title="Find Technologies Admin", Description="Explore different Technologies", IconUrl="octicon:database", DefaultSearchField="Tier", DefaultSearchType="=", DefaultSearchText="Data")
interface FindTechnologiesAdmin extends QueryDb<Technology>, IReturn<QueryResponse<Technology>>
{
    Name?: string;
}
