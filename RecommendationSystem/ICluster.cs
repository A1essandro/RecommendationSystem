using System.Collections.Generic;

namespace RecommendationSystem
{

    /// <summary>
    /// Cluster is a set of items with similar data
    /// </summary>
    public interface ICluster<T> : IReadOnlyDictionary<int, T>
    {

        void Add(int key, T item);

    }
}