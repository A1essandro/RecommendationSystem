using System.Collections.Generic;
using System.Linq;

namespace RecommendationSystem
{
    public class CollaborativeFiltering<TClusterDefinder, TUser, TItem>
        where TClusterDefinder : IClusterDefiner<TUser, TItem>
    {

        private IEnumerable<IMark<TUser, TItem>> _marks;

        public CollaborativeFiltering(IEnumerable<IMark<TUser, TItem>> marks, TClusterDefinder clusterDefinder)
        {
            _marks = marks;
            clusterDefinder.SetMarks(_marks);
        }

    }
}