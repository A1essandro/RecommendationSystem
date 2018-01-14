using System.Collections.Generic;
using System.Threading.Tasks;

namespace RecommendationSystem
{
    public interface IRecommendationSystem<T>
    {

        IEnumerable<T> GetRecommendations();

    }

    public interface ICollaborativeRecommendationSystem<TUser, TThing> : IRecommendationSystem<TThing>
    {

        void SetMarks(IEnumerable<IMark<TUser, TThing>> marks);

        Task<ICluster<TUser>> GetCluster(IClusterStrategy<TUser, TThing> strategy);

    }

    public interface IContentBasedRecommendationSystem<TThing> : IRecommendationSystem<TThing>
    {

        void SetMarks(IEnumerable<IMark<TThing>> marks);

        Task<ICluster<TThing>> GetCluster(IClusterStrategy<TThing> strategy);

    }

}