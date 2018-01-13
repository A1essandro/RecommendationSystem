using System.Collections.Generic;

namespace RecommendationSystem
{
    public interface ICluster<T> : IReadOnlyDictionary<int, T>
    {

        void Add(int key, T item);

    }
}