using Newtonsoft.Json;

namespace Grohe.Ondus.Api.Model
{
  public class Location
  {
    #region Fields
    [JsonProperty]
    public int id;
    [JsonProperty]
    public string name;
    [JsonProperty]
    public int type;
    [JsonProperty]
    public string role;
    [JsonProperty]
    public string timezone;
    [JsonProperty("water_cost")]
    public float waterCost;
    [JsonProperty("energy_cost")]
    public float energyCost;
    [JsonProperty("heating_type")]
    public int heatingType;
    [JsonProperty("currency")]
    public string currency;
    [JsonProperty("default_water_cost")]
    public float defaultWaterCost;
    [JsonProperty("default_energy_cost")]
    public float defaultEnergyCost;
    [JsonProperty("default_heating_type")]
    public int defaultHeatingType;
    [JsonProperty("emergency_shutdown_enable")]
    public bool emergencyShutdownEnable;
    [JsonProperty]
    public Address address;
    #endregion

    #region Location(int id)
    public Location(int id)
    {
      this.id = id;
    }
    #endregion

    #region getId()
    public int getId()
    {
      return id;
    }
    #endregion

    #region ToString()
    public override string ToString()
    {
      return $"{this.name} [{this.address}]";
    }
    #endregion
  }
}
