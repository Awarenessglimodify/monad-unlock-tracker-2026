using System;
using System.Collections.Generic;

namespace MonadUnlockTracker;

/// <summary>
/// Vesting tracker for the MON token.
/// Builds the linear weekly schedule and reports the largest weeks.
/// </summary>
public class VestingTracker
{
    private readonly List<VestingWeek> _schedule = new List<VestingWeek>();

    /// <summary>
    /// Builds a linear vesting schedule.
    /// Each week receives totalAmount / weeks, and the last week takes the remainder.
    /// </summary>
    public List<VestingWeek> GetSchedule(DateTime startDate, decimal totalAmount, int weeks)
    {
        List<VestingWeek> schedule = new List<VestingWeek>();

        _schedule.Clear();

        if (weeks <= 0 || totalAmount <= 0m)
        {
            return schedule;
        }

        decimal weeklyAmount = totalAmount / weeks;
        decimal distributed = 0m;

        for (int index = 0; index < weeks; index++)
        {
            decimal amount = index == weeks - 1 ? totalAmount - distributed : weeklyAmount;
            distributed = distributed + amount;

            schedule.Add(new VestingWeek
            {
                WeekNumber = index + 1,
                Date = startDate.AddDays(7 * index),
                Amount = amount
            });
        }

        _schedule.AddRange(schedule);

        return schedule;
    }

    /// <summary>
    /// Returns the largest weeks by amount.
    /// Under a linear schedule the amounts are equal, so the first weeks win the tie.
    /// </summary>
    public List<VestingWeek> GetLargestWeeks(int count)
    {
        List<VestingWeek> ordered = new List<VestingWeek>(_schedule);

        ordered.Sort((left, right) =>
        {
            int byAmount = right.Amount.CompareTo(left.Amount);

            if (byAmount != 0)
            {
                return byAmount;
            }

            return left.WeekNumber.CompareTo(right.WeekNumber);
        });

        if (count <= 0)
        {
            return new List<VestingWeek>();
        }

        if (count > ordered.Count)
        {
            count = ordered.Count;
        }

        return ordered.GetRange(0, count);
    }

    /// <summary>
    /// Returns the stored schedule without rebuilding it.
    /// </summary>
    public List<VestingWeek> GetStoredSchedule()
    {
        return new List<VestingWeek>(_schedule);
    }
}
