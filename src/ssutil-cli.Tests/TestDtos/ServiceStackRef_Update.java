/* Options:
Date: 2015-06-14 07:47:22
Version: 1
BaseUrl: http://techstacks.io

//Package: 
//GlobalNamespace: dto
//AddPropertyAccessors: True
//SettersReturnThis: True
//AddServiceStackTypes: True
//AddResponseStatus: False
//AddImplicitVersion: 
//IncludeTypes: 
//ExcludeTypes: 
//DefaultImports: java.math.*,java.util.*,net.servicestack.client.*,com.google.gson.annotations.*,com.google.gson.reflect.*
*/

import java.math.*;
import java.util.*;
import net.servicestack.client.*;
import com.google.gson.annotations.*;
import com.google.gson.reflect.*;

public class dto
{

    public static class Technology extends TechnologyBase
    {
        
    }

    public static enum TechnologyTier
    {
        ProgrammingLanguage,
        Client,
        Http,
        Server,
        Data,
        SoftwareInfrastructure,
        OperatingSystem,
        HardwareInfrastructure,
        ThirdPartyServices;
    }

    public static class TechnologyStack extends TechnologyStackBase
    {
        
    }

    public static class TechnologyHistory extends TechnologyBase
    {
        public Long TechnologyId = null;
        public String Operation = null;
        
        public Long getTechnologyId() { return TechnologyId; }
        public TechnologyHistory setTechnologyId(Long value) { this.TechnologyId = value; return this; }
        public String getOperation() { return Operation; }
        public TechnologyHistory setOperation(String value) { this.Operation = value; return this; }
    }

    public static class QueryBase_1<T> extends QueryBase
    {
        
    }

    public static class TechStackDetails extends TechnologyStackBase
    {
        public String DetailsHtml = null;
        public ArrayList<TechnologyInStack> TechnologyChoices = null;
        
        public String getDetailsHtml() { return DetailsHtml; }
        public TechStackDetails setDetailsHtml(String value) { this.DetailsHtml = value; return this; }
        public ArrayList<TechnologyInStack> getTechnologyChoices() { return TechnologyChoices; }
        public TechStackDetails setTechnologyChoices(ArrayList<TechnologyInStack> value) { this.TechnologyChoices = value; return this; }
    }

    public static class TechnologyStackHistory extends TechnologyStackBase
    {
        public Long TechnologyStackId = null;
        public String Operation = null;
        public ArrayList<Long> TechnologyIds = null;
        
        public Long getTechnologyStackId() { return TechnologyStackId; }
        public TechnologyStackHistory setTechnologyStackId(Long value) { this.TechnologyStackId = value; return this; }
        public String getOperation() { return Operation; }
        public TechnologyStackHistory setOperation(String value) { this.Operation = value; return this; }
        public ArrayList<Long> getTechnologyIds() { return TechnologyIds; }
        public TechnologyStackHistory setTechnologyIds(ArrayList<Long> value) { this.TechnologyIds = value; return this; }
    }

    @DataContract
    public static class Option
    {
        @DataMember(Name="name")
        @SerializedName("name")
        public String Name = null;

        @DataMember(Name="title")
        @SerializedName("title")
        public String Title = null;

        @DataMember(Name="value")
        @SerializedName("value")
        public TechnologyTier Value = null;
        
        public String getName() { return Name; }
        public Option setName(String value) { this.Name = value; return this; }
        public String getTitle() { return Title; }
        public Option setTitle(String value) { this.Title = value; return this; }
        public TechnologyTier getValue() { return Value; }
        public Option setValue(TechnologyTier value) { this.Value = value; return this; }
    }

    public static class UserInfo
    {
        public String UserName = null;
        public String AvatarUrl = null;
        public Integer StacksCount = null;
        
        public String getUserName() { return UserName; }
        public UserInfo setUserName(String value) { this.UserName = value; return this; }
        public String getAvatarUrl() { return AvatarUrl; }
        public UserInfo setAvatarUrl(String value) { this.AvatarUrl = value; return this; }
        public Integer getStacksCount() { return StacksCount; }
        public UserInfo setStacksCount(Integer value) { this.StacksCount = value; return this; }
    }

    public static class TechnologyInfo
    {
        public TechnologyTier Tier = null;
        public String Slug = null;
        public String Name = null;
        public String LogoUrl = null;
        public Integer StacksCount = null;
        
        public TechnologyTier getTier() { return Tier; }
        public TechnologyInfo setTier(TechnologyTier value) { this.Tier = value; return this; }
        public String getSlug() { return Slug; }
        public TechnologyInfo setSlug(String value) { this.Slug = value; return this; }
        public String getName() { return Name; }
        public TechnologyInfo setName(String value) { this.Name = value; return this; }
        public String getLogoUrl() { return LogoUrl; }
        public TechnologyInfo setLogoUrl(String value) { this.LogoUrl = value; return this; }
        public Integer getStacksCount() { return StacksCount; }
        public TechnologyInfo setStacksCount(Integer value) { this.StacksCount = value; return this; }
    }

    public static class Post
    {
        public Integer Id = null;
        public String UserId = null;
        public String UserName = null;
        public String Date = null;
        public String ShortDate = null;
        public String TextHtml = null;
        public ArrayList<PostComment> Comments = null;
        
        public Integer getId() { return Id; }
        public Post setId(Integer value) { this.Id = value; return this; }
        public String getUserId() { return UserId; }
        public Post setUserId(String value) { this.UserId = value; return this; }
        public String getUserName() { return UserName; }
        public Post setUserName(String value) { this.UserName = value; return this; }
        public String getDate() { return Date; }
        public Post setDate(String value) { this.Date = value; return this; }
        public String getShortDate() { return ShortDate; }
        public Post setShortDate(String value) { this.ShortDate = value; return this; }
        public String getTextHtml() { return TextHtml; }
        public Post setTextHtml(String value) { this.TextHtml = value; return this; }
        public ArrayList<PostComment> getComments() { return Comments; }
        public Post setComments(ArrayList<PostComment> value) { this.Comments = value; return this; }
    }

    public static class TechnologyBase
    {
        public Long Id = null;
        public String Name = null;
        public String VendorName = null;
        public String VendorUrl = null;
        public String ProductUrl = null;
        public String LogoUrl = null;
        public String Description = null;
        public Date Created = null;
        public String CreatedBy = null;
        public Date LastModified = null;
        public String LastModifiedBy = null;
        public String OwnerId = null;
        public String Slug = null;
        public Boolean LogoApproved = null;
        public Boolean IsLocked = null;
        public TechnologyTier Tier = null;
        public Date LastStatusUpdate = null;
        
        public Long getId() { return Id; }
        public TechnologyBase setId(Long value) { this.Id = value; return this; }
        public String getName() { return Name; }
        public TechnologyBase setName(String value) { this.Name = value; return this; }
        public String getVendorName() { return VendorName; }
        public TechnologyBase setVendorName(String value) { this.VendorName = value; return this; }
        public String getVendorUrl() { return VendorUrl; }
        public TechnologyBase setVendorUrl(String value) { this.VendorUrl = value; return this; }
        public String getProductUrl() { return ProductUrl; }
        public TechnologyBase setProductUrl(String value) { this.ProductUrl = value; return this; }
        public String getLogoUrl() { return LogoUrl; }
        public TechnologyBase setLogoUrl(String value) { this.LogoUrl = value; return this; }
        public String getDescription() { return Description; }
        public TechnologyBase setDescription(String value) { this.Description = value; return this; }
        public Date getCreated() { return Created; }
        public TechnologyBase setCreated(Date value) { this.Created = value; return this; }
        public String getCreatedBy() { return CreatedBy; }
        public TechnologyBase setCreatedBy(String value) { this.CreatedBy = value; return this; }
        public Date getLastModified() { return LastModified; }
        public TechnologyBase setLastModified(Date value) { this.LastModified = value; return this; }
        public String getLastModifiedBy() { return LastModifiedBy; }
        public TechnologyBase setLastModifiedBy(String value) { this.LastModifiedBy = value; return this; }
        public String getOwnerId() { return OwnerId; }
        public TechnologyBase setOwnerId(String value) { this.OwnerId = value; return this; }
        public String getSlug() { return Slug; }
        public TechnologyBase setSlug(String value) { this.Slug = value; return this; }
        public Boolean isLogoApproved() { return LogoApproved; }
        public TechnologyBase setLogoApproved(Boolean value) { this.LogoApproved = value; return this; }
        public Boolean getIsLocked() { return IsLocked; }
        public TechnologyBase setIsLocked(Boolean value) { this.IsLocked = value; return this; }
        public TechnologyTier getTier() { return Tier; }
        public TechnologyBase setTier(TechnologyTier value) { this.Tier = value; return this; }
        public Date getLastStatusUpdate() { return LastStatusUpdate; }
        public TechnologyBase setLastStatusUpdate(Date value) { this.LastStatusUpdate = value; return this; }
    }

    public static class TechnologyStackBase
    {
        public Long Id = null;
        public String Name = null;
        public String VendorName = null;
        public String Description = null;
        public String AppUrl = null;
        public String ScreenshotUrl = null;
        public Date Created = null;
        public String CreatedBy = null;
        public Date LastModified = null;
        public String LastModifiedBy = null;
        public Boolean IsLocked = null;
        public String OwnerId = null;
        public String Slug = null;
        public String Details = null;
        public Date LastStatusUpdate = null;
        
        public Long getId() { return Id; }
        public TechnologyStackBase setId(Long value) { this.Id = value; return this; }
        public String getName() { return Name; }
        public TechnologyStackBase setName(String value) { this.Name = value; return this; }
        public String getVendorName() { return VendorName; }
        public TechnologyStackBase setVendorName(String value) { this.VendorName = value; return this; }
        public String getDescription() { return Description; }
        public TechnologyStackBase setDescription(String value) { this.Description = value; return this; }
        public String getAppUrl() { return AppUrl; }
        public TechnologyStackBase setAppUrl(String value) { this.AppUrl = value; return this; }
        public String getScreenshotUrl() { return ScreenshotUrl; }
        public TechnologyStackBase setScreenshotUrl(String value) { this.ScreenshotUrl = value; return this; }
        public Date getCreated() { return Created; }
        public TechnologyStackBase setCreated(Date value) { this.Created = value; return this; }
        public String getCreatedBy() { return CreatedBy; }
        public TechnologyStackBase setCreatedBy(String value) { this.CreatedBy = value; return this; }
        public Date getLastModified() { return LastModified; }
        public TechnologyStackBase setLastModified(Date value) { this.LastModified = value; return this; }
        public String getLastModifiedBy() { return LastModifiedBy; }
        public TechnologyStackBase setLastModifiedBy(String value) { this.LastModifiedBy = value; return this; }
        public Boolean getIsLocked() { return IsLocked; }
        public TechnologyStackBase setIsLocked(Boolean value) { this.IsLocked = value; return this; }
        public String getOwnerId() { return OwnerId; }
        public TechnologyStackBase setOwnerId(String value) { this.OwnerId = value; return this; }
        public String getSlug() { return Slug; }
        public TechnologyStackBase setSlug(String value) { this.Slug = value; return this; }
        public String getDetails() { return Details; }
        public TechnologyStackBase setDetails(String value) { this.Details = value; return this; }
        public Date getLastStatusUpdate() { return LastStatusUpdate; }
        public TechnologyStackBase setLastStatusUpdate(Date value) { this.LastStatusUpdate = value; return this; }
    }

    public static class QueryBase
    {
        @DataMember(Order=1)
        public Integer Skip = null;

        @DataMember(Order=2)
        public Integer Take = null;

        @DataMember(Order=3)
        public String OrderBy = null;

        @DataMember(Order=4)
        public String OrderByDesc = null;
        
        public Integer getSkip() { return Skip; }
        public QueryBase setSkip(Integer value) { this.Skip = value; return this; }
        public Integer getTake() { return Take; }
        public QueryBase setTake(Integer value) { this.Take = value; return this; }
        public String getOrderBy() { return OrderBy; }
        public QueryBase setOrderBy(String value) { this.OrderBy = value; return this; }
        public String getOrderByDesc() { return OrderByDesc; }
        public QueryBase setOrderByDesc(String value) { this.OrderByDesc = value; return this; }
    }

    public static class TechnologyInStack extends TechnologyBase
    {
        public Long TechnologyId = null;
        public Long TechnologyStackId = null;
        public String Justification = null;
        
        public Long getTechnologyId() { return TechnologyId; }
        public TechnologyInStack setTechnologyId(Long value) { this.TechnologyId = value; return this; }
        public Long getTechnologyStackId() { return TechnologyStackId; }
        public TechnologyInStack setTechnologyStackId(Long value) { this.TechnologyStackId = value; return this; }
        public String getJustification() { return Justification; }
        public TechnologyInStack setJustification(String value) { this.Justification = value; return this; }
    }

    public static class PostComment
    {
        public Integer Id = null;
        public Integer PostId = null;
        public String UserId = null;
        public String UserName = null;
        public String Date = null;
        public String ShortDate = null;
        public String TextHtml = null;
        
        public Integer getId() { return Id; }
        public PostComment setId(Integer value) { this.Id = value; return this; }
        public Integer getPostId() { return PostId; }
        public PostComment setPostId(Integer value) { this.PostId = value; return this; }
        public String getUserId() { return UserId; }
        public PostComment setUserId(String value) { this.UserId = value; return this; }
        public String getUserName() { return UserName; }
        public PostComment setUserName(String value) { this.UserName = value; return this; }
        public String getDate() { return Date; }
        public PostComment setDate(String value) { this.Date = value; return this; }
        public String getShortDate() { return ShortDate; }
        public PostComment setShortDate(String value) { this.ShortDate = value; return this; }
        public String getTextHtml() { return TextHtml; }
        public PostComment setTextHtml(String value) { this.TextHtml = value; return this; }
    }

    public static class LogoUrlApprovalResponse
    {
        public Technology Result = null;
        
        public Technology getResult() { return Result; }
        public LogoUrlApprovalResponse setResult(Technology value) { this.Result = value; return this; }
    }

    public static class LockStackResponse
    {
        
    }

    public static class CreateTechnologyResponse
    {
        public Technology Result = null;
        public ResponseStatus ResponseStatus = null;
        
        public Technology getResult() { return Result; }
        public CreateTechnologyResponse setResult(Technology value) { this.Result = value; return this; }
        public ResponseStatus getResponseStatus() { return ResponseStatus; }
        public CreateTechnologyResponse setResponseStatus(ResponseStatus value) { this.ResponseStatus = value; return this; }
    }

    public static class UpdateTechnologyResponse
    {
        public Technology Result = null;
        public ResponseStatus ResponseStatus = null;
        
        public Technology getResult() { return Result; }
        public UpdateTechnologyResponse setResult(Technology value) { this.Result = value; return this; }
        public ResponseStatus getResponseStatus() { return ResponseStatus; }
        public UpdateTechnologyResponse setResponseStatus(ResponseStatus value) { this.ResponseStatus = value; return this; }
    }

    public static class DeleteTechnologyResponse
    {
        public Technology Result = null;
        public ResponseStatus ResponseStatus = null;
        
        public Technology getResult() { return Result; }
        public DeleteTechnologyResponse setResult(Technology value) { this.Result = value; return this; }
        public ResponseStatus getResponseStatus() { return ResponseStatus; }
        public DeleteTechnologyResponse setResponseStatus(ResponseStatus value) { this.ResponseStatus = value; return this; }
    }

    public static class GetTechnologyResponse
    {
        public Date Created = null;
        public Technology Technology = null;
        public ArrayList<TechnologyStack> TechnologyStacks = null;
        public ResponseStatus ResponseStatus = null;
        
        public Date getCreated() { return Created; }
        public GetTechnologyResponse setCreated(Date value) { this.Created = value; return this; }
        public Technology getTechnology() { return Technology; }
        public GetTechnologyResponse setTechnology(Technology value) { this.Technology = value; return this; }
        public ArrayList<TechnologyStack> getTechnologyStacks() { return TechnologyStacks; }
        public GetTechnologyResponse setTechnologyStacks(ArrayList<TechnologyStack> value) { this.TechnologyStacks = value; return this; }
        public ResponseStatus getResponseStatus() { return ResponseStatus; }
        public GetTechnologyResponse setResponseStatus(ResponseStatus value) { this.ResponseStatus = value; return this; }
    }

    public static class GetTechnologyPreviousVersionsResponse
    {
        public ArrayList<TechnologyHistory> Results = null;
        
        public ArrayList<TechnologyHistory> getResults() { return Results; }
        public GetTechnologyPreviousVersionsResponse setResults(ArrayList<TechnologyHistory> value) { this.Results = value; return this; }
    }

    public static class GetTechnologyFavoriteDetailsResponse
    {
        public ArrayList<String> Users = null;
        public Integer FavoriteCount = null;
        
        public ArrayList<String> getUsers() { return Users; }
        public GetTechnologyFavoriteDetailsResponse setUsers(ArrayList<String> value) { this.Users = value; return this; }
        public Integer getFavoriteCount() { return FavoriteCount; }
        public GetTechnologyFavoriteDetailsResponse setFavoriteCount(Integer value) { this.FavoriteCount = value; return this; }
    }

    public static class GetAllTechnologiesResponse
    {
        public ArrayList<Technology> Results = null;
        
        public ArrayList<Technology> getResults() { return Results; }
        public GetAllTechnologiesResponse setResults(ArrayList<Technology> value) { this.Results = value; return this; }
    }

    @DataContract
    public static class QueryResponse<T>
    {
        @DataMember(Order=1)
        public Integer Offset = null;

        @DataMember(Order=2)
        public Integer Total = null;

        @DataMember(Order=3)
        public ArrayList<T> Results = null;

        @DataMember(Order=4)
        public HashMap<String,String> Meta = null;

        @DataMember(Order=5)
        public ResponseStatus ResponseStatus = null;
        
        public Integer getOffset() { return Offset; }
        public QueryResponse<T> setOffset(Integer value) { this.Offset = value; return this; }
        public Integer getTotal() { return Total; }
        public QueryResponse<T> setTotal(Integer value) { this.Total = value; return this; }
        public ArrayList<T> getResults() { return Results; }
        public QueryResponse<T> setResults(ArrayList<T> value) { this.Results = value; return this; }
        public HashMap<String,String> getMeta() { return Meta; }
        public QueryResponse<T> setMeta(HashMap<String,String> value) { this.Meta = value; return this; }
        public ResponseStatus getResponseStatus() { return ResponseStatus; }
        public QueryResponse<T> setResponseStatus(ResponseStatus value) { this.ResponseStatus = value; return this; }
    }

    public static class CreateTechnologyStackResponse
    {
        public TechStackDetails Result = null;
        public ResponseStatus ResponseStatus = null;
        
        public TechStackDetails getResult() { return Result; }
        public CreateTechnologyStackResponse setResult(TechStackDetails value) { this.Result = value; return this; }
        public ResponseStatus getResponseStatus() { return ResponseStatus; }
        public CreateTechnologyStackResponse setResponseStatus(ResponseStatus value) { this.ResponseStatus = value; return this; }
    }

    public static class UpdateTechnologyStackResponse
    {
        public TechStackDetails Result = null;
        public ResponseStatus ResponseStatus = null;
        
        public TechStackDetails getResult() { return Result; }
        public UpdateTechnologyStackResponse setResult(TechStackDetails value) { this.Result = value; return this; }
        public ResponseStatus getResponseStatus() { return ResponseStatus; }
        public UpdateTechnologyStackResponse setResponseStatus(ResponseStatus value) { this.ResponseStatus = value; return this; }
    }

    public static class DeleteTechnologyStackResponse
    {
        public TechStackDetails Result = null;
        public ResponseStatus ResponseStatus = null;
        
        public TechStackDetails getResult() { return Result; }
        public DeleteTechnologyStackResponse setResult(TechStackDetails value) { this.Result = value; return this; }
        public ResponseStatus getResponseStatus() { return ResponseStatus; }
        public DeleteTechnologyStackResponse setResponseStatus(ResponseStatus value) { this.ResponseStatus = value; return this; }
    }

    public static class GetAllTechnologyStacksResponse
    {
        public ArrayList<TechnologyStack> Results = null;
        
        public ArrayList<TechnologyStack> getResults() { return Results; }
        public GetAllTechnologyStacksResponse setResults(ArrayList<TechnologyStack> value) { this.Results = value; return this; }
    }

    public static class GetTechnologyStackResponse
    {
        public Date Created = null;
        public TechStackDetails Result = null;
        public ResponseStatus ResponseStatus = null;
        
        public Date getCreated() { return Created; }
        public GetTechnologyStackResponse setCreated(Date value) { this.Created = value; return this; }
        public TechStackDetails getResult() { return Result; }
        public GetTechnologyStackResponse setResult(TechStackDetails value) { this.Result = value; return this; }
        public ResponseStatus getResponseStatus() { return ResponseStatus; }
        public GetTechnologyStackResponse setResponseStatus(ResponseStatus value) { this.ResponseStatus = value; return this; }
    }

    public static class GetTechnologyStackPreviousVersionsResponse
    {
        public ArrayList<TechnologyStackHistory> Results = null;
        
        public ArrayList<TechnologyStackHistory> getResults() { return Results; }
        public GetTechnologyStackPreviousVersionsResponse setResults(ArrayList<TechnologyStackHistory> value) { this.Results = value; return this; }
    }

    public static class GetTechnologyStackFavoriteDetailsResponse
    {
        public ArrayList<String> Users = null;
        public Integer FavoriteCount = null;
        
        public ArrayList<String> getUsers() { return Users; }
        public GetTechnologyStackFavoriteDetailsResponse setUsers(ArrayList<String> value) { this.Users = value; return this; }
        public Integer getFavoriteCount() { return FavoriteCount; }
        public GetTechnologyStackFavoriteDetailsResponse setFavoriteCount(Integer value) { this.FavoriteCount = value; return this; }
    }

    public static class GetConfigResponse
    {
        public ArrayList<Option> AllTiers = null;
        
        public ArrayList<Option> getAllTiers() { return AllTiers; }
        public GetConfigResponse setAllTiers(ArrayList<Option> value) { this.AllTiers = value; return this; }
    }

    public static class OverviewResponse
    {
        public Date Created = null;
        public ArrayList<UserInfo> TopUsers = null;
        public ArrayList<TechnologyInfo> TopTechnologies = null;
        public ArrayList<TechStackDetails> LatestTechStacks = null;
        public HashMap<TechnologyTier,ArrayList<TechnologyInfo>> TopTechnologiesByTier = null;
        public ResponseStatus ResponseStatus = null;
        
        public Date getCreated() { return Created; }
        public OverviewResponse setCreated(Date value) { this.Created = value; return this; }
        public ArrayList<UserInfo> getTopUsers() { return TopUsers; }
        public OverviewResponse setTopUsers(ArrayList<UserInfo> value) { this.TopUsers = value; return this; }
        public ArrayList<TechnologyInfo> getTopTechnologies() { return TopTechnologies; }
        public OverviewResponse setTopTechnologies(ArrayList<TechnologyInfo> value) { this.TopTechnologies = value; return this; }
        public ArrayList<TechStackDetails> getLatestTechStacks() { return LatestTechStacks; }
        public OverviewResponse setLatestTechStacks(ArrayList<TechStackDetails> value) { this.LatestTechStacks = value; return this; }
        public HashMap<TechnologyTier,ArrayList<TechnologyInfo>> getTopTechnologiesByTier() { return TopTechnologiesByTier; }
        public OverviewResponse setTopTechnologiesByTier(HashMap<TechnologyTier,ArrayList<TechnologyInfo>> value) { this.TopTechnologiesByTier = value; return this; }
        public ResponseStatus getResponseStatus() { return ResponseStatus; }
        public OverviewResponse setResponseStatus(ResponseStatus value) { this.ResponseStatus = value; return this; }
    }

    public static class AppOverviewResponse
    {
        public Date Created = null;
        public ArrayList<Option> AllTiers = null;
        public ArrayList<TechnologyInfo> TopTechnologies = null;
        public ResponseStatus ResponseStatus = null;
        
        public Date getCreated() { return Created; }
        public AppOverviewResponse setCreated(Date value) { this.Created = value; return this; }
        public ArrayList<Option> getAllTiers() { return AllTiers; }
        public AppOverviewResponse setAllTiers(ArrayList<Option> value) { this.AllTiers = value; return this; }
        public ArrayList<TechnologyInfo> getTopTechnologies() { return TopTechnologies; }
        public AppOverviewResponse setTopTechnologies(ArrayList<TechnologyInfo> value) { this.TopTechnologies = value; return this; }
        public ResponseStatus getResponseStatus() { return ResponseStatus; }
        public AppOverviewResponse setResponseStatus(ResponseStatus value) { this.ResponseStatus = value; return this; }
    }

    public static class GetFavoriteTechStackResponse
    {
        public ArrayList<TechnologyStack> Results = null;
        
        public ArrayList<TechnologyStack> getResults() { return Results; }
        public GetFavoriteTechStackResponse setResults(ArrayList<TechnologyStack> value) { this.Results = value; return this; }
    }

    public static class FavoriteTechStackResponse
    {
        public TechnologyStack Result = null;
        
        public TechnologyStack getResult() { return Result; }
        public FavoriteTechStackResponse setResult(TechnologyStack value) { this.Result = value; return this; }
    }

    public static class GetFavoriteTechnologiesResponse
    {
        public ArrayList<Technology> Results = null;
        
        public ArrayList<Technology> getResults() { return Results; }
        public GetFavoriteTechnologiesResponse setResults(ArrayList<Technology> value) { this.Results = value; return this; }
    }

    public static class FavoriteTechnologyResponse
    {
        public Technology Result = null;
        
        public Technology getResult() { return Result; }
        public FavoriteTechnologyResponse setResult(Technology value) { this.Result = value; return this; }
    }

    public static class GetUserFeedResponse
    {
        public ArrayList<TechStackDetails> Results = null;
        
        public ArrayList<TechStackDetails> getResults() { return Results; }
        public GetUserFeedResponse setResults(ArrayList<TechStackDetails> value) { this.Results = value; return this; }
    }

    public static class GetUserInfoResponse
    {
        public String UserName = null;
        public Date Created = null;
        public String AvatarUrl = null;
        public ArrayList<TechnologyStack> TechStacks = null;
        public ArrayList<TechnologyStack> FavoriteTechStacks = null;
        public ArrayList<Technology> FavoriteTechnologies = null;
        
        public String getUserName() { return UserName; }
        public GetUserInfoResponse setUserName(String value) { this.UserName = value; return this; }
        public Date getCreated() { return Created; }
        public GetUserInfoResponse setCreated(Date value) { this.Created = value; return this; }
        public String getAvatarUrl() { return AvatarUrl; }
        public GetUserInfoResponse setAvatarUrl(String value) { this.AvatarUrl = value; return this; }
        public ArrayList<TechnologyStack> getTechStacks() { return TechStacks; }
        public GetUserInfoResponse setTechStacks(ArrayList<TechnologyStack> value) { this.TechStacks = value; return this; }
        public ArrayList<TechnologyStack> getFavoriteTechStacks() { return FavoriteTechStacks; }
        public GetUserInfoResponse setFavoriteTechStacks(ArrayList<TechnologyStack> value) { this.FavoriteTechStacks = value; return this; }
        public ArrayList<Technology> getFavoriteTechnologies() { return FavoriteTechnologies; }
        public GetUserInfoResponse setFavoriteTechnologies(ArrayList<Technology> value) { this.FavoriteTechnologies = value; return this; }
    }

    @DataContract
    public static class AuthenticateResponse
    {
        @DataMember(Order=1)
        public String UserId = null;

        @DataMember(Order=2)
        public String SessionId = null;

        @DataMember(Order=3)
        public String UserName = null;

        @DataMember(Order=4)
        public String DisplayName = null;

        @DataMember(Order=5)
        public String ReferrerUrl = null;

        @DataMember(Order=6)
        public ResponseStatus ResponseStatus = null;

        @DataMember(Order=7)
        public HashMap<String,String> Meta = null;
        
        public String getUserId() { return UserId; }
        public AuthenticateResponse setUserId(String value) { this.UserId = value; return this; }
        public String getSessionId() { return SessionId; }
        public AuthenticateResponse setSessionId(String value) { this.SessionId = value; return this; }
        public String getUserName() { return UserName; }
        public AuthenticateResponse setUserName(String value) { this.UserName = value; return this; }
        public String getDisplayName() { return DisplayName; }
        public AuthenticateResponse setDisplayName(String value) { this.DisplayName = value; return this; }
        public String getReferrerUrl() { return ReferrerUrl; }
        public AuthenticateResponse setReferrerUrl(String value) { this.ReferrerUrl = value; return this; }
        public ResponseStatus getResponseStatus() { return ResponseStatus; }
        public AuthenticateResponse setResponseStatus(ResponseStatus value) { this.ResponseStatus = value; return this; }
        public HashMap<String,String> getMeta() { return Meta; }
        public AuthenticateResponse setMeta(HashMap<String,String> value) { this.Meta = value; return this; }
    }

    public static class AssignRolesResponse
    {
        public ArrayList<String> AllRoles = null;
        public ArrayList<String> AllPermissions = null;
        public ResponseStatus ResponseStatus = null;
        
        public ArrayList<String> getAllRoles() { return AllRoles; }
        public AssignRolesResponse setAllRoles(ArrayList<String> value) { this.AllRoles = value; return this; }
        public ArrayList<String> getAllPermissions() { return AllPermissions; }
        public AssignRolesResponse setAllPermissions(ArrayList<String> value) { this.AllPermissions = value; return this; }
        public ResponseStatus getResponseStatus() { return ResponseStatus; }
        public AssignRolesResponse setResponseStatus(ResponseStatus value) { this.ResponseStatus = value; return this; }
    }

    public static class UnAssignRolesResponse
    {
        public ArrayList<String> AllRoles = null;
        public ArrayList<String> AllPermissions = null;
        public ResponseStatus ResponseStatus = null;
        
        public ArrayList<String> getAllRoles() { return AllRoles; }
        public UnAssignRolesResponse setAllRoles(ArrayList<String> value) { this.AllRoles = value; return this; }
        public ArrayList<String> getAllPermissions() { return AllPermissions; }
        public UnAssignRolesResponse setAllPermissions(ArrayList<String> value) { this.AllPermissions = value; return this; }
        public ResponseStatus getResponseStatus() { return ResponseStatus; }
        public UnAssignRolesResponse setResponseStatus(ResponseStatus value) { this.ResponseStatus = value; return this; }
    }

    @Route("/admin/technology/{TechnologyId}/logo")
    public static class LogoUrlApproval implements IReturn<LogoUrlApprovalResponse>
    {
        public Long TechnologyId = null;
        public Boolean Approved = null;
        
        public Long getTechnologyId() { return TechnologyId; }
        public LogoUrlApproval setTechnologyId(Long value) { this.TechnologyId = value; return this; }
        public Boolean isApproved() { return Approved; }
        public LogoUrlApproval setApproved(Boolean value) { this.Approved = value; return this; }
        private static Object responseType = LogoUrlApprovalResponse.class;
        public Object getResponseType() { return responseType; }
    }

    @Route("/admin/techstacks/{TechnologyStackId}/lock")
    public static class LockTechStack implements IReturn<LockStackResponse>
    {
        public Long TechnologyStackId = null;
        public Boolean IsLocked = null;
        
        public Long getTechnologyStackId() { return TechnologyStackId; }
        public LockTechStack setTechnologyStackId(Long value) { this.TechnologyStackId = value; return this; }
        public Boolean getIsLocked() { return IsLocked; }
        public LockTechStack setIsLocked(Boolean value) { this.IsLocked = value; return this; }
        private static Object responseType = LockStackResponse.class;
        public Object getResponseType() { return responseType; }
    }

    @Route("/admin/technology/{TechnologyId}/lock")
    public static class LockTech implements IReturn<LockStackResponse>
    {
        public Long TechnologyId = null;
        public Boolean IsLocked = null;
        
        public Long getTechnologyId() { return TechnologyId; }
        public LockTech setTechnologyId(Long value) { this.TechnologyId = value; return this; }
        public Boolean getIsLocked() { return IsLocked; }
        public LockTech setIsLocked(Boolean value) { this.IsLocked = value; return this; }
        private static Object responseType = LockStackResponse.class;
        public Object getResponseType() { return responseType; }
    }

    @Route("/ping")
    public static class Ping
    {
        
    }

    @Route("/{PathInfo*}")
    public static class FallbackForClientRoutes
    {
        public String PathInfo = null;
        
        public String getPathInfo() { return PathInfo; }
        public FallbackForClientRoutes setPathInfo(String value) { this.PathInfo = value; return this; }
    }

    @Route("/stacks")
    public static class ClientAllTechnologyStacks
    {
        
    }

    @Route("/tech")
    public static class ClientAllTechnologies
    {
        
    }

    @Route("/tech/{Slug}")
    public static class ClientTechnology
    {
        public String Slug = null;
        
        public String getSlug() { return Slug; }
        public ClientTechnology setSlug(String value) { this.Slug = value; return this; }
    }

    @Route("/users/{UserName}")
    public static class ClientUser
    {
        public String UserName = null;
        
        public String getUserName() { return UserName; }
        public ClientUser setUserName(String value) { this.UserName = value; return this; }
    }

    @Route("/my-session")
    public static class SessionInfo
    {
        
    }

    @Route(Path="/technology", Verbs="POST")
    public static class CreateTechnology implements IReturn<CreateTechnologyResponse>
    {
        public String Name = null;
        public String VendorName = null;
        public String VendorUrl = null;
        public String ProductUrl = null;
        public String LogoUrl = null;
        public String Description = null;
        public Boolean IsLocked = null;
        public TechnologyTier Tier = null;
        
        public String getName() { return Name; }
        public CreateTechnology setName(String value) { this.Name = value; return this; }
        public String getVendorName() { return VendorName; }
        public CreateTechnology setVendorName(String value) { this.VendorName = value; return this; }
        public String getVendorUrl() { return VendorUrl; }
        public CreateTechnology setVendorUrl(String value) { this.VendorUrl = value; return this; }
        public String getProductUrl() { return ProductUrl; }
        public CreateTechnology setProductUrl(String value) { this.ProductUrl = value; return this; }
        public String getLogoUrl() { return LogoUrl; }
        public CreateTechnology setLogoUrl(String value) { this.LogoUrl = value; return this; }
        public String getDescription() { return Description; }
        public CreateTechnology setDescription(String value) { this.Description = value; return this; }
        public Boolean getIsLocked() { return IsLocked; }
        public CreateTechnology setIsLocked(Boolean value) { this.IsLocked = value; return this; }
        public TechnologyTier getTier() { return Tier; }
        public CreateTechnology setTier(TechnologyTier value) { this.Tier = value; return this; }
        private static Object responseType = CreateTechnologyResponse.class;
        public Object getResponseType() { return responseType; }
    }

    @Route(Path="/technology/{Id}", Verbs="PUT")
    public static class UpdateTechnology implements IReturn<UpdateTechnologyResponse>
    {
        public Long Id = null;
        public String Name = null;
        public String VendorName = null;
        public String VendorUrl = null;
        public String ProductUrl = null;
        public String LogoUrl = null;
        public String Description = null;
        public Boolean IsLocked = null;
        public TechnologyTier Tier = null;
        
        public Long getId() { return Id; }
        public UpdateTechnology setId(Long value) { this.Id = value; return this; }
        public String getName() { return Name; }
        public UpdateTechnology setName(String value) { this.Name = value; return this; }
        public String getVendorName() { return VendorName; }
        public UpdateTechnology setVendorName(String value) { this.VendorName = value; return this; }
        public String getVendorUrl() { return VendorUrl; }
        public UpdateTechnology setVendorUrl(String value) { this.VendorUrl = value; return this; }
        public String getProductUrl() { return ProductUrl; }
        public UpdateTechnology setProductUrl(String value) { this.ProductUrl = value; return this; }
        public String getLogoUrl() { return LogoUrl; }
        public UpdateTechnology setLogoUrl(String value) { this.LogoUrl = value; return this; }
        public String getDescription() { return Description; }
        public UpdateTechnology setDescription(String value) { this.Description = value; return this; }
        public Boolean getIsLocked() { return IsLocked; }
        public UpdateTechnology setIsLocked(Boolean value) { this.IsLocked = value; return this; }
        public TechnologyTier getTier() { return Tier; }
        public UpdateTechnology setTier(TechnologyTier value) { this.Tier = value; return this; }
        private static Object responseType = UpdateTechnologyResponse.class;
        public Object getResponseType() { return responseType; }
    }

    @Route(Path="/technology/{Id}", Verbs="DELETE")
    public static class DeleteTechnology implements IReturn<DeleteTechnologyResponse>
    {
        public Long Id = null;
        
        public Long getId() { return Id; }
        public DeleteTechnology setId(Long value) { this.Id = value; return this; }
        private static Object responseType = DeleteTechnologyResponse.class;
        public Object getResponseType() { return responseType; }
    }

    @Route("/technology/{Slug}")
    public static class GetTechnology implements IReturn<GetTechnologyResponse>
    {
        public Boolean Reload = null;
        public String Slug = null;
        
        public Boolean isReload() { return Reload; }
        public GetTechnology setReload(Boolean value) { this.Reload = value; return this; }
        public String getSlug() { return Slug; }
        public GetTechnology setSlug(String value) { this.Slug = value; return this; }
        private static Object responseType = GetTechnologyResponse.class;
        public Object getResponseType() { return responseType; }
    }

    @Route(Path="/technology/{Slug}/previous-versions", Verbs="GET")
    public static class GetTechnologyPreviousVersions implements IReturn<GetTechnologyPreviousVersionsResponse>
    {
        public String Slug = null;
        
        public String getSlug() { return Slug; }
        public GetTechnologyPreviousVersions setSlug(String value) { this.Slug = value; return this; }
        private static Object responseType = GetTechnologyPreviousVersionsResponse.class;
        public Object getResponseType() { return responseType; }
    }

    @Route("/technology/{Slug}/favorites")
    public static class GetTechnologyFavoriteDetails implements IReturn<GetTechnologyFavoriteDetailsResponse>
    {
        public String Slug = null;
        public Boolean Reload = null;
        
        public String getSlug() { return Slug; }
        public GetTechnologyFavoriteDetails setSlug(String value) { this.Slug = value; return this; }
        public Boolean isReload() { return Reload; }
        public GetTechnologyFavoriteDetails setReload(Boolean value) { this.Reload = value; return this; }
        private static Object responseType = GetTechnologyFavoriteDetailsResponse.class;
        public Object getResponseType() { return responseType; }
    }

    @Route(Path="/technology", Verbs="GET")
    public static class GetAllTechnologies implements IReturn<GetAllTechnologiesResponse>
    {
        
        private static Object responseType = GetAllTechnologiesResponse.class;
        public Object getResponseType() { return responseType; }
    }

    @Route("/technology/search")
    @AutoQueryViewer(Title="Find Technologies", Description="Explore different Technologies", IconUrl="/img/app/tech-white-75.png", DefaultSearchField="Tier", DefaultSearchType="=", DefaultSearchText="Data")
    public static class FindTechnologies extends QueryBase_1<Technology> implements IReturn<QueryResponse<Technology>>
    {
        public String Name = null;
        public Boolean Reload = null;
        
        public String getName() { return Name; }
        public FindTechnologies setName(String value) { this.Name = value; return this; }
        public Boolean isReload() { return Reload; }
        public FindTechnologies setReload(Boolean value) { this.Reload = value; return this; }
        private static Object responseType = new TypeToken<QueryResponse<Technology>>(){}.getType();
        public Object getResponseType() { return responseType; }
    }

    @Route(Path="/techstacks", Verbs="POST")
    public static class CreateTechnologyStack implements IReturn<CreateTechnologyStackResponse>
    {
        public String Name = null;
        public String VendorName = null;
        public String AppUrl = null;
        public String ScreenshotUrl = null;
        public String Description = null;
        public String Details = null;
        public Boolean IsLocked = null;
        public ArrayList<Long> TechnologyIds = null;
        
        public String getName() { return Name; }
        public CreateTechnologyStack setName(String value) { this.Name = value; return this; }
        public String getVendorName() { return VendorName; }
        public CreateTechnologyStack setVendorName(String value) { this.VendorName = value; return this; }
        public String getAppUrl() { return AppUrl; }
        public CreateTechnologyStack setAppUrl(String value) { this.AppUrl = value; return this; }
        public String getScreenshotUrl() { return ScreenshotUrl; }
        public CreateTechnologyStack setScreenshotUrl(String value) { this.ScreenshotUrl = value; return this; }
        public String getDescription() { return Description; }
        public CreateTechnologyStack setDescription(String value) { this.Description = value; return this; }
        public String getDetails() { return Details; }
        public CreateTechnologyStack setDetails(String value) { this.Details = value; return this; }
        public Boolean getIsLocked() { return IsLocked; }
        public CreateTechnologyStack setIsLocked(Boolean value) { this.IsLocked = value; return this; }
        public ArrayList<Long> getTechnologyIds() { return TechnologyIds; }
        public CreateTechnologyStack setTechnologyIds(ArrayList<Long> value) { this.TechnologyIds = value; return this; }
        private static Object responseType = CreateTechnologyStackResponse.class;
        public Object getResponseType() { return responseType; }
    }

    @Route(Path="/techstacks/{Id}", Verbs="PUT")
    public static class UpdateTechnologyStack implements IReturn<UpdateTechnologyStackResponse>
    {
        public Long Id = null;
        public String Name = null;
        public String VendorName = null;
        public String AppUrl = null;
        public String ScreenshotUrl = null;
        public String Description = null;
        public String Details = null;
        public Boolean IsLocked = null;
        public ArrayList<Long> TechnologyIds = null;
        
        public Long getId() { return Id; }
        public UpdateTechnologyStack setId(Long value) { this.Id = value; return this; }
        public String getName() { return Name; }
        public UpdateTechnologyStack setName(String value) { this.Name = value; return this; }
        public String getVendorName() { return VendorName; }
        public UpdateTechnologyStack setVendorName(String value) { this.VendorName = value; return this; }
        public String getAppUrl() { return AppUrl; }
        public UpdateTechnologyStack setAppUrl(String value) { this.AppUrl = value; return this; }
        public String getScreenshotUrl() { return ScreenshotUrl; }
        public UpdateTechnologyStack setScreenshotUrl(String value) { this.ScreenshotUrl = value; return this; }
        public String getDescription() { return Description; }
        public UpdateTechnologyStack setDescription(String value) { this.Description = value; return this; }
        public String getDetails() { return Details; }
        public UpdateTechnologyStack setDetails(String value) { this.Details = value; return this; }
        public Boolean getIsLocked() { return IsLocked; }
        public UpdateTechnologyStack setIsLocked(Boolean value) { this.IsLocked = value; return this; }
        public ArrayList<Long> getTechnologyIds() { return TechnologyIds; }
        public UpdateTechnologyStack setTechnologyIds(ArrayList<Long> value) { this.TechnologyIds = value; return this; }
        private static Object responseType = UpdateTechnologyStackResponse.class;
        public Object getResponseType() { return responseType; }
    }

    @Route(Path="/techstacks/{Id}", Verbs="DELETE")
    public static class DeleteTechnologyStack implements IReturn<DeleteTechnologyStackResponse>
    {
        public Long Id = null;
        
        public Long getId() { return Id; }
        public DeleteTechnologyStack setId(Long value) { this.Id = value; return this; }
        private static Object responseType = DeleteTechnologyStackResponse.class;
        public Object getResponseType() { return responseType; }
    }

    @Route(Path="/techstacks", Verbs="GET")
    public static class GetAllTechnologyStacks implements IReturn<GetAllTechnologyStacksResponse>
    {
        
        private static Object responseType = GetAllTechnologyStacksResponse.class;
        public Object getResponseType() { return responseType; }
    }

    @Route(Path="/techstacks/{Slug}", Verbs="GET")
    public static class GetTechnologyStack implements IReturn<GetTechnologyStackResponse>
    {
        public Boolean Reload = null;
        public String Slug = null;
        
        public Boolean isReload() { return Reload; }
        public GetTechnologyStack setReload(Boolean value) { this.Reload = value; return this; }
        public String getSlug() { return Slug; }
        public GetTechnologyStack setSlug(String value) { this.Slug = value; return this; }
        private static Object responseType = GetTechnologyStackResponse.class;
        public Object getResponseType() { return responseType; }
    }

    @Route(Path="/techstacks/{Slug}/previous-versions", Verbs="GET")
    public static class GetTechnologyStackPreviousVersions implements IReturn<GetTechnologyStackPreviousVersionsResponse>
    {
        public String Slug = null;
        
        public String getSlug() { return Slug; }
        public GetTechnologyStackPreviousVersions setSlug(String value) { this.Slug = value; return this; }
        private static Object responseType = GetTechnologyStackPreviousVersionsResponse.class;
        public Object getResponseType() { return responseType; }
    }

    @Route("/techstacks/{Slug}/favorites")
    public static class GetTechnologyStackFavoriteDetails implements IReturn<GetTechnologyStackFavoriteDetailsResponse>
    {
        public String Slug = null;
        public Boolean Reload = null;
        
        public String getSlug() { return Slug; }
        public GetTechnologyStackFavoriteDetails setSlug(String value) { this.Slug = value; return this; }
        public Boolean isReload() { return Reload; }
        public GetTechnologyStackFavoriteDetails setReload(Boolean value) { this.Reload = value; return this; }
        private static Object responseType = GetTechnologyStackFavoriteDetailsResponse.class;
        public Object getResponseType() { return responseType; }
    }

    @Route("/config")
    public static class GetConfig implements IReturn<GetConfigResponse>
    {
        
        private static Object responseType = GetConfigResponse.class;
        public Object getResponseType() { return responseType; }
    }

    @Route("/overview")
    public static class Overview implements IReturn<OverviewResponse>
    {
        public Boolean Reload = null;
        
        public Boolean isReload() { return Reload; }
        public Overview setReload(Boolean value) { this.Reload = value; return this; }
        private static Object responseType = OverviewResponse.class;
        public Object getResponseType() { return responseType; }
    }

    @Route("/app-overview")
    public static class AppOverview implements IReturn<AppOverviewResponse>
    {
        public Boolean Reload = null;
        
        public Boolean isReload() { return Reload; }
        public AppOverview setReload(Boolean value) { this.Reload = value; return this; }
        private static Object responseType = AppOverviewResponse.class;
        public Object getResponseType() { return responseType; }
    }

    @Route("/techstacks/search")
    @AutoQueryViewer(Title="Find Technology Stacks", Description="Explore different Technology Stacks", IconUrl="/img/app/stacks-white-75.png", DefaultSearchField="Description", DefaultSearchType="Contains", DefaultSearchText="ServiceStack")
    public static class FindTechStacks extends QueryBase_1<TechnologyStack> implements IReturn<QueryResponse<TechnologyStack>>
    {
        public Boolean Reload = null;
        
        public Boolean isReload() { return Reload; }
        public FindTechStacks setReload(Boolean value) { this.Reload = value; return this; }
        private static Object responseType = new TypeToken<QueryResponse<TechnologyStack>>(){}.getType();
        public Object getResponseType() { return responseType; }
    }

    @Route(Path="/favorites/techtacks", Verbs="GET")
    public static class GetFavoriteTechStack implements IReturn<GetFavoriteTechStackResponse>
    {
        public Integer TechnologyStackId = null;
        
        public Integer getTechnologyStackId() { return TechnologyStackId; }
        public GetFavoriteTechStack setTechnologyStackId(Integer value) { this.TechnologyStackId = value; return this; }
        private static Object responseType = GetFavoriteTechStackResponse.class;
        public Object getResponseType() { return responseType; }
    }

    @Route(Path="/favorites/techtacks/{TechnologyStackId}", Verbs="PUT")
    public static class AddFavoriteTechStack implements IReturn<FavoriteTechStackResponse>
    {
        public Integer TechnologyStackId = null;
        
        public Integer getTechnologyStackId() { return TechnologyStackId; }
        public AddFavoriteTechStack setTechnologyStackId(Integer value) { this.TechnologyStackId = value; return this; }
        private static Object responseType = FavoriteTechStackResponse.class;
        public Object getResponseType() { return responseType; }
    }

    @Route(Path="/favorites/techtacks/{TechnologyStackId}", Verbs="DELETE")
    public static class RemoveFavoriteTechStack implements IReturn<FavoriteTechStackResponse>
    {
        public Integer TechnologyStackId = null;
        
        public Integer getTechnologyStackId() { return TechnologyStackId; }
        public RemoveFavoriteTechStack setTechnologyStackId(Integer value) { this.TechnologyStackId = value; return this; }
        private static Object responseType = FavoriteTechStackResponse.class;
        public Object getResponseType() { return responseType; }
    }

    @Route(Path="/favorites/technology", Verbs="GET")
    public static class GetFavoriteTechnologies implements IReturn<GetFavoriteTechnologiesResponse>
    {
        public Integer TechnologyId = null;
        
        public Integer getTechnologyId() { return TechnologyId; }
        public GetFavoriteTechnologies setTechnologyId(Integer value) { this.TechnologyId = value; return this; }
        private static Object responseType = GetFavoriteTechnologiesResponse.class;
        public Object getResponseType() { return responseType; }
    }

    @Route(Path="/favorites/technology/{TechnologyId}", Verbs="PUT")
    public static class AddFavoriteTechnology implements IReturn<FavoriteTechnologyResponse>
    {
        public Integer TechnologyId = null;
        
        public Integer getTechnologyId() { return TechnologyId; }
        public AddFavoriteTechnology setTechnologyId(Integer value) { this.TechnologyId = value; return this; }
        private static Object responseType = FavoriteTechnologyResponse.class;
        public Object getResponseType() { return responseType; }
    }

    @Route(Path="/favorites/technology/{TechnologyId}", Verbs="DELETE")
    public static class RemoveFavoriteTechnology implements IReturn<FavoriteTechnologyResponse>
    {
        public Integer TechnologyId = null;
        
        public Integer getTechnologyId() { return TechnologyId; }
        public RemoveFavoriteTechnology setTechnologyId(Integer value) { this.TechnologyId = value; return this; }
        private static Object responseType = FavoriteTechnologyResponse.class;
        public Object getResponseType() { return responseType; }
    }

    @Route("/my-feed")
    public static class GetUserFeed implements IReturn<GetUserFeedResponse>
    {
        
        private static Object responseType = GetUserFeedResponse.class;
        public Object getResponseType() { return responseType; }
    }

    @Route("/userinfo/{UserName}")
    public static class GetUserInfo implements IReturn<GetUserInfoResponse>
    {
        public Boolean Reload = null;
        public String UserName = null;
        
        public Boolean isReload() { return Reload; }
        public GetUserInfo setReload(Boolean value) { this.Reload = value; return this; }
        public String getUserName() { return UserName; }
        public GetUserInfo setUserName(String value) { this.UserName = value; return this; }
        private static Object responseType = GetUserInfoResponse.class;
        public Object getResponseType() { return responseType; }
    }

    @Route("/auth")
    // @Route("/auth/{provider}")
    // @Route("/authenticate")
    // @Route("/authenticate/{provider}")
    @DataContract
    public static class Authenticate implements IReturn<AuthenticateResponse>
    {
        @DataMember(Order=1)
        public String provider = null;

        @DataMember(Order=2)
        public String State = null;

        @DataMember(Order=3)
        public String oauth_token = null;

        @DataMember(Order=4)
        public String oauth_verifier = null;

        @DataMember(Order=5)
        public String UserName = null;

        @DataMember(Order=6)
        public String Password = null;

        @DataMember(Order=7)
        public Boolean RememberMe = null;

        @DataMember(Order=8)
        public String Continue = null;

        @DataMember(Order=9)
        public String nonce = null;

        @DataMember(Order=10)
        public String uri = null;

        @DataMember(Order=11)
        public String response = null;

        @DataMember(Order=12)
        public String qop = null;

        @DataMember(Order=13)
        public String nc = null;

        @DataMember(Order=14)
        public String cnonce = null;

        @DataMember(Order=15)
        public HashMap<String,String> Meta = null;
        
        public String getProvider() { return provider; }
        public Authenticate setProvider(String value) { this.provider = value; return this; }
        public String getState() { return State; }
        public Authenticate setState(String value) { this.State = value; return this; }
        public String getOauthToken() { return oauth_token; }
        public Authenticate setOauthToken(String value) { this.oauth_token = value; return this; }
        public String getOauthVerifier() { return oauth_verifier; }
        public Authenticate setOauthVerifier(String value) { this.oauth_verifier = value; return this; }
        public String getUserName() { return UserName; }
        public Authenticate setUserName(String value) { this.UserName = value; return this; }
        public String getPassword() { return Password; }
        public Authenticate setPassword(String value) { this.Password = value; return this; }
        public Boolean isRememberMe() { return RememberMe; }
        public Authenticate setRememberMe(Boolean value) { this.RememberMe = value; return this; }
        public String getContinue() { return Continue; }
        public Authenticate setContinue(String value) { this.Continue = value; return this; }
        public String getNonce() { return nonce; }
        public Authenticate setNonce(String value) { this.nonce = value; return this; }
        public String getUri() { return uri; }
        public Authenticate setUri(String value) { this.uri = value; return this; }
        public String getResponse() { return response; }
        public Authenticate setResponse(String value) { this.response = value; return this; }
        public String getQop() { return qop; }
        public Authenticate setQop(String value) { this.qop = value; return this; }
        public String getNc() { return nc; }
        public Authenticate setNc(String value) { this.nc = value; return this; }
        public String getCnonce() { return cnonce; }
        public Authenticate setCnonce(String value) { this.cnonce = value; return this; }
        public HashMap<String,String> getMeta() { return Meta; }
        public Authenticate setMeta(HashMap<String,String> value) { this.Meta = value; return this; }
        private static Object responseType = AuthenticateResponse.class;
        public Object getResponseType() { return responseType; }
    }

    @Route("/assignroles")
    public static class AssignRoles implements IReturn<AssignRolesResponse>
    {
        public String UserName = null;
        public ArrayList<String> Permissions = null;
        public ArrayList<String> Roles = null;
        
        public String getUserName() { return UserName; }
        public AssignRoles setUserName(String value) { this.UserName = value; return this; }
        public ArrayList<String> getPermissions() { return Permissions; }
        public AssignRoles setPermissions(ArrayList<String> value) { this.Permissions = value; return this; }
        public ArrayList<String> getRoles() { return Roles; }
        public AssignRoles setRoles(ArrayList<String> value) { this.Roles = value; return this; }
        private static Object responseType = AssignRolesResponse.class;
        public Object getResponseType() { return responseType; }
    }

    @Route("/unassignroles")
    public static class UnAssignRoles implements IReturn<UnAssignRolesResponse>
    {
        public String UserName = null;
        public ArrayList<String> Permissions = null;
        public ArrayList<String> Roles = null;
        
        public String getUserName() { return UserName; }
        public UnAssignRoles setUserName(String value) { this.UserName = value; return this; }
        public ArrayList<String> getPermissions() { return Permissions; }
        public UnAssignRoles setPermissions(ArrayList<String> value) { this.Permissions = value; return this; }
        public ArrayList<String> getRoles() { return Roles; }
        public UnAssignRoles setRoles(ArrayList<String> value) { this.Roles = value; return this; }
        private static Object responseType = UnAssignRolesResponse.class;
        public Object getResponseType() { return responseType; }
    }

    @Route("/posts")
    public static class QueryPosts extends QueryBase_1<Post> implements IReturn<QueryResponse<Post>>
    {
        
        private static Object responseType = new TypeToken<QueryResponse<Post>>(){}.getType();
        public Object getResponseType() { return responseType; }
    }

}
