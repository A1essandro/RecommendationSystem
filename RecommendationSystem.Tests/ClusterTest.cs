using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

            using (var cluster = new RangedCluster<object>())
            {
                cluster.Add(int.MinValue, value2);
                cluster.Add(int.MaxValue, value1);
                cluster.Add(int.MaxValue, new object());
                cluster.Add(0, new object());

                Assert.Equal(cluster.First().Value, value1);
                Assert.Equal(cluster.Last().Value, value2);
            }
        }

        [Fact]
        public void MultithreadClusterItemsSortingTest()
        {
            var tasks = new List<Task>();
            using (var cluster = new RangedCluster<int>())
            {
                for (var i = 0; i < 10; i++)
                {
                    var tempI = i;
                    Action action = () => _workWithClusterData(cluster, tempI);
                    tasks.Add(Task.Factory.StartNew(action));
                }

                Task.WaitAll(tasks.ToArray());

                Assert.Equal(0, cluster.First().Value);
            }
        }

        /// <summary>
        /// simulation of active work with the cluster
        /// </summary>
        /// <param name="cluster"></param>
        /// <param name="i"></param>
        private void _workWithClusterData(ICluster<int> cluster, int i)
        {
            int tempItem = 0;
            for (var j = 0; j < 100; j++)
            {
                cluster.Add(i, i * j);
                if (cluster.ContainsKey(i + j))
                {
                    tempItem += cluster[i + j];
                }
            }

            foreach (var item in cluster)
            {
                tempItem += item.Value;
            }
        }

    }
}
