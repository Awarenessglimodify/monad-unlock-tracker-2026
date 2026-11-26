using System;

namespace MonadUnlockTracker;

/// <summary>
/// Result of the sell pressure calculation for one unlock event.
/// </summary>
public class SellPressureResult
{
    public decimal DollarValue { get; set; }

    public decimal PercentOfSupply { get; set; }

    public decimal SellValueAt25 { get; set; }

    public decimal SellValueAt50 { get; set; }

    public decimal SellValueAt100 { get; set; }
}

/// <summary>
/// Estimated price impact for a sell value against the daily volume.
/// </summary>
public class ImpactEstimate
{
    public decimal EstimatedDropPercent { get; set; }

    public string Confidence { get; set; } = string.Empty;
}

/// <summary>
/// One week of the linear vesting schedule.
/// </summary>
public class VestingWeek
{
    public int WeekNumber { get; set; }

    public DateTime Date { get; set; }

    public decimal Amount { get; set; }
}
