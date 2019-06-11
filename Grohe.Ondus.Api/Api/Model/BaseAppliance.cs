using Newtonsoft.Json;
using System;

namespace Grohe.Ondus.Api.Model
{
  public class BaseAppliance
  {
    #region Fields
    [JsonProperty("appliance_id")]
    string applianceId;
    [JsonProperty("installation_date")]
    string installationDate;
    [JsonProperty]
    string name;
    [JsonProperty("serial_number")]
    string serialNumber;
    [JsonProperty]
    int type;
    [JsonProperty]
    string version;
    [JsonProperty]
    string tdt;
    [JsonProperty]
    int timezone;
    [JsonProperty]
    string role;
    [JsonIgnore]
    Room room = new Room(0, null);
    #endregion

    #region BaseAppliance(String applianceId, Room inRoom)
    public BaseAppliance(String applianceId, Room inRoom)
    {
      this.applianceId = applianceId;
      this.room = inRoom;
    }
    #endregion

    #region getRoom()
    public Room getRoom()
    {
      return this.room;
    }
    #endregion
    #region setRoom(Room room)
    public void setRoom(Room room)
    {
      this.room = room;
    }
    #endregion

    #region getApplianceId()
    public string getApplianceId()
    {
      return this.applianceId;
    }
    #endregion

    #region getType()
    public int getType()
    {
      return this.type;
    }
    #endregion

    #region ToString()
    public override string ToString()
    {
      return $"{this.name}";
    }
    #endregion
  }
}
