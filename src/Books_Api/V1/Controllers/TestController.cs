using Books_Api.Controllers;
using Books_Business.Core.Notifications;
using Books_Business.Modules.Users;
using Microsoft.AspNetCore.Mvc;

namespace Books_Api.V1.Controllers
{
    [ApiVersion("1.0", Deprecated = true)]
    [Route("api/v{version:apiVersion}/test")]
    public class TestController : MainController
    {
        public TestController(INotifier notifier, IUser appUser) : base(notifier, appUser)
        {
        }

        [HttpGet]
        public string Valor()
        {
            return "Test V1";
        }
    }
}