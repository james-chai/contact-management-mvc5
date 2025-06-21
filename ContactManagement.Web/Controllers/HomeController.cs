using ContactManagement.Data;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ContactManagement.MVC5.Controllers
{
    public class HomeController : Controller
    {
        private readonly IContactData _contactData;

        public HomeController(IContactData contactData)
            => _contactData = contactData;

        public async Task<ActionResult> Index(string searchTerm)
        {
            ViewBag.SearchTerm = searchTerm;
            var cancellationToken = Response.ClientDisconnectedToken;
            var contacts = await _contactData.SearchContactsAsync(searchTerm, cancellationToken);

            return View(contacts);
        }
    }
}