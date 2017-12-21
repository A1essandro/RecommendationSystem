using System;
using System.Linq;
using Xunit;

namespace RecommendationSystem.Tests
{
    public class ClusterTest
    {
        [Fact]
        public void ClusterItemsSortingTest()
        {
            var value1 = "Object1";
            var value2 = "Object2";
            var cluster = new Cluster<object>();

            cluster.Add(int.MinValue, value2);
            cluster.Add(int.MaxValue, value1);
            cluster.Add(int.MaxValue, new object());
            cluster.Add(0, new object());

            Assert.Equal(cluster.Items.First().Value, value1);
            Assert.Equal(cluster.Items.Last().Value, value2);
        }

    }
}
