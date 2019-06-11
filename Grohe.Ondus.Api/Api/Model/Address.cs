using Newtonsoft.Json;

namespace Grohe.Ondus.Api.Model
{
  public class Address
  {
    #region Fields
    [JsonProperty]
    public string street;
    [JsonProperty]
    public string city;
    [JsonProperty]
    public string zipcode;
    [JsonProperty]
    public string housenumber;
    [JsonProperty]
    public string country;
    [JsonProperty("country_code")]
    public string countryCode;
    [JsonProperty]
    public string additionalInfo;
    #endregion

    #region ToString()
    public override string ToString()
    {
      return $"{this.street} {this.housenumber}, {this.zipcode} {this.city}, {this.country}";
    }
    #endregion
  }
}
