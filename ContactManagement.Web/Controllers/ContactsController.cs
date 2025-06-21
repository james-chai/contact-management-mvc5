using ContactManagement.Core;
using ContactManagement.Data;
using System;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ContactManagement.MVC5.Controllers
{
    public class ContactsController : Controller
    {
        private readonly IContactData _contactData;
        private const string HomeIndexAction = nameof(HomeController.Index);

        public ContactsController(IContactData contactData)
        {
            _contactData = contactData ?? throw new ArgumentNullException(nameof(contactData));
        }

        // GET: Contacts
        public ActionResult Index() => RedirectToAction(HomeIndexAction, "Home");

        // GET: Contacts/Details/5
        public async Task<ActionResult> Details(Guid id)
        {
            var contact = await GetContactOrNotFoundAsync(id);
            return contact == null ? (ActionResult)HttpNotFound() : View(contact);
        }

        // GET: Contacts/Create
        public ActionResult Create() => View(new Contact());

        // POST: Contacts/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Contact contact)
        {
            if (!ModelState.IsValid)
                return View(contact);

            InitializeNewContact(contact);

            _contactData.Add(contact);
            await _contactData.CommitAsync();

            return RedirectToHome();
        }

        // GET: Contacts/Edit/5
        public async Task<ActionResult> Edit(Guid id)
        {
            var contact = await GetContactOrNotFoundAsync(id);

            return contact == null ? (ActionResult)HttpNotFound() : View(contact);
        }

        // POST: Contacts/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Guid id, Contact updatedContact)
        {
            if (!ModelState.IsValid)
                return View(updatedContact);

            var existingContact = await GetContactOrNotFoundAsync(id);
            if (existingContact == null)
                return HttpNotFound();

            _contactData.Update(updatedContact);
            await _contactData.CommitAsync();

            return RedirectToHome();
        }

        // GET: Contacts/Delete/5
        public async Task<ActionResult> Delete(Guid id)
        {
            var contact = await GetContactOrNotFoundAsync(id);

            return contact == null ? (ActionResult)HttpNotFound() : View(contact);
        }

        // POST: Contacts/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(Guid id)
        {
            try
            {
                var contact = await GetContactOrNotFoundAsync(id);

                if (contact == null)
                    return HttpNotFound();

                await _contactData.DeleteAsync(id);
                await _contactData.CommitAsync();

                return RedirectToHome();
            }
            catch (Exception ex)
            {
                // In MVC5, consider using a logging framework like log4net or NLog
                System.Diagnostics.Trace.TraceError($"Error deleting contact {id}: {ex}");
                ModelState.AddModelError(string.Empty, "Failed to delete contact");
                
                return View("Delete",await GetContactOrNotFoundAsync(id));
            }
        }

        private async Task<Contact> GetContactOrNotFoundAsync(Guid id)
            => await _contactData.GetByIdAsync(id);

        private static void InitializeNewContact(Contact contact)
        {
            contact.Id = Guid.NewGuid();
            contact.CreatedAt = DateTime.UtcNow;
            contact.UpdatedAt = contact.CreatedAt;
        }

        private ActionResult RedirectToHome()
            => RedirectToAction(HomeIndexAction, "Home");
    }
}