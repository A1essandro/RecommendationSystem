using System.Collections.Generic;
using System.Linq;

namespace RecommendationSystem
{
    public class MarkByMarkClusterDefiner<TUser, TItem> : IClusterDefiner<TUser, TItem>
    {

        private IEnumerable<IMark<TUser, TItem>> _marks;

        private IDictionary<TUser, IList<IMark<TUser, TItem>>> _users;
        
        private int _maxMark;

        int Threshold { get; set; }

        public MarkByMarkClusterDefiner(int threshold = int.MinValue)
        {
            Threshold = threshold;
        }

        public void SetMarks(IEnumerable<IMark<TUser, TItem>> marks)
        {
            _marks = marks;
            _users = new Dictionary<TUser, IList<IMark<TUser, TItem>>>();
            _maxMark = _marks.Max(x => x.GetNumber());

            foreach (var mark in marks) _defineUsersToMarks(mark);
        }

        public Cluster<TUser> GetCluster(TUser user)
        {
            var userComparer = EqualityComparer<TUser>.Default;
            var itemComparer = EqualityComparer<TItem>.Default;
            var cluster = new Cluster<TUser>();

            foreach (var other in _users.Where(x => !userComparer.Equals(x.Key, user)))
            {
                int rank = 0;
                foreach (var otherMark in other.Value)
                {
                    var ownMark = _users[user].FirstOrDefault(m => itemComparer.Equals(m.Thing, otherMark.Thing));
                    if (ownMark != null)
                        rank += (_maxMark - otherMark.GetDifference(ownMark));
                }
                if (rank > Threshold)
                {
                    cluster.Add(rank, other.Key);
                }
            }

            return cluster;
        }

        private void _defineUsersToMarks(IMark<TUser, TItem> mark)
        {
            if (_users.ContainsKey(mark.User))
            {
                _users[mark.User].Add(mark);
            }
            else
            {
                _users.Add(mark.User, new List<IMark<TUser, TItem>> { mark });
            }
        }

    }
}