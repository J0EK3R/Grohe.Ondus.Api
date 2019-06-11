using Newtonsoft.Json;

namespace Grohe.Ondus.Api.Model
{
  public class Room
  {
    #region Fields
    [JsonProperty]
    private int id;
    [JsonProperty]
    private string name;
    [JsonProperty]
    private int type;
    [JsonProperty("room_type")]
    private int roomType;
    [JsonProperty]
    private string role;
    [JsonIgnore]
    private Location location = new Location(0);
    #endregion

    #region Room(int id, Location location)
    public Room(int id, Location location)
    {
      this.id = id;
      this.location = location;
    }
    #endregion

    #region getLocation()
    public Location getLocation()
    {
      return this.location;
    }
    #endregion
    #region setLocation(Location location)
    public void setLocation(Location location)
    {
      this.location = location;
    }
    #endregion

    #region getId()
    public int getId()
    {
      return this.id;
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
