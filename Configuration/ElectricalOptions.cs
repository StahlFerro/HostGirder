namespace HostGirder;

public class ElectricalOptions
{
    public BatteryMode BatteryMode { get; set; } = BatteryMode.Offline;
}

public enum BatteryMode
{
    Offline = 0,
    Suspend = 1,
    Discharge = 2,
    Recharge = 3,
}