using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace RecommendationSystem.Tests
{
    public class CollaborativeFilteringTest
    {
        [Fact]
        public async Task CollaborativeFilteringCalculationTest()
        {
            var marks = new List<Mark>()
            {
                new Mark(1, 1, 5),
                new Mark(1, 2, 1),
                new Mark(1, 3, 4),
                new Mark(1, 4, 3),

                new Mark(2, 1, 4),
                new Mark(2, 2, 2),
                new Mark(2, 3, 4),
                new Mark(2, 4, 5),
                new Mark(2, 5, 5),
                new Mark(2, 6, 4),
                new Mark(2, 7, 1),

                new Mark(3, 1, 1),
                new Mark(3, 2, 4),
                new Mark(3, 3, 2),
                new Mark(3, 6, 1),
            };

            var clusterDefiner = new MarkByMarkClusterDefiner<int, int>();
            var filtering = new CollaborativeFiltering<MarkByMarkClusterDefiner<int, int>, int, int>(marks, clusterDefiner);
            var cluster =  await filtering.GetCluster(1);
            Console.WriteLine(cluster);

            Assert.Contains(2, cluster.Values);
            Assert.Contains(3, cluster.Values);
            Assert.DoesNotContain(1, cluster.Values);
            Assert.Equal(2, cluster.Count);
            Assert.Equal(2, cluster.First().Value);
        }

        private class Mark : IMark<int, int>
        {
            public int User { get; }

            public int Thing { get; }

            private int _value { get; }

            public Mark(int user, int thing, int value)
            {
                User = user;
                Thing = thing;
                _value = value;
            }

            public ushort GetDifference(IMark<int, int> another) => (ushort)Math.Abs(another.GetNumber() - GetNumber());

            public int GetNumber() => _value;
        }

    }

}
