namespace LibAoE2net.Tests;

using AoE2NetDesktop.LibAoE2Net.Functions;
using AoE2NetDesktop.LibAoE2Net.JsonFormat;
using AoE2NetDesktop.LibAoE2Net.Parameters;
using AoE2NetDesktop.Utility;
using AoE2NetDesktop.Utility.SysApi;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

[TestClass]
public class StringsExtTests
{
    [TestMethod]
    [DataRow(-1, null)]
    [DataRow(0, null)]
    [DataRow(1, "testString1")]
    [DataRow(2, "testString21")]
    [DataRow(3, "testString3")]
    [DataRow(4, null)]
    public void GetStringTest(int? id, string expVal)
    {
        // Arrange
        List<StringId> stringIds = new()
        {
            new StringId() { Id = 1, String = "testString1" },
            new StringId() { Id = 2, String = "testString21" },
            new StringId() { Id = 2, String = "testString22" },
            new StringId() { Id = 3, String = "testString3" },
        };

        // Act
        var actVal = stringIds.GetString(id);

        // Assert
        Assert.AreEqual(expVal, actVal);
    }

    [TestMethod]
    public void GetOpenedTimeTest()
    {
        // Arrange
        DateTimeExt.TimeZoneInfo = TimeZoneInfo.Local;
        var expVal = new DateTime(1970, 1, 1);
        var dateTimeSec = (expVal - DateTimeExt.TimeZoneInfo.BaseUtcOffset).ToUnixTimeSeconds();

        // Act
        var testClass = new Match() {
            Opened = dateTimeSec,
        };

        var actVal = testClass.GetOpenedTime();

        // Assert
        Assert.AreEqual(expVal.ToString(), actVal.ToString());
    }

    [TestMethod]
    [DataRow(0, "invalid civ:0")]
    [DataRow(1, "ブリトン")]
    [DataRow(37, "Sicilians")]
    [DataRow(40, "invalid civ:40")]
    [DataRow(null, "invalid civ:")]
    [SuppressMessage("Usage", "VSTHRD002:Avoid problematic synchronous waits", Justification = SuppressReason.IntentionalSyncTest)]
    [SuppressMessage("Usage", "VSTHRD104:Offer async methods", Justification = SuppressReason.IntentionalSyncTest)]
    public void GetCivNameTest(int? civ, string expVal)
    {
        // Arrange
        var player = new Player() {
            Civ = civ,
        };

        // Act
        _ = Task.Run(() => StringsExt.InitAsync(Language.ja)).Result;

        var actVal = player.GetCivName();

        // Assert
        Assert.AreEqual(expVal, actVal);

        // cleanup
        StringsExt.InitAsync(Language.en).Wait();
    }

    [TestMethod]
    public void GetColorStringTestPlayerN()
    {
        // Arrange
        Player player = null;

        // Act
        // Assert
        Assert.ThrowsException<ArgumentNullException>(() =>
        {
            _ = player.GetColorString();
        });
    }
}
