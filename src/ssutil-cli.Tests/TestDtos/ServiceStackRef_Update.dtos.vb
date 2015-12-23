' Options:
'Date: 2015-12-23 23:52:35
'Version: 4.051
'Tip: To override a DTO option, remove "''" prefix before updating
'BaseUrl: http://techstacks.io
'
'''GlobalNamespace: 
'''MakePartial: True
'''MakeVirtual: True
'''MakeDataContractsExtensible: False
'''AddReturnMarker: True
'''AddDescriptionAsComments: True
'''AddDataContractAttributes: False
'''AddIndexesToDataMembers: False
'''AddGeneratedCodeAttributes: False
'''AddResponseStatus: False
'''AddImplicitVersion: 
'''InitializeCollections: True
'''IncludeTypes: 
'''ExcludeTypes: 
'''AddDefaultXmlNamespace: http://schemas.servicestack.net/types

Imports System
Imports System.Collections
Imports System.Collections.Generic
Imports System.Runtime.Serialization
Imports ServiceStack
Imports ServiceStack.DataAnnotations
Imports TechStacks.ServiceModel.Types
Imports TechStacks.ServiceModel
Imports TechStacks.ServiceInterface

Namespace Global

    Namespace TechStacks.ServiceInterface

        <Route("/tech")>
        Public Partial Class ClientAllTechnologies
        End Class

        <Route("/stacks")>
        Public Partial Class ClientAllTechnologyStacks
        End Class

        <Route("/tech/{Slug}")>
        Public Partial Class ClientTechnology
            Public Overridable Property Slug As String
        End Class

        <Route("/users/{UserName}")>
        Public Partial Class ClientUser
            Public Overridable Property UserName As String
        End Class

        <Route("/{PathInfo*}")>
        Public Partial Class FallbackForClientRoutes
            Public Overridable Property PathInfo As String
        End Class

        <Route("/ping")>
        Public Partial Class Ping
        End Class
    End Namespace

    Namespace TechStacks.ServiceModel

        <Route("/favorites/technology/{TechnologyId}", "PUT")>
        Public Partial Class AddFavoriteTechnology
            Implements IReturn(Of FavoriteTechnologyResponse)
            Public Overridable Property TechnologyId As Integer
        End Class

        <Route("/favorites/techtacks/{TechnologyStackId}", "PUT")>
        Public Partial Class AddFavoriteTechStack
            Implements IReturn(Of FavoriteTechStackResponse)
            Public Overridable Property TechnologyStackId As Integer
        End Class

        <Route("/app-overview")>
        Public Partial Class AppOverview
            Implements IReturn(Of AppOverviewResponse)
            Public Overridable Property Reload As Boolean
        End Class

        Public Partial Class AppOverviewResponse
            Public Sub New()
                AllTiers = New List(Of Option)
                TopTechnologies = New List(Of TechnologyInfo)
            End Sub

            Public Overridable Property Created As Date
            Public Overridable Property AllTiers As List(Of Option)
            Public Overridable Property TopTechnologies As List(Of TechnologyInfo)
            Public Overridable Property ResponseStatus As ResponseStatus
        End Class

        <Route("/technology", "POST")>
        Public Partial Class CreateTechnology
            Implements IReturn(Of CreateTechnologyResponse)
            Public Overridable Property Name As String
            Public Overridable Property VendorName As String
            Public Overridable Property VendorUrl As String
            Public Overridable Property ProductUrl As String
            Public Overridable Property LogoUrl As String
            Public Overridable Property Description As String
            Public Overridable Property IsLocked As Boolean
            Public Overridable Property Tier As TechnologyTier
        End Class

        Public Partial Class CreateTechnologyResponse
            Public Overridable Property Result As Technology
            Public Overridable Property ResponseStatus As ResponseStatus
        End Class

        <Route("/techstacks", "POST")>
        Public Partial Class CreateTechnologyStack
            Implements IReturn(Of CreateTechnologyStackResponse)
            Public Sub New()
                TechnologyIds = New List(Of Long)
            End Sub

            Public Overridable Property Name As String
            Public Overridable Property VendorName As String
            Public Overridable Property AppUrl As String
            Public Overridable Property ScreenshotUrl As String
            Public Overridable Property Description As String
            Public Overridable Property Details As String
            Public Overridable Property IsLocked As Boolean
            Public Overridable Property TechnologyIds As List(Of Long)
        End Class

        Public Partial Class CreateTechnologyStackResponse
            Public Overridable Property Result As TechStackDetails
            Public Overridable Property ResponseStatus As ResponseStatus
        End Class

        <Route("/technology/{Id}", "DELETE")>
        Public Partial Class DeleteTechnology
            Implements IReturn(Of DeleteTechnologyResponse)
            Public Overridable Property Id As Long
        End Class

        Public Partial Class DeleteTechnologyResponse
            Public Overridable Property Result As Technology
            Public Overridable Property ResponseStatus As ResponseStatus
        End Class

        <Route("/techstacks/{Id}", "DELETE")>
        Public Partial Class DeleteTechnologyStack
            Implements IReturn(Of DeleteTechnologyStackResponse)
            Public Overridable Property Id As Long
        End Class

        Public Partial Class DeleteTechnologyStackResponse
            Public Overridable Property Result As TechStackDetails
            Public Overridable Property ResponseStatus As ResponseStatus
        End Class

        Public Partial Class FavoriteTechnologyResponse
            Public Overridable Property Result As Technology
        End Class

        Public Partial Class FavoriteTechStackResponse
            Public Overridable Property Result As TechnologyStack
        End Class

        <Route("/technology/search")>
        <AutoQueryViewer(Title:="Find Technologies", Description:="Explore different Technologies", IconUrl:="/img/app/tech-white-75.png", DefaultSearchField:="Tier", DefaultSearchType:="=", DefaultSearchText:="Data")>
        Public Partial Class FindTechnologies
            Inherits QueryBase(Of Technology)
            Implements IReturn(Of QueryResponse(Of Technology))
            Public Overridable Property Name As String
            Public Overridable Property Reload As Boolean
        End Class

        <Route("/techstacks/search")>
        <AutoQueryViewer(Title:="Find Technology Stacks", Description:="Explore different Technology Stacks", IconUrl:="/img/app/stacks-white-75.png", DefaultSearchField:="Description", DefaultSearchType:="Contains", DefaultSearchText:="ServiceStack")>
        Public Partial Class FindTechStacks
            Inherits QueryBase(Of TechnologyStack)
            Implements IReturn(Of QueryResponse(Of TechnologyStack))
            Public Overridable Property Reload As Boolean
        End Class

        <Route("/technology", "GET")>
        Public Partial Class GetAllTechnologies
            Implements IReturn(Of GetAllTechnologiesResponse)
        End Class

        Public Partial Class GetAllTechnologiesResponse
            Public Sub New()
                Results = New List(Of Technology)
            End Sub

            Public Overridable Property Results As List(Of Technology)
        End Class

        <Route("/techstacks", "GET")>
        Public Partial Class GetAllTechnologyStacks
            Implements IReturn(Of GetAllTechnologyStacksResponse)
        End Class

        Public Partial Class GetAllTechnologyStacksResponse
            Public Sub New()
                Results = New List(Of TechnologyStack)
            End Sub

            Public Overridable Property Results As List(Of TechnologyStack)
        End Class

        <Route("/config")>
        Public Partial Class GetConfig
            Implements IReturn(Of GetConfigResponse)
        End Class

        Public Partial Class GetConfigResponse
            Public Sub New()
                AllTiers = New List(Of Option)
            End Sub

            Public Overridable Property AllTiers As List(Of Option)
        End Class

        <Route("/favorites/technology", "GET")>
        Public Partial Class GetFavoriteTechnologies
            Implements IReturn(Of GetFavoriteTechnologiesResponse)
            Public Overridable Property TechnologyId As Integer
        End Class

        Public Partial Class GetFavoriteTechnologiesResponse
            Public Sub New()
                Results = New List(Of Technology)
            End Sub

            Public Overridable Property Results As List(Of Technology)
        End Class

        <Route("/favorites/techtacks", "GET")>
        Public Partial Class GetFavoriteTechStack
            Implements IReturn(Of GetFavoriteTechStackResponse)
            Public Overridable Property TechnologyStackId As Integer
        End Class

        Public Partial Class GetFavoriteTechStackResponse
            Public Sub New()
                Results = New List(Of TechnologyStack)
            End Sub

            Public Overridable Property Results As List(Of TechnologyStack)
        End Class

        <Route("/pagestats/{Type}/{Slug}")>
        Public Partial Class GetPageStats
            Implements IReturn(Of GetPageStatsResponse)
            Public Overridable Property Type As String
            Public Overridable Property Slug As String
        End Class

        Public Partial Class GetPageStatsResponse
            Public Overridable Property Type As String
            Public Overridable Property Slug As String
            Public Overridable Property ViewCount As Long
            Public Overridable Property FavCount As Long
        End Class

        <Route("/technology/{Slug}")>
        Public Partial Class GetTechnology
            Implements IReturn(Of GetTechnologyResponse)
            Public Overridable Property Reload As Boolean
            Public Overridable Property Slug As String
        End Class

        <Route("/technology/{Slug}/favorites")>
        Public Partial Class GetTechnologyFavoriteDetails
            Implements IReturn(Of GetTechnologyFavoriteDetailsResponse)
            Public Overridable Property Slug As String
            Public Overridable Property Reload As Boolean
        End Class

        Public Partial Class GetTechnologyFavoriteDetailsResponse
            Public Sub New()
                Users = New List(Of String)
            End Sub

            Public Overridable Property Users As List(Of String)
            Public Overridable Property FavoriteCount As Integer
        End Class

        <Route("/technology/{Slug}/previous-versions", "GET")>
        Public Partial Class GetTechnologyPreviousVersions
            Implements IReturn(Of GetTechnologyPreviousVersionsResponse)
            Public Overridable Property Slug As String
        End Class

        Public Partial Class GetTechnologyPreviousVersionsResponse
            Public Sub New()
                Results = New List(Of TechnologyHistory)
            End Sub

            Public Overridable Property Results As List(Of TechnologyHistory)
        End Class

        Public Partial Class GetTechnologyResponse
            Public Sub New()
                TechnologyStacks = New List(Of TechnologyStack)
            End Sub

            Public Overridable Property Created As Date
            Public Overridable Property Technology As Technology
            Public Overridable Property TechnologyStacks As List(Of TechnologyStack)
            Public Overridable Property ResponseStatus As ResponseStatus
        End Class

        <Route("/techstacks/{Slug}", "GET")>
        Public Partial Class GetTechnologyStack
            Implements IReturn(Of GetTechnologyStackResponse)
            Public Overridable Property Reload As Boolean
            Public Overridable Property Slug As String
        End Class

        <Route("/techstacks/{Slug}/favorites")>
        Public Partial Class GetTechnologyStackFavoriteDetails
            Implements IReturn(Of GetTechnologyStackFavoriteDetailsResponse)
            Public Overridable Property Slug As String
            Public Overridable Property Reload As Boolean
        End Class

        Public Partial Class GetTechnologyStackFavoriteDetailsResponse
            Public Sub New()
                Users = New List(Of String)
            End Sub

            Public Overridable Property Users As List(Of String)
            Public Overridable Property FavoriteCount As Integer
        End Class

        <Route("/techstacks/{Slug}/previous-versions", "GET")>
        Public Partial Class GetTechnologyStackPreviousVersions
            Implements IReturn(Of GetTechnologyStackPreviousVersionsResponse)
            Public Overridable Property Slug As String
        End Class

        Public Partial Class GetTechnologyStackPreviousVersionsResponse
            Public Sub New()
                Results = New List(Of TechnologyStackHistory)
            End Sub

            Public Overridable Property Results As List(Of TechnologyStackHistory)
        End Class

        Public Partial Class GetTechnologyStackResponse
            Public Overridable Property Created As Date
            Public Overridable Property Result As TechStackDetails
            Public Overridable Property ResponseStatus As ResponseStatus
        End Class

        <Route("/my-feed")>
        Public Partial Class GetUserFeed
            Implements IReturn(Of GetUserFeedResponse)
        End Class

        Public Partial Class GetUserFeedResponse
            Public Sub New()
                Results = New List(Of TechStackDetails)
            End Sub

            Public Overridable Property Results As List(Of TechStackDetails)
        End Class

        <Route("/userinfo/{UserName}")>
        Public Partial Class GetUserInfo
            Implements IReturn(Of GetUserInfoResponse)
            Public Overridable Property Reload As Boolean
            Public Overridable Property UserName As String
        End Class

        Public Partial Class GetUserInfoResponse
            Public Sub New()
                TechStacks = New List(Of TechnologyStack)
                FavoriteTechStacks = New List(Of TechnologyStack)
                FavoriteTechnologies = New List(Of Technology)
            End Sub

            Public Overridable Property UserName As String
            Public Overridable Property Created As Date
            Public Overridable Property AvatarUrl As String
            Public Overridable Property TechStacks As List(Of TechnologyStack)
            Public Overridable Property FavoriteTechStacks As List(Of TechnologyStack)
            Public Overridable Property FavoriteTechnologies As List(Of Technology)
        End Class

        Public Partial Class LockStackResponse
        End Class

        <Route("/admin/technology/{TechnologyId}/lock")>
        Public Partial Class LockTech
            Implements IReturn(Of LockStackResponse)
            Public Overridable Property TechnologyId As Long
            Public Overridable Property IsLocked As Boolean
        End Class

        <Route("/admin/techstacks/{TechnologyStackId}/lock")>
        Public Partial Class LockTechStack
            Implements IReturn(Of LockStackResponse)
            Public Overridable Property TechnologyStackId As Long
            Public Overridable Property IsLocked As Boolean
        End Class

        <Route("/admin/technology/{TechnologyId}/logo")>
        Public Partial Class LogoUrlApproval
            Implements IReturn(Of LogoUrlApprovalResponse)
            Public Overridable Property TechnologyId As Long
            Public Overridable Property Approved As Boolean
        End Class

        Public Partial Class LogoUrlApprovalResponse
            Public Overridable Property Result As Technology
        End Class

        <DataContract>
        Public Partial Class Option
            <DataMember(Name:="name")>
            Public Overridable Property Name As String

            <DataMember(Name:="title")>
            Public Overridable Property Title As String

            <DataMember(Name:="value")>
            Public Overridable Property Value As Nullable(Of TechnologyTier)
        End Class

        <Route("/overview")>
        Public Partial Class Overview
            Implements IReturn(Of OverviewResponse)
            Public Overridable Property Reload As Boolean
        End Class

        Public Partial Class OverviewResponse
            Public Sub New()
                TopUsers = New List(Of UserInfo)
                TopTechnologies = New List(Of TechnologyInfo)
                LatestTechStacks = New List(Of TechStackDetails)
                PopularTechStacks = New List(Of TechnologyStack)
                TopTechnologiesByTier = New Dictionary(Of TechnologyTier, List(Of TechnologyInfo))
            End Sub

            Public Overridable Property Created As Date
            Public Overridable Property TopUsers As List(Of UserInfo)
            Public Overridable Property TopTechnologies As List(Of TechnologyInfo)
            Public Overridable Property LatestTechStacks As List(Of TechStackDetails)
            Public Overridable Property PopularTechStacks As List(Of TechnologyStack)
            Public Overridable Property TopTechnologiesByTier As Dictionary(Of TechnologyTier, List(Of TechnologyInfo))
            Public Overridable Property ResponseStatus As ResponseStatus
        End Class

        <Route("/favorites/technology/{TechnologyId}", "DELETE")>
        Public Partial Class RemoveFavoriteTechnology
            Implements IReturn(Of FavoriteTechnologyResponse)
            Public Overridable Property TechnologyId As Integer
        End Class

        <Route("/favorites/techtacks/{TechnologyStackId}", "DELETE")>
        Public Partial Class RemoveFavoriteTechStack
            Implements IReturn(Of FavoriteTechStackResponse)
            Public Overridable Property TechnologyStackId As Integer
        End Class

        <Route("/my-session")>
        Public Partial Class SessionInfo
        End Class

        Public Partial Class TechnologyInfo
            Public Overridable Property Tier As TechnologyTier
            Public Overridable Property Slug As String
            Public Overridable Property Name As String
            Public Overridable Property LogoUrl As String
            Public Overridable Property StacksCount As Integer
        End Class

        Public Partial Class TechnologyInStack
            Inherits TechnologyBase
            Public Overridable Property TechnologyId As Long
            Public Overridable Property TechnologyStackId As Long
            Public Overridable Property Justification As String
        End Class

        Public Partial Class TechStackDetails
            Inherits TechnologyStackBase
            Public Sub New()
                TechnologyChoices = New List(Of TechnologyInStack)
            End Sub

            Public Overridable Property DetailsHtml As String
            Public Overridable Property TechnologyChoices As List(Of TechnologyInStack)
        End Class

        <Route("/technology/{Id}", "PUT")>
        Public Partial Class UpdateTechnology
            Implements IReturn(Of UpdateTechnologyResponse)
            Public Overridable Property Id As Long
            Public Overridable Property Name As String
            Public Overridable Property VendorName As String
            Public Overridable Property VendorUrl As String
            Public Overridable Property ProductUrl As String
            Public Overridable Property LogoUrl As String
            Public Overridable Property Description As String
            Public Overridable Property IsLocked As Boolean
            Public Overridable Property Tier As TechnologyTier
        End Class

        Public Partial Class UpdateTechnologyResponse
            Public Overridable Property Result As Technology
            Public Overridable Property ResponseStatus As ResponseStatus
        End Class

        <Route("/techstacks/{Id}", "PUT")>
        Public Partial Class UpdateTechnologyStack
            Implements IReturn(Of UpdateTechnologyStackResponse)
            Public Sub New()
                TechnologyIds = New List(Of Long)
            End Sub

            Public Overridable Property Id As Long
            Public Overridable Property Name As String
            Public Overridable Property VendorName As String
            Public Overridable Property AppUrl As String
            Public Overridable Property ScreenshotUrl As String
            Public Overridable Property Description As String
            Public Overridable Property Details As String
            Public Overridable Property IsLocked As Boolean
            Public Overridable Property TechnologyIds As List(Of Long)
        End Class

        Public Partial Class UpdateTechnologyStackResponse
            Public Overridable Property Result As TechStackDetails
            Public Overridable Property ResponseStatus As ResponseStatus
        End Class

        Public Partial Class UserInfo
            Public Overridable Property UserName As String
            Public Overridable Property AvatarUrl As String
            Public Overridable Property StacksCount As Integer
        End Class
    End Namespace

    Namespace TechStacks.ServiceModel.Types

        Public Partial Class Technology
            Inherits TechnologyBase
        End Class

        Public Partial Class TechnologyBase
            Public Overridable Property Id As Long
            Public Overridable Property Name As String
            Public Overridable Property VendorName As String
            Public Overridable Property VendorUrl As String
            Public Overridable Property ProductUrl As String
            Public Overridable Property LogoUrl As String
            Public Overridable Property Description As String
            Public Overridable Property Created As Date
            Public Overridable Property CreatedBy As String
            Public Overridable Property LastModified As Date
            Public Overridable Property LastModifiedBy As String
            Public Overridable Property OwnerId As String
            Public Overridable Property Slug As String
            Public Overridable Property LogoApproved As Boolean
            Public Overridable Property IsLocked As Boolean
            Public Overridable Property Tier As TechnologyTier
            Public Overridable Property LastStatusUpdate As Nullable(Of Date)
        End Class

        Public Partial Class TechnologyHistory
            Inherits TechnologyBase
            Public Overridable Property TechnologyId As Long
            Public Overridable Property Operation As String
        End Class

        Public Partial Class TechnologyStack
            Inherits TechnologyStackBase
        End Class

        Public Partial Class TechnologyStackBase
            Public Overridable Property Id As Long
            Public Overridable Property Name As String
            Public Overridable Property VendorName As String
            Public Overridable Property Description As String
            Public Overridable Property AppUrl As String
            Public Overridable Property ScreenshotUrl As String
            Public Overridable Property Created As Date
            Public Overridable Property CreatedBy As String
            Public Overridable Property LastModified As Date
            Public Overridable Property LastModifiedBy As String
            Public Overridable Property IsLocked As Boolean
            Public Overridable Property OwnerId As String
            Public Overridable Property Slug As String
            Public Overridable Property Details As String
            Public Overridable Property LastStatusUpdate As Nullable(Of Date)
        End Class

        Public Partial Class TechnologyStackHistory
            Inherits TechnologyStackBase
            Public Sub New()
                TechnologyIds = New List(Of Long)
            End Sub

            Public Overridable Property TechnologyStackId As Long
            Public Overridable Property Operation As String
            Public Overridable Property TechnologyIds As List(Of Long)
        End Class

        Public Enum TechnologyTier
            ProgrammingLanguage
            Client
            Http
            Server
            Data
            SoftwareInfrastructure
            OperatingSystem
            HardwareInfrastructure
            ThirdPartyServices
        End Enum
    End Namespace
End Namespace

