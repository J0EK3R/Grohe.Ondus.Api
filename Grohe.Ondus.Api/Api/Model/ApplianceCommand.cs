using Newtonsoft.Json;

namespace Grohe.Ondus.Api.Model
{
  public class ApplianceCommand
  {
    #region class Command
    public class Command
    {
      [JsonProperty("measure_now")]
      public bool measureNow;
      [JsonProperty("buzzer_on")]
      public bool buzzerOn;
      [JsonProperty("buzzer_sound_profile")]
      public int buzzerSoundProfile;
      [JsonProperty("valve_open")]
      public bool valveOpen;
      [JsonProperty("temp_user_unlock_on")]
      public bool tempUserUnlockOn;

      #region setValveOpen(bool valveOpen)
      public void setValveOpen(bool valveOpen)
      {
        this.valveOpen = valveOpen;
      }
      #endregion
    }
    #endregion

    #region Fields
    [JsonProperty("appliance_id")]
    public string applianceId;
    [JsonProperty]
    public int type;
    [JsonProperty]
    public Command command;
    [JsonIgnore]
    private SenseGuardAppliance appliance = new SenseGuardAppliance(string.Empty, null);
    #endregion

    #region ApplianceCommand()
    public ApplianceCommand()
    {
    }
    #endregion
    #region ApplianceCommand(SenseGuardAppliance forAppliance)
    public ApplianceCommand(SenseGuardAppliance forAppliance)
    {
      this.appliance = forAppliance;
      this.applianceId = forAppliance.getApplianceId();
    }
    #endregion

    #region setAppliance(SenseGuardAppliance appliance)
    public void setAppliance(SenseGuardAppliance appliance)
    {
      this.appliance = appliance;
    }
    #endregion

    #region getCommand()
    public Command getCommand()
    {
      return this.command;
    }
    #endregion
    #region setCommand(Command command)
    public void setCommand(Command command)
    {
      this.command = command;
    }
    #endregion
  }
}
