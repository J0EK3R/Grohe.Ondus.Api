using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Grohe.Ondus.Api.Model
{
  public class ApplianceStatus
  {
    #region Constants
    private const string TYPE_BATTERY = "battery";
    private const string TYPE_UPDATE_AVAILABLE = "update_available";
    #endregion

    #region class ApplianceStatusModel
    public class ApplianceStatusModel
    {
      [JsonProperty]
      private string type;
      [JsonProperty]
      private string value;

      #region ToString()
      public override string ToString()
      {
        return $"{this.type}: {this.value}";
      }
      #endregion
    }
    #endregion

    #region Fields
    private BaseAppliance appliance = new BaseAppliance(string.Empty, null);
    private List<ApplianceStatusModel> statuses;
    #endregion

    #region ApplianceStatus(BaseAppliance appliance)
    public ApplianceStatus(BaseAppliance appliance)
    {
      this.appliance = appliance;
    }
    #endregion
    #region ApplianceStatus(BaseAppliance appliance, ApplianceStatusModel[] applianceStatuses)
    public ApplianceStatus(BaseAppliance appliance, ApplianceStatusModel[] applianceStatuses)
      :this(appliance)
    {
      this.statuses = applianceStatuses.ToList();
    }
    #endregion

    #region getApplianceId()
    public String getApplianceId()
    {
      return appliance.getApplianceId();
    }
    #endregion

    #region getBatteryStatus()
    public int getBatteryStatus()
    {
      //return statuses.stream()
      //        .filter(status->TYPE_BATTERY.equals(status.getType()))
      //        .findFirst()
      //        .map(applianceStatusModel->Integer.valueOf(applianceStatusModel.getValue()))
      //        .orElse(-1);
      return -1;
    }
    #endregion
    #region isUpdateAvailable()
    public bool isUpdateAvailable()
    {
      //return statuses.stream()
      //        .filter(status->TYPE_UPDATE_AVAILABLE.equals(status.getType()))
      //        .findFirst()
      //        .map(applianceStatusModel->applianceStatusModel.getValue().equals("1"))
      //        .orElse(false);
      return false;
    }
    #endregion
  }
}
