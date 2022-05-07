using AoE2NetDesktop.Utility.Forms;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using System.Windows.Forms;

namespace LibAoE2net.Tests
{
    [TestClass()]
    public class ListViewItemComparerTests
    {
        [TestMethod()]
        public void ListViewItemComparerTest()
        {
            // Arrange

            // Act
            var testClass = new ListViewItemComparer();

            // Assert
            Assert.AreEqual(0, testClass.Column);
            Assert.AreEqual(SortOrder.None, testClass.Order);
        }

        [TestMethod()]
        public void CompareTestColumnGet()
        {
            // Arrange
            var expVal = 10;

            // Act
            var testClass = new ListViewItemComparer {
                Column = expVal
            };
            var actVal = testClass.Column;

            // Assert
            Assert.AreEqual(expVal, actVal);
        }

        [TestMethod()]
        public void CompareTestColumnSetNotSameValue()
        {
            // Arrange
            var expVal = SortOrder.None;

            // Act
            var testClass = new ListViewItemComparer {
                Column = 10,
            };
            var actVal = testClass.Order;

            // Assert
            Assert.AreEqual(expVal, actVal);
        }

        [TestMethod()]
        [DataRow(SortOrder.None, SortOrder.Ascending)]
        [DataRow(SortOrder.Ascending, SortOrder.Descending)]
        [DataRow(SortOrder.Descending, SortOrder.Ascending)]
        [DataRow((SortOrder)(-1), SortOrder.Ascending)]
        public void CompareTestColumnSetSameValue(SortOrder initValue, SortOrder expVal)
        {
            // Arrange

            // Act
            const int setColumnValue = 10;
            var testClass = new ListViewItemComparer {
                Column = setColumnValue,
                Order = initValue,
            };

            testClass.Column = setColumnValue;
            var actVal = testClass.Order;

            // Assert
            Assert.AreEqual(expVal, actVal);
        }

        [TestMethod()]
        [DataRow("1", "2", ComparerMode.Integer, SortOrder.Ascending, -1)]
        [DataRow("2", "1", ComparerMode.Integer, SortOrder.Ascending, 1)]
        [DataRow("3", "3", ComparerMode.Integer, SortOrder.Ascending, 0)]
        [DataRow("x", "y", ComparerMode.String, SortOrder.Ascending, -1)]
        [DataRow("y", "x", ComparerMode.String, SortOrder.Ascending, 1)]
        [DataRow("z", "z", ComparerMode.String, SortOrder.Ascending, 0)]
        [DataRow("2000/1/1", "2000/2/1", ComparerMode.DateTime, SortOrder.Ascending, -1)]
        [DataRow("2000/2/1", "2000/1/1", ComparerMode.DateTime, SortOrder.Ascending, 1)]
        [DataRow("2000/3/1", "2000/3/1", ComparerMode.DateTime, SortOrder.Ascending, 0)]
        [DataRow("1", "2", ComparerMode.Integer, SortOrder.Descending, 1)]
        [DataRow("2", "1", ComparerMode.Integer, SortOrder.Descending, -1)]
        [DataRow("3", "3", ComparerMode.Integer, SortOrder.Descending, 0)]
        [DataRow("x", "y", ComparerMode.String, SortOrder.Descending, 1)]
        [DataRow("y", "x", ComparerMode.String, SortOrder.Descending, -1)]
        [DataRow("z", "z", ComparerMode.String, SortOrder.Descending, 0)]
        [DataRow("2000/1/1", "2000/2/1", ComparerMode.DateTime, SortOrder.Descending, 1)]
        [DataRow("2000/2/1", "2000/1/1", ComparerMode.DateTime, SortOrder.Descending, -1)]
        [DataRow("2000/3/1", "2000/3/1", ComparerMode.DateTime, SortOrder.Descending, 0)]
        public void CompareTest(string x, string y, ComparerMode comparerMode, SortOrder sortOrder, int expVal)
        {
            // Arrange
            var testClass = new ListViewItemComparer { 
                Order = sortOrder,
                ColumnModes = new ComparerMode[]  {
                    comparerMode
                },
            };

            var item1 = new ListViewItem() { Text = x };
            var item2 = new ListViewItem() { Text = y };

            // Act
            var actVal = testClass.Compare(item1, item2);

            // Assert
            Assert.AreEqual(expVal, actVal);
        }

        [TestMethod()]
        public void CompareTestInvalidColumnModes()
        {
            // Arrange
            int expVal = 0;
            var testClass = new ListViewItemComparer {
                Order = SortOrder.Ascending,
            };

            var item1 = new ListViewItem() { Text = "x" };
            var item2 = new ListViewItem() { Text = "y" };

            // Act
            var actVal = testClass.Compare(item1, item2);

            // Assert
            Assert.AreEqual(expVal, actVal);
        }

    }
}