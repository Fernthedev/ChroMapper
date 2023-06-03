using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace Tests.Collections
{
    public class FastSortedListTest
    {
        [Test]
        public void OrderedListContains()
        {
            // Disable because we want to use `Add`
            // ReSharper disable once UseObjectOrCollectionInitializer
            var list = new FastSortedList<int>();
            list.Add(4);
            list.Add(2);
            list.Add(1);

            Assert.IsTrue(list.Contains(4));
            Assert.IsTrue(list.Contains(2));
            Assert.IsTrue(list.Contains(1));
        }


        [Test]
        public void OrderedListCopy()
        {
            var oldList = new List<int>
            {
                1,
                2,
                4
            };
            var list = new FastSortedList<int>(oldList);

            // TODO: Sort on construct?
            Assert.True(list.Contains(1));
            Assert.True(list.Contains(4));
            Assert.True(list.Contains(2));

            oldList.Add(5);

            Assert.AreNotEqual(oldList.Count, list.Count);
        }

        [Test]
        public void OrderedListClear()
        {
            var oldList = new List<int>
            {
                1,
                4,
                2
            };
            var list = new FastSortedList<int>(oldList);

            // TODO: Sort on construct?
            Assert.Greater(list.Count, 0);

            list.Clear();

            Assert.AreEqual(0, list.Count);
            Assert.Greater(oldList.Count, 0);
        }

        [Test]
        public void OrderedListOrder()
        {
            // Disable because we want to use `Add`
            // ReSharper disable once UseObjectOrCollectionInitializer
            var list = new FastSortedList<int>();
            list.Add(4);
            list.Add(2);
            list.Add(1);

            Assert.AreEqual(1, list[0]);
            Assert.AreEqual(2, list[1]);
            Assert.AreEqual(4, list[2]);
        }

        [Test]
        public void OrderedListRemove()
        {
            // Disable because we want to use `Add`
            // ReSharper disable once UseObjectOrCollectionInitializer
            var list = new FastSortedList<int>();
            list.Add(4);
            list.Add(2);
            list.Add(1);

            list.Remove(2);

            Assert.AreEqual(1, list[0]);
            Assert.AreEqual(4, list[1]);
        }

        [Test]
        public void OrderedListNoRemove()
        {
            // Disable because we want to use `Add`
            // ReSharper disable once UseObjectOrCollectionInitializer
            var list = new FastSortedList<int>();
            list.Add(4);
            list.Add(2);
            list.Add(1);

            Assert.False(list.Remove(5));
        }

        [Test]
        public void OrderedListReverse()
        {
            var reverseComparer = Comparer<int>.Create((a, b) => -a.CompareTo(b));
            // Disable because we want to use `Add`
            // ReSharper disable once UseObjectOrCollectionInitializer
            var list = new FastSortedList<int>(reverseComparer);
            list.Add(4);
            list.Add(2);
            list.Add(1);

            Assert.AreEqual(1, list[2]);
            Assert.AreEqual(2, list[1]);
            Assert.AreEqual(4, list[0]);
        }

        [Test]
        public void OrderedListViewBetween()
        {
            // Disable because we want to use `Add`
            // ReSharper disable once UseObjectOrCollectionInitializer
            var list = new FastSortedList<int>();
            list.Add(0);
            list.Add(1);
            list.Add(2);
            list.Add(3);
            list.Add(4);
            list.Add(10);
            list.Add(5);
            list.Add(8);

            var between = list.GetViewBetween(6, 11).ToList();
            Assert.AreEqual(5, between[0]);
            Assert.AreEqual(8, between[1]);
            Assert.AreEqual(10, between[2]);
        }
    }
}