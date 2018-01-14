using System.Collections.Generic;
using System.Threading.Tasks;

namespace RecommendationSystem
{
    public interface IClusterStrategy<TUser, TItem>
    {

        void SetMarks(IEnumerable<IMark<TUser, TItem>> marks);

        Task<ICluster<TUser>> GetCluster(TUser user);

    }
}
