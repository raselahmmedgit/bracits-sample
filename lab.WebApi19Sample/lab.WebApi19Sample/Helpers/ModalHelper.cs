using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using lab.WebApi19Sample.Exception;
using System;
using lab.WebApi19Sample.ViewModels;

namespace lab.WebApi19Sample.Helpers
{
    public static class ModalHelper
    {
        public static string Content(Result result)
        {
            string strContent = string.Empty;

            if (result.Success)
            {
                strContent = (Boolean.TrueString.ToString() + "_" + result.MessageType + "_" + result.Message).ToString();
            }
            else
            {
                strContent = (Boolean.FalseString.ToString() + "_" + result.MessageType + "_" + result.Message).ToString();
            }

            return strContent;
        }

        public static string ContentMessage()
        {
            string result = string.Empty;
            result = (Boolean.FalseString + "_" + MessageHelper.MessageTypeDanger + "_" + MessageHelper.Error).ToString();
            return result;
        }

        public static string ContentMessage(System.Exception ex)
        {
            string result = string.Empty;
            ErrorViewModel errorViewModel = ExceptionHelper.ExceptionErrorMessageFormat(ex);
            string errorMessage = errorViewModel.ErrorMessage;
            result = (Boolean.FalseString + "_" + MessageHelper.MessageTypeDanger + "_" + errorMessage).ToString();
            return result;
        }

        public static string ContentModelMessage(ModelStateDictionary modelStateDictionary)
        {
            string result = string.Empty;
            string errorMessage = ExceptionHelper.ModelStateErrorFirstFormat(modelStateDictionary);
            result = (Boolean.FalseString + "_" + MessageHelper.MessageTypeDanger + "_" + errorMessage).ToString();
            return result;
        }

        public static string ContentNullMessage()
        {
            string result = string.Empty;
            result = (Boolean.FalseString + "_" + MessageHelper.MessageTypeWarning + "_" + MessageHelper.NullError).ToString();
            return result;
        }

        #region JsonResult

        public static JsonResult Json(Result result)
        {
            var json = new
            {
                success = result.Success,
                error = result.Message,
                errortype = result.MessageType
            };

            return new JsonResult(json);
        }

        public static JsonResult JsonMessage()
        {
            var json = new
            {
                success = false,
                error = MessageHelper.MessageTypeDanger,
                errortype = MessageHelper.Error
            };

            return new JsonResult(json);
        }

        public static JsonResult JsonMessage(System.Exception ex)
        {
            ErrorViewModel errorViewModel = ExceptionHelper.ExceptionErrorMessageFormat(ex);
            string errorMessage = errorViewModel.ErrorMessage;

            var json = new
            {
                success = false,
                error = MessageHelper.MessageTypeDanger,
                errortype = errorMessage
            };

            return new JsonResult(json);
        }

        public static JsonResult JsonModelMessage(ModelStateDictionary modelStateDictionary)
        {
            string errorMessage = ExceptionHelper.ModelStateErrorFirstFormat(modelStateDictionary);

            var json = new
            {
                success = false,
                error = MessageHelper.MessageTypeDanger,
                errortype = errorMessage
            };

            return new JsonResult(json);
        }

        public static JsonResult JsonNullMessage()
        {
            var json = new
            {
                success = false,
                error = MessageHelper.MessageTypeWarning,
                errortype = MessageHelper.NullError
            };

            return new JsonResult(json);
        }

        #endregion
    }
}
