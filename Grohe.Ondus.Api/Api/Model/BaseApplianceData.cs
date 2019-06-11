using Newtonsoft.Json;
using System;

namespace Grohe.Ondus.Api.Model
{
  public class BaseApplianceData
  {
    [JsonProperty("appliance_id")]
    public string applianceId;
    [JsonProperty("type")]
    public int type;
    [JsonIgnore]
    public BaseAppliance appliance;

    #region BaseApplianceData(String applianceId, BaseAppliance appliance)
    public BaseApplianceData(String applianceId, BaseAppliance appliance)
    {
      this.applianceId = applianceId;
      this.appliance = appliance;
    }
    #endregion

    #region setAppliance(BaseAppliance appliance)
    public void setAppliance(BaseAppliance appliance)
    {
      this.appliance = appliance;
    }
    #endregion
  }
}
