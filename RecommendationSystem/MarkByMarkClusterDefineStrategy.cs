using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecommendationSystem
{
    public class MarkByMarkClusterDefineStrategy<TUser, TThing> : IClusterDefineStrategy<TUser, TThing>
    {

        #region Private fields

        private IEnumerable<IMark<TUser, TThing>> _marks;
        private IDictionary<TUser, IList<IMark<TUser, TThing>>> _users;
        private int _maxMark;

        #endregion

        public int Threshold { get; set; }

        public ushort MaxLength { get; set; }

        public MarkByMarkClusterDefineStrategy(int threshold = int.MinValue, ushort maxLength = ushort.MaxValue)
        {
            Threshold = threshold;
            MaxLength = maxLength;
        }

        public void SetMarks(IEnumerable<IMark<TUser, TThing>> marks)
        {
            _marks = marks;
            _users = new Dictionary<TUser, IList<IMark<TUser, TThing>>>();
            _maxMark = _marks.Max(x => x.GetNumber());

            foreach (var mark in marks) _defineUsersToMarks(mark);
        }

        /// <summary>
        /// Getting cluster for specified user
        /// </summary>
        /// <param name="user"></param>
        /// <returns><see cref="Cluster<T>"/></returns>
        public async Task<ICluster<TUser>> GetCluster(TUser user)
        {
            return await Task.Run(() => _calculateCluster(user)).ConfigureAwait(false);
        }

        #region Private Methods

        private RangedCluster<TUser> _calculateCluster(TUser user)
        {
            var userComparer = EqualityComparer<TUser>.Default;
            var itemComparer = EqualityComparer<TThing>.Default;
            var cluster = new RangedCluster<TUser>();

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

        private void _defineUsersToMarks(IMark<TUser, TThing> mark)
        {
            if (_users.ContainsKey(mark.User))
            {
                _users[mark.User].Add(mark);
            }
            else
            {
                _users.Add(mark.User, new List<IMark<TUser, TThing>> { mark });
            }
        }

        #endregion

    }
}