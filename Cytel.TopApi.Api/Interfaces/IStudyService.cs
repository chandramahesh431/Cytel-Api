using Cytel.Top.Api.Models;
using System.Collections.Generic;

namespace Cytel.Top.Api.Interfaces
{
    /// <summary>
    /// Interface used to create an abstraction layer for the database operations.
    /// </summary>
    public interface IStudyService
    {
        void Add(Study item);
        void Remove(int id);
        void Update(Study item);
        Study FindByID(int id);
        IEnumerable<Study> FindAll();
    }
}
