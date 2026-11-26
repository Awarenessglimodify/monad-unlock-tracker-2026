using System;
using System.IO;
using System.Text.Json;

namespace MonadUnlockTracker;

/// <summary>
/// User settings for the tracker: current price, circulating supply and daily volume.
/// The defaults are sample values, not live market data.
/// </summary>
public class TrackerSettings
{
    public decimal CurrentPrice { get; set; } = 0.05m;

    public decimal CirculatingSupply { get; set; } = 1000000000m;

    public decimal DailyVolume { get; set; } = 20000000m;
}

/// <summary>
/// Loads and saves the tracker settings as a JSON file in the user application data folder.
/// No wallet address, key or personal data is stored.
/// </summary>
public class SettingsStore
{
    private static readonly JsonSerializerOptions Options = new JsonSerializerOptions
    {
        WriteIndented = true
    };

    public SettingsStore()
    {
        string folder = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            "MonadUnlockTracker");

        SettingsPath = Path.Combine(folder, "settings.json");
    }

    public string SettingsPath { get; }

    /// <summary>
    /// Loads the settings. Returns the defaults when the file is missing or unreadable.
    /// </summary>
    public TrackerSettings Load()
    {
        try
        {
            if (!File.Exists(SettingsPath))
            {
                return new TrackerSettings();
            }

            string json = File.ReadAllText(SettingsPath);
            TrackerSettings settings = JsonSerializer.Deserialize<TrackerSettings>(json, Options);

            return settings ?? new TrackerSettings();
        }
        catch (Exception)
        {
            return new TrackerSettings();
        }
    }

    /// <summary>
    /// Saves the settings and creates the settings folder when needed.
    /// </summary>
    public void Save(TrackerSettings settings)
    {
        if (settings == null)
        {
            return;
        }

        string folder = Path.GetDirectoryName(SettingsPath);

        if (!string.IsNullOrEmpty(folder))
        {
            Directory.CreateDirectory(folder);
        }

        string json = JsonSerializer.Serialize(settings, Options);

        File.WriteAllText(SettingsPath, json);
    }
}
