using System;

namespace MonadUnlockTracker;

/// <summary>
/// Unlock calculator for the MON token.
/// Turns an unlock amount into sell pressure values and a price impact estimate.
/// </summary>
public class UnlockCalculator
{
    /// <summary>
    /// Calculates the sell pressure for an unlock.
    /// Formula: sellValue = unlockAmount * price * (sellPercentage / 100).
    /// </summary>
    public SellPressureResult CalculateSellPressure(decimal unlockAmount, decimal price, decimal circulatingSupply, decimal sellPercentage)
    {
        decimal safeUnlock = Math.Max(unlockAmount, 0m);
        decimal safePrice = Math.Max(price, 0m);
        decimal safeSupply = Math.Max(circulatingSupply, 0m);
        decimal safePercentage = Math.Clamp(sellPercentage, 0m, 100m);

        decimal fullValue = safeUnlock * safePrice;
        decimal sellValue = fullValue * (safePercentage / 100m);
        decimal percentOfSupply = safeSupply > 0m ? (safeUnlock / safeSupply) * 100m : 0m;

        return new SellPressureResult
        {
            DollarValue = Math.Round(sellValue, 2),
            PercentOfSupply = Math.Round(percentOfSupply, 2),
            SellValueAt25 = Math.Round(fullValue * 0.25m, 2),
            SellValueAt50 = Math.Round(fullValue * 0.50m, 2),
            SellValueAt100 = Math.Round(fullValue, 2)
        };
    }

    /// <summary>
    /// Estimates the price impact of a sell value against the daily volume.
    /// The estimate is a share of the daily volume, not a guaranteed move.
    /// </summary>
    public ImpactEstimate EstimatePriceImpact(decimal sellValue, decimal dailyVolume)
    {
        if (dailyVolume <= 0m)
        {
            return new ImpactEstimate
            {
                EstimatedDropPercent = 0m,
                Confidence = "Low"
            };
        }

        decimal safeSellValue = Math.Max(sellValue, 0m);
        decimal ratio = safeSellValue / dailyVolume;
        decimal drop = Math.Clamp(ratio * 100m, 0m, 100m);

        string confidence;

        if (ratio <= 0.10m)
        {
            confidence = "High";
        }
        else if (ratio <= 0.35m)
        {
            confidence = "Medium";
        }
        else
        {
            confidence = "Low";
        }

        return new ImpactEstimate
        {
            EstimatedDropPercent = Math.Round(drop, 2),
            Confidence = confidence
        };
    }
}
