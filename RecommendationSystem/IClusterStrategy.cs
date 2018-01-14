using System.Collections.Generic;
using System.Threading.Tasks;

namespace RecommendationSystem
{

    public interface IClusterStrategy<TThing>
    {

        void SetMarks(IEnumerable<IMark<TThing>> marks);

        Task<ICluster<TThing>> GetCluster(TThing user);

    }
    
    public interface IClusterStrategy<TUser, TThing>
    {

        void SetMarks(IEnumerable<IMark<TUser, TThing>> marks);

        Task<ICluster<TUser>> GetCluster(TUser user);

    }

}
