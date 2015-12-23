(* Options:
Date: 2015-12-23 23:52:30
Version: 4.051
Tip: To override a DTO option, remove "//" prefix before updating
BaseUrl: http://techstacks.io

//GlobalNamespace: 
//MakeDataContractsExtensible: False
//AddReturnMarker: True
//AddDescriptionAsComments: True
//AddDataContractAttributes: False
//AddIndexesToDataMembers: False
//AddGeneratedCodeAttributes: False
//AddResponseStatus: False
//AddImplicitVersion: 
//IncludeTypes: 
//ExcludeTypes: 
//InitializeCollections: True
*)

namespace TechStacks.ServiceModel

open System
open System.Collections
open System.Collections.Generic
open System.Runtime.Serialization
open ServiceStack
open ServiceStack.DataAnnotations

    type TechnologyTier =
        | ProgrammingLanguage = 0
        | Client = 1
        | Http = 2
        | Server = 3
        | Data = 4
        | SoftwareInfrastructure = 5
        | OperatingSystem = 6
        | HardwareInfrastructure = 7
        | ThirdPartyServices = 8

    [<AllowNullLiteral>]
    type TechnologyBase() = 
        member val Id:Int64 = new Int64() with get,set
        member val Name:String = null with get,set
        member val VendorName:String = null with get,set
        member val VendorUrl:String = null with get,set
        member val ProductUrl:String = null with get,set
        member val LogoUrl:String = null with get,set
        member val Description:String = null with get,set
        member val Created:DateTime = new DateTime() with get,set
        member val CreatedBy:String = null with get,set
        member val LastModified:DateTime = new DateTime() with get,set
        member val LastModifiedBy:String = null with get,set
        member val OwnerId:String = null with get,set
        member val Slug:String = null with get,set
        member val LogoApproved:Boolean = new Boolean() with get,set
        member val IsLocked:Boolean = new Boolean() with get,set
        member val Tier:TechnologyTier = new TechnologyTier() with get,set
        member val LastStatusUpdate:Nullable<DateTime> = new Nullable<DateTime>() with get,set

    [<AllowNullLiteral>]
    type Technology() = 
        inherit TechnologyBase()

    [<AllowNullLiteral>]
    type TechnologyStackBase() = 
        member val Id:Int64 = new Int64() with get,set
        member val Name:String = null with get,set
        member val VendorName:String = null with get,set
        member val Description:String = null with get,set
        member val AppUrl:String = null with get,set
        member val ScreenshotUrl:String = null with get,set
        member val Created:DateTime = new DateTime() with get,set
        member val CreatedBy:String = null with get,set
        member val LastModified:DateTime = new DateTime() with get,set
        member val LastModifiedBy:String = null with get,set
        member val IsLocked:Boolean = new Boolean() with get,set
        member val OwnerId:String = null with get,set
        member val Slug:String = null with get,set
        member val Details:String = null with get,set
        member val LastStatusUpdate:Nullable<DateTime> = new Nullable<DateTime>() with get,set

    [<AllowNullLiteral>]
    type TechnologyStack() = 
        inherit TechnologyStackBase()

    [<AllowNullLiteral>]
    type TechnologyHistory() = 
        inherit TechnologyBase()
        member val TechnologyId:Int64 = new Int64() with get,set
        member val Operation:String = null with get,set

    [<AllowNullLiteral>]
    type TechnologyInStack() = 
        inherit TechnologyBase()
        member val TechnologyId:Int64 = new Int64() with get,set
        member val TechnologyStackId:Int64 = new Int64() with get,set
        member val Justification:String = null with get,set

    [<AllowNullLiteral>]
    type TechStackDetails() = 
        inherit TechnologyStackBase()
        member val DetailsHtml:String = null with get,set
        member val TechnologyChoices:List<TechnologyInStack> = new List<TechnologyInStack>() with get,set

    [<AllowNullLiteral>]
    type TechnologyStackHistory() = 
        inherit TechnologyStackBase()
        member val TechnologyStackId:Int64 = new Int64() with get,set
        member val Operation:String = null with get,set
        member val TechnologyIds:List<Int64> = new List<Int64>() with get,set

    [<DataContract>]
    [<AllowNullLiteral>]
    type Option() = 
        [<DataMember(Name="name")>]
        member val Name:String = null with get,set

        [<DataMember(Name="title")>]
        member val Title:String = null with get,set

        [<DataMember(Name="value")>]
        member val Value:Nullable<TechnologyTier> = new Nullable<TechnologyTier>() with get,set

    [<AllowNullLiteral>]
    type UserInfo() = 
        member val UserName:String = null with get,set
        member val AvatarUrl:String = null with get,set
        member val StacksCount:Int32 = new Int32() with get,set

    [<AllowNullLiteral>]
    type TechnologyInfo() = 
        member val Tier:TechnologyTier = new TechnologyTier() with get,set
        member val Slug:String = null with get,set
        member val Name:String = null with get,set
        member val LogoUrl:String = null with get,set
        member val StacksCount:Int32 = new Int32() with get,set

    [<AllowNullLiteral>]
    type LogoUrlApprovalResponse() = 
        member val Result:Technology = null with get,set

    [<AllowNullLiteral>]
    type LockStackResponse() = 
        class end

    [<AllowNullLiteral>]
    type CreateTechnologyResponse() = 
        member val Result:Technology = null with get,set
        member val ResponseStatus:ResponseStatus = null with get,set

    [<AllowNullLiteral>]
    type UpdateTechnologyResponse() = 
        member val Result:Technology = null with get,set
        member val ResponseStatus:ResponseStatus = null with get,set

    [<AllowNullLiteral>]
    type DeleteTechnologyResponse() = 
        member val Result:Technology = null with get,set
        member val ResponseStatus:ResponseStatus = null with get,set

    [<AllowNullLiteral>]
    type GetTechnologyResponse() = 
        member val Created:DateTime = new DateTime() with get,set
        member val Technology:Technology = null with get,set
        member val TechnologyStacks:List<TechnologyStack> = new List<TechnologyStack>() with get,set
        member val ResponseStatus:ResponseStatus = null with get,set

    [<AllowNullLiteral>]
    type GetTechnologyPreviousVersionsResponse() = 
        member val Results:List<TechnologyHistory> = new List<TechnologyHistory>() with get,set

    [<AllowNullLiteral>]
    type GetTechnologyFavoriteDetailsResponse() = 
        member val Users:List<String> = new List<String>() with get,set
        member val FavoriteCount:Int32 = new Int32() with get,set

    [<AllowNullLiteral>]
    type GetAllTechnologiesResponse() = 
        member val Results:List<Technology> = new List<Technology>() with get,set

    [<AllowNullLiteral>]
    type CreateTechnologyStackResponse() = 
        member val Result:TechStackDetails = null with get,set
        member val ResponseStatus:ResponseStatus = null with get,set

    [<AllowNullLiteral>]
    type UpdateTechnologyStackResponse() = 
        member val Result:TechStackDetails = null with get,set
        member val ResponseStatus:ResponseStatus = null with get,set

    [<AllowNullLiteral>]
    type DeleteTechnologyStackResponse() = 
        member val Result:TechStackDetails = null with get,set
        member val ResponseStatus:ResponseStatus = null with get,set

    [<AllowNullLiteral>]
    type GetAllTechnologyStacksResponse() = 
        member val Results:List<TechnologyStack> = new List<TechnologyStack>() with get,set

    [<AllowNullLiteral>]
    type GetTechnologyStackResponse() = 
        member val Created:DateTime = new DateTime() with get,set
        member val Result:TechStackDetails = null with get,set
        member val ResponseStatus:ResponseStatus = null with get,set

    [<AllowNullLiteral>]
    type GetTechnologyStackPreviousVersionsResponse() = 
        member val Results:List<TechnologyStackHistory> = new List<TechnologyStackHistory>() with get,set

    [<AllowNullLiteral>]
    type GetTechnologyStackFavoriteDetailsResponse() = 
        member val Users:List<String> = new List<String>() with get,set
        member val FavoriteCount:Int32 = new Int32() with get,set

    [<AllowNullLiteral>]
    type GetConfigResponse() = 
        member val AllTiers:List<Option> = new List<Option>() with get,set

    [<AllowNullLiteral>]
    type OverviewResponse() = 
        member val Created:DateTime = new DateTime() with get,set
        member val TopUsers:List<UserInfo> = new List<UserInfo>() with get,set
        member val TopTechnologies:List<TechnologyInfo> = new List<TechnologyInfo>() with get,set
        member val LatestTechStacks:List<TechStackDetails> = new List<TechStackDetails>() with get,set
        member val PopularTechStacks:List<TechnologyStack> = new List<TechnologyStack>() with get,set
        member val TopTechnologiesByTier:Dictionary<TechnologyTier, List<TechnologyInfo>> = new Dictionary<TechnologyTier, List<TechnologyInfo>>() with get,set
        member val ResponseStatus:ResponseStatus = null with get,set

    [<AllowNullLiteral>]
    type AppOverviewResponse() = 
        member val Created:DateTime = new DateTime() with get,set
        member val AllTiers:List<Option> = new List<Option>() with get,set
        member val TopTechnologies:List<TechnologyInfo> = new List<TechnologyInfo>() with get,set
        member val ResponseStatus:ResponseStatus = null with get,set

    [<AllowNullLiteral>]
    type GetPageStatsResponse() = 
        member val Type:String = null with get,set
        member val Slug:String = null with get,set
        member val ViewCount:Int64 = new Int64() with get,set
        member val FavCount:Int64 = new Int64() with get,set

    [<AllowNullLiteral>]
    type GetFavoriteTechStackResponse() = 
        member val Results:List<TechnologyStack> = new List<TechnologyStack>() with get,set

    [<AllowNullLiteral>]
    type FavoriteTechStackResponse() = 
        member val Result:TechnologyStack = null with get,set

    [<AllowNullLiteral>]
    type GetFavoriteTechnologiesResponse() = 
        member val Results:List<Technology> = new List<Technology>() with get,set

    [<AllowNullLiteral>]
    type FavoriteTechnologyResponse() = 
        member val Result:Technology = null with get,set

    [<AllowNullLiteral>]
    type GetUserFeedResponse() = 
        member val Results:List<TechStackDetails> = new List<TechStackDetails>() with get,set

    [<AllowNullLiteral>]
    type GetUserInfoResponse() = 
        member val UserName:String = null with get,set
        member val Created:DateTime = new DateTime() with get,set
        member val AvatarUrl:String = null with get,set
        member val TechStacks:List<TechnologyStack> = new List<TechnologyStack>() with get,set
        member val FavoriteTechStacks:List<TechnologyStack> = new List<TechnologyStack>() with get,set
        member val FavoriteTechnologies:List<Technology> = new List<Technology>() with get,set

    [<Route("/admin/technology/{TechnologyId}/logo")>]
    [<AllowNullLiteral>]
    type LogoUrlApproval() = 
        interface IReturn<LogoUrlApprovalResponse>
        member val TechnologyId:Int64 = new Int64() with get,set
        member val Approved:Boolean = new Boolean() with get,set

    [<Route("/admin/techstacks/{TechnologyStackId}/lock")>]
    [<AllowNullLiteral>]
    type LockTechStack() = 
        interface IReturn<LockStackResponse>
        member val TechnologyStackId:Int64 = new Int64() with get,set
        member val IsLocked:Boolean = new Boolean() with get,set

    [<Route("/admin/technology/{TechnologyId}/lock")>]
    [<AllowNullLiteral>]
    type LockTech() = 
        interface IReturn<LockStackResponse>
        member val TechnologyId:Int64 = new Int64() with get,set
        member val IsLocked:Boolean = new Boolean() with get,set

    [<Route("/ping")>]
    [<AllowNullLiteral>]
    type Ping() = 
        class end

    [<Route("/{PathInfo*}")>]
    [<AllowNullLiteral>]
    type FallbackForClientRoutes() = 
        member val PathInfo:String = null with get,set

    [<Route("/stacks")>]
    [<AllowNullLiteral>]
    type ClientAllTechnologyStacks() = 
        class end

    [<Route("/tech")>]
    [<AllowNullLiteral>]
    type ClientAllTechnologies() = 
        class end

    [<Route("/tech/{Slug}")>]
    [<AllowNullLiteral>]
    type ClientTechnology() = 
        member val Slug:String = null with get,set

    [<Route("/users/{UserName}")>]
    [<AllowNullLiteral>]
    type ClientUser() = 
        member val UserName:String = null with get,set

    [<Route("/my-session")>]
    [<AllowNullLiteral>]
    type SessionInfo() = 
        class end

    [<Route("/technology", "POST")>]
    [<AllowNullLiteral>]
    type CreateTechnology() = 
        interface IReturn<CreateTechnologyResponse>
        member val Name:String = null with get,set
        member val VendorName:String = null with get,set
        member val VendorUrl:String = null with get,set
        member val ProductUrl:String = null with get,set
        member val LogoUrl:String = null with get,set
        member val Description:String = null with get,set
        member val IsLocked:Boolean = new Boolean() with get,set
        member val Tier:TechnologyTier = new TechnologyTier() with get,set

    [<Route("/technology/{Id}", "PUT")>]
    [<AllowNullLiteral>]
    type UpdateTechnology() = 
        interface IReturn<UpdateTechnologyResponse>
        member val Id:Int64 = new Int64() with get,set
        member val Name:String = null with get,set
        member val VendorName:String = null with get,set
        member val VendorUrl:String = null with get,set
        member val ProductUrl:String = null with get,set
        member val LogoUrl:String = null with get,set
        member val Description:String = null with get,set
        member val IsLocked:Boolean = new Boolean() with get,set
        member val Tier:TechnologyTier = new TechnologyTier() with get,set

    [<Route("/technology/{Id}", "DELETE")>]
    [<AllowNullLiteral>]
    type DeleteTechnology() = 
        interface IReturn<DeleteTechnologyResponse>
        member val Id:Int64 = new Int64() with get,set

    [<Route("/technology/{Slug}")>]
    [<AllowNullLiteral>]
    type GetTechnology() = 
        interface IReturn<GetTechnologyResponse>
        member val Reload:Boolean = new Boolean() with get,set
        member val Slug:String = null with get,set

    [<Route("/technology/{Slug}/previous-versions", "GET")>]
    [<AllowNullLiteral>]
    type GetTechnologyPreviousVersions() = 
        interface IReturn<GetTechnologyPreviousVersionsResponse>
        member val Slug:String = null with get,set

    [<Route("/technology/{Slug}/favorites")>]
    [<AllowNullLiteral>]
    type GetTechnologyFavoriteDetails() = 
        interface IReturn<GetTechnologyFavoriteDetailsResponse>
        member val Slug:String = null with get,set
        member val Reload:Boolean = new Boolean() with get,set

    [<Route("/technology", "GET")>]
    [<AllowNullLiteral>]
    type GetAllTechnologies() = 
        interface IReturn<GetAllTechnologiesResponse>

    [<Route("/technology/search")>]
    [<AutoQueryViewer(Title="Find Technologies", Description="Explore different Technologies", IconUrl="/img/app/tech-white-75.png", DefaultSearchField="Tier", DefaultSearchType="=", DefaultSearchText="Data")>]
    [<AllowNullLiteral>]
    type FindTechnologies() = 
        inherit QueryBase<Technology>()
        interface IReturn<QueryResponse<Technology>>
        member val Name:String = null with get,set
        member val Reload:Boolean = new Boolean() with get,set

    [<Route("/techstacks", "POST")>]
    [<AllowNullLiteral>]
    type CreateTechnologyStack() = 
        interface IReturn<CreateTechnologyStackResponse>
        member val Name:String = null with get,set
        member val VendorName:String = null with get,set
        member val AppUrl:String = null with get,set
        member val ScreenshotUrl:String = null with get,set
        member val Description:String = null with get,set
        member val Details:String = null with get,set
        member val IsLocked:Boolean = new Boolean() with get,set
        member val TechnologyIds:List<Int64> = new List<Int64>() with get,set

    [<Route("/techstacks/{Id}", "PUT")>]
    [<AllowNullLiteral>]
    type UpdateTechnologyStack() = 
        interface IReturn<UpdateTechnologyStackResponse>
        member val Id:Int64 = new Int64() with get,set
        member val Name:String = null with get,set
        member val VendorName:String = null with get,set
        member val AppUrl:String = null with get,set
        member val ScreenshotUrl:String = null with get,set
        member val Description:String = null with get,set
        member val Details:String = null with get,set
        member val IsLocked:Boolean = new Boolean() with get,set
        member val TechnologyIds:List<Int64> = new List<Int64>() with get,set

    [<Route("/techstacks/{Id}", "DELETE")>]
    [<AllowNullLiteral>]
    type DeleteTechnologyStack() = 
        interface IReturn<DeleteTechnologyStackResponse>
        member val Id:Int64 = new Int64() with get,set

    [<Route("/techstacks", "GET")>]
    [<AllowNullLiteral>]
    type GetAllTechnologyStacks() = 
        interface IReturn<GetAllTechnologyStacksResponse>

    [<Route("/techstacks/{Slug}", "GET")>]
    [<AllowNullLiteral>]
    type GetTechnologyStack() = 
        interface IReturn<GetTechnologyStackResponse>
        member val Reload:Boolean = new Boolean() with get,set
        member val Slug:String = null with get,set

    [<Route("/techstacks/{Slug}/previous-versions", "GET")>]
    [<AllowNullLiteral>]
    type GetTechnologyStackPreviousVersions() = 
        interface IReturn<GetTechnologyStackPreviousVersionsResponse>
        member val Slug:String = null with get,set

    [<Route("/techstacks/{Slug}/favorites")>]
    [<AllowNullLiteral>]
    type GetTechnologyStackFavoriteDetails() = 
        interface IReturn<GetTechnologyStackFavoriteDetailsResponse>
        member val Slug:String = null with get,set
        member val Reload:Boolean = new Boolean() with get,set

    [<Route("/config")>]
    [<AllowNullLiteral>]
    type GetConfig() = 
        interface IReturn<GetConfigResponse>

    [<Route("/overview")>]
    [<AllowNullLiteral>]
    type Overview() = 
        interface IReturn<OverviewResponse>
        member val Reload:Boolean = new Boolean() with get,set

    [<Route("/app-overview")>]
    [<AllowNullLiteral>]
    type AppOverview() = 
        interface IReturn<AppOverviewResponse>
        member val Reload:Boolean = new Boolean() with get,set

    [<Route("/pagestats/{Type}/{Slug}")>]
    [<AllowNullLiteral>]
    type GetPageStats() = 
        interface IReturn<GetPageStatsResponse>
        member val Type:String = null with get,set
        member val Slug:String = null with get,set

    [<Route("/techstacks/search")>]
    [<AutoQueryViewer(Title="Find Technology Stacks", Description="Explore different Technology Stacks", IconUrl="/img/app/stacks-white-75.png", DefaultSearchField="Description", DefaultSearchType="Contains", DefaultSearchText="ServiceStack")>]
    [<AllowNullLiteral>]
    type FindTechStacks() = 
        inherit QueryBase<TechnologyStack>()
        interface IReturn<QueryResponse<TechnologyStack>>
        member val Reload:Boolean = new Boolean() with get,set

    [<Route("/favorites/techtacks", "GET")>]
    [<AllowNullLiteral>]
    type GetFavoriteTechStack() = 
        interface IReturn<GetFavoriteTechStackResponse>
        member val TechnologyStackId:Int32 = new Int32() with get,set

    [<Route("/favorites/techtacks/{TechnologyStackId}", "PUT")>]
    [<AllowNullLiteral>]
    type AddFavoriteTechStack() = 
        interface IReturn<FavoriteTechStackResponse>
        member val TechnologyStackId:Int32 = new Int32() with get,set

    [<Route("/favorites/techtacks/{TechnologyStackId}", "DELETE")>]
    [<AllowNullLiteral>]
    type RemoveFavoriteTechStack() = 
        interface IReturn<FavoriteTechStackResponse>
        member val TechnologyStackId:Int32 = new Int32() with get,set

    [<Route("/favorites/technology", "GET")>]
    [<AllowNullLiteral>]
    type GetFavoriteTechnologies() = 
        interface IReturn<GetFavoriteTechnologiesResponse>
        member val TechnologyId:Int32 = new Int32() with get,set

    [<Route("/favorites/technology/{TechnologyId}", "PUT")>]
    [<AllowNullLiteral>]
    type AddFavoriteTechnology() = 
        interface IReturn<FavoriteTechnologyResponse>
        member val TechnologyId:Int32 = new Int32() with get,set

    [<Route("/favorites/technology/{TechnologyId}", "DELETE")>]
    [<AllowNullLiteral>]
    type RemoveFavoriteTechnology() = 
        interface IReturn<FavoriteTechnologyResponse>
        member val TechnologyId:Int32 = new Int32() with get,set

    [<Route("/my-feed")>]
    [<AllowNullLiteral>]
    type GetUserFeed() = 
        interface IReturn<GetUserFeedResponse>

    [<Route("/userinfo/{UserName}")>]
    [<AllowNullLiteral>]
    type GetUserInfo() = 
        interface IReturn<GetUserInfoResponse>
        member val Reload:Boolean = new Boolean() with get,set
        member val UserName:String = null with get,set

