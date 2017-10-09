using System.Collections.Generic;
using System.Threading.Tasks;

namespace Data
{
    public interface IGateway<TDataModel, in TDataFilter>
    {
        Task<IEnumerable<TDataModel>> Get(TDataFilter filter);
        Task<TDataModel> GetSingle(int id);
        Task<int> Add(TDataModel model);
        Task<bool> Update(TDataModel model);
        Task<bool> Delete(TDataModel model);
    }
}