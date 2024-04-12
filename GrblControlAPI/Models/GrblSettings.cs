namespace GrblControlAPI.Models;

/*public class GrblSetting
{

}*/
public enum GrblSettingName
{
    StepPulseTime = 0,
    StepIdleDelay = 1,
    StepPulseInvert = 2,
    StepDirectionInvert = 3,
    InvertStepEnablePin = 4,
    InvertLimitPins = 5,
    InvertProbePin = 6,
    StatusReportOptions = 10,
    JunctionDevation = 11,
    ArcTolerance = 12,
    ReportInInches = 13,
    SoftLimitsEnable = 20,
    HardLimitsEnable = 21,
    HomingCycleEnable = 22,
    HomingDirectionInvert = 23,
    HomingLocateFeedRate = 24,
    HomingSearchSeekRate = 25,
    HomingSwitchDebounceDelay = 26,
    HomingSwitchPulloffDistance = 27,
    MaximumSpindleSpeed = 30,
    MinimumSpindleSpeed = 31,
    LaserModeEnable = 32,
    X_AxisTravelResolution = 100,
    Y_AxisTravelResolution = 101,
    Z_AxisTravelResolution = 102,
    X_AxisMaximumRate = 110,
    Y_AxisMaximumRate = 111,
    Z_AxisMaximumRate = 112,
    X_AxisAcceleration = 120,
    Y_AxisAcceleration = 121,
    Z_AxisAcceleration = 122,
    X_AxisMaximumTravel = 130,
    Y_AxisMaximumTravel = 131,
    Z_AxisMaximumTravel = 132
}