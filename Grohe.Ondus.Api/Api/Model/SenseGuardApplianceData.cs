using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Grohe.Ondus.Api.Model
{
  public class SenseGuardApplianceData : BaseApplianceData
  {
    #region class Withdrawals
    public class Withdrawals
    {
      [JsonProperty("starttime")]
      public DateTime starttime;
      [JsonProperty("stoptime")]
      public DateTime stoptime;
      [JsonProperty("waterconsumption")]
      public float waterconsumption;
      [JsonProperty("maxflowrate")]
      public float maxflowrate;

      #region ToString()
      public override string ToString()
      {
        return $"{this.starttime}-{this.stoptime}: Waterconsumption: {this.waterconsumption} Maxflowrate: {this.maxflowrate}";
      }
      #endregion
    }
    #endregion
    #region class Measurement
    public class Measurement
    {
      [JsonProperty("timestamp")]
      public string timestamp;
      [JsonProperty("flowrate")] // ToDo: int flowrate -> float flowrate
      public float flowrate;
      [JsonProperty("pressure")]
      public float pressure;
      [JsonProperty("temperature_guard")]
      public float temperatureGuard;

      #region ToString()
      public override string ToString()
      {
        return $"{this.timestamp}: Flowrate: {this.flowrate} Pressure: {this.pressure} Temperature: {this.temperatureGuard}";
      }
      #endregion
    }
    #endregion
    #region class Data
    public class Data
    {
      [JsonProperty("measurement")]
      public List<Measurement> measurement = null;
      [JsonProperty("withdrawals")]
      public List<Withdrawals> withdrawals = null;
    }
    #endregion

    #region Fields
    [JsonProperty("data")]
    public Data data;
    #endregion

    #region SenseGuardApplianceData(String applianceId, BaseAppliance appliance)
    public SenseGuardApplianceData(String applianceId, BaseAppliance appliance)
      : base(applianceId, appliance)
    {
    }
    #endregion
  }
}
