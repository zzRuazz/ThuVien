using Microsoft.AspNetCore.Mvc;
using Common.Abstractions;
using Salvation.Services.Models.Request;

namespace Salvation.Presentation.WebApp.Controllers
{
    [Route("tai-khoan")]
    public class AccountController : Controller
    {
        private readonly ICustomerApiProvider _customerApiProvider;

        private readonly ILogProvider _logProvider;

        public AccountController(ICustomerApiProvider customerApiProvider, ILogProvider logProvider)
        {
            _customerApiProvider = customerApiProvider;
            _logProvider = logProvider;
        }

        [Route("thong-tin-ca-nhan")]
        public IActionResult Index()
        {
            return View();
        }

        [Route("dang-nhap")]
        public IActionResult Login()
        {
            return View();
        }

        [Route("dang-ky")]
        public IActionResult Register()
        {
            return View();
        }

        [Route("thay-doi-thong-tin")]
        public IActionResult ChangeInformation()
        {
            return View();
        }

        [Route("thay-doi-anh-dai-dien")]
        public IActionResult ChangeAvatar()
        {
            return View();
        }

        [Route("thay-doi-mat-khau")]
        public IActionResult ChangePassword()
        {
            return View();
        }

        [Route("don-hang")]
        public IActionResult OrderList()
        {
            return View();
        }

        [Route("danh-sach-yeu-thich")]
        public IActionResult Wishlist()
        {
            return View();
        }

        [Route("thay-doi-avatar")]
        public IActionResult HandleChangeAvatar(IFormFile file)
        {
            try
            {
                //var fileUpload = new FileUploadRequest
                //{
                //    FileUpload = file,
                //    AccountId = Guid.NewGuid().ToString(),
                //    ClientSystem = "customer-system",
                //    FileDirectory = "customer-avatar",
                //    FileName = file.FileName,
                //    FileSalt = Guid.NewGuid().ToString(),
                //    UploadTime = DateTime.Now,
                //};

                //var result = _customerApiProvider.PostCore<object>("FileUpload/handle-upload", fileUpload);
            }
            catch (Exception ex)
            {
                _logProvider.Error(ex);
            }

            return View();
        }
    }
}
