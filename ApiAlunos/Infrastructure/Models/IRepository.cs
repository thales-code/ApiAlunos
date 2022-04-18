using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiAlunos.Infrastructure.Models
{
    public interface IRepository<T>
    {
        Task<IEnumerable<T>> GetAll();
        Task<T> GetById(int id);

        Task<int> Add(T item);

        Task Edit(T item);

        Task Delete(int id);
    }
}
