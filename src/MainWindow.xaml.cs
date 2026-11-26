using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;

namespace MonadUnlockTracker;

/// <summary>
/// Main window of the Monad unlock tracker.
/// Hosts the unlock calculator and the linear vesting schedule.
/// </summary>
public partial class MainWindow : Window
{
    private readonly UnlockCalculator _unlockCalculator = new UnlockCalculator();

    private readonly VestingTracker _vestingTracker = new VestingTracker();

    private readonly SettingsStore _settingsStore = new SettingsStore();

    private TrackerSettings _settings = new TrackerSettings();

    public MainWindow()
    {
        InitializeComponent();
    }

    private void Window_Loaded(object sender, RoutedEventArgs e)
    {
        _settings = _settingsStore.Load();

        PriceTextBox.Text = _settings.CurrentPrice.ToString(CultureInfo.InvariantCulture);
        SupplyTextBox.Text = _settings.CirculatingSupply.ToString(CultureInfo.InvariantCulture);
        VolumeTextBox.Text = _settings.DailyVolume.ToString(CultureInfo.InvariantCulture);

        UnlockAmountTextBox.Text = "104000000";
        SellPercentTextBox.Text = "25";

        StartDateTextBox.Text = DateTime.Today.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
        TotalAmountTextBox.Text = "10000000000";
        WeeksTextBox.Text = "156";

        BuildSchedule();
    }

    private void CalculateButton_Click(object sender, RoutedEventArgs e)
    {
        if (!TryReadDecimal(UnlockAmountTextBox.Text, out decimal unlockAmount) ||
            !TryReadDecimal(PriceTextBox.Text, out decimal price) ||
            !TryReadDecimal(SupplyTextBox.Text, out decimal circulatingSupply) ||
            !TryReadDecimal(SellPercentTextBox.Text, out decimal sellPercentage) ||
            !TryReadDecimal(VolumeTextBox.Text, out decimal dailyVolume))
        {
            ResultTextBlock.Text = "Enter valid numbers for amount, price, supply, sell percentage and daily volume.";
            return;
        }

        SellPressureResult result = _unlockCalculator.CalculateSellPressure(
            unlockAmount,
            price,
            circulatingSupply,
            sellPercentage);

        ImpactEstimate impact = _unlockCalculator.EstimatePriceImpact(result.DollarValue, dailyVolume);

        string percentText = sellPercentage.ToString("0.##", CultureInfo.InvariantCulture);

        ResultTextBlock.Text =
            "Dollar value at " + percentText + "% sell: $" + FormatMoney(result.DollarValue) + Environment.NewLine +
            "Share of circulating supply: " + result.PercentOfSupply.ToString("N2", CultureInfo.InvariantCulture) + "%" + Environment.NewLine +
            "Sell value at 25%: $" + FormatMoney(result.SellValueAt25) + Environment.NewLine +
            "Sell value at 50%: $" + FormatMoney(result.SellValueAt50) + Environment.NewLine +
            "Sell value at 100%: $" + FormatMoney(result.SellValueAt100) + Environment.NewLine +
            "Estimated price drop: " + impact.EstimatedDropPercent.ToString("N2", CultureInfo.InvariantCulture) + "%" + Environment.NewLine +
            "Estimate confidence: " + impact.Confidence;
    }

    private void SaveSettingsButton_Click(object sender, RoutedEventArgs e)
    {
        SaveSettingsFromInputs();
        ResultTextBlock.Text = "Settings saved to " + _settingsStore.SettingsPath;
    }

    private void BuildScheduleButton_Click(object sender, RoutedEventArgs e)
    {
        BuildSchedule();
    }

    private void BuildSchedule()
    {
        if (!DateTime.TryParseExact(
                StartDateTextBox.Text.Trim(),
                "yyyy-MM-dd",
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out DateTime startDate))
        {
            ScheduleSummaryTextBlock.Text = "Enter the start date as yyyy-MM-dd.";
            return;
        }

        if (!TryReadDecimal(TotalAmountTextBox.Text, out decimal totalAmount) || totalAmount <= 0m)
        {
            ScheduleSummaryTextBlock.Text = "Enter a positive total amount.";
            return;
        }

        if (!int.TryParse(
                WeeksTextBox.Text.Trim(),
                NumberStyles.Integer,
                CultureInfo.InvariantCulture,
                out int weeks) || weeks <= 0)
        {
            ScheduleSummaryTextBlock.Text = "Enter a positive number of weeks.";
            return;
        }

        List<VestingWeek> schedule = _vestingTracker.GetSchedule(startDate, totalAmount, weeks);
        ScheduleDataGrid.ItemsSource = schedule;

        List<VestingWeek> largest = _vestingTracker.GetLargestWeeks(5);
        decimal weeklyAmount = totalAmount / weeks;

        string largestText = string.Join(", ", largest.Select(week => "W" + week.WeekNumber.ToString(CultureInfo.InvariantCulture)));

        ScheduleSummaryTextBlock.Text =
            "Weeks: " + schedule.Count.ToString(CultureInfo.InvariantCulture) +
            " · Weekly amount: " + weeklyAmount.ToString("N0", CultureInfo.InvariantCulture) + " MON" +
            " · First unlock: " + schedule[0].Date.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture) + Environment.NewLine +
            "Largest weeks: " + largestText + " (equal amounts under a linear schedule)";
    }

    private void Window_Closed(object sender, EventArgs e)
    {
        SaveSettingsFromInputs();
    }

    private void SaveSettingsFromInputs()
    {
        if (TryReadDecimal(PriceTextBox.Text, out decimal price))
        {
            _settings.CurrentPrice = price;
        }

        if (TryReadDecimal(SupplyTextBox.Text, out decimal supply))
        {
            _settings.CirculatingSupply = supply;
        }

        if (TryReadDecimal(VolumeTextBox.Text, out decimal volume))
        {
            _settings.DailyVolume = volume;
        }

        _settingsStore.Save(_settings);
    }

    private static bool TryReadDecimal(string text, out decimal value)
    {
        return decimal.TryParse(text, NumberStyles.Number, CultureInfo.InvariantCulture, out value);
    }

    private static string FormatMoney(decimal value)
    {
        return value.ToString("N2", CultureInfo.InvariantCulture);
    }
}
