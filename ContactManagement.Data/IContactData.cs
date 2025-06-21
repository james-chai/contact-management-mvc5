using ContactManagement.Core;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ContactManagement.Data
{
    public interface IContactData
    {
        Contact Add(Contact newContact);
        Task<int> CommitAsync();
        Task<Contact> DeleteAsync(Guid id);
        Task<Contact> GetByIdAsync(Guid id);
        Task<int> GetCountOfContactsAsync();
        Task<IEnumerable<Contact>> SearchContactsAsync(string searchTerm, CancellationToken cancellationToken = default);
        Contact Update(Contact updatedContact);
    }
}
