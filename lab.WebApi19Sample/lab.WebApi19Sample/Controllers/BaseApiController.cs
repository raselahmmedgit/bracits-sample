using lab.WebApi19Sample.Exception;
using lab.WebApi19Sample.Helpers;
using lab.WebApi19Sample.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Logging;
using System;
using static lab.WebApi19Sample.Utility.Enums;

namespace lab.WebApi19Sample.Controllers
{
    public class BaseApiController : ControllerBase
    {
        #region Global Variable Declaration
        internal ResultApi _resultApi = new ResultApi();
        #endregion

        #region Constructor
        public BaseApiController()
        {
        }
        #endregion

        #region Actions

        internal IActionResult Ok(ResultApi resultApi)
        {
            return ResponseHelper.ObjectResult(resultApi);
        }

        internal IActionResult Error(ResultApi resultApi)
        {
            return ResponseHelper.ObjectResult(resultApi, (int)StatusCodeEnum.ServerError);
        }
        
        internal IActionResult Error(System.Exception ex)
        {
            var errorViewModel = ExceptionHelper.ExceptionErrorMessageFormat(ex);
            return ResponseHelper.ObjectResult(errorViewModel);
        }

        internal IActionResult Error(ErrorViewModel errorViewModel)
        {
            return ResponseHelper.ObjectResult(errorViewModel);
        }

        internal IActionResult Error(ModelStateDictionary modelStateDictionary)
        {
            return ResponseHelper.ObjectResult(modelStateDictionary);
        }

        internal IActionResult ErrorNull()
        {
            ErrorViewModel errorViewModel = new ErrorViewModel();
            errorViewModel.ErrorMessage = MessageHelper.NullError;
            errorViewModel.ErrorType = MessageHelper.MessageTypeDanger;
            return ResponseHelper.ObjectResult(errorViewModel);
        }

        internal IActionResult RedirectResult(string actionName)
        {
            return RedirectToAction(actionName);
        }

        internal IActionResult RedirectResult(string actionName, string controllerName)
        {
            return RedirectToAction(actionName, controllerName);
        }

        internal IActionResult RedirectResult(string actionName, string controllerName, string areaName)
        {
            return RedirectToAction(actionName, controllerName, new { @area = areaName });
        }

        internal bool IsAjaxRequest()
        {
            var request = Request;
            if (request == null)
            {
                throw new ArgumentNullException("request");
            }
            return (request.Headers != null) && (request.Headers["X-Requested-With"] == "XMLHttpRequest");
        }

        #endregion
    }
}