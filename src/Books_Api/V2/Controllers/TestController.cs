using Books_Api.Controllers;
using Books_Business.Core.Notifications;
using Books_Business.Modules.Users;
using Microsoft.AspNetCore.Mvc;

namespace Books_Api.V2.Controllers
{
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/test")]
    public class TestController : MainController
    {
        public TestController(INotifier notifier, IUser appUser) : base(notifier, appUser)
        {
        }

        [HttpGet]
        public string Valor()
        {
            return "Test V2";
        }
    }
}