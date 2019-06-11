using Grohe.Ondus.Api.Client;
using Grohe.Ondus.Api.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Grohe.Ondus.Api.Actions
{
  public class ApplianceAction : AbstractionAction
  {
    #region class BaseApplianceList
    public class BaseApplianceList : List<BaseAppliance>
    {
    }
    #endregion
    #region class SenseGuardApplianceList
    public class SenseGuardApplianceList : List<SenseGuardAppliance>
    {
    }
    #endregion
    #region class SenseApplianceList
    public class SenseApplianceList : List<SenseAppliance>
    {
    }
    #endregion

    #region Constants
    //private const string APPLIANCES_URL_TEMPLATE = "iot/locations/%d/rooms/%d/appliances";
    private const string APPLIANCES_URL_TEMPLATE = "iot/locations/{0}/rooms/{1}/appliances";
    //private const string APPLIANCE_URL_TEMPLATE = "iot/locations/%d/rooms/%d/appliances/%s";
    private const string APPLIANCE_URL_TEMPLATE = "iot/locations/{0}/rooms/{1}/appliances/{2}";
    //private const string APPLIANCE_DATA_URL_TEMPLATE = "iot/locations/%d/rooms/%d/appliances/%s/data";
    private const string APPLIANCE_DATA_URL_TEMPLATE = "iot/locations/{0}/rooms/{1}/appliances/{2}/data";
    //private const string APPLIANCE_DATA_WITH_RANGE_URL_TEMPLATE = "iot/locations/%d/rooms/%d/appliances/%s/data?from=%s&to=%s";
    private const string APPLIANCE_DATA_WITH_RANGE_URL_TEMPLATE = "iot/locations/{0}/rooms/{1}/appliances/{2}/data?from={3}&to={4}";
    //private const string APPLIANCE_COMMAND_URL_TEMPLATE = "iot/locations/%d/rooms/%d/appliances/%s/command";
    private const string APPLIANCE_COMMAND_URL_TEMPLATE = "iot/locations/{0}/rooms/{1}/appliances/{2}/command";
    //private const string APPLIANCE_STATUS_URL_TEMPLATE = "iot/locations/%d/rooms/%d/appliances/%s/status";
    private const string APPLIANCE_STATUS_URL_TEMPLATE = "iot/locations/{0}/rooms/{1}/appliances/{2}/status";
    #endregion

    #region getAppliances(Room inRoom)
    public List<BaseAppliance> getAppliances(Room inRoom)
    {
      ApiResponse<BaseAppliance[]> locationsResponse = getApiClient()
        .get<BaseAppliance[]>(String.Format(getApiClient().apiPath() + APPLIANCES_URL_TEMPLATE, inRoom.getLocation().getId(), inRoom.getId()));

      if (locationsResponse.getStatusCode() != 200)
      {
        return new List<BaseAppliance>();
      }

      List<BaseAppliance> appliances = (locationsResponse.getContent() ?? new BaseAppliance[] { }).ToList();

      //return appliances.stream().peek(appliance->appliance.setRoom(inRoom)).collect(Collectors.toList());
      appliances.ForEach(appliance => appliance.setRoom(inRoom));

      return appliances;
    }
    #endregion

    #region getAppliance(Room inRoom, String applianceId)
    public BaseAppliance getAppliance(Room inRoom, String applianceId)
    {
      //ApiResponse<List> applianceApiResponse = getApiClient().get<List>(String.Format(getApiClient().apiPath() + APPLIANCE_URL_TEMPLATE, inRoom.getLocation().getId(), inRoom.getId(), applianceId));
      ApiResponse<ArrayList> applianceApiResponse = getApiClient()
        .get<ArrayList>(String.Format(getApiClient().apiPath() + APPLIANCE_URL_TEMPLATE, inRoom.getLocation().getId(), inRoom.getId(), applianceId));

      if (applianceApiResponse.getStatusCode() != 200)
      {
        return null;
      }

      BaseAppliance applianceOptional = null;
      BaseApplianceList applianceListOptional = applianceApiResponse.getContentAs<BaseApplianceList>();
      if (applianceListOptional != null)
      {
        BaseApplianceList applianceList = applianceListOptional;
        if (applianceList.Count == 1)
        {
          BaseAppliance appliance = applianceList[0];

          switch (appliance.getType())
          {
            case SenseGuardAppliance.TYPE:
              appliance = applianceApiResponse.getContentAs<SenseGuardApplianceList>()[0];
              break;
            case SenseAppliance.TYPE:
              appliance = applianceApiResponse.getContentAs<SenseApplianceList>()[0];
              break;
          }

          appliance.setRoom(inRoom);
          applianceOptional = appliance;
        }
      }

      return applianceOptional;
    }
    #endregion

    #region getApplianceData(BaseAppliance appliance)
    public BaseApplianceData getApplianceData(BaseAppliance appliance)
    {
      return this.getApplianceData(appliance, null, null);
    }
    #endregion

    #region getApplianceData(BaseAppliance appliance, DateTime? from, DateTime? to)
    public BaseApplianceData getApplianceData(BaseAppliance appliance, DateTime? from, DateTime? to)
    {
      ApiResponse<BaseApplianceData> applianceApiResponse = getApiClient()
        .get<BaseApplianceData>(createApplianceDataRequestUrl(appliance, from, to));

      if (applianceApiResponse.getStatusCode() != 200)
      {
        return null;
      }

      BaseApplianceData applianceOptional = applianceApiResponse.getContent();

      if (applianceOptional != null)
      {
        BaseApplianceData applianceData = applianceOptional;

        switch (applianceData.type)
        {
          case SenseGuardAppliance.TYPE:

            applianceData = applianceApiResponse.getContentAs<SenseGuardApplianceData>();
            break;
          case SenseAppliance.TYPE:
            applianceData = applianceApiResponse.getContentAs<SenseApplianceData>();
            break;
        }

        applianceData.setAppliance(appliance);
        applianceOptional = applianceData;
      }

      return applianceOptional;
    }
    #endregion

    #region createApplianceDataRequestUrl(BaseAppliance appliance, DateTime? from, DateTime? to)
    private String createApplianceDataRequestUrl(BaseAppliance appliance, DateTime? from, DateTime? to)
    {
      if (from == null ||
        to == null)
      {
        return String.Format(getApiClient().apiPath() + APPLIANCE_DATA_URL_TEMPLATE,
          appliance.getRoom().getLocation().getId(),
          appliance.getRoom().getId(),
          appliance.getApplianceId());
      }

      return String.Format(getApiClient().apiPath() + APPLIANCE_DATA_WITH_RANGE_URL_TEMPLATE,
        appliance.getRoom().getLocation().getId(),
        appliance.getRoom().getId(),
        appliance.getApplianceId(),
        createOndusDateString(from.Value),
        createOndusDateString(to.Value));
    }
    #endregion

    #region createOndusDateString(DateTime from)
    private String createOndusDateString(DateTime from)
    {
      //return new SimpleDateFormat("yyyy-MM-dd").format(Date.from(from));
      return from.ToString("yyyy-MM-dd");
    }
    #endregion

    #region getApplianceCommand(SenseGuardAppliance appliance)
    public ApplianceCommand getApplianceCommand(SenseGuardAppliance appliance)
    {
      ApiResponse<ApplianceCommand> applianceApiResponse = getApiClient()
        .get<ApplianceCommand>(String.Format(getApiClient().apiPath() + APPLIANCE_COMMAND_URL_TEMPLATE,
           appliance.getRoom().getLocation().getId(),
           appliance.getRoom().getId(),
           appliance.getApplianceId()
        ));

      if (applianceApiResponse.getStatusCode() != 200)
      {
        return null;
      }

      ApplianceCommand applianceDataOptional = applianceApiResponse.getContent();

      if (applianceDataOptional != null)
      {
        ApplianceCommand applianceData = applianceDataOptional;
        applianceData.setAppliance(appliance);
        applianceDataOptional = applianceData;
      }

      return applianceDataOptional;
    }
    #endregion

    #region putApplianceCommand(SenseGuardAppliance appliance, ApplianceCommand command)
    public void putApplianceCommand(SenseGuardAppliance appliance, ApplianceCommand command)
    {
      getApiClient()
        .post<ApplianceCommand>(String.Format(getApiClient().apiPath() + APPLIANCE_COMMAND_URL_TEMPLATE,
          appliance.getRoom().getLocation().getId(),
          appliance.getRoom().getId(),
          appliance.getApplianceId()),
          command);
    }
    #endregion
    #region getApplianceStatus(BaseAppliance appliance)
    public ApplianceStatus getApplianceStatus(BaseAppliance appliance)
    {
      ApiResponse<ApplianceStatus.ApplianceStatusModel[]> applianceApiResponse = getApiClient()
        .get<ApplianceStatus.ApplianceStatusModel[]>(String.Format(getApiClient().apiPath() + APPLIANCE_STATUS_URL_TEMPLATE,
           appliance.getRoom().getLocation().getId(),
           appliance.getRoom().getId(),
           appliance.getApplianceId()
        ));

      if (applianceApiResponse.getStatusCode() != 200)
      {
        return null;
      }

      ApplianceStatus.ApplianceStatusModel[] applianceStatusesOptional = applianceApiResponse.getContent();
      ApplianceStatus applianceStatusOptional = null;
      if (applianceStatusesOptional != null)
      {
        ApplianceStatus.ApplianceStatusModel[] applianceStatuses = applianceStatusesOptional;
        ApplianceStatus applianceStatus = new ApplianceStatus(appliance, applianceStatuses);
        applianceStatusOptional = applianceStatus;
      }

      return applianceStatusOptional;
    }
    #endregion
  }
}
