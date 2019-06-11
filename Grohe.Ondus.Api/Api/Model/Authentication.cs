using Newtonsoft.Json;

namespace Grohe.Ondus.Api.Model
{
  public class Authentication
  {
    #region class IotAttributes
    public class IotAttributes
    {
      [JsonProperty("user_id")]
      public string userId;
      [JsonProperty]
      public string language;
      [JsonProperty("contact_via_sms")]
      public bool contactViaSms;
      [JsonProperty("contact_via_call")]
      public bool contactViaCall;
      [JsonProperty("contact_via_email")]
      public bool contactViaEmail;
      [JsonProperty]
      public string username;
      [JsonProperty]
      public string firstname;
      [JsonProperty]
      public string lastname;
      [JsonProperty("email_address")]
      public string emailAddress;
      [JsonProperty("phone_number")]
      public string phoneNumber;
    }
    #endregion
    #region class UserAttributes
    public class UserAttributes
    {
      [JsonProperty]
      public string username;
      [JsonProperty]
      public bool emailVerified;
      [JsonProperty]
      public string firstName;
      [JsonProperty]
      public string lastName;
      [JsonProperty]
      public string email;
      [JsonProperty]
      public string language;
      [JsonProperty]
      public string country;
      [JsonProperty]
      public bool hasPassword;
    }
    #endregion

    [JsonProperty]
    public string token;
    [JsonProperty]
    public string uid;
    [JsonProperty("user_attributes")]
    public UserAttributes userAttributes;
    [JsonProperty("iot_attributes")]
    public IotAttributes iotAttributes;
  }
}
