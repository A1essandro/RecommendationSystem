using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecommendationSystem
{
    public class CollaborativeFiltering<TUser, TThing>
    {

        private IEnumerable<IMark<TUser, TThing>> _marks;

        private IClusterStrategy<TUser, TThing> _clusterDefinder;

        public CollaborativeFiltering(IEnumerable<IMark<TUser, TThing>> marks, IClusterStrategy<TUser, TThing> clusterDefinder)
        {
            _marks = marks;
            _clusterDefinder = clusterDefinder;
        }

        public async Task<ICluster<TUser>> GetCluster(TUser user)
        {
            _clusterDefinder.SetMarks(_marks);

            return await _clusterDefinder.GetCluster(user).ConfigureAwait(false);
        }

    }
}