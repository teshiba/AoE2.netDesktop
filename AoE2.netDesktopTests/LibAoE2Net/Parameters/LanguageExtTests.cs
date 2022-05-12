namespace LibAoE2net.Tests;

using System.Collections.Generic;
using AoE2NetDesktop.LibAoE2Net.Parameters;
using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public class LanguageExtTests
{
    [TestMethod]
    public void ToApiStringTest()
    {
        // Arrange
        var expVal = new Dictionary<Language, string> {
            { Language.en, "en" },
            { Language.de, "de" },
            { Language.el, "el" },
            { Language.es, "es" },
            { Language.es_MX, "es-MX" },
            { Language.fr, "fr" },
            { Language.hi, "hi" },
            { Language.it, "it" },
            { Language.ja, "ja" },
            { Language.ko, "ko" },
            { Language.ms, "ms" },
            { Language.nl, "nl" },
            { Language.pt, "pt" },
            { Language.ru, "ru" },
            { Language.tr, "tr" },
            { Language.vi, "vi" },
            { Language.zh, "zh" },
            { Language.zh_TW, "zh-TW" },
        };

        foreach (var item in expVal.Keys) {
            // Act
            var actVal = item.ToApiString();

            // Assert
            Assert.AreEqual(expVal[item], actVal);
        }
    }
}
