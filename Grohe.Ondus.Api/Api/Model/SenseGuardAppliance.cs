using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Grohe.Ondus.Api.Model
{
  public class SenseGuardAppliance : BaseAppliance
  {
    #region Constants
    public const int TYPE = 103;
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
      private List<Threshold> thresholds = null;
      [JsonProperty("measurement_period")]
      private int measurementPeriod;
      [JsonProperty("measurement_transmission_intervall")]
      private int measurementTransmissionIntervall;
      [JsonProperty("measurement_transmission_intervall_offset")]
      private int measurementTransmissionIntervallOffset;
      [JsonProperty("action_on_major_leakage")]
      private int actionOnMajorLeakage;
      [JsonProperty("action_on_minor_leakage")]
      private int actionOnMinorLeakage;
      [JsonProperty("action_on_micro_leakage")]
      private int actionOnMicroLeakage;
      [JsonProperty("monitor_frost_alert")]
      private bool monitorFrostAlert;
      [JsonProperty("monitor_lower_flow_limit")]
      private bool monitorLowerFlowLimit;
      [JsonProperty("monitor_upper_flow_limit")]
      private bool monitorUpperFlowLimit;
      [JsonProperty("monitor_lower_pressure_limit")]
      private bool monitorLowerPressureLimit;
      [JsonProperty("monitor_upper_pressure_limit")]
      private bool monitorUpperPressureLimit;
      [JsonProperty("monitor_lower_temperature_limit")]
      private bool monitorLowerTemperatureLimit;
      [JsonProperty("monitor_upper_temperature_limit")]
      private bool monitorUpperTemperatureLimit;
      [JsonProperty("monitor_major_leakage")]
      private bool monitorMajorLeakage;
      [JsonProperty("monitor_minor_leakage")]
      private bool monitorMinorLeakage;
      [JsonProperty("monitor_micro_leakage")]
      private bool monitorMicroLeakage;
      [JsonProperty("monitor_system_error")]
      private bool monitorSystemError;
      [JsonProperty("monitor_btw_0_1_and_0_8_leakage")]
      private bool monitorBtw01And08Leakage;
      [JsonProperty("monitor_withdrawel_amount_limit_breach")]
      private bool monitorWithdrawelAmountLimitBreach;
      [JsonProperty("detection_interval")]
      private int detectionInterval;
      [JsonProperty("impulse_ignore")]
      private int impulseIgnore;
      [JsonProperty("time_ignore")]
      private int timeIgnore;
      [JsonProperty("pressure_tolerance_band")]
      private int pressureToleranceBand;
      [JsonProperty("pressure_drop")]
      private int pressureDrop;
      [JsonProperty("detection_time")]
      private int detectionTime;
      [JsonProperty("action_on_btw_0_1_and_0_8_leakage")]
      private int actionOnBtw01And08Leakage;
      [JsonProperty("action_on_withdrawel_amount_limit_breach")]
      private int actionOnWithdrawelAmountLimitBreach;
      [JsonProperty("withdrawel_amount_limit")]
      private int withdrawelAmountLimit;
      [JsonProperty("sprinkler_mode_start_time")]
      private int sprinklerModeStartTime;
      [JsonProperty("sprinkler_mode_stop_time")]
      private int sprinklerModeStopTime;
      [JsonProperty("sprinkler_mode_active_monday")]
      private bool sprinklerModeActiveMonday;
      [JsonProperty("sprinkler_mode_active_tuesday")]
      private bool sprinklerModeActiveTuesday;
      [JsonProperty("sprinkler_mode_active_wednesday")]
      private bool sprinklerModeActiveWednesday;
      [JsonProperty("sprinkler_mode_active_thursday")]
      private bool sprinklerModeActiveThursday;
      [JsonProperty("sprinkler_mode_active_friday")]
      private bool sprinklerModeActiveFriday;
      [JsonProperty("sprinkler_mode_active_saturday")]
      private bool sprinklerModeActiveSaturday;
      [JsonProperty("sprinkler_mode_active_sunday")]
      private bool sprinklerModeActiveSunday;
    }
    #endregion

    #region Fields
    [JsonProperty]
    private Config config;
    [JsonProperty("registration_complete")]
    private bool registrationComplete;
    [JsonProperty("calculate_average_since")]
    private string calculateAverageSince;
    #endregion

    #region SenseGuardAppliance(String applianceId, Room inRoom)
    public SenseGuardAppliance(String applianceId, Room inRoom)
      : base(applianceId, inRoom)
    {
    }
    #endregion
  }
}
