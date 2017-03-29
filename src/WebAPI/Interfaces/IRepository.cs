using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebAPI.Interfaces
{
    public interface IRepository<in TEditModel, TViewModel, in TFilter> : IRepository<TViewModel, TFilter>
    {
        Task<int> Add(TEditModel model);
        Task<int> Update(TEditModel model);
        Task<bool> Delete(int id);
    }

    public interface IRepository<TViewModel, in TFilter>
    {
        Task<IEnumerable<TViewModel>> Get(TFilter filter);
        Task<TViewModel> GetSingle(int id);
    }
}