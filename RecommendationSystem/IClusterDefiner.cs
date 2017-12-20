using System.Collections.Generic;

namespace RecommendationSystem
{
    public interface IClusterDefiner<TUser, TItem>
    {

        void SetMarks(IEnumerable<IMark<TUser, TItem>> marks);

        Cluster<TUser> GetCluster(TUser user);

    }
}
