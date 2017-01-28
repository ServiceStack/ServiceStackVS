/* Options:
Date: 2017-01-28 22:45:52
SwiftVersion: 3.0
Version: 4.55
Tip: To override a DTO option, remove "//" prefix before updating
BaseUrl: http://techstacks.io

//BaseClass: 
//AddModelExtensions: True
//AddServiceStackTypes: True
//IncludeTypes: 
//ExcludeTypes: 
//ExcludeGenericBaseTypes: False
//AddResponseStatus: False
//AddImplicitVersion: 
//AddDescriptionAsComments: True
//InitializeCollections: True
//TreatTypesAsStrings: 
//DefaultImports: Foundation
*/

import Foundation;

// @Route("/admin/technology/{TechnologyId}/logo")
public class LogoUrlApproval : IReturn
{
    public typealias Return = LogoUrlApprovalResponse

    required public init(){}
    public var technologyId:Int64?
    public var approved:Bool?
}

// @Route("/admin/techstacks/{TechnologyStackId}/lock")
public class LockTechStack : IReturn
{
    public typealias Return = LockStackResponse

    required public init(){}
    public var technologyStackId:Int64?
    public var isLocked:Bool?
}

// @Route("/admin/technology/{TechnologyId}/lock")
public class LockTech : IReturn
{
    public typealias Return = LockStackResponse

    required public init(){}
    public var technologyId:Int64?
    public var isLocked:Bool?
}

// @Route("/ping")
public class Ping
{
    required public init(){}
}

// @Route("/{PathInfo*}")
public class FallbackForClientRoutes
{
    required public init(){}
    public var pathInfo:String?
}

// @Route("/stacks")
public class ClientAllTechnologyStacks
{
    required public init(){}
}

// @Route("/tech")
public class ClientAllTechnologies
{
    required public init(){}
}

// @Route("/tech/{Slug}")
public class ClientTechnology
{
    required public init(){}
    public var slug:String?
}

// @Route("/users/{UserName}")
public class ClientUser
{
    required public init(){}
    public var userName:String?
}

// @Route("/technology", "POST")
public class CreateTechnology : IReturn
{
    public typealias Return = CreateTechnologyResponse

    required public init(){}
    public var name:String?
    public var vendorName:String?
    public var vendorUrl:String?
    public var productUrl:String?
    public var logoUrl:String?
    public var Description:String?
    public var isLocked:Bool?
    public var tier:TechnologyTier?
}

// @Route("/technology/{Id}", "PUT")
public class UpdateTechnology : IReturn
{
    public typealias Return = UpdateTechnologyResponse

    required public init(){}
    public var id:Int64?
    public var name:String?
    public var vendorName:String?
    public var vendorUrl:String?
    public var productUrl:String?
    public var logoUrl:String?
    public var Description:String?
    public var isLocked:Bool?
    public var tier:TechnologyTier?
}

// @Route("/technology/{Id}", "DELETE")
public class DeleteTechnology : IReturn
{
    public typealias Return = DeleteTechnologyResponse

    required public init(){}
    public var id:Int64?
}

// @Route("/techstacks", "POST")
public class CreateTechnologyStack : IReturn
{
    public typealias Return = CreateTechnologyStackResponse

    required public init(){}
    public var name:String?
    public var vendorName:String?
    public var appUrl:String?
    public var screenshotUrl:String?
    public var Description:String?
    public var details:String?
    public var isLocked:Bool?
    public var technologyIds:[Int64] = []
}

// @Route("/techstacks/{Id}", "PUT")
public class UpdateTechnologyStack : IReturn
{
    public typealias Return = UpdateTechnologyStackResponse

    required public init(){}
    public var id:Int64?
    public var name:String?
    public var vendorName:String?
    public var appUrl:String?
    public var screenshotUrl:String?
    public var Description:String?
    public var details:String?
    public var isLocked:Bool?
    public var technologyIds:[Int64] = []
}

// @Route("/techstacks/{Id}", "DELETE")
public class DeleteTechnologyStack : IReturn
{
    public typealias Return = DeleteTechnologyStackResponse

    required public init(){}
    public var id:Int64?
}

// @Route("/my-session")
public class SessionInfo
{
    required public init(){}
}

// @Route("/technology/{Slug}/previous-versions", "GET")
public class GetTechnologyPreviousVersions : IReturn
{
    public typealias Return = GetTechnologyPreviousVersionsResponse

    required public init(){}
    public var slug:String?
}

// @Route("/technology", "GET")
public class GetAllTechnologies : IReturn
{
    public typealias Return = GetAllTechnologiesResponse

    required public init(){}
}

// @Route("/technology/search")
// @AutoQueryViewer(Title="Find Technologies", Description="Explore different Technologies", IconUrl="octicon:database", DefaultSearchField="Tier", DefaultSearchType="=", DefaultSearchText="Data")
public class FindTechnologies<Technology : JsonSerializable> : QueryDb<Technology>, IReturn
{
    public typealias Return = QueryResponse<Technology>

    required public init(){}
    public var name:String?
    public var nameContains:String?
}

// @Route("/technology/{Slug}")
public class GetTechnology : IReturn
{
    public typealias Return = GetTechnologyResponse

    required public init(){}
    public var slug:String?
}

// @Route("/technology/{Slug}/favorites")
public class GetTechnologyFavoriteDetails : IReturn
{
    public typealias Return = GetTechnologyFavoriteDetailsResponse

    required public init(){}
    public var slug:String?
}

// @Route("/techstacks/{Slug}/previous-versions", "GET")
public class GetTechnologyStackPreviousVersions : IReturn
{
    public typealias Return = GetTechnologyStackPreviousVersionsResponse

    required public init(){}
    public var slug:String?
}

// @Route("/pagestats/{Type}/{Slug}")
public class GetPageStats : IReturn
{
    public typealias Return = GetPageStatsResponse

    required public init(){}
    public var type:String?
    public var slug:String?
}

// @Route("/techstacks/search")
// @AutoQueryViewer(Title="Find Technology Stacks", Description="Explore different Technology Stacks", IconUrl="material-icons:cloud", DefaultSearchField="Description", DefaultSearchType="Contains", DefaultSearchText="ServiceStack")
public class FindTechStacks<TechnologyStack : JsonSerializable> : QueryDb<TechnologyStack>, IReturn
{
    public typealias Return = QueryResponse<TechnologyStack>

    required public init(){}
    public var nameContains:String?
}

// @Route("/overview")
public class Overview : IReturn
{
    public typealias Return = OverviewResponse

    required public init(){}
    public var reload:Bool?
}

// @Route("/app-overview")
public class AppOverview : IReturn
{
    public typealias Return = AppOverviewResponse

    required public init(){}
    public var reload:Bool?
}

// @Route("/techstacks", "GET")
public class GetAllTechnologyStacks : IReturn
{
    public typealias Return = GetAllTechnologyStacksResponse

    required public init(){}
}

// @Route("/techstacks/{Slug}", "GET")
public class GetTechnologyStack : IReturn
{
    public typealias Return = GetTechnologyStackResponse

    required public init(){}
    public var slug:String?
}

// @Route("/techstacks/{Slug}/favorites")
public class GetTechnologyStackFavoriteDetails : IReturn
{
    public typealias Return = GetTechnologyStackFavoriteDetailsResponse

    required public init(){}
    public var slug:String?
}

// @Route("/config")
public class GetConfig : IReturn
{
    public typealias Return = GetConfigResponse

    required public init(){}
}

// @Route("/favorites/techtacks", "GET")
public class GetFavoriteTechStack : IReturn
{
    public typealias Return = GetFavoriteTechStackResponse

    required public init(){}
    public var technologyStackId:Int?
}

// @Route("/favorites/techtacks/{TechnologyStackId}", "PUT")
public class AddFavoriteTechStack : IReturn
{
    public typealias Return = FavoriteTechStackResponse

    required public init(){}
    public var technologyStackId:Int?
}

// @Route("/favorites/techtacks/{TechnologyStackId}", "DELETE")
public class RemoveFavoriteTechStack : IReturn
{
    public typealias Return = FavoriteTechStackResponse

    required public init(){}
    public var technologyStackId:Int?
}

// @Route("/favorites/technology", "GET")
public class GetFavoriteTechnologies : IReturn
{
    public typealias Return = GetFavoriteTechnologiesResponse

    required public init(){}
    public var technologyId:Int?
}

// @Route("/favorites/technology/{TechnologyId}", "PUT")
public class AddFavoriteTechnology : IReturn
{
    public typealias Return = FavoriteTechnologyResponse

    required public init(){}
    public var technologyId:Int?
}

// @Route("/favorites/technology/{TechnologyId}", "DELETE")
public class RemoveFavoriteTechnology : IReturn
{
    public typealias Return = FavoriteTechnologyResponse

    required public init(){}
    public var technologyId:Int?
}

// @Route("/my-feed")
public class GetUserFeed : IReturn
{
    public typealias Return = GetUserFeedResponse

    required public init(){}
}

// @Route("/userinfo/{UserName}")
public class GetUserInfo : IReturn
{
    public typealias Return = GetUserInfoResponse

    required public init(){}
    public var userName:String?
}

// @Route("/auth")
// @Route("/auth/{provider}")
// @Route("/authenticate")
// @Route("/authenticate/{provider}")
// @DataContract
public class Authenticate : IReturn, IPost
{
    public typealias Return = AuthenticateResponse

    required public init(){}
    // @DataMember(Order=1)
    public var provider:String?

    // @DataMember(Order=2)
    public var state:String?

    // @DataMember(Order=3)
    public var oauth_token:String?

    // @DataMember(Order=4)
    public var oauth_verifier:String?

    // @DataMember(Order=5)
    public var userName:String?

    // @DataMember(Order=6)
    public var password:String?

    // @DataMember(Order=7)
    public var rememberMe:Bool?

    // @DataMember(Order=8)
    public var `continue`:String?

    // @DataMember(Order=9)
    public var nonce:String?

    // @DataMember(Order=10)
    public var uri:String?

    // @DataMember(Order=11)
    public var response:String?

    // @DataMember(Order=12)
    public var qop:String?

    // @DataMember(Order=13)
    public var nc:String?

    // @DataMember(Order=14)
    public var cnonce:String?

    // @DataMember(Order=15)
    public var useTokenCookie:Bool?

    // @DataMember(Order=16)
    public var meta:[String:String] = [:]
}

// @Route("/assignroles")
// @DataContract
public class AssignRoles : IReturn, IPost
{
    public typealias Return = AssignRolesResponse

    required public init(){}
    // @DataMember(Order=1)
    public var userName:String?

    // @DataMember(Order=2)
    public var permissions:[String] = []

    // @DataMember(Order=3)
    public var roles:[String] = []
}

// @Route("/unassignroles")
// @DataContract
public class UnAssignRoles : IReturn, IPost
{
    public typealias Return = UnAssignRolesResponse

    required public init(){}
    // @DataMember(Order=1)
    public var userName:String?

    // @DataMember(Order=2)
    public var permissions:[String] = []

    // @DataMember(Order=3)
    public var roles:[String] = []
}

// @Route("/session-to-token")
// @DataContract
public class ConvertSessionToToken : IReturn, IPost
{
    public typealias Return = ConvertSessionToTokenResponse

    required public init(){}
    // @DataMember(Order=1)
    public var preserveSession:Bool?
}

// @Route("/admin/technology/search")
// @AutoQueryViewer(Title="Find Technologies Admin", Description="Explore different Technologies", IconUrl="octicon:database", DefaultSearchField="Tier", DefaultSearchType="=", DefaultSearchText="Data")
public class FindTechnologiesAdmin<Technology : JsonSerializable> : QueryDb<Technology>, IReturn
{
    public typealias Return = QueryResponse<Technology>

    required public init(){}
    public var name:String?
}

public class LogoUrlApprovalResponse
{
    required public init(){}
    public var result:Technology?
}

public class LockStackResponse
{
    required public init(){}
}

public class CreateTechnologyResponse
{
    required public init(){}
    public var result:Technology?
    public var responseStatus:ResponseStatus?
}

public class UpdateTechnologyResponse
{
    required public init(){}
    public var result:Technology?
    public var responseStatus:ResponseStatus?
}

public class DeleteTechnologyResponse
{
    required public init(){}
    public var result:Technology?
    public var responseStatus:ResponseStatus?
}

public class CreateTechnologyStackResponse
{
    required public init(){}
    public var result:TechStackDetails?
    public var responseStatus:ResponseStatus?
}

public class UpdateTechnologyStackResponse
{
    required public init(){}
    public var result:TechStackDetails?
    public var responseStatus:ResponseStatus?
}

public class DeleteTechnologyStackResponse
{
    required public init(){}
    public var result:TechStackDetails?
    public var responseStatus:ResponseStatus?
}

public class GetTechnologyPreviousVersionsResponse
{
    required public init(){}
    public var results:[TechnologyHistory] = []
}

public class GetAllTechnologiesResponse
{
    required public init(){}
    public var results:[Technology] = []
}

// @DataContract
public class QueryResponse<T : JsonSerializable>
{
    required public init(){}
    // @DataMember(Order=1)
    public var offset:Int?

    // @DataMember(Order=2)
    public var total:Int?

    // @DataMember(Order=3)
    public var results:[T] = []

    // @DataMember(Order=4)
    public var meta:[String:String] = [:]

    // @DataMember(Order=5)
    public var responseStatus:ResponseStatus?
}

public class GetTechnologyResponse
{
    required public init(){}
    public var created:Date?
    public var technology:Technology?
    public var technologyStacks:[TechnologyStack] = []
    public var responseStatus:ResponseStatus?
}

public class GetTechnologyFavoriteDetailsResponse
{
    required public init(){}
    public var users:[String] = []
    public var favoriteCount:Int?
}

public class GetTechnologyStackPreviousVersionsResponse
{
    required public init(){}
    public var results:[TechnologyStackHistory] = []
}

public class GetPageStatsResponse
{
    required public init(){}
    public var type:String?
    public var slug:String?
    public var viewCount:Int64?
    public var favCount:Int64?
}

public class OverviewResponse
{
    required public init(){}
    public var created:Date?
    public var topUsers:[UserInfo] = []
    public var topTechnologies:[TechnologyInfo] = []
    public var latestTechStacks:[TechStackDetails] = []
    public var popularTechStacks:[TechnologyStack] = []
    public var topTechnologiesByTier:[String:[TechnologyInfo]] = [:]
    public var responseStatus:ResponseStatus?
}

public class AppOverviewResponse
{
    required public init(){}
    public var created:Date?
    public var allTiers:[Option] = []
    public var topTechnologies:[TechnologyInfo] = []
    public var responseStatus:ResponseStatus?
}

public class GetAllTechnologyStacksResponse
{
    required public init(){}
    public var results:[TechnologyStack] = []
}

public class GetTechnologyStackResponse
{
    required public init(){}
    public var created:Date?
    public var result:TechStackDetails?
    public var responseStatus:ResponseStatus?
}

public class GetTechnologyStackFavoriteDetailsResponse
{
    required public init(){}
    public var users:[String] = []
    public var favoriteCount:Int?
}

public class GetConfigResponse
{
    required public init(){}
    public var allTiers:[Option] = []
}

public class GetFavoriteTechStackResponse
{
    required public init(){}
    public var results:[TechnologyStack] = []
}

public class FavoriteTechStackResponse
{
    required public init(){}
    public var result:TechnologyStack?
}

public class GetFavoriteTechnologiesResponse
{
    required public init(){}
    public var results:[Technology] = []
}

public class FavoriteTechnologyResponse
{
    required public init(){}
    public var result:Technology?
}

public class GetUserFeedResponse
{
    required public init(){}
    public var results:[TechStackDetails] = []
}

public class GetUserInfoResponse
{
    required public init(){}
    public var userName:String?
    public var created:Date?
    public var avatarUrl:String?
    public var techStacks:[TechnologyStack] = []
    public var favoriteTechStacks:[TechnologyStack] = []
    public var favoriteTechnologies:[Technology] = []
}

// @DataContract
public class AuthenticateResponse
{
    required public init(){}
    // @DataMember(Order=1)
    public var userId:String?

    // @DataMember(Order=2)
    public var sessionId:String?

    // @DataMember(Order=3)
    public var userName:String?

    // @DataMember(Order=4)
    public var displayName:String?

    // @DataMember(Order=5)
    public var referrerUrl:String?

    // @DataMember(Order=6)
    public var bearerToken:String?

    // @DataMember(Order=7)
    public var responseStatus:ResponseStatus?

    // @DataMember(Order=8)
    public var meta:[String:String] = [:]
}

// @DataContract
public class AssignRolesResponse
{
    required public init(){}
    // @DataMember(Order=1)
    public var allRoles:[String] = []

    // @DataMember(Order=2)
    public var allPermissions:[String] = []

    // @DataMember(Order=3)
    public var responseStatus:ResponseStatus?
}

// @DataContract
public class UnAssignRolesResponse
{
    required public init(){}
    // @DataMember(Order=1)
    public var allRoles:[String] = []

    // @DataMember(Order=2)
    public var allPermissions:[String] = []

    // @DataMember(Order=3)
    public var responseStatus:ResponseStatus?
}

// @DataContract
public class ConvertSessionToTokenResponse
{
    required public init(){}
    // @DataMember(Order=1)
    public var meta:[String:String] = [:]

    // @DataMember(Order=2)
    public var responseStatus:ResponseStatus?
}

public class Technology : TechnologyBase
{
    required public init(){}
}

public enum TechnologyTier : Int
{
    case ProgrammingLanguage
    case Client
    case Http
    case Server
    case Data
    case SoftwareInfrastructure
    case OperatingSystem
    case HardwareInfrastructure
    case ThirdPartyServices
}

public class TechStackDetails : TechnologyStackBase
{
    required public init(){}
    public var detailsHtml:String?
    public var technologyChoices:[TechnologyInStack] = []
}

public class TechnologyHistory : TechnologyBase
{
    required public init(){}
    public var technologyId:Int64?
    public var operation:String?
}

public class QueryDb<T : JsonSerializable> : QueryBase
{
    required public init(){}
}

public class TechnologyStack : TechnologyStackBase
{
    required public init(){}
}

public class TechnologyStackHistory : TechnologyStackBase
{
    required public init(){}
    public var technologyStackId:Int64?
    public var operation:String?
    public var technologyIds:[Int64] = []
}

public class UserInfo
{
    required public init(){}
    public var userName:String?
    public var avatarUrl:String?
    public var stacksCount:Int?
}

public class TechnologyInfo
{
    required public init(){}
    public var tier:TechnologyTier?
    public var slug:String?
    public var name:String?
    public var logoUrl:String?
    public var stacksCount:Int?
}

// @DataContract
public class Option
{
    required public init(){}
    // @DataMember(Name="name")
    public var name:String?

    // @DataMember(Name="title")
    public var title:String?

    // @DataMember(Name="value")
    public var value:TechnologyTier?
}

public class TechnologyBase
{
    required public init(){}
    public var id:Int64?
    public var name:String?
    public var vendorName:String?
    public var vendorUrl:String?
    public var productUrl:String?
    public var logoUrl:String?
    public var Description:String?
    public var created:Date?
    public var createdBy:String?
    public var lastModified:Date?
    public var lastModifiedBy:String?
    public var ownerId:String?
    public var slug:String?
    public var logoApproved:Bool?
    public var isLocked:Bool?
    public var tier:TechnologyTier?
    public var lastStatusUpdate:Date?
}

public class TechnologyInStack : TechnologyBase
{
    required public init(){}
    public var technologyId:Int64?
    public var technologyStackId:Int64?
    public var justification:String?
}

public class TechnologyStackBase
{
    required public init(){}
    public var id:Int64?
    public var name:String?
    public var vendorName:String?
    public var Description:String?
    public var appUrl:String?
    public var screenshotUrl:String?
    public var created:Date?
    public var createdBy:String?
    public var lastModified:Date?
    public var lastModifiedBy:String?
    public var isLocked:Bool?
    public var ownerId:String?
    public var slug:String?
    public var details:String?
    public var lastStatusUpdate:Date?
}

public class QueryBase
{
    required public init(){}
    // @DataMember(Order=1)
    public var skip:Int?

    // @DataMember(Order=2)
    public var take:Int?

    // @DataMember(Order=3)
    public var orderBy:String?

    // @DataMember(Order=4)
    public var orderByDesc:String?

    // @DataMember(Order=5)
    public var include:String?

    // @DataMember(Order=6)
    public var fields:String?

    // @DataMember(Order=7)
    public var meta:[String:String] = [:]
}


extension LogoUrlApproval : JsonSerializable
{
    public static var typeName:String { return "LogoUrlApproval" }
    public static var metadata = Metadata.create([
            Type<LogoUrlApproval>.optionalProperty("technologyId", get: { $0.technologyId }, set: { $0.technologyId = $1 }),
            Type<LogoUrlApproval>.optionalProperty("approved", get: { $0.approved }, set: { $0.approved = $1 }),
        ])
}

extension LockTechStack : JsonSerializable
{
    public static var typeName:String { return "LockTechStack" }
    public static var metadata = Metadata.create([
            Type<LockTechStack>.optionalProperty("technologyStackId", get: { $0.technologyStackId }, set: { $0.technologyStackId = $1 }),
            Type<LockTechStack>.optionalProperty("isLocked", get: { $0.isLocked }, set: { $0.isLocked = $1 }),
        ])
}

extension LockTech : JsonSerializable
{
    public static var typeName:String { return "LockTech" }
    public static var metadata = Metadata.create([
            Type<LockTech>.optionalProperty("technologyId", get: { $0.technologyId }, set: { $0.technologyId = $1 }),
            Type<LockTech>.optionalProperty("isLocked", get: { $0.isLocked }, set: { $0.isLocked = $1 }),
        ])
}

extension Ping : JsonSerializable
{
    public static var typeName:String { return "Ping" }
    public static var metadata = Metadata.create([
        ])
}

extension FallbackForClientRoutes : JsonSerializable
{
    public static var typeName:String { return "FallbackForClientRoutes" }
    public static var metadata = Metadata.create([
            Type<FallbackForClientRoutes>.optionalProperty("pathInfo", get: { $0.pathInfo }, set: { $0.pathInfo = $1 }),
        ])
}

extension ClientAllTechnologyStacks : JsonSerializable
{
    public static var typeName:String { return "ClientAllTechnologyStacks" }
    public static var metadata = Metadata.create([
        ])
}

extension ClientAllTechnologies : JsonSerializable
{
    public static var typeName:String { return "ClientAllTechnologies" }
    public static var metadata = Metadata.create([
        ])
}

extension ClientTechnology : JsonSerializable
{
    public static var typeName:String { return "ClientTechnology" }
    public static var metadata = Metadata.create([
            Type<ClientTechnology>.optionalProperty("slug", get: { $0.slug }, set: { $0.slug = $1 }),
        ])
}

extension ClientUser : JsonSerializable
{
    public static var typeName:String { return "ClientUser" }
    public static var metadata = Metadata.create([
            Type<ClientUser>.optionalProperty("userName", get: { $0.userName }, set: { $0.userName = $1 }),
        ])
}

extension CreateTechnology : JsonSerializable
{
    public static var typeName:String { return "CreateTechnology" }
    public static var metadata = Metadata.create([
            Type<CreateTechnology>.optionalProperty("name", get: { $0.name }, set: { $0.name = $1 }),
            Type<CreateTechnology>.optionalProperty("vendorName", get: { $0.vendorName }, set: { $0.vendorName = $1 }),
            Type<CreateTechnology>.optionalProperty("vendorUrl", get: { $0.vendorUrl }, set: { $0.vendorUrl = $1 }),
            Type<CreateTechnology>.optionalProperty("productUrl", get: { $0.productUrl }, set: { $0.productUrl = $1 }),
            Type<CreateTechnology>.optionalProperty("logoUrl", get: { $0.logoUrl }, set: { $0.logoUrl = $1 }),
            Type<CreateTechnology>.optionalProperty("Description", get: { $0.Description }, set: { $0.Description = $1 }),
            Type<CreateTechnology>.optionalProperty("isLocked", get: { $0.isLocked }, set: { $0.isLocked = $1 }),
            Type<CreateTechnology>.optionalProperty("tier", get: { $0.tier }, set: { $0.tier = $1 }),
        ])
}

extension UpdateTechnology : JsonSerializable
{
    public static var typeName:String { return "UpdateTechnology" }
    public static var metadata = Metadata.create([
            Type<UpdateTechnology>.optionalProperty("id", get: { $0.id }, set: { $0.id = $1 }),
            Type<UpdateTechnology>.optionalProperty("name", get: { $0.name }, set: { $0.name = $1 }),
            Type<UpdateTechnology>.optionalProperty("vendorName", get: { $0.vendorName }, set: { $0.vendorName = $1 }),
            Type<UpdateTechnology>.optionalProperty("vendorUrl", get: { $0.vendorUrl }, set: { $0.vendorUrl = $1 }),
            Type<UpdateTechnology>.optionalProperty("productUrl", get: { $0.productUrl }, set: { $0.productUrl = $1 }),
            Type<UpdateTechnology>.optionalProperty("logoUrl", get: { $0.logoUrl }, set: { $0.logoUrl = $1 }),
            Type<UpdateTechnology>.optionalProperty("Description", get: { $0.Description }, set: { $0.Description = $1 }),
            Type<UpdateTechnology>.optionalProperty("isLocked", get: { $0.isLocked }, set: { $0.isLocked = $1 }),
            Type<UpdateTechnology>.optionalProperty("tier", get: { $0.tier }, set: { $0.tier = $1 }),
        ])
}

extension DeleteTechnology : JsonSerializable
{
    public static var typeName:String { return "DeleteTechnology" }
    public static var metadata = Metadata.create([
            Type<DeleteTechnology>.optionalProperty("id", get: { $0.id }, set: { $0.id = $1 }),
        ])
}

extension CreateTechnologyStack : JsonSerializable
{
    public static var typeName:String { return "CreateTechnologyStack" }
    public static var metadata = Metadata.create([
            Type<CreateTechnologyStack>.optionalProperty("name", get: { $0.name }, set: { $0.name = $1 }),
            Type<CreateTechnologyStack>.optionalProperty("vendorName", get: { $0.vendorName }, set: { $0.vendorName = $1 }),
            Type<CreateTechnologyStack>.optionalProperty("appUrl", get: { $0.appUrl }, set: { $0.appUrl = $1 }),
            Type<CreateTechnologyStack>.optionalProperty("screenshotUrl", get: { $0.screenshotUrl }, set: { $0.screenshotUrl = $1 }),
            Type<CreateTechnologyStack>.optionalProperty("Description", get: { $0.Description }, set: { $0.Description = $1 }),
            Type<CreateTechnologyStack>.optionalProperty("details", get: { $0.details }, set: { $0.details = $1 }),
            Type<CreateTechnologyStack>.optionalProperty("isLocked", get: { $0.isLocked }, set: { $0.isLocked = $1 }),
            Type<CreateTechnologyStack>.arrayProperty("technologyIds", get: { $0.technologyIds }, set: { $0.technologyIds = $1 }),
        ])
}

extension UpdateTechnologyStack : JsonSerializable
{
    public static var typeName:String { return "UpdateTechnologyStack" }
    public static var metadata = Metadata.create([
            Type<UpdateTechnologyStack>.optionalProperty("id", get: { $0.id }, set: { $0.id = $1 }),
            Type<UpdateTechnologyStack>.optionalProperty("name", get: { $0.name }, set: { $0.name = $1 }),
            Type<UpdateTechnologyStack>.optionalProperty("vendorName", get: { $0.vendorName }, set: { $0.vendorName = $1 }),
            Type<UpdateTechnologyStack>.optionalProperty("appUrl", get: { $0.appUrl }, set: { $0.appUrl = $1 }),
            Type<UpdateTechnologyStack>.optionalProperty("screenshotUrl", get: { $0.screenshotUrl }, set: { $0.screenshotUrl = $1 }),
            Type<UpdateTechnologyStack>.optionalProperty("Description", get: { $0.Description }, set: { $0.Description = $1 }),
            Type<UpdateTechnologyStack>.optionalProperty("details", get: { $0.details }, set: { $0.details = $1 }),
            Type<UpdateTechnologyStack>.optionalProperty("isLocked", get: { $0.isLocked }, set: { $0.isLocked = $1 }),
            Type<UpdateTechnologyStack>.arrayProperty("technologyIds", get: { $0.technologyIds }, set: { $0.technologyIds = $1 }),
        ])
}

extension DeleteTechnologyStack : JsonSerializable
{
    public static var typeName:String { return "DeleteTechnologyStack" }
    public static var metadata = Metadata.create([
            Type<DeleteTechnologyStack>.optionalProperty("id", get: { $0.id }, set: { $0.id = $1 }),
        ])
}

extension SessionInfo : JsonSerializable
{
    public static var typeName:String { return "SessionInfo" }
    public static var metadata = Metadata.create([
        ])
}

extension GetTechnologyPreviousVersions : JsonSerializable
{
    public static var typeName:String { return "GetTechnologyPreviousVersions" }
    public static var metadata = Metadata.create([
            Type<GetTechnologyPreviousVersions>.optionalProperty("slug", get: { $0.slug }, set: { $0.slug = $1 }),
        ])
}

extension GetAllTechnologies : JsonSerializable
{
    public static var typeName:String { return "GetAllTechnologies" }
    public static var metadata = Metadata.create([
        ])
}

extension FindTechnologies : JsonSerializable
{
    public static var typeName:String { return "FindTechnologies" }
    public static var metadata:Metadata {
        return Metadata.create([
            Type<FindTechnologies>.optionalProperty("name", get: { $0.name }, set: { $0.name = $1 }),
            Type<FindTechnologies>.optionalProperty("nameContains", get: { $0.nameContains }, set: { $0.nameContains = $1 }),
            Type<FindTechnologies>.optionalProperty("skip", get: { $0.skip }, set: { $0.skip = $1 }),
            Type<FindTechnologies>.optionalProperty("take", get: { $0.take }, set: { $0.take = $1 }),
            Type<FindTechnologies>.optionalProperty("orderBy", get: { $0.orderBy }, set: { $0.orderBy = $1 }),
            Type<FindTechnologies>.optionalProperty("orderByDesc", get: { $0.orderByDesc }, set: { $0.orderByDesc = $1 }),
            Type<FindTechnologies>.optionalProperty("include", get: { $0.include }, set: { $0.include = $1 }),
            Type<FindTechnologies>.optionalProperty("fields", get: { $0.fields }, set: { $0.fields = $1 }),
            Type<FindTechnologies>.objectProperty("meta", get: { $0.meta }, set: { $0.meta = $1 }),
        ])
    }
}

extension GetTechnology : JsonSerializable
{
    public static var typeName:String { return "GetTechnology" }
    public static var metadata = Metadata.create([
            Type<GetTechnology>.optionalProperty("slug", get: { $0.slug }, set: { $0.slug = $1 }),
        ])
}

extension GetTechnologyFavoriteDetails : JsonSerializable
{
    public static var typeName:String { return "GetTechnologyFavoriteDetails" }
    public static var metadata = Metadata.create([
            Type<GetTechnologyFavoriteDetails>.optionalProperty("slug", get: { $0.slug }, set: { $0.slug = $1 }),
        ])
}

extension GetTechnologyStackPreviousVersions : JsonSerializable
{
    public static var typeName:String { return "GetTechnologyStackPreviousVersions" }
    public static var metadata = Metadata.create([
            Type<GetTechnologyStackPreviousVersions>.optionalProperty("slug", get: { $0.slug }, set: { $0.slug = $1 }),
        ])
}

extension GetPageStats : JsonSerializable
{
    public static var typeName:String { return "GetPageStats" }
    public static var metadata = Metadata.create([
            Type<GetPageStats>.optionalProperty("type", get: { $0.type }, set: { $0.type = $1 }),
            Type<GetPageStats>.optionalProperty("slug", get: { $0.slug }, set: { $0.slug = $1 }),
        ])
}

extension FindTechStacks : JsonSerializable
{
    public static var typeName:String { return "FindTechStacks" }
    public static var metadata:Metadata {
        return Metadata.create([
            Type<FindTechStacks>.optionalProperty("nameContains", get: { $0.nameContains }, set: { $0.nameContains = $1 }),
            Type<FindTechStacks>.optionalProperty("skip", get: { $0.skip }, set: { $0.skip = $1 }),
            Type<FindTechStacks>.optionalProperty("take", get: { $0.take }, set: { $0.take = $1 }),
            Type<FindTechStacks>.optionalProperty("orderBy", get: { $0.orderBy }, set: { $0.orderBy = $1 }),
            Type<FindTechStacks>.optionalProperty("orderByDesc", get: { $0.orderByDesc }, set: { $0.orderByDesc = $1 }),
            Type<FindTechStacks>.optionalProperty("include", get: { $0.include }, set: { $0.include = $1 }),
            Type<FindTechStacks>.optionalProperty("fields", get: { $0.fields }, set: { $0.fields = $1 }),
            Type<FindTechStacks>.objectProperty("meta", get: { $0.meta }, set: { $0.meta = $1 }),
        ])
    }
}

extension Overview : JsonSerializable
{
    public static var typeName:String { return "Overview" }
    public static var metadata = Metadata.create([
            Type<Overview>.optionalProperty("reload", get: { $0.reload }, set: { $0.reload = $1 }),
        ])
}

extension AppOverview : JsonSerializable
{
    public static var typeName:String { return "AppOverview" }
    public static var metadata = Metadata.create([
            Type<AppOverview>.optionalProperty("reload", get: { $0.reload }, set: { $0.reload = $1 }),
        ])
}

extension GetAllTechnologyStacks : JsonSerializable
{
    public static var typeName:String { return "GetAllTechnologyStacks" }
    public static var metadata = Metadata.create([
        ])
}

extension GetTechnologyStack : JsonSerializable
{
    public static var typeName:String { return "GetTechnologyStack" }
    public static var metadata = Metadata.create([
            Type<GetTechnologyStack>.optionalProperty("slug", get: { $0.slug }, set: { $0.slug = $1 }),
        ])
}

extension GetTechnologyStackFavoriteDetails : JsonSerializable
{
    public static var typeName:String { return "GetTechnologyStackFavoriteDetails" }
    public static var metadata = Metadata.create([
            Type<GetTechnologyStackFavoriteDetails>.optionalProperty("slug", get: { $0.slug }, set: { $0.slug = $1 }),
        ])
}

extension GetConfig : JsonSerializable
{
    public static var typeName:String { return "GetConfig" }
    public static var metadata = Metadata.create([
        ])
}

extension GetFavoriteTechStack : JsonSerializable
{
    public static var typeName:String { return "GetFavoriteTechStack" }
    public static var metadata = Metadata.create([
            Type<GetFavoriteTechStack>.optionalProperty("technologyStackId", get: { $0.technologyStackId }, set: { $0.technologyStackId = $1 }),
        ])
}

extension AddFavoriteTechStack : JsonSerializable
{
    public static var typeName:String { return "AddFavoriteTechStack" }
    public static var metadata = Metadata.create([
            Type<AddFavoriteTechStack>.optionalProperty("technologyStackId", get: { $0.technologyStackId }, set: { $0.technologyStackId = $1 }),
        ])
}

extension RemoveFavoriteTechStack : JsonSerializable
{
    public static var typeName:String { return "RemoveFavoriteTechStack" }
    public static var metadata = Metadata.create([
            Type<RemoveFavoriteTechStack>.optionalProperty("technologyStackId", get: { $0.technologyStackId }, set: { $0.technologyStackId = $1 }),
        ])
}

extension GetFavoriteTechnologies : JsonSerializable
{
    public static var typeName:String { return "GetFavoriteTechnologies" }
    public static var metadata = Metadata.create([
            Type<GetFavoriteTechnologies>.optionalProperty("technologyId", get: { $0.technologyId }, set: { $0.technologyId = $1 }),
        ])
}

extension AddFavoriteTechnology : JsonSerializable
{
    public static var typeName:String { return "AddFavoriteTechnology" }
    public static var metadata = Metadata.create([
            Type<AddFavoriteTechnology>.optionalProperty("technologyId", get: { $0.technologyId }, set: { $0.technologyId = $1 }),
        ])
}

extension RemoveFavoriteTechnology : JsonSerializable
{
    public static var typeName:String { return "RemoveFavoriteTechnology" }
    public static var metadata = Metadata.create([
            Type<RemoveFavoriteTechnology>.optionalProperty("technologyId", get: { $0.technologyId }, set: { $0.technologyId = $1 }),
        ])
}

extension GetUserFeed : JsonSerializable
{
    public static var typeName:String { return "GetUserFeed" }
    public static var metadata = Metadata.create([
        ])
}

extension GetUserInfo : JsonSerializable
{
    public static var typeName:String { return "GetUserInfo" }
    public static var metadata = Metadata.create([
            Type<GetUserInfo>.optionalProperty("userName", get: { $0.userName }, set: { $0.userName = $1 }),
        ])
}

extension Authenticate : JsonSerializable
{
    public static var typeName:String { return "Authenticate" }
    public static var metadata = Metadata.create([
            Type<Authenticate>.optionalProperty("provider", get: { $0.provider }, set: { $0.provider = $1 }),
            Type<Authenticate>.optionalProperty("state", get: { $0.state }, set: { $0.state = $1 }),
            Type<Authenticate>.optionalProperty("oauth_token", get: { $0.oauth_token }, set: { $0.oauth_token = $1 }),
            Type<Authenticate>.optionalProperty("oauth_verifier", get: { $0.oauth_verifier }, set: { $0.oauth_verifier = $1 }),
            Type<Authenticate>.optionalProperty("userName", get: { $0.userName }, set: { $0.userName = $1 }),
            Type<Authenticate>.optionalProperty("password", get: { $0.password }, set: { $0.password = $1 }),
            Type<Authenticate>.optionalProperty("rememberMe", get: { $0.rememberMe }, set: { $0.rememberMe = $1 }),
            Type<Authenticate>.optionalProperty("continue", get: { $0.`continue` }, set: { $0.`continue` = $1 }),
            Type<Authenticate>.optionalProperty("nonce", get: { $0.nonce }, set: { $0.nonce = $1 }),
            Type<Authenticate>.optionalProperty("uri", get: { $0.uri }, set: { $0.uri = $1 }),
            Type<Authenticate>.optionalProperty("response", get: { $0.response }, set: { $0.response = $1 }),
            Type<Authenticate>.optionalProperty("qop", get: { $0.qop }, set: { $0.qop = $1 }),
            Type<Authenticate>.optionalProperty("nc", get: { $0.nc }, set: { $0.nc = $1 }),
            Type<Authenticate>.optionalProperty("cnonce", get: { $0.cnonce }, set: { $0.cnonce = $1 }),
            Type<Authenticate>.optionalProperty("useTokenCookie", get: { $0.useTokenCookie }, set: { $0.useTokenCookie = $1 }),
            Type<Authenticate>.objectProperty("meta", get: { $0.meta }, set: { $0.meta = $1 }),
        ])
}

extension AssignRoles : JsonSerializable
{
    public static var typeName:String { return "AssignRoles" }
    public static var metadata = Metadata.create([
            Type<AssignRoles>.optionalProperty("userName", get: { $0.userName }, set: { $0.userName = $1 }),
            Type<AssignRoles>.arrayProperty("permissions", get: { $0.permissions }, set: { $0.permissions = $1 }),
            Type<AssignRoles>.arrayProperty("roles", get: { $0.roles }, set: { $0.roles = $1 }),
        ])
}

extension UnAssignRoles : JsonSerializable
{
    public static var typeName:String { return "UnAssignRoles" }
    public static var metadata = Metadata.create([
            Type<UnAssignRoles>.optionalProperty("userName", get: { $0.userName }, set: { $0.userName = $1 }),
            Type<UnAssignRoles>.arrayProperty("permissions", get: { $0.permissions }, set: { $0.permissions = $1 }),
            Type<UnAssignRoles>.arrayProperty("roles", get: { $0.roles }, set: { $0.roles = $1 }),
        ])
}

extension ConvertSessionToToken : JsonSerializable
{
    public static var typeName:String { return "ConvertSessionToToken" }
    public static var metadata = Metadata.create([
            Type<ConvertSessionToToken>.optionalProperty("preserveSession", get: { $0.preserveSession }, set: { $0.preserveSession = $1 }),
        ])
}

extension FindTechnologiesAdmin : JsonSerializable
{
    public static var typeName:String { return "FindTechnologiesAdmin" }
    public static var metadata:Metadata {
        return Metadata.create([
            Type<FindTechnologiesAdmin>.optionalProperty("name", get: { $0.name }, set: { $0.name = $1 }),
            Type<FindTechnologiesAdmin>.optionalProperty("skip", get: { $0.skip }, set: { $0.skip = $1 }),
            Type<FindTechnologiesAdmin>.optionalProperty("take", get: { $0.take }, set: { $0.take = $1 }),
            Type<FindTechnologiesAdmin>.optionalProperty("orderBy", get: { $0.orderBy }, set: { $0.orderBy = $1 }),
            Type<FindTechnologiesAdmin>.optionalProperty("orderByDesc", get: { $0.orderByDesc }, set: { $0.orderByDesc = $1 }),
            Type<FindTechnologiesAdmin>.optionalProperty("include", get: { $0.include }, set: { $0.include = $1 }),
            Type<FindTechnologiesAdmin>.optionalProperty("fields", get: { $0.fields }, set: { $0.fields = $1 }),
            Type<FindTechnologiesAdmin>.objectProperty("meta", get: { $0.meta }, set: { $0.meta = $1 }),
        ])
    }
}

extension LogoUrlApprovalResponse : JsonSerializable
{
    public static var typeName:String { return "LogoUrlApprovalResponse" }
    public static var metadata = Metadata.create([
            Type<LogoUrlApprovalResponse>.optionalObjectProperty("result", get: { $0.result }, set: { $0.result = $1 }),
        ])
}

extension LockStackResponse : JsonSerializable
{
    public static var typeName:String { return "LockStackResponse" }
    public static var metadata = Metadata.create([
        ])
}

extension CreateTechnologyResponse : JsonSerializable
{
    public static var typeName:String { return "CreateTechnologyResponse" }
    public static var metadata = Metadata.create([
            Type<CreateTechnologyResponse>.optionalObjectProperty("result", get: { $0.result }, set: { $0.result = $1 }),
            Type<CreateTechnologyResponse>.optionalProperty("responseStatus", get: { $0.responseStatus }, set: { $0.responseStatus = $1 }),
        ])
}

extension UpdateTechnologyResponse : JsonSerializable
{
    public static var typeName:String { return "UpdateTechnologyResponse" }
    public static var metadata = Metadata.create([
            Type<UpdateTechnologyResponse>.optionalObjectProperty("result", get: { $0.result }, set: { $0.result = $1 }),
            Type<UpdateTechnologyResponse>.optionalProperty("responseStatus", get: { $0.responseStatus }, set: { $0.responseStatus = $1 }),
        ])
}

extension DeleteTechnologyResponse : JsonSerializable
{
    public static var typeName:String { return "DeleteTechnologyResponse" }
    public static var metadata = Metadata.create([
            Type<DeleteTechnologyResponse>.optionalObjectProperty("result", get: { $0.result }, set: { $0.result = $1 }),
            Type<DeleteTechnologyResponse>.optionalProperty("responseStatus", get: { $0.responseStatus }, set: { $0.responseStatus = $1 }),
        ])
}

extension CreateTechnologyStackResponse : JsonSerializable
{
    public static var typeName:String { return "CreateTechnologyStackResponse" }
    public static var metadata = Metadata.create([
            Type<CreateTechnologyStackResponse>.optionalObjectProperty("result", get: { $0.result }, set: { $0.result = $1 }),
            Type<CreateTechnologyStackResponse>.optionalProperty("responseStatus", get: { $0.responseStatus }, set: { $0.responseStatus = $1 }),
        ])
}

extension UpdateTechnologyStackResponse : JsonSerializable
{
    public static var typeName:String { return "UpdateTechnologyStackResponse" }
    public static var metadata = Metadata.create([
            Type<UpdateTechnologyStackResponse>.optionalObjectProperty("result", get: { $0.result }, set: { $0.result = $1 }),
            Type<UpdateTechnologyStackResponse>.optionalProperty("responseStatus", get: { $0.responseStatus }, set: { $0.responseStatus = $1 }),
        ])
}

extension DeleteTechnologyStackResponse : JsonSerializable
{
    public static var typeName:String { return "DeleteTechnologyStackResponse" }
    public static var metadata = Metadata.create([
            Type<DeleteTechnologyStackResponse>.optionalObjectProperty("result", get: { $0.result }, set: { $0.result = $1 }),
            Type<DeleteTechnologyStackResponse>.optionalProperty("responseStatus", get: { $0.responseStatus }, set: { $0.responseStatus = $1 }),
        ])
}

extension GetTechnologyPreviousVersionsResponse : JsonSerializable
{
    public static var typeName:String { return "GetTechnologyPreviousVersionsResponse" }
    public static var metadata = Metadata.create([
            Type<GetTechnologyPreviousVersionsResponse>.arrayProperty("results", get: { $0.results }, set: { $0.results = $1 }),
        ])
}

extension GetAllTechnologiesResponse : JsonSerializable
{
    public static var typeName:String { return "GetAllTechnologiesResponse" }
    public static var metadata = Metadata.create([
            Type<GetAllTechnologiesResponse>.arrayProperty("results", get: { $0.results }, set: { $0.results = $1 }),
        ])
}

extension QueryResponse : JsonSerializable
{
    public static var typeName:String { return "QueryResponse" }
    public static var metadata:Metadata {
        return Metadata.create([
            Type<QueryResponse>.optionalProperty("offset", get: { $0.offset }, set: { $0.offset = $1 }),
            Type<QueryResponse>.optionalProperty("total", get: { $0.total }, set: { $0.total = $1 }),
            Type<QueryResponse>.arrayProperty("results", get: { $0.results }, set: { $0.results = $1 }),
            Type<QueryResponse>.objectProperty("meta", get: { $0.meta }, set: { $0.meta = $1 }),
            Type<QueryResponse>.optionalProperty("responseStatus", get: { $0.responseStatus }, set: { $0.responseStatus = $1 }),
        ])
    }
}

extension GetTechnologyResponse : JsonSerializable
{
    public static var typeName:String { return "GetTechnologyResponse" }
    public static var metadata = Metadata.create([
            Type<GetTechnologyResponse>.optionalProperty("created", get: { $0.created }, set: { $0.created = $1 }),
            Type<GetTechnologyResponse>.optionalObjectProperty("technology", get: { $0.technology }, set: { $0.technology = $1 }),
            Type<GetTechnologyResponse>.arrayProperty("technologyStacks", get: { $0.technologyStacks }, set: { $0.technologyStacks = $1 }),
            Type<GetTechnologyResponse>.optionalProperty("responseStatus", get: { $0.responseStatus }, set: { $0.responseStatus = $1 }),
        ])
}

extension GetTechnologyFavoriteDetailsResponse : JsonSerializable
{
    public static var typeName:String { return "GetTechnologyFavoriteDetailsResponse" }
    public static var metadata = Metadata.create([
            Type<GetTechnologyFavoriteDetailsResponse>.arrayProperty("users", get: { $0.users }, set: { $0.users = $1 }),
            Type<GetTechnologyFavoriteDetailsResponse>.optionalProperty("favoriteCount", get: { $0.favoriteCount }, set: { $0.favoriteCount = $1 }),
        ])
}

extension GetTechnologyStackPreviousVersionsResponse : JsonSerializable
{
    public static var typeName:String { return "GetTechnologyStackPreviousVersionsResponse" }
    public static var metadata = Metadata.create([
            Type<GetTechnologyStackPreviousVersionsResponse>.arrayProperty("results", get: { $0.results }, set: { $0.results = $1 }),
        ])
}

extension GetPageStatsResponse : JsonSerializable
{
    public static var typeName:String { return "GetPageStatsResponse" }
    public static var metadata = Metadata.create([
            Type<GetPageStatsResponse>.optionalProperty("type", get: { $0.type }, set: { $0.type = $1 }),
            Type<GetPageStatsResponse>.optionalProperty("slug", get: { $0.slug }, set: { $0.slug = $1 }),
            Type<GetPageStatsResponse>.optionalProperty("viewCount", get: { $0.viewCount }, set: { $0.viewCount = $1 }),
            Type<GetPageStatsResponse>.optionalProperty("favCount", get: { $0.favCount }, set: { $0.favCount = $1 }),
        ])
}

extension OverviewResponse : JsonSerializable
{
    public static var typeName:String { return "OverviewResponse" }
    public static var metadata = Metadata.create([
            Type<OverviewResponse>.optionalProperty("created", get: { $0.created }, set: { $0.created = $1 }),
            Type<OverviewResponse>.arrayProperty("topUsers", get: { $0.topUsers }, set: { $0.topUsers = $1 }),
            Type<OverviewResponse>.arrayProperty("topTechnologies", get: { $0.topTechnologies }, set: { $0.topTechnologies = $1 }),
            Type<OverviewResponse>.arrayProperty("latestTechStacks", get: { $0.latestTechStacks }, set: { $0.latestTechStacks = $1 }),
            Type<OverviewResponse>.arrayProperty("popularTechStacks", get: { $0.popularTechStacks }, set: { $0.popularTechStacks = $1 }),
            Type<OverviewResponse>.objectProperty("topTechnologiesByTier", get: { $0.topTechnologiesByTier }, set: { $0.topTechnologiesByTier = $1 }),
            Type<OverviewResponse>.optionalProperty("responseStatus", get: { $0.responseStatus }, set: { $0.responseStatus = $1 }),
        ])
}

extension AppOverviewResponse : JsonSerializable
{
    public static var typeName:String { return "AppOverviewResponse" }
    public static var metadata = Metadata.create([
            Type<AppOverviewResponse>.optionalProperty("created", get: { $0.created }, set: { $0.created = $1 }),
            Type<AppOverviewResponse>.arrayProperty("allTiers", get: { $0.allTiers }, set: { $0.allTiers = $1 }),
            Type<AppOverviewResponse>.arrayProperty("topTechnologies", get: { $0.topTechnologies }, set: { $0.topTechnologies = $1 }),
            Type<AppOverviewResponse>.optionalProperty("responseStatus", get: { $0.responseStatus }, set: { $0.responseStatus = $1 }),
        ])
}

extension GetAllTechnologyStacksResponse : JsonSerializable
{
    public static var typeName:String { return "GetAllTechnologyStacksResponse" }
    public static var metadata = Metadata.create([
            Type<GetAllTechnologyStacksResponse>.arrayProperty("results", get: { $0.results }, set: { $0.results = $1 }),
        ])
}

extension GetTechnologyStackResponse : JsonSerializable
{
    public static var typeName:String { return "GetTechnologyStackResponse" }
    public static var metadata = Metadata.create([
            Type<GetTechnologyStackResponse>.optionalProperty("created", get: { $0.created }, set: { $0.created = $1 }),
            Type<GetTechnologyStackResponse>.optionalObjectProperty("result", get: { $0.result }, set: { $0.result = $1 }),
            Type<GetTechnologyStackResponse>.optionalProperty("responseStatus", get: { $0.responseStatus }, set: { $0.responseStatus = $1 }),
        ])
}

extension GetTechnologyStackFavoriteDetailsResponse : JsonSerializable
{
    public static var typeName:String { return "GetTechnologyStackFavoriteDetailsResponse" }
    public static var metadata = Metadata.create([
            Type<GetTechnologyStackFavoriteDetailsResponse>.arrayProperty("users", get: { $0.users }, set: { $0.users = $1 }),
            Type<GetTechnologyStackFavoriteDetailsResponse>.optionalProperty("favoriteCount", get: { $0.favoriteCount }, set: { $0.favoriteCount = $1 }),
        ])
}

extension GetConfigResponse : JsonSerializable
{
    public static var typeName:String { return "GetConfigResponse" }
    public static var metadata = Metadata.create([
            Type<GetConfigResponse>.arrayProperty("allTiers", get: { $0.allTiers }, set: { $0.allTiers = $1 }),
        ])
}

extension GetFavoriteTechStackResponse : JsonSerializable
{
    public static var typeName:String { return "GetFavoriteTechStackResponse" }
    public static var metadata = Metadata.create([
            Type<GetFavoriteTechStackResponse>.arrayProperty("results", get: { $0.results }, set: { $0.results = $1 }),
        ])
}

extension FavoriteTechStackResponse : JsonSerializable
{
    public static var typeName:String { return "FavoriteTechStackResponse" }
    public static var metadata = Metadata.create([
            Type<FavoriteTechStackResponse>.optionalObjectProperty("result", get: { $0.result }, set: { $0.result = $1 }),
        ])
}

extension GetFavoriteTechnologiesResponse : JsonSerializable
{
    public static var typeName:String { return "GetFavoriteTechnologiesResponse" }
    public static var metadata = Metadata.create([
            Type<GetFavoriteTechnologiesResponse>.arrayProperty("results", get: { $0.results }, set: { $0.results = $1 }),
        ])
}

extension FavoriteTechnologyResponse : JsonSerializable
{
    public static var typeName:String { return "FavoriteTechnologyResponse" }
    public static var metadata = Metadata.create([
            Type<FavoriteTechnologyResponse>.optionalObjectProperty("result", get: { $0.result }, set: { $0.result = $1 }),
        ])
}

extension GetUserFeedResponse : JsonSerializable
{
    public static var typeName:String { return "GetUserFeedResponse" }
    public static var metadata = Metadata.create([
            Type<GetUserFeedResponse>.arrayProperty("results", get: { $0.results }, set: { $0.results = $1 }),
        ])
}

extension GetUserInfoResponse : JsonSerializable
{
    public static var typeName:String { return "GetUserInfoResponse" }
    public static var metadata = Metadata.create([
            Type<GetUserInfoResponse>.optionalProperty("userName", get: { $0.userName }, set: { $0.userName = $1 }),
            Type<GetUserInfoResponse>.optionalProperty("created", get: { $0.created }, set: { $0.created = $1 }),
            Type<GetUserInfoResponse>.optionalProperty("avatarUrl", get: { $0.avatarUrl }, set: { $0.avatarUrl = $1 }),
            Type<GetUserInfoResponse>.arrayProperty("techStacks", get: { $0.techStacks }, set: { $0.techStacks = $1 }),
            Type<GetUserInfoResponse>.arrayProperty("favoriteTechStacks", get: { $0.favoriteTechStacks }, set: { $0.favoriteTechStacks = $1 }),
            Type<GetUserInfoResponse>.arrayProperty("favoriteTechnologies", get: { $0.favoriteTechnologies }, set: { $0.favoriteTechnologies = $1 }),
        ])
}

extension AuthenticateResponse : JsonSerializable
{
    public static var typeName:String { return "AuthenticateResponse" }
    public static var metadata = Metadata.create([
            Type<AuthenticateResponse>.optionalProperty("userId", get: { $0.userId }, set: { $0.userId = $1 }),
            Type<AuthenticateResponse>.optionalProperty("sessionId", get: { $0.sessionId }, set: { $0.sessionId = $1 }),
            Type<AuthenticateResponse>.optionalProperty("userName", get: { $0.userName }, set: { $0.userName = $1 }),
            Type<AuthenticateResponse>.optionalProperty("displayName", get: { $0.displayName }, set: { $0.displayName = $1 }),
            Type<AuthenticateResponse>.optionalProperty("referrerUrl", get: { $0.referrerUrl }, set: { $0.referrerUrl = $1 }),
            Type<AuthenticateResponse>.optionalProperty("bearerToken", get: { $0.bearerToken }, set: { $0.bearerToken = $1 }),
            Type<AuthenticateResponse>.optionalProperty("responseStatus", get: { $0.responseStatus }, set: { $0.responseStatus = $1 }),
            Type<AuthenticateResponse>.objectProperty("meta", get: { $0.meta }, set: { $0.meta = $1 }),
        ])
}

extension AssignRolesResponse : JsonSerializable
{
    public static var typeName:String { return "AssignRolesResponse" }
    public static var metadata = Metadata.create([
            Type<AssignRolesResponse>.arrayProperty("allRoles", get: { $0.allRoles }, set: { $0.allRoles = $1 }),
            Type<AssignRolesResponse>.arrayProperty("allPermissions", get: { $0.allPermissions }, set: { $0.allPermissions = $1 }),
            Type<AssignRolesResponse>.optionalProperty("responseStatus", get: { $0.responseStatus }, set: { $0.responseStatus = $1 }),
        ])
}

extension UnAssignRolesResponse : JsonSerializable
{
    public static var typeName:String { return "UnAssignRolesResponse" }
    public static var metadata = Metadata.create([
            Type<UnAssignRolesResponse>.arrayProperty("allRoles", get: { $0.allRoles }, set: { $0.allRoles = $1 }),
            Type<UnAssignRolesResponse>.arrayProperty("allPermissions", get: { $0.allPermissions }, set: { $0.allPermissions = $1 }),
            Type<UnAssignRolesResponse>.optionalProperty("responseStatus", get: { $0.responseStatus }, set: { $0.responseStatus = $1 }),
        ])
}

extension ConvertSessionToTokenResponse : JsonSerializable
{
    public static var typeName:String { return "ConvertSessionToTokenResponse" }
    public static var metadata = Metadata.create([
            Type<ConvertSessionToTokenResponse>.objectProperty("meta", get: { $0.meta }, set: { $0.meta = $1 }),
            Type<ConvertSessionToTokenResponse>.optionalProperty("responseStatus", get: { $0.responseStatus }, set: { $0.responseStatus = $1 }),
        ])
}

extension Technology : JsonSerializable
{
    public static var typeName:String { return "Technology" }
    public static var metadata = Metadata.create([
            Type<Technology>.optionalProperty("id", get: { $0.id }, set: { $0.id = $1 }),
            Type<Technology>.optionalProperty("name", get: { $0.name }, set: { $0.name = $1 }),
            Type<Technology>.optionalProperty("vendorName", get: { $0.vendorName }, set: { $0.vendorName = $1 }),
            Type<Technology>.optionalProperty("vendorUrl", get: { $0.vendorUrl }, set: { $0.vendorUrl = $1 }),
            Type<Technology>.optionalProperty("productUrl", get: { $0.productUrl }, set: { $0.productUrl = $1 }),
            Type<Technology>.optionalProperty("logoUrl", get: { $0.logoUrl }, set: { $0.logoUrl = $1 }),
            Type<Technology>.optionalProperty("Description", get: { $0.Description }, set: { $0.Description = $1 }),
            Type<Technology>.optionalProperty("created", get: { $0.created }, set: { $0.created = $1 }),
            Type<Technology>.optionalProperty("createdBy", get: { $0.createdBy }, set: { $0.createdBy = $1 }),
            Type<Technology>.optionalProperty("lastModified", get: { $0.lastModified }, set: { $0.lastModified = $1 }),
            Type<Technology>.optionalProperty("lastModifiedBy", get: { $0.lastModifiedBy }, set: { $0.lastModifiedBy = $1 }),
            Type<Technology>.optionalProperty("ownerId", get: { $0.ownerId }, set: { $0.ownerId = $1 }),
            Type<Technology>.optionalProperty("slug", get: { $0.slug }, set: { $0.slug = $1 }),
            Type<Technology>.optionalProperty("logoApproved", get: { $0.logoApproved }, set: { $0.logoApproved = $1 }),
            Type<Technology>.optionalProperty("isLocked", get: { $0.isLocked }, set: { $0.isLocked = $1 }),
            Type<Technology>.optionalProperty("tier", get: { $0.tier }, set: { $0.tier = $1 }),
            Type<Technology>.optionalProperty("lastStatusUpdate", get: { $0.lastStatusUpdate }, set: { $0.lastStatusUpdate = $1 }),
        ])
}

extension TechnologyTier : StringSerializable
{
    public static var typeName:String { return "TechnologyTier" }
    public func toJson() -> String {
        return jsonStringRaw(toString())
    }
    public func toString() -> String {
        switch self {
        case .ProgrammingLanguage: return "ProgrammingLanguage"
        case .Client: return "Client"
        case .Http: return "Http"
        case .Server: return "Server"
        case .Data: return "Data"
        case .SoftwareInfrastructure: return "SoftwareInfrastructure"
        case .OperatingSystem: return "OperatingSystem"
        case .HardwareInfrastructure: return "HardwareInfrastructure"
        case .ThirdPartyServices: return "ThirdPartyServices"
        }
    }
    public static func fromString(_ strValue:String) -> TechnologyTier? {
        switch strValue {
        case "ProgrammingLanguage": return .ProgrammingLanguage
        case "Client": return .Client
        case "Http": return .Http
        case "Server": return .Server
        case "Data": return .Data
        case "SoftwareInfrastructure": return .SoftwareInfrastructure
        case "OperatingSystem": return .OperatingSystem
        case "HardwareInfrastructure": return .HardwareInfrastructure
        case "ThirdPartyServices": return .ThirdPartyServices
        default: return nil
        }
    }
    public static func fromObject(_ any:Any) -> TechnologyTier? {
        switch any {
        case let i as Int: return TechnologyTier(rawValue: i)
        case let s as String: return fromString(s)
        default: return nil
        }
    }
}

extension TechStackDetails : JsonSerializable
{
    public static var typeName:String { return "TechStackDetails" }
    public static var metadata = Metadata.create([
            Type<TechStackDetails>.optionalProperty("detailsHtml", get: { $0.detailsHtml }, set: { $0.detailsHtml = $1 }),
            Type<TechStackDetails>.arrayProperty("technologyChoices", get: { $0.technologyChoices }, set: { $0.technologyChoices = $1 }),
            Type<TechStackDetails>.optionalProperty("id", get: { $0.id }, set: { $0.id = $1 }),
            Type<TechStackDetails>.optionalProperty("name", get: { $0.name }, set: { $0.name = $1 }),
            Type<TechStackDetails>.optionalProperty("vendorName", get: { $0.vendorName }, set: { $0.vendorName = $1 }),
            Type<TechStackDetails>.optionalProperty("Description", get: { $0.Description }, set: { $0.Description = $1 }),
            Type<TechStackDetails>.optionalProperty("appUrl", get: { $0.appUrl }, set: { $0.appUrl = $1 }),
            Type<TechStackDetails>.optionalProperty("screenshotUrl", get: { $0.screenshotUrl }, set: { $0.screenshotUrl = $1 }),
            Type<TechStackDetails>.optionalProperty("created", get: { $0.created }, set: { $0.created = $1 }),
            Type<TechStackDetails>.optionalProperty("createdBy", get: { $0.createdBy }, set: { $0.createdBy = $1 }),
            Type<TechStackDetails>.optionalProperty("lastModified", get: { $0.lastModified }, set: { $0.lastModified = $1 }),
            Type<TechStackDetails>.optionalProperty("lastModifiedBy", get: { $0.lastModifiedBy }, set: { $0.lastModifiedBy = $1 }),
            Type<TechStackDetails>.optionalProperty("isLocked", get: { $0.isLocked }, set: { $0.isLocked = $1 }),
            Type<TechStackDetails>.optionalProperty("ownerId", get: { $0.ownerId }, set: { $0.ownerId = $1 }),
            Type<TechStackDetails>.optionalProperty("slug", get: { $0.slug }, set: { $0.slug = $1 }),
            Type<TechStackDetails>.optionalProperty("details", get: { $0.details }, set: { $0.details = $1 }),
            Type<TechStackDetails>.optionalProperty("lastStatusUpdate", get: { $0.lastStatusUpdate }, set: { $0.lastStatusUpdate = $1 }),
        ])
}

extension TechnologyHistory : JsonSerializable
{
    public static var typeName:String { return "TechnologyHistory" }
    public static var metadata = Metadata.create([
            Type<TechnologyHistory>.optionalProperty("technologyId", get: { $0.technologyId }, set: { $0.technologyId = $1 }),
            Type<TechnologyHistory>.optionalProperty("operation", get: { $0.operation }, set: { $0.operation = $1 }),
            Type<TechnologyHistory>.optionalProperty("id", get: { $0.id }, set: { $0.id = $1 }),
            Type<TechnologyHistory>.optionalProperty("name", get: { $0.name }, set: { $0.name = $1 }),
            Type<TechnologyHistory>.optionalProperty("vendorName", get: { $0.vendorName }, set: { $0.vendorName = $1 }),
            Type<TechnologyHistory>.optionalProperty("vendorUrl", get: { $0.vendorUrl }, set: { $0.vendorUrl = $1 }),
            Type<TechnologyHistory>.optionalProperty("productUrl", get: { $0.productUrl }, set: { $0.productUrl = $1 }),
            Type<TechnologyHistory>.optionalProperty("logoUrl", get: { $0.logoUrl }, set: { $0.logoUrl = $1 }),
            Type<TechnologyHistory>.optionalProperty("Description", get: { $0.Description }, set: { $0.Description = $1 }),
            Type<TechnologyHistory>.optionalProperty("created", get: { $0.created }, set: { $0.created = $1 }),
            Type<TechnologyHistory>.optionalProperty("createdBy", get: { $0.createdBy }, set: { $0.createdBy = $1 }),
            Type<TechnologyHistory>.optionalProperty("lastModified", get: { $0.lastModified }, set: { $0.lastModified = $1 }),
            Type<TechnologyHistory>.optionalProperty("lastModifiedBy", get: { $0.lastModifiedBy }, set: { $0.lastModifiedBy = $1 }),
            Type<TechnologyHistory>.optionalProperty("ownerId", get: { $0.ownerId }, set: { $0.ownerId = $1 }),
            Type<TechnologyHistory>.optionalProperty("slug", get: { $0.slug }, set: { $0.slug = $1 }),
            Type<TechnologyHistory>.optionalProperty("logoApproved", get: { $0.logoApproved }, set: { $0.logoApproved = $1 }),
            Type<TechnologyHistory>.optionalProperty("isLocked", get: { $0.isLocked }, set: { $0.isLocked = $1 }),
            Type<TechnologyHistory>.optionalProperty("tier", get: { $0.tier }, set: { $0.tier = $1 }),
            Type<TechnologyHistory>.optionalProperty("lastStatusUpdate", get: { $0.lastStatusUpdate }, set: { $0.lastStatusUpdate = $1 }),
        ])
}

extension TechnologyStack : JsonSerializable
{
    public static var typeName:String { return "TechnologyStack" }
    public static var metadata = Metadata.create([
            Type<TechnologyStack>.optionalProperty("id", get: { $0.id }, set: { $0.id = $1 }),
            Type<TechnologyStack>.optionalProperty("name", get: { $0.name }, set: { $0.name = $1 }),
            Type<TechnologyStack>.optionalProperty("vendorName", get: { $0.vendorName }, set: { $0.vendorName = $1 }),
            Type<TechnologyStack>.optionalProperty("Description", get: { $0.Description }, set: { $0.Description = $1 }),
            Type<TechnologyStack>.optionalProperty("appUrl", get: { $0.appUrl }, set: { $0.appUrl = $1 }),
            Type<TechnologyStack>.optionalProperty("screenshotUrl", get: { $0.screenshotUrl }, set: { $0.screenshotUrl = $1 }),
            Type<TechnologyStack>.optionalProperty("created", get: { $0.created }, set: { $0.created = $1 }),
            Type<TechnologyStack>.optionalProperty("createdBy", get: { $0.createdBy }, set: { $0.createdBy = $1 }),
            Type<TechnologyStack>.optionalProperty("lastModified", get: { $0.lastModified }, set: { $0.lastModified = $1 }),
            Type<TechnologyStack>.optionalProperty("lastModifiedBy", get: { $0.lastModifiedBy }, set: { $0.lastModifiedBy = $1 }),
            Type<TechnologyStack>.optionalProperty("isLocked", get: { $0.isLocked }, set: { $0.isLocked = $1 }),
            Type<TechnologyStack>.optionalProperty("ownerId", get: { $0.ownerId }, set: { $0.ownerId = $1 }),
            Type<TechnologyStack>.optionalProperty("slug", get: { $0.slug }, set: { $0.slug = $1 }),
            Type<TechnologyStack>.optionalProperty("details", get: { $0.details }, set: { $0.details = $1 }),
            Type<TechnologyStack>.optionalProperty("lastStatusUpdate", get: { $0.lastStatusUpdate }, set: { $0.lastStatusUpdate = $1 }),
        ])
}

extension TechnologyStackHistory : JsonSerializable
{
    public static var typeName:String { return "TechnologyStackHistory" }
    public static var metadata = Metadata.create([
            Type<TechnologyStackHistory>.optionalProperty("technologyStackId", get: { $0.technologyStackId }, set: { $0.technologyStackId = $1 }),
            Type<TechnologyStackHistory>.optionalProperty("operation", get: { $0.operation }, set: { $0.operation = $1 }),
            Type<TechnologyStackHistory>.arrayProperty("technologyIds", get: { $0.technologyIds }, set: { $0.technologyIds = $1 }),
            Type<TechnologyStackHistory>.optionalProperty("id", get: { $0.id }, set: { $0.id = $1 }),
            Type<TechnologyStackHistory>.optionalProperty("name", get: { $0.name }, set: { $0.name = $1 }),
            Type<TechnologyStackHistory>.optionalProperty("vendorName", get: { $0.vendorName }, set: { $0.vendorName = $1 }),
            Type<TechnologyStackHistory>.optionalProperty("Description", get: { $0.Description }, set: { $0.Description = $1 }),
            Type<TechnologyStackHistory>.optionalProperty("appUrl", get: { $0.appUrl }, set: { $0.appUrl = $1 }),
            Type<TechnologyStackHistory>.optionalProperty("screenshotUrl", get: { $0.screenshotUrl }, set: { $0.screenshotUrl = $1 }),
            Type<TechnologyStackHistory>.optionalProperty("created", get: { $0.created }, set: { $0.created = $1 }),
            Type<TechnologyStackHistory>.optionalProperty("createdBy", get: { $0.createdBy }, set: { $0.createdBy = $1 }),
            Type<TechnologyStackHistory>.optionalProperty("lastModified", get: { $0.lastModified }, set: { $0.lastModified = $1 }),
            Type<TechnologyStackHistory>.optionalProperty("lastModifiedBy", get: { $0.lastModifiedBy }, set: { $0.lastModifiedBy = $1 }),
            Type<TechnologyStackHistory>.optionalProperty("isLocked", get: { $0.isLocked }, set: { $0.isLocked = $1 }),
            Type<TechnologyStackHistory>.optionalProperty("ownerId", get: { $0.ownerId }, set: { $0.ownerId = $1 }),
            Type<TechnologyStackHistory>.optionalProperty("slug", get: { $0.slug }, set: { $0.slug = $1 }),
            Type<TechnologyStackHistory>.optionalProperty("details", get: { $0.details }, set: { $0.details = $1 }),
            Type<TechnologyStackHistory>.optionalProperty("lastStatusUpdate", get: { $0.lastStatusUpdate }, set: { $0.lastStatusUpdate = $1 }),
        ])
}

extension UserInfo : JsonSerializable
{
    public static var typeName:String { return "UserInfo" }
    public static var metadata = Metadata.create([
            Type<UserInfo>.optionalProperty("userName", get: { $0.userName }, set: { $0.userName = $1 }),
            Type<UserInfo>.optionalProperty("avatarUrl", get: { $0.avatarUrl }, set: { $0.avatarUrl = $1 }),
            Type<UserInfo>.optionalProperty("stacksCount", get: { $0.stacksCount }, set: { $0.stacksCount = $1 }),
        ])
}

extension TechnologyInfo : JsonSerializable
{
    public static var typeName:String { return "TechnologyInfo" }
    public static var metadata = Metadata.create([
            Type<TechnologyInfo>.optionalProperty("tier", get: { $0.tier }, set: { $0.tier = $1 }),
            Type<TechnologyInfo>.optionalProperty("slug", get: { $0.slug }, set: { $0.slug = $1 }),
            Type<TechnologyInfo>.optionalProperty("name", get: { $0.name }, set: { $0.name = $1 }),
            Type<TechnologyInfo>.optionalProperty("logoUrl", get: { $0.logoUrl }, set: { $0.logoUrl = $1 }),
            Type<TechnologyInfo>.optionalProperty("stacksCount", get: { $0.stacksCount }, set: { $0.stacksCount = $1 }),
        ])
}

extension Option : JsonSerializable
{
    public static var typeName:String { return "Option" }
    public static var metadata = Metadata.create([
            Type<Option>.optionalProperty("name", get: { $0.name }, set: { $0.name = $1 }),
            Type<Option>.optionalProperty("title", get: { $0.title }, set: { $0.title = $1 }),
            Type<Option>.optionalProperty("value", get: { $0.value }, set: { $0.value = $1 }),
        ])
}

extension TechnologyInStack : JsonSerializable
{
    public static var typeName:String { return "TechnologyInStack" }
    public static var metadata = Metadata.create([
            Type<TechnologyInStack>.optionalProperty("technologyId", get: { $0.technologyId }, set: { $0.technologyId = $1 }),
            Type<TechnologyInStack>.optionalProperty("technologyStackId", get: { $0.technologyStackId }, set: { $0.technologyStackId = $1 }),
            Type<TechnologyInStack>.optionalProperty("justification", get: { $0.justification }, set: { $0.justification = $1 }),
            Type<TechnologyInStack>.optionalProperty("id", get: { $0.id }, set: { $0.id = $1 }),
            Type<TechnologyInStack>.optionalProperty("name", get: { $0.name }, set: { $0.name = $1 }),
            Type<TechnologyInStack>.optionalProperty("vendorName", get: { $0.vendorName }, set: { $0.vendorName = $1 }),
            Type<TechnologyInStack>.optionalProperty("vendorUrl", get: { $0.vendorUrl }, set: { $0.vendorUrl = $1 }),
            Type<TechnologyInStack>.optionalProperty("productUrl", get: { $0.productUrl }, set: { $0.productUrl = $1 }),
            Type<TechnologyInStack>.optionalProperty("logoUrl", get: { $0.logoUrl }, set: { $0.logoUrl = $1 }),
            Type<TechnologyInStack>.optionalProperty("Description", get: { $0.Description }, set: { $0.Description = $1 }),
            Type<TechnologyInStack>.optionalProperty("created", get: { $0.created }, set: { $0.created = $1 }),
            Type<TechnologyInStack>.optionalProperty("createdBy", get: { $0.createdBy }, set: { $0.createdBy = $1 }),
            Type<TechnologyInStack>.optionalProperty("lastModified", get: { $0.lastModified }, set: { $0.lastModified = $1 }),
            Type<TechnologyInStack>.optionalProperty("lastModifiedBy", get: { $0.lastModifiedBy }, set: { $0.lastModifiedBy = $1 }),
            Type<TechnologyInStack>.optionalProperty("ownerId", get: { $0.ownerId }, set: { $0.ownerId = $1 }),
            Type<TechnologyInStack>.optionalProperty("slug", get: { $0.slug }, set: { $0.slug = $1 }),
            Type<TechnologyInStack>.optionalProperty("logoApproved", get: { $0.logoApproved }, set: { $0.logoApproved = $1 }),
            Type<TechnologyInStack>.optionalProperty("isLocked", get: { $0.isLocked }, set: { $0.isLocked = $1 }),
            Type<TechnologyInStack>.optionalProperty("tier", get: { $0.tier }, set: { $0.tier = $1 }),
            Type<TechnologyInStack>.optionalProperty("lastStatusUpdate", get: { $0.lastStatusUpdate }, set: { $0.lastStatusUpdate = $1 }),
        ])
}

