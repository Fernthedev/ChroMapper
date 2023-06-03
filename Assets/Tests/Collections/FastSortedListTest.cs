using System.Collections.Generic;
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
            // This has to be sorted so the Contains call can work properly
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

            Assert.AreEqual(list.Count, 0);
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

            Assert.AreEqual(list[0], 1);
            Assert.AreEqual(list[1], 2);
            Assert.AreEqual(list[2], 4);
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

            Assert.AreEqual(list[0], 1);
            Assert.AreEqual(list[1], 4);
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

            Assert.AreEqual(list[2], 1);
            Assert.AreEqual(list[1], 2);
            Assert.AreEqual(list[0], 4);
        }
    }
}