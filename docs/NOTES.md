# Developer Notes

## Scope

Monad Unlock Tracker is a read-only tracker for the MON token unlock schedule.
It helps readers estimate the sell pressure of one unlock and inspect the linear
vesting schedule. The tracker never connects to a wallet, never signs a
transaction and never reads on-chain balances.

## Published tokenomics

The reference assumptions follow the published tokenomics: a cliff on November 24,
then linear vesting over 3 years. Community summaries describe the linear phase as
roughly 104M MON per week. The exact values can change, so the calculator and the
vesting schedule always use the numbers that the user enters.

## Unlock calculator

The sell pressure formula is:

`sellValue = unlockAmount * price * (sellPercentage / 100)`

The calculator returns the dollar value of the selected sell share, the share of the
circulating supply, and the sell value at 25%, 50% and 100%.

`EstimatePriceImpact` compares the sell value with the daily volume and returns an
estimated drop percentage with a confidence label. A small sell value relative to
the daily volume gives a higher confidence.

## Vesting schedule

`GetSchedule(startDate, totalAmount, weeks)` splits the total amount into equal
weekly parts. The last week takes the remainder so the sum stays exact.
`GetLargestWeeks(count)` returns the top weeks by amount. Under a linear schedule
the weekly amounts are equal, so the first weeks win the tie.

## Updating the data

1. Check the official Monad announcement for the current unlock terms.
2. Update the default values in `TrackerSettings` when the terms change.
3. Update `docs/NOTES.md` and `CHANGELOG.md` with the new reference values.
4. Keep the calculator formulas unchanged unless the published method changes.

## Settings

Settings are stored as JSON in the user application data folder.
The file keeps the current price, the circulating supply and the daily volume.
No wallet address, key or personal data is stored.

## Limitations

Price impact is an estimate, not a guarantee. The estimate ignores order book
depth, market makers and news flow. Unlock terms can change with a new
announcement. Use the tracker as a reference, not as financial advice.
