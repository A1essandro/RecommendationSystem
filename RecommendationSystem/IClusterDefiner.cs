using System.Collections.Generic;
using System.Threading.Tasks;

namespace RecommendationSystem
{
    public interface IClusterDefiner<TUser, TItem>
    {

        void SetMarks(IEnumerable<IMark<TUser, TItem>> marks);

        Task<Cluster<TUser>> GetCluster(TUser user);

    }
}
