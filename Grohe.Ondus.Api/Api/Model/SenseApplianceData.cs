using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Grohe.Ondus.Api.Model
{
  public class SenseApplianceData : BaseApplianceData
  {
    #region class Measurement
    public class Measurement
    {
      [JsonProperty("timestamp")]
      public string timestamp;
      [JsonProperty("temperature")]
      public float temperature;
      [JsonProperty("humidity")]
      public float humidity;

      #region ToString()
      public override string ToString()
      {
        return $"{this.timestamp} Temperature: {this.temperature} Humidity: {this.humidity}";
      }
      #endregion
    }
    #endregion
    #region class Data
    public class Data
    {
      [JsonProperty("measurement")]
      public List<Measurement> measurement = null;
    }
    #endregion

    #region Fields
    [JsonProperty("data")]
    public Data data;
    #endregion

    #region SenseApplianceData(String applianceId, BaseAppliance appliance)
    public SenseApplianceData(String applianceId, BaseAppliance appliance)
      : base(applianceId, appliance)
    {
    }
    #endregion
  }
}
