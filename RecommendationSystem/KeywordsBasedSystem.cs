using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecommendationSystem
{

    public class KeywordsBasedSystem<T> : IKeywordsBasedRecommendationSystem<T>
    {

        private IEnumerable<KeyValuePair<T, int>> _marks = new Dictionary<T, int>();

        private IEnumerable<KeyValuePair<T, IEnumerable<string>>> _keywordsByThings = new Dictionary<T, IEnumerable<string>>();

        public void SetMarks(IEnumerable<IMark<T>> marks)
        {
            SetMarks(marks.Select(x => new KeyValuePair<T, int>(x.Thing, x.GetNumber())));
        }

        public void SetMarks(IEnumerable<KeyValuePair<T, int>> marks)
        {
            _marks = marks;
        }

        public void SetKeywords(IEnumerable<KeyValuePair<T, IEnumerable<string>>> keywordsByThings)
        {
            _keywordsByThings = keywordsByThings;
        }

        public Task<ICluster<T>> GetCluster(IClusterStrategy<T> strategy)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<T> GetRecommendations()
        {
            throw new System.NotImplementedException();
        }

    }

}