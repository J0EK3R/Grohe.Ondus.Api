using Grohe.Ondus.Api.Actions;
using Grohe.Ondus.Api.Client;
using Grohe.Ondus.Api.Model;
using System;
using System.Collections.Generic;

namespace Grohe.Ondus.Api
{
  public class OndusService
  {
    #region Constants
    private const string BASE_URL = "https://idp-apigw.cloud.grohe.com";
    #endregion

    #region Fields
    private RefreshTokenResponse refreshTokenResponse;
    private ApiClient apiClient;
    #endregion

    #region static login(String username, String password)
    /// <summary>
    /// Main entry point for the OndusService to obtain an initialized instance of it. When calling this method,
    /// the provided credentials will be checked against the GROHE Api and an access token will be saved in this
    /// OndusService instance.
    /// 
    /// The access token currently is valid for 6 months, however it will not be refreshed automatically. If it expires,
    /// you need to create a new instance of OndusService.
    /// </summary>
    /// <param name="username">The username of the GROHE account</param>
    /// <param name="password">The password of the GROHE account</param>
    /// <returns>An initialized instance of OndusService with the username or password</returns>
    public static OndusService login(String username, String password)
    {
      return login(username, password, new ApiClient(BASE_URL));
    }
    #endregion
    #region static login(String refreshToken)
    /// <summary>
    /// Main entry point for the OndusService to obtain an initialized instance of it. When calling this method,
    /// the provided refreshAuthorization token will be used to obtain a fresh access token from the GROHE Api, which will be saved
    /// in this OndusService instance.
    /// 
    /// The access token currently is valid for one hour, however it will not be refreshed automatically. If it expires,
    /// you need to create a new instance of OndusService.
    /// </summary>
    /// <param name="refreshToken">The refreshTokenResponse of the GROHE account</param>
    /// <returns>An initialized instance of OndusService with the username or password</returns>
    public static OndusService login(String refreshToken)
    {
      return login(refreshToken, new ApiClient(BASE_URL));
    }
    #endregion
    #region static login(String username, String password, ApiClient apiClient)
    /// <summary>
    /// private login
    /// </summary>
    /// <param name="username">The username of the GROHE account</param>
    /// <param name="password">The password of the GROHE account</param>
    /// <param name="apiClient">APIClient</param>
    /// <returns>An initialized instance of OndusService with the username or password</returns>
    private static OndusService login(String username, String password, ApiClient apiClient)
    {
      OndusService service = new OndusService();
      service.apiClient = apiClient;

      LoginAction loginAction = apiClient.getAction<LoginAction>();

      apiClient.setToken(loginAction.getToken(username, password));
      return service;
    }
    #endregion
    #region static login(String refreshToken, ApiClient apiClient)
    /// <summary>
    /// private login
    /// </summary>
    /// <param name="refreshToken">The refreshTokenResponse of the GROHE account</param>
    /// <param name="apiClient">APIClient</param>
    /// <returns>An initialized instance of OndusService with the username or password</returns>
    private static OndusService login(String refreshToken, ApiClient apiClient)
    {
      OndusService service = new OndusService();
      service.apiClient = apiClient;

      RefreshTokenAction refreshTokenAction = apiClient.getAction<RefreshTokenAction>();
      service.refreshTokenResponse = refreshTokenAction.refresh(refreshToken);

      apiClient.setToken(service.refreshTokenResponse.accessToken);
      apiClient.setVersion(ApiClient.Version.v3);
      return service;
    }
    #endregion

    #region OndusService()
    /// <summary>
    /// Private Constructor
    /// </summary>
    private OndusService()
    {
    }
    #endregion

    #region authorizationExpiresAt()
    /// <summary>
    /// Returns the time at which the internally saved authorization will expire. It is advised that users of this class
    /// use this value after logging in to ensure that the authorization is refreshed before it actually expires.
    /// Actually refreshing the authorization is done by refreshAuthorization().
    /// </summary>
    /// <returns>The point in time when the authorization is expired</returns>
    public DateTime authorizationExpiresAt()
    {
      if (refreshTokenResponse == null)
      {
        return DateTime.MaxValue;
      }
      return refreshTokenResponse.expiresAt();
    }
    #endregion
    #region refreshAuthorization()
    /// <summary>
    /// Refreshed the internally saved authorization information (if necessary) and uses the refreshed authorization for
    /// upcoming requests to the GROHE Api.
    /// </summary>
    /// <returns>Returns the new refreshToken, which is also now saved internally in the service</returns>
    public string refreshAuthorization()
    {
      if (refreshTokenResponse == null)
      {
        return null;
      }
      RefreshTokenAction refreshTokenAction = apiClient.getAction<RefreshTokenAction>();
      this.refreshTokenResponse = refreshTokenAction.refresh(refreshTokenResponse.refreshToken);
      apiClient.setToken(this.refreshTokenResponse.accessToken);

      return refreshTokenResponse.refreshToken;
    }
    #endregion

    #region getLocations()
    /// <summary>
    /// Locations are the top-level organizational structure inside the GROHE account. They're most likely used to
    /// separate multiple buildings or houses within one account.
    /// </summary>
    /// <returns>The list of saved Locations in the GROHE account</returns>
    public List<Location> getLocations()
    {
      LocationAction action = apiClient.getAction<LocationAction>();

      return action.getLocations();
    }
    #endregion
    #region getLocation(int id)
    /// <summary>
    /// Retrieves a single Location object from the Api without querying for all locations inside the GROHE
    /// account.
    /// </summary>
    /// <param name="id">The location ID as retrieved by the GROHE Api</param>
    /// <returns>One specific Location</returns>
    public Location getLocation(int id)
    {
      LocationAction action = apiClient.getAction<LocationAction>();

      return action.getLocation(id);
    }
    #endregion

    #region getRooms(Location forLocation)
    /// <summary>
    /// A Room is an intermediate organizational structure element inside the GROHE account. It is usually
    /// used to separate multiple appliances in different rooms from each other.
    /// </summary>
    /// <param name="forLocation">The Location to look for rooms in</param>
    /// <returns>The list of saved Rooms in the GROHE account</returns>
    public List<Room> getRooms(Location forLocation)
    {
      RoomAction action = apiClient.getAction<RoomAction>();

      return action.getRooms(forLocation);
    }
    #endregion
    #region getRoom(Location inLocation, int id)
    /// <summary>
    /// Retrieves a single Room object from the Api without querying for all rooms inside the GROHE
    /// account.
    /// </summary>
    /// <param name="inLocation">The Location to look for the room in</param>
    /// <param name="id">The room ID as retrieved by the GROHE Api</param>
    /// <returns>One specific Room</returns>
    public Room getRoom(Location inLocation, int id)
    {
      RoomAction action = apiClient.getAction<RoomAction>();

      return action.getRoom(inLocation, id);
    }
    #endregion

    #region getAppliances(Room inRoom)
    /// <summary>
    /// SenseGuardAppliances are real devices from GROHE, saved inside the GROHE account. They provide an interface
    /// to the appliance's features and data.
    /// </summary>
    /// <param name="inRoom">The Room to look for appliances in</param>
    /// <returns>The list of saved SenseGuardAppliances in the GROHE account</returns>
    public List<BaseAppliance> getAppliances(Room inRoom)
    {
      ApplianceAction action = apiClient.getAction<ApplianceAction>();

      return action.getAppliances(inRoom);
    }
    #endregion
    #region getAppliance(Room inRoom, String applianceId)
    /// <summary>
    /// Retrieves a single SenseGuardAppliance object from the Api without querying for all appliances inside the GROHE
    /// account.
    /// </summary>
    /// <param name="inRoom">The Room to look for the appliance in</param>
    /// <param name="applianceId">The room ID as retrieved by the GROHE Api</param>
    /// <returns>One specific SenseGuardAppliance</returns>
    public BaseAppliance getAppliance(Room inRoom, String applianceId)
    {
      ApplianceAction action = apiClient.getAction<ApplianceAction>();

      return action.getAppliance(inRoom, applianceId);
    }
    #endregion

    #region getApplianceData(BaseAppliance appliance, DateTime? from = null, DateTime? to = null)
    /// <summary>
    /// The same as #getApplianceData(BaseAppliance), however, limits the requested data to a specific time range
    /// instead of requesting all data from all time.
    /// </summary>
    /// <param name="appliance">The BaseAppliance to retrieve data from</param>
    /// <param name="from">Needs to be an instance of DateTime which is at least one day before to</param>
    /// <param name="to">Needs to be an instance of DateTime which is at least one day after from</param>
    /// <returns>The SenseGuardApplianceData of the appliance in the given time range</returns>
    public BaseApplianceData getApplianceData(BaseAppliance appliance, DateTime? from = null, DateTime? to = null)
    {
      ApplianceAction action = apiClient.getAction<ApplianceAction>();

      return action.getApplianceData(appliance, from, to);
    }
    #endregion

    #region getApplianceCommand(SenseGuardAppliance appliance)
    /// <summary>
    /// Retrieves the current state of the appliances {@link ApplianceCommand} saved for the appliance in the
    /// GROHE account. This can be used to inspect the current state of the appliance and activated/queued commands.
    /// </summary>
    /// <param name="appliance">The SenseGuardAppliance to retrieve command information from</param>
    /// <returns>The ApplianceCommand of the appliance</returns>
    public ApplianceCommand getApplianceCommand(SenseGuardAppliance appliance)
    {
      ApplianceAction action = apiClient.getAction<ApplianceAction>();

      return action.getApplianceCommand(appliance);
    }
    #endregion

    #region getApplianceStatus(BaseAppliance appliance)
    /// <summary>
    /// Retrieves the current status of the appliance. Note that the available properties of the returned ApplianceStatus
    /// object may differ from appliance type to appliance type.
    /// </summary>
    /// <param name="appliance">The BaseAppliance to retrieve command information from</param>
    /// <returns>The ApplianceStatus of the appliance</returns>
    public ApplianceStatus getApplianceStatus(BaseAppliance appliance)
    {
      ApplianceAction action = apiClient.getAction<ApplianceAction>();

      return action.getApplianceStatus(appliance);
    }
    #endregion

    #region setValveOpen(SenseGuardAppliance appliance, bool open)
    /// <summary>
    /// Changes the valve state of the appliance. The call to this function is blocking until the API acknowledges the
    /// execution or failure of the command.
    /// </summary>
    /// <param name="appliance">The appliance to change the valve state of</param>
    /// <param name="open">The requested valve state</param>
    public void setValveOpen(SenseGuardAppliance appliance, bool open)
    {
      ApplianceAction action = apiClient.getAction<ApplianceAction>();

      ApplianceCommand applianceCommandOptional = getApplianceCommand(appliance);

      if (applianceCommandOptional == null)
      {
        return;
      }

      ApplianceCommand applianceCommand = applianceCommandOptional;
      ApplianceCommand.Command command = applianceCommand.getCommand();
      command.setValveOpen(open);
      applianceCommand.setCommand(command);

      action.putApplianceCommand(appliance, applianceCommand);
    }
    #endregion
  }
}
