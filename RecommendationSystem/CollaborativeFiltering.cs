using System.Collections.Generic;
using System.Linq;

namespace RecommendationSystem
{
    public class CollaborativeFiltering<TClusterDefinder, TUser, TThing>
        where TClusterDefinder : IClusterDefiner<TUser, TThing>
    {

        private IEnumerable<IMark<TUser, TThing>> _marks;

        private TClusterDefinder _clusterDefinder;

        public CollaborativeFiltering(IEnumerable<IMark<TUser, TThing>> marks, TClusterDefinder clusterDefinder)
        {
            _marks = marks;
            _clusterDefinder = clusterDefinder;
        }

        public Cluster<TUser> GetCluster(TUser user)
        {
            _clusterDefinder.SetMarks(_marks);

            return _clusterDefinder.GetCluster(user);
        }

    }
}