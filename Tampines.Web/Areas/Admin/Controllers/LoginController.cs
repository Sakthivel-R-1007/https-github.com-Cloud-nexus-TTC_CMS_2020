using Sita_Aircraft.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Tampines.Web.Helpers;
using Tampines_CMS.Domain;
using Tampines_CMS.Persistence.Interface;
using Tampines_CMS.Service.Interfaces;

namespace Tampines.Web.Areas.Admin.Controllers
{
    
    public class LoginController : Controller
    {
        // GET: Admin/Login
        #region Constructor And Private Members

        private IUserAccountDao _userAccountDao;
        private IUtilityService _utilityService;

        public LoginController(IUserAccountDao userAccountDao, IUtilityService utilityService)
        {
            _userAccountDao = userAccountDao;
            _utilityService = utilityService;
        }

        #endregion
        public ActionResult Index()
        {
            // ModelState.AddModelError("Id", Security.Encrypt<string>("m@LLURZrR7"));
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(UserAccount UA)
        {
            if (UA != null)
            {
                #region Validation

                List<ValidationParam> loginParam = new List<ValidationParam>{
                        new ValidationParam{
                            PropertyName="UserName",
                            Value=UA.UserName,
                            Type=typeof(string),
                            Methodologies=new Dictionary<ValidationMethodology,string>{
                               {ValidationMethodology.Required,null}
                            }
                        },
                        new ValidationParam{
                            PropertyName="Password",
                            Value=UA.Password,
                            Type=typeof(string),
                            Methodologies=new Dictionary<ValidationMethodology,string>{
                               {ValidationMethodology.Required,null}
                            }
                        }
                };

                Validator.Validate(loginParam, ModelState);
                #endregion


                if (UA.Captcha == Session["Captcha"].ToString())
                {
                    if (ModelState.IsValid)
                    {
                        UA.SecurityCode = string.Empty;

                        UA.Password = Security.Encrypt<string>(UA.Password);

                        UA.SystemIp = GetRemoteIp.GetIPAddress(HttpContext);

                        string EncDetail = Security.Encrypt<string>(UA.UserName + "|" + UA.Password + "|" + new Guid().ToString());

                        UA = _userAccountDao.AuthenticateUser(UA);

                        if (UA != null && UA.SecurityCode == "BLOCKED")
                        {
                            ModelState.AddModelError("Id", "Account is locked");
                        }
                        else if (UA == null)
                        {
                            ModelState.AddModelError("Id", "Invalid username or password");
                        }
                        else if (UA != null)
                        {

                            UA.SystemIp = GetRemoteIp.GetIPAddress(HttpContext);

                            UA.LastLoginStatus = _userAccountDao.CheckLoginStatus(Guid.Empty, UA.GUID);

                            if (UA.LastLoginStatus)
                            {
                                ModelState.AddModelError("Id", "User Already having an open session.");
                                ViewBag.LogDetails = EncDetail;
                            }
                            else
                            {
                                Guid SessionId;

                                if (_userAccountDao.SaveUserLoginLog(UA, out SessionId) > 0)
                                {
                                    UA.SessionId = SessionId;
                                    Session["UserAccount"] = UA;
                                    return RedirectToRoute("ViewBanners");
                                }
                            }

                        }
                        else
                        {
                            ModelState.AddModelError("Id", "Invalid username or password");
                        }
                    }
                }
                else
                {
                    ModelState.AddModelError("Id", "Please enter valid Captcha");
                }
            }
            return View(UA);

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ForceLogin(string EncDetail)
        {

            if (!string.IsNullOrEmpty(EncDetail))
            {

                string[] encDetailSplitUps = Security.Decrypt<string>(EncDetail).Split('|');

                UserAccount UA = new UserAccount
                {
                    UserName = encDetailSplitUps[0],
                    Password = encDetailSplitUps[1],
                    SystemIp = GetRemoteIp.GetIPAddress(HttpContext)
                };

                UA = _userAccountDao.AuthenticateUser(UA);

                UA.LastLoginStatus = true;

                _userAccountDao.UpdateUserLoginLog(UA);

                if (UA != null && UA.SecurityCode == "BLOCKED")
                {
                    ModelState.AddModelError("Id", "Account is locked");
                }
                else if (UA == null)
                {
                    ModelState.AddModelError("Id", "Invalid username or password");
                }
                else if (UA != null)
                {
                    UA.SystemIp = GetRemoteIp.GetIPAddress(HttpContext);

                    UA.LastLoginStatus = _userAccountDao.CheckLoginStatus(Guid.Empty, UA.GUID);

                    if (UA.LastLoginStatus)
                    {
                        ModelState.AddModelError("Id", "User Already having an open session.");
                        ViewBag.LogDetails = EncDetail;
                    }
                    else
                    {
                        Guid SessionId;

                        if (_userAccountDao.SaveUserLoginLog(UA, out SessionId) > 0)
                        {
                            UA.SessionId = SessionId;
                            Session["UserAccount"] = UA;
                            return RedirectToRoute("ViewBanners");
                        }
                    }

                }
                else
                {
                    ModelState.AddModelError("Id", "Invalid username or password");
                }
            }

            return View("Index");
        }



        #region ForgetPassword

        public ActionResult ForgotPassword()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ForgotPassword(ForgotPassword FP)
        {
            dynamic error = null; bool result = false;

            if (FP != null && FP.UserAccount != null && !string.IsNullOrEmpty(FP.UserAccount.Email))
            {
                FP.SystemIp = GetRemoteIp.GetIPAddress(HttpContext);

                FP = _userAccountDao.SaveForgotPassword(FP);

                if (FP == null)
                {
                    error = "Please enter valid email.";
                }
                else
                {
                    StringBuilder contents = new StringBuilder();
                    FP.Key = Security.EncryptandEncodeUrl(FP.UniqueId + "_" + FP.UserAccount.GUID);
                    contents.Append(RenderRazorViewToString("_EDMForgotPassword", FP));

                    if (_utilityService.SendEmail("Sita Aircraft - Reset Forgot Password", contents.ToString(), FP.UserAccount.Email, true, null) == "success")
                    {
                        result = true;
                    }
                    else
                    {
                        error = "Error occured. Please try again later";
                    }
                }
            }
            // return Json(new { Valid = (ModelState.IsValid), Success = result, Error = error }, JsonRequestBehavior.DenyGet);
            return Json(new { Valid = (ModelState.IsValid), Success = result, Error = error });
        }
        #endregion


        public ActionResult Logout()
        {
            if (Session["UserAccount"] != null)
            {
                UserAccount U = ((UserAccount)Session["UserAccount"]);

                if (U != null)
                {
                    U.SystemIp = GetRemoteIp.GetIPAddress(HttpContext);

                    _userAccountDao.UpdateUserLoginLog(U);
                }
            }

            Session.Abandon();

            Response.Cookies.Clear();

            return RedirectToAction("Index");
        }

        #region Reset Password

        [HttpGet]
        public ActionResult ResetPassword(string EncDetail)
        {
            ForgotPassword FP = null;

            string errorMessage = "Invalid reset password link";

            if (!string.IsNullOrEmpty(EncDetail))
            {
                string data = Security.DecodeUrlandDecrypt(EncDetail);

                string[] verificationParams = data.Split('_');

                FP = _userAccountDao.VerifyForgotPasswordUniqueId(new ForgotPassword
                {
                    UniqueId = new Guid(verificationParams[0]),
                    UserAccount = new UserAccount
                    {
                        GUID = new Guid(verificationParams[1])
                    }
                });

                if (FP != null)
                {
                    errorMessage = (FP.IsDeleted) ? "Reset password link expired." : ((FP.IsChanged) ? "Reset password link used already." : null);

                    FP.Key = EncDetail;
                }
            }

            ViewBag.Error = errorMessage;

            return View(FP);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ResetPassword(ForgotPassword FP)
        {
            if (FP != null && FP.UserAccount != null && (FP.UserAccount.NewPassword == FP.UserAccount.ConfirmPassword))
            {
                string data = Security.DecodeUrlandDecrypt(FP.Key);

                string[] verificationParams = data.Split('_');

                FP.UniqueId = new Guid(verificationParams[0]);

                FP.UserAccount.GUID = new Guid(verificationParams[1]);

                FP.UserAccount.Password = Security.Encrypt<string>(FP.UserAccount.NewPassword);

                FP.SystemIp = GetRemoteIp.GetIPAddress(HttpContext);

                FP.UserAccount = _userAccountDao.UpdatePassword(FP);

                if (FP.UserAccount != null && !string.IsNullOrEmpty(FP.UserAccount.Email))
                {
                    StringBuilder contents = new StringBuilder();
                    contents.Append(RenderRazorViewToString("_EDMPasswordChangedAcknowledge", FP.UserAccount));

                    if (_utilityService.SendEmail("SITA AIRCRAFT - Reset Forgot Password", contents.ToString(), FP.UserAccount.Email, true, null) == "success")
                    {
                        ViewBag.Success = "Password updated successfully";
                    }
                    else
                    {
                        ModelState.AddModelError("Id", "Error occured. Please try again later");
                    }
                }
                else
                {
                    ViewBag.Error = "Error Occured";
                    FP = null;
                }
            }
            return View(FP);
        }

        #endregion

        #region Private methods

        public JsonResult GetCaptcha()
        {
            return Json(new { captchaImage = Captcha.GetBase64(HttpContext), code = Convert.ToString(Session["Captcha"]) }, JsonRequestBehavior.AllowGet);
        }

        private string RenderRazorViewToString(string viewName, object model)
        {
            ViewData.Model = model;

            using (var sw = new StringWriter())
            {
                var viewResult = ViewEngines.Engines.FindPartialView(ControllerContext, viewName);

                var viewContext = new ViewContext(ControllerContext, viewResult.View, ViewData, TempData, sw);

                viewResult.View.Render(viewContext, sw);

                viewResult.ViewEngine.ReleaseView(ControllerContext, viewResult.View);

                return sw.GetStringBuilder().ToString();
            }
        }

        #endregion



    }
}