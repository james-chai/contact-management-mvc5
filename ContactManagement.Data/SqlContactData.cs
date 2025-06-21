using ContactManagement.Core;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ContactManagement.Data
{
    public class SqlContactData : IContactData
    {
        private readonly ContactDbContext _db;

        public SqlContactData(ContactDbContext db) => _db = db;

        public Contact Add(Contact newContact)
        {
            _db.Contacts.Add(newContact);

            return newContact;
        }

        public async Task<int> CommitAsync()
            => await _db.SaveChangesAsync();

        public async Task<Contact> DeleteAsync(Guid id)
        {
            var contact = await _db.Contacts.FirstOrDefaultAsync(c => c.Id == id);

            if (contact != null)
            {
                _db.Contacts.Remove(contact);
            }
            return contact;
        }

        public async Task<Contact> GetByIdAsync(Guid id)
            => await _db.Contacts
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == id);

        public async Task<int> GetCountOfContactsAsync()
            => await _db.Contacts.CountAsync();

        public async Task<IEnumerable<Contact>> SearchContactsAsync(string searchTerm, CancellationToken cancellationToken = default)
        {
            IQueryable<Contact> query = _db.Contacts.AsNoTracking();

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                var normalizedTerm = searchTerm.Trim().ToLower();
                query = query.Where(c =>
                    c.FirstName.ToLower().Contains(normalizedTerm) ||
                    c.LastName.ToLower().Contains(normalizedTerm) ||
                    (c.CompanyName != null && c.CompanyName.ToLower().Contains(normalizedTerm)) ||
                    (c.Mobile != null && c.Mobile.Contains(normalizedTerm)) ||
                    c.Email.ToLower().Contains(normalizedTerm));
            }

            await Task.Delay(1000); // simulate loading spinner delay

            return await query
                .OrderBy(c => c.LastName)
                .ThenBy(c => c.FirstName)
                .ToListAsync(cancellationToken);
        }

        public Contact Update(Contact updatedContact)
        {
            var entry = _db.Entry(updatedContact);
            entry.State = EntityState.Modified;
            return updatedContact;
        }
    }
}
