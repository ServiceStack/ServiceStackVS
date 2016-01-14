/* Options:
Date: 2015-12-23 23:52:39
Version: 4.051
Tip: To override a DTO option, remove "//" prefix before updating
BaseUrl: http://techstacks.io

//Package: 
//AddServiceStackTypes: True
//AddResponseStatus: False
//AddImplicitVersion: 
//IncludeTypes: 
//ExcludeTypes: 
//InitializeCollections: True
//TreatTypesAsStrings: 
//DefaultImports: java.math.*,java.util.*,net.servicestack.client.*,com.google.gson.annotations.*,com.google.gson.reflect.*
*/

import java.math.*
import java.util.*
import net.servicestack.client.*
import com.google.gson.annotations.*
import com.google.gson.reflect.*


@Route("/admin/technology/{TechnologyId}/logo")
open class LogoUrlApproval : IReturn<LogoUrlApprovalResponse>
{
    var TechnologyId:Long? = null
    var Approved:Boolean? = null
    companion object { private val responseType = LogoUrlApprovalResponse::class.java }
    override fun getResponseType(): Any? = responseType
}

@Route("/admin/techstacks/{TechnologyStackId}/lock")
open class LockTechStack : IReturn<LockStackResponse>
{
    var TechnologyStackId:Long? = null
    var IsLocked:Boolean? = null
    companion object { private val responseType = LockStackResponse::class.java }
    override fun getResponseType(): Any? = responseType
}

@Route("/admin/technology/{TechnologyId}/lock")
open class LockTech : IReturn<LockStackResponse>
{
    var TechnologyId:Long? = null
    var IsLocked:Boolean? = null
    companion object { private val responseType = LockStackResponse::class.java }
    override fun getResponseType(): Any? = responseType
}

@Route("/ping")
open class Ping
{
}

@Route("/{PathInfo*}")
open class FallbackForClientRoutes
{
    var PathInfo:String? = null
}

@Route("/stacks")
open class ClientAllTechnologyStacks
{
}

@Route("/tech")
open class ClientAllTechnologies
{
}

@Route("/tech/{Slug}")
open class ClientTechnology
{
    var Slug:String? = null
}

@Route("/users/{UserName}")
open class ClientUser
{
    var UserName:String? = null
}

@Route("/my-session")
open class SessionInfo
{
}

@Route(Path="/technology", Verbs="POST")
open class CreateTechnology : IReturn<CreateTechnologyResponse>
{
    var Name:String? = null
    var VendorName:String? = null
    var VendorUrl:String? = null
    var ProductUrl:String? = null
    var LogoUrl:String? = null
    var Description:String? = null
    var IsLocked:Boolean? = null
    var Tier:TechnologyTier? = null
    companion object { private val responseType = CreateTechnologyResponse::class.java }
    override fun getResponseType(): Any? = responseType
}

@Route(Path="/technology/{Id}", Verbs="PUT")
open class UpdateTechnology : IReturn<UpdateTechnologyResponse>
{
    var Id:Long? = null
    var Name:String? = null
    var VendorName:String? = null
    var VendorUrl:String? = null
    var ProductUrl:String? = null
    var LogoUrl:String? = null
    var Description:String? = null
    var IsLocked:Boolean? = null
    var Tier:TechnologyTier? = null
    companion object { private val responseType = UpdateTechnologyResponse::class.java }
    override fun getResponseType(): Any? = responseType
}

@Route(Path="/technology/{Id}", Verbs="DELETE")
open class DeleteTechnology : IReturn<DeleteTechnologyResponse>
{
    var Id:Long? = null
    companion object { private val responseType = DeleteTechnologyResponse::class.java }
    override fun getResponseType(): Any? = responseType
}

@Route("/technology/{Slug}")
open class GetTechnology : IReturn<GetTechnologyResponse>
{
    var Reload:Boolean? = null
    var Slug:String? = null
    companion object { private val responseType = GetTechnologyResponse::class.java }
    override fun getResponseType(): Any? = responseType
}

@Route(Path="/technology/{Slug}/previous-versions", Verbs="GET")
open class GetTechnologyPreviousVersions : IReturn<GetTechnologyPreviousVersionsResponse>
{
    var Slug:String? = null
    companion object { private val responseType = GetTechnologyPreviousVersionsResponse::class.java }
    override fun getResponseType(): Any? = responseType
}

@Route("/technology/{Slug}/favorites")
open class GetTechnologyFavoriteDetails : IReturn<GetTechnologyFavoriteDetailsResponse>
{
    var Slug:String? = null
    var Reload:Boolean? = null
    companion object { private val responseType = GetTechnologyFavoriteDetailsResponse::class.java }
    override fun getResponseType(): Any? = responseType
}

@Route(Path="/technology", Verbs="GET")
open class GetAllTechnologies : IReturn<GetAllTechnologiesResponse>
{
    companion object { private val responseType = GetAllTechnologiesResponse::class.java }
    override fun getResponseType(): Any? = responseType
}

@Route("/technology/search")
@AutoQueryViewer(Title="Find Technologies", Description="Explore different Technologies", IconUrl="/img/app/tech-white-75.png", DefaultSearchField="Tier", DefaultSearchType="=", DefaultSearchText="Data")
open class FindTechnologies : QueryBase_1<Technology>(), IReturn<QueryResponse<Technology>>
{
    var Name:String? = null
    var Reload:Boolean? = null
    companion object { private val responseType = object : TypeToken<QueryResponse<Technology>>(){}.type }
    override fun getResponseType(): Any? = responseType
}

@Route(Path="/techstacks", Verbs="POST")
open class CreateTechnologyStack : IReturn<CreateTechnologyStackResponse>
{
    var Name:String? = null
    var VendorName:String? = null
    var AppUrl:String? = null
    var ScreenshotUrl:String? = null
    var Description:String? = null
    var Details:String? = null
    var IsLocked:Boolean? = null
    var TechnologyIds:ArrayList<Long> = ArrayList<Long>()
    companion object { private val responseType = CreateTechnologyStackResponse::class.java }
    override fun getResponseType(): Any? = responseType
}

@Route(Path="/techstacks/{Id}", Verbs="PUT")
open class UpdateTechnologyStack : IReturn<UpdateTechnologyStackResponse>
{
    var Id:Long? = null
    var Name:String? = null
    var VendorName:String? = null
    var AppUrl:String? = null
    var ScreenshotUrl:String? = null
    var Description:String? = null
    var Details:String? = null
    var IsLocked:Boolean? = null
    var TechnologyIds:ArrayList<Long> = ArrayList<Long>()
    companion object { private val responseType = UpdateTechnologyStackResponse::class.java }
    override fun getResponseType(): Any? = responseType
}

@Route(Path="/techstacks/{Id}", Verbs="DELETE")
open class DeleteTechnologyStack : IReturn<DeleteTechnologyStackResponse>
{
    var Id:Long? = null
    companion object { private val responseType = DeleteTechnologyStackResponse::class.java }
    override fun getResponseType(): Any? = responseType
}

@Route(Path="/techstacks", Verbs="GET")
open class GetAllTechnologyStacks : IReturn<GetAllTechnologyStacksResponse>
{
    companion object { private val responseType = GetAllTechnologyStacksResponse::class.java }
    override fun getResponseType(): Any? = responseType
}

@Route(Path="/techstacks/{Slug}", Verbs="GET")
open class GetTechnologyStack : IReturn<GetTechnologyStackResponse>
{
    var Reload:Boolean? = null
    var Slug:String? = null
    companion object { private val responseType = GetTechnologyStackResponse::class.java }
    override fun getResponseType(): Any? = responseType
}

@Route(Path="/techstacks/{Slug}/previous-versions", Verbs="GET")
open class GetTechnologyStackPreviousVersions : IReturn<GetTechnologyStackPreviousVersionsResponse>
{
    var Slug:String? = null
    companion object { private val responseType = GetTechnologyStackPreviousVersionsResponse::class.java }
    override fun getResponseType(): Any? = responseType
}

@Route("/techstacks/{Slug}/favorites")
open class GetTechnologyStackFavoriteDetails : IReturn<GetTechnologyStackFavoriteDetailsResponse>
{
    var Slug:String? = null
    var Reload:Boolean? = null
    companion object { private val responseType = GetTechnologyStackFavoriteDetailsResponse::class.java }
    override fun getResponseType(): Any? = responseType
}

@Route("/config")
open class GetConfig : IReturn<GetConfigResponse>
{
    companion object { private val responseType = GetConfigResponse::class.java }
    override fun getResponseType(): Any? = responseType
}

@Route("/overview")
open class Overview : IReturn<OverviewResponse>
{
    var Reload:Boolean? = null
    companion object { private val responseType = OverviewResponse::class.java }
    override fun getResponseType(): Any? = responseType
}

@Route("/app-overview")
open class AppOverview : IReturn<AppOverviewResponse>
{
    var Reload:Boolean? = null
    companion object { private val responseType = AppOverviewResponse::class.java }
    override fun getResponseType(): Any? = responseType
}

@Route("/pagestats/{Type}/{Slug}")
open class GetPageStats : IReturn<GetPageStatsResponse>
{
    var Type:String? = null
    var Slug:String? = null
    companion object { private val responseType = GetPageStatsResponse::class.java }
    override fun getResponseType(): Any? = responseType
}

@Route("/techstacks/search")
@AutoQueryViewer(Title="Find Technology Stacks", Description="Explore different Technology Stacks", IconUrl="/img/app/stacks-white-75.png", DefaultSearchField="Description", DefaultSearchType="Contains", DefaultSearchText="ServiceStack")
open class FindTechStacks : QueryBase_1<TechnologyStack>(), IReturn<QueryResponse<TechnologyStack>>
{
    var Reload:Boolean? = null
    companion object { private val responseType = object : TypeToken<QueryResponse<TechnologyStack>>(){}.type }
    override fun getResponseType(): Any? = responseType
}

@Route(Path="/favorites/techtacks", Verbs="GET")
open class GetFavoriteTechStack : IReturn<GetFavoriteTechStackResponse>
{
    var TechnologyStackId:Int? = null
    companion object { private val responseType = GetFavoriteTechStackResponse::class.java }
    override fun getResponseType(): Any? = responseType
}

@Route(Path="/favorites/techtacks/{TechnologyStackId}", Verbs="PUT")
open class AddFavoriteTechStack : IReturn<FavoriteTechStackResponse>
{
    var TechnologyStackId:Int? = null
    companion object { private val responseType = FavoriteTechStackResponse::class.java }
    override fun getResponseType(): Any? = responseType
}

@Route(Path="/favorites/techtacks/{TechnologyStackId}", Verbs="DELETE")
open class RemoveFavoriteTechStack : IReturn<FavoriteTechStackResponse>
{
    var TechnologyStackId:Int? = null
    companion object { private val responseType = FavoriteTechStackResponse::class.java }
    override fun getResponseType(): Any? = responseType
}

@Route(Path="/favorites/technology", Verbs="GET")
open class GetFavoriteTechnologies : IReturn<GetFavoriteTechnologiesResponse>
{
    var TechnologyId:Int? = null
    companion object { private val responseType = GetFavoriteTechnologiesResponse::class.java }
    override fun getResponseType(): Any? = responseType
}

@Route(Path="/favorites/technology/{TechnologyId}", Verbs="PUT")
open class AddFavoriteTechnology : IReturn<FavoriteTechnologyResponse>
{
    var TechnologyId:Int? = null
    companion object { private val responseType = FavoriteTechnologyResponse::class.java }
    override fun getResponseType(): Any? = responseType
}

@Route(Path="/favorites/technology/{TechnologyId}", Verbs="DELETE")
open class RemoveFavoriteTechnology : IReturn<FavoriteTechnologyResponse>
{
    var TechnologyId:Int? = null
    companion object { private val responseType = FavoriteTechnologyResponse::class.java }
    override fun getResponseType(): Any? = responseType
}

@Route("/my-feed")
open class GetUserFeed : IReturn<GetUserFeedResponse>
{
    companion object { private val responseType = GetUserFeedResponse::class.java }
    override fun getResponseType(): Any? = responseType
}

@Route("/userinfo/{UserName}")
open class GetUserInfo : IReturn<GetUserInfoResponse>
{
    var Reload:Boolean? = null
    var UserName:String? = null
    companion object { private val responseType = GetUserInfoResponse::class.java }
    override fun getResponseType(): Any? = responseType
}

@Route("/auth")
// @Route("/auth/{provider}")
// @Route("/authenticate")
// @Route("/authenticate/{provider}")
@DataContract
open class Authenticate : IReturn<AuthenticateResponse>
{
    @DataMember(Order=1)
    var provider:String? = null

    @DataMember(Order=2)
    var State:String? = null

    @DataMember(Order=3)
    var oauth_token:String? = null

    @DataMember(Order=4)
    var oauth_verifier:String? = null

    @DataMember(Order=5)
    var UserName:String? = null

    @DataMember(Order=6)
    var Password:String? = null

    @DataMember(Order=7)
    var RememberMe:Boolean? = null

    @DataMember(Order=8)
    var Continue:String? = null

    @DataMember(Order=9)
    var nonce:String? = null

    @DataMember(Order=10)
    var uri:String? = null

    @DataMember(Order=11)
    var response:String? = null

    @DataMember(Order=12)
    var qop:String? = null

    @DataMember(Order=13)
    var nc:String? = null

    @DataMember(Order=14)
    var cnonce:String? = null

    @DataMember(Order=15)
    var Meta:HashMap<String,String> = HashMap<String,String>()
    companion object { private val responseType = AuthenticateResponse::class.java }
    override fun getResponseType(): Any? = responseType
}

@Route("/assignroles")
@DataContract
open class AssignRoles : IReturn<AssignRolesResponse>
{
    @DataMember(Order=1)
    var UserName:String? = null

    @DataMember(Order=2)
    var Permissions:ArrayList<String> = ArrayList<String>()

    @DataMember(Order=3)
    var Roles:ArrayList<String> = ArrayList<String>()
    companion object { private val responseType = AssignRolesResponse::class.java }
    override fun getResponseType(): Any? = responseType
}

@Route("/unassignroles")
@DataContract
open class UnAssignRoles : IReturn<UnAssignRolesResponse>
{
    @DataMember(Order=1)
    var UserName:String? = null

    @DataMember(Order=2)
    var Permissions:ArrayList<String> = ArrayList<String>()

    @DataMember(Order=3)
    var Roles:ArrayList<String> = ArrayList<String>()
    companion object { private val responseType = UnAssignRolesResponse::class.java }
    override fun getResponseType(): Any? = responseType
}

open class LogoUrlApprovalResponse
{
    var Result:Technology? = null
}

open class LockStackResponse
{
}

open class CreateTechnologyResponse
{
    var Result:Technology? = null
    var ResponseStatus:ResponseStatus? = null
}

open class UpdateTechnologyResponse
{
    var Result:Technology? = null
    var ResponseStatus:ResponseStatus? = null
}

open class DeleteTechnologyResponse
{
    var Result:Technology? = null
    var ResponseStatus:ResponseStatus? = null
}

open class GetTechnologyResponse
{
    var Created:Date? = null
    var Technology:Technology? = null
    var TechnologyStacks:ArrayList<TechnologyStack> = ArrayList<TechnologyStack>()
    var ResponseStatus:ResponseStatus? = null
}

open class GetTechnologyPreviousVersionsResponse
{
    var Results:ArrayList<TechnologyHistory> = ArrayList<TechnologyHistory>()
}

open class GetTechnologyFavoriteDetailsResponse
{
    var Users:ArrayList<String> = ArrayList<String>()
    var FavoriteCount:Int? = null
}

open class GetAllTechnologiesResponse
{
    var Results:ArrayList<Technology> = ArrayList<Technology>()
}

@DataContract
open class QueryResponse<T>
{
    @DataMember(Order=1)
    var Offset:Int? = null

    @DataMember(Order=2)
    var Total:Int? = null

    @DataMember(Order=3)
    var Results:ArrayList<T> = ArrayList<T>()

    @DataMember(Order=4)
    var Meta:HashMap<String,String> = HashMap<String,String>()

    @DataMember(Order=5)
    var ResponseStatus:ResponseStatus? = null
}

open class CreateTechnologyStackResponse
{
    var Result:TechStackDetails? = null
    var ResponseStatus:ResponseStatus? = null
}

open class UpdateTechnologyStackResponse
{
    var Result:TechStackDetails? = null
    var ResponseStatus:ResponseStatus? = null
}

open class DeleteTechnologyStackResponse
{
    var Result:TechStackDetails? = null
    var ResponseStatus:ResponseStatus? = null
}

open class GetAllTechnologyStacksResponse
{
    var Results:ArrayList<TechnologyStack> = ArrayList<TechnologyStack>()
}

open class GetTechnologyStackResponse
{
    var Created:Date? = null
    var Result:TechStackDetails? = null
    var ResponseStatus:ResponseStatus? = null
}

open class GetTechnologyStackPreviousVersionsResponse
{
    var Results:ArrayList<TechnologyStackHistory> = ArrayList<TechnologyStackHistory>()
}

open class GetTechnologyStackFavoriteDetailsResponse
{
    var Users:ArrayList<String> = ArrayList<String>()
    var FavoriteCount:Int? = null
}

open class GetConfigResponse
{
    var AllTiers:ArrayList<Option> = ArrayList<Option>()
}

open class OverviewResponse
{
    var Created:Date? = null
    var TopUsers:ArrayList<UserInfo> = ArrayList<UserInfo>()
    var TopTechnologies:ArrayList<TechnologyInfo> = ArrayList<TechnologyInfo>()
    var LatestTechStacks:ArrayList<TechStackDetails> = ArrayList<TechStackDetails>()
    var PopularTechStacks:ArrayList<TechnologyStack> = ArrayList<TechnologyStack>()
    var TopTechnologiesByTier:HashMap<TechnologyTier,ArrayList<TechnologyInfo>> = HashMap<TechnologyTier,ArrayList<TechnologyInfo>>()
    var ResponseStatus:ResponseStatus? = null
}

open class AppOverviewResponse
{
    var Created:Date? = null
    var AllTiers:ArrayList<Option> = ArrayList<Option>()
    var TopTechnologies:ArrayList<TechnologyInfo> = ArrayList<TechnologyInfo>()
    var ResponseStatus:ResponseStatus? = null
}

open class GetPageStatsResponse
{
    var Type:String? = null
    var Slug:String? = null
    var ViewCount:Long? = null
    var FavCount:Long? = null
}

open class GetFavoriteTechStackResponse
{
    var Results:ArrayList<TechnologyStack> = ArrayList<TechnologyStack>()
}

open class FavoriteTechStackResponse
{
    var Result:TechnologyStack? = null
}

open class GetFavoriteTechnologiesResponse
{
    var Results:ArrayList<Technology> = ArrayList<Technology>()
}

open class FavoriteTechnologyResponse
{
    var Result:Technology? = null
}

open class GetUserFeedResponse
{
    var Results:ArrayList<TechStackDetails> = ArrayList<TechStackDetails>()
}

open class GetUserInfoResponse
{
    var UserName:String? = null
    var Created:Date? = null
    var AvatarUrl:String? = null
    var TechStacks:ArrayList<TechnologyStack> = ArrayList<TechnologyStack>()
    var FavoriteTechStacks:ArrayList<TechnologyStack> = ArrayList<TechnologyStack>()
    var FavoriteTechnologies:ArrayList<Technology> = ArrayList<Technology>()
}

@DataContract
open class AuthenticateResponse
{
    @DataMember(Order=1)
    var UserId:String? = null

    @DataMember(Order=2)
    var SessionId:String? = null

    @DataMember(Order=3)
    var UserName:String? = null

    @DataMember(Order=4)
    var DisplayName:String? = null

    @DataMember(Order=5)
    var ReferrerUrl:String? = null

    @DataMember(Order=6)
    var ResponseStatus:ResponseStatus? = null

    @DataMember(Order=7)
    var Meta:HashMap<String,String> = HashMap<String,String>()
}

@DataContract
open class AssignRolesResponse
{
    @DataMember(Order=1)
    var AllRoles:ArrayList<String> = ArrayList<String>()

    @DataMember(Order=2)
    var AllPermissions:ArrayList<String> = ArrayList<String>()

    @DataMember(Order=3)
    var ResponseStatus:ResponseStatus? = null
}

@DataContract
open class UnAssignRolesResponse
{
    @DataMember(Order=1)
    var AllRoles:ArrayList<String> = ArrayList<String>()

    @DataMember(Order=2)
    var AllPermissions:ArrayList<String> = ArrayList<String>()

    @DataMember(Order=3)
    var ResponseStatus:ResponseStatus? = null
}

open class Technology : TechnologyBase()
{
}

enum class TechnologyTier
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

open class TechnologyStack : TechnologyStackBase()
{
}

open class TechnologyHistory : TechnologyBase()
{
    var TechnologyId:Long? = null
    var Operation:String? = null
}

open class QueryBase_1<T> : QueryBase()
{
}

open class TechStackDetails : TechnologyStackBase()
{
    var DetailsHtml:String? = null
    var TechnologyChoices:ArrayList<TechnologyInStack> = ArrayList<TechnologyInStack>()
}

open class TechnologyStackHistory : TechnologyStackBase()
{
    var TechnologyStackId:Long? = null
    var Operation:String? = null
    var TechnologyIds:ArrayList<Long> = ArrayList<Long>()
}

@DataContract
open class Option
{
    @DataMember(Name="name")
    @SerializedName("name")
    var Name:String? = null

    @DataMember(Name="title")
    @SerializedName("title")
    var Title:String? = null

    @DataMember(Name="value")
    @SerializedName("value")
    var Value:TechnologyTier? = null
}

open class UserInfo
{
    var UserName:String? = null
    var AvatarUrl:String? = null
    var StacksCount:Int? = null
}

open class TechnologyInfo
{
    var Tier:TechnologyTier? = null
    var Slug:String? = null
    var Name:String? = null
    var LogoUrl:String? = null
    var StacksCount:Int? = null
}

open class TechnologyBase
{
    var Id:Long? = null
    var Name:String? = null
    var VendorName:String? = null
    var VendorUrl:String? = null
    var ProductUrl:String? = null
    var LogoUrl:String? = null
    var Description:String? = null
    var Created:Date? = null
    var CreatedBy:String? = null
    var LastModified:Date? = null
    var LastModifiedBy:String? = null
    var OwnerId:String? = null
    var Slug:String? = null
    var LogoApproved:Boolean? = null
    var IsLocked:Boolean? = null
    var Tier:TechnologyTier? = null
    var LastStatusUpdate:Date? = null
}

open class TechnologyStackBase
{
    var Id:Long? = null
    var Name:String? = null
    var VendorName:String? = null
    var Description:String? = null
    var AppUrl:String? = null
    var ScreenshotUrl:String? = null
    var Created:Date? = null
    var CreatedBy:String? = null
    var LastModified:Date? = null
    var LastModifiedBy:String? = null
    var IsLocked:Boolean? = null
    var OwnerId:String? = null
    var Slug:String? = null
    var Details:String? = null
    var LastStatusUpdate:Date? = null
}

open class QueryBase
{
    @DataMember(Order=1)
    var Skip:Int? = null

    @DataMember(Order=2)
    var Take:Int? = null

    @DataMember(Order=3)
    var OrderBy:String? = null

    @DataMember(Order=4)
    var OrderByDesc:String? = null

    @DataMember(Order=5)
    var Include:String? = null

    @DataMember(Order=6)
    var Meta:HashMap<String,String> = HashMap<String,String>()
}

open class TechnologyInStack : TechnologyBase()
{
    var TechnologyId:Long? = null
    var TechnologyStackId:Long? = null
    var Justification:String? = null
}
