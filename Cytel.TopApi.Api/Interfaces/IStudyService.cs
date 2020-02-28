using Cytel.Top.Api.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cytel.Top.Api.Interfaces
{
    /// <summary>
    /// Interface used to create an abstraction layer for the database operations.
    /// </summary>
    public interface IStudyService
    {
        Task Add(Study item);
        Task Remove(int id);
        Task Update(Study item);
        Task<Study> FindByID(int id);
        Task<IEnumerable<Study>> FindAll();
    }
}
