using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Grohe.Ondus.Api.Model
{
  public class SenseAppliance : BaseAppliance
  {
    #region Constants
    public const int TYPE = 101;
    #endregion

    #region class Threshold
    public class Threshold
    {
      [JsonProperty("quantity")]
      private string quantity;
      [JsonProperty("type")]
      private string type;
      [JsonProperty("value")]
      private int value;
      [JsonProperty("enabled")]
      private bool enabled;
    }
    #endregion
    #region class Config
    public class Config
    {
      [JsonProperty]
      private List<Threshold> thresholds = null;
    }
    #endregion

    #region Fields
    [JsonProperty]
    private Config config;
    #endregion

    #region SenseAppliance(String applianceId, Room inRoom)
    public SenseAppliance(String applianceId, Room inRoom)
      : base(applianceId, inRoom)
    {
    }
    #endregion
  }
}
