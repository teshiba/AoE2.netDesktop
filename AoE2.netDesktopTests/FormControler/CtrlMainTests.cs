using Microsoft.VisualStudio.TestTools.UnitTesting;
using LibAoE2net;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using System.Net.Http;
using AoE2NetDesktop.Tests;

namespace AoE2NetDesktop.From.Tests
{
    [TestClass()]
    public class CtrlMainTests
    {
        [TestMethod()]
        public void CtrlMainTest()
        {
            // Arrange
            // Act
            var testClass = new CtrlMain();

            // Assert
            Assert.AreEqual("-- Invalid Steam ID --", testClass.UserCountry);
            Assert.AreEqual("-- Invalid Steam ID --", testClass.UserName);
        }

        [TestMethod()]
        public void InitAsyncTest()
        {
            // Arrange
            AoE2net.ComClient = new TestHttpClient();

            // Act
            var testClass = new CtrlMain();
            var actVal = Task.Run(
                () => CtrlMain.InitAsync(Language.en)
                ).Result;

            // Assert
            Assert.IsTrue(actVal);
        }

        [TestMethod()]
        [DataRow(true, true, false)]
        [DataRow(false, true, true)]
        [DataRow(null, true, false)]
        public void GetFontStyleTest(bool? won, bool isBold, bool isStrikeout)
        {
            // Arrange
            var player = new Player {
                Won = won
            };
            var prototype = new Font(new Label().Font, FontStyle.Regular);

            // Act
            var expVal = CtrlMain.GetFontStyle(player, prototype);

            // Assert
            Assert.AreEqual(isBold, expVal.Bold);
            Assert.AreEqual(isStrikeout, expVal.Strikeout);
        }

        [TestMethod()]
        [DataRow(TeamType.OddColorNo, 4)]
        [DataRow(TeamType.EvenColorNo, 40)]
        public void GetAverageRateTest(TeamType teamType, int expVal)
        {
            // Arrange
            var players = new List<Player> {
                new Player { Color = 1, Rating = 1 },
                new Player { Color = 2, Rating = 10 },
                new Player { Color = 3, Rating = 3 },
                new Player { Color = 4, Rating = 30 },
                new Player { Color = 5, Rating = 5 },
                new Player { Color = 6, Rating = 50 },
                new Player { Color = 7, Rating = 7 },
                new Player { Color = 8, Rating = 70 },
            };

            // Act
            var actVal = CtrlMain.GetAverageRate(players, teamType);

            // Assert
            Assert.AreEqual(expVal, actVal);
        }

        [TestMethod()]
        [DataRow(TeamType.OddColorNo, 3)]
        [DataRow(TeamType.EvenColorNo, 30)]
        public void GetAverageRateTestIncludeRateNull(TeamType teamType, int expVal)
        {
            // Arrange
            var players = new List<Player> {
                new Player { Color = 1, Rating = 1 },
                new Player { Color = 2, Rating = 10 },
                new Player { Color = 3, Rating = 3 },
                new Player { Color = 4, Rating = 30 },
                new Player { Color = 5, Rating = 5 },
                new Player { Color = 6, Rating = 50 },
                new Player { Color = 7, Rating = null },
                new Player { Color = 8, Rating = null },
            };

            // Act
            var actVal = CtrlMain.GetAverageRate(players, teamType);

            // Assert
            Assert.AreEqual(expVal, actVal);
        }

        [TestMethod()]
        public void GetAverageRateTestArgumentOutOfRangeException()
        {
            // Arrange
            // Act
            // Assert
            Assert.ThrowsException<ArgumentOutOfRangeException>(() =>
            {
                _ = CtrlMain.GetAverageRate(new List<Player>(), (TeamType)(-1));
            });
        }

        [TestMethod()]
        [DataRow(TestInit.AvailableUserSteamId)]
        public void GetPlayerLastMatchAsyncTest(string steamId)
        {
            // Arrange
            AoE2net.ComClient = new TestHttpClient();

            // Act
            var actVal = Task.Run(
                () => CtrlMain.GetPlayerLastMatchAsync(IdType.Steam, steamId)
                ).Result;

            // Assert
            Assert.AreEqual(steamId, actVal.SteamId);
            Assert.AreEqual(3333, actVal.LastMatch.Players[2].Rating);
        }

        [TestMethod()]
        public async Task GetPlayerLastMatchAsyncTestFormatExceptionAsync()
        {
            // Arrange
            AoE2net.ComClient = new TestHttpClient();

            // Act
            // Assert
            await Assert.ThrowsExceptionAsync<FormatException>(() =>
                CtrlMain.GetPlayerLastMatchAsync(IdType.Steam, "FormatException")
            );
        }

        [TestMethod()]
        public async Task GetPlayerLastMatchAsyncTestNullAsync()
        {
            // Arrange
            AoE2net.ComClient = new TestHttpClient();

            // Act
            // Assert
            await Assert.ThrowsExceptionAsync<ArgumentNullException>(() =>
                CtrlMain.GetPlayerLastMatchAsync(IdType.Steam, null)
            );
        }

        [TestMethod()]
        public async Task GetPlayerLastMatchAsyncTestHttpRequestExceptionAsync()
        {
            // Arrange
            AoE2net.ComClient = new TestHttpClient() {
                ForceHttpRequestException = true,
            };
            // Act
            // Assert
            await Assert.ThrowsExceptionAsync<HttpRequestException>(() =>
                CtrlMain.GetPlayerLastMatchAsync(IdType.Steam, TestInit.AvailableUserSteamId)
            );
        }

        [TestMethod()]
        [DataRow(1, "1")]
        [DataRow(null, " N/A")]
        public void GetRateStringTest(int? rate, string expVal)
        {
            // Arrange
            // Act
            var actVal = CtrlMain.GetRateString(rate);

            // Assert
            Assert.AreEqual(expVal, actVal);
        }

        [TestMethod()]
        [DataRow("testName", "testName")]
        [DataRow(null, "-- AI --")]
        public void GetPlayerNameStringTest(string name, string expVal)
        {
            // Arrange
            // Act
            var actVal = CtrlMain.GetPlayerNameString(name);

            // Assert
            Assert.AreEqual(expVal, actVal);
        }

        [TestMethod()]
        public void DelayStartTest()
        {
            // Arrange
            var isCalledFunction = false;

            // Act
            var testClass = new CtrlMain() {
                Scheduler = TaskScheduler.Current,
            };

            Task function()
            {
                isCalledFunction = true;
                return Task.CompletedTask;
            };

            testClass.DelayStart(function);
            Task.Delay(1600).Wait();

            // Assert
            Assert.IsTrue(isCalledFunction);
        }

        [TestMethod()]
        public void GetPlayerDataAsyncTest()
        {
            // Arrange
            AoE2net.ComClient = new TestHttpClient();
            var expVal = "-- Invalid Steam ID --";

            // Act
            var testClass = new CtrlMain();
            var actVal = Task.Run(
                () => testClass.GetPlayerDataAsync(IdType.Steam, TestInit.AvailableUserSteamId)
                ).Result;

            // Assert
            Assert.IsTrue(actVal);
            Assert.AreNotEqual(expVal, testClass.UserCountry);
            Assert.AreNotEqual(expVal, testClass.UserName);
        }

        [TestMethod()]
        [DataRow(9, "Arabia")]
        [DataRow(0, "Unknown(Map No.0)")]
        [DataRow(null, "Unknown(Map No. N/A)")]
        public void GetMapNameTest(int? mapType, string expVal)
        {
            // Arrange
            AoE2net.ComClient = new TestHttpClient();
            var match = new Match() {
                MapType = mapType,
            };

            // Act
            var testClass = new CtrlMain();
            _ = Task.Run(
                () => CtrlMain.InitAsync(Language.en)
                ).Result;

            var actVal = match.GetMapName();

            // Assert
            Assert.AreEqual(expVal, actVal);
        }

        [TestMethod()]
        [DataRow(0, "Aztecs")]
        [DataRow(37, "invalid civ:37")]
        [DataRow(null, "invalid civ:null")]
        public void GetCivEnNameTest(int? civ, string expVal)
        {
            // Arrange
            AoE2net.ComClient = new TestHttpClient();
            var player = new Player() {
                Civ = civ,
            };

            // Act
            var testClass = new CtrlMain();
            _ = Task.Run(
                () => CtrlMain.InitAsync(Language.en)
                ).Result;

            var actVal = player.GetCivEnName();

            // Assert
            Assert.AreEqual(expVal, actVal);
        }

        [TestMethod()]
        [DataRow(0, "Aztecs")]
        [DataRow(37, "invalid civ:37")]
        [DataRow(null, "invalid civ:null")]
        public void GetCivNameTest(int? civ, string expVal)
        {
            // Arrange
            AoE2net.ComClient = new TestHttpClient();
            var player = new Player() {
                Civ = civ,
            };

            // Act
            var testClass = new CtrlMain();
            _ = Task.Run(
                () => CtrlMain.InitAsync(Language.en)
                ).Result;

            var actVal = player.GetCivName();

            // Assert
            Assert.AreEqual(expVal, actVal);
        }
    }
}