using lab.WebApi19Sample.Exception;
using lab.WebApi19Sample.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static lab.WebApi19Sample.Utility.Enums;

namespace lab.WebApi19Sample.Helpers
{
    public static class ResponseHelper
    {
        public static string Success(string message = null)
        {
            var responseModel = string.IsNullOrEmpty(message) ? ResultApi.Ok() : ResultApi.Ok(message);

            var jsonObj = new
            {
                statuscode = responseModel.StatusCode,
                success = responseModel.Success,
                messagetype = responseModel.MessageType,
                message = responseModel.Message,
                data = string.Empty
            };

            return JsonConvert.SerializeObject(jsonObj);
        }

        public static Object Success(object obj)
        {
            var responseModel = ResultApi.Ok(obj);
            return responseModel;
        }

        public static object Success<T>(IEnumerable<T> result)
        {
            var responseModel = ResultApi.Ok();
            var jsonObj = new
            {
                statuscode = responseModel.StatusCode,
                success = responseModel.Success,
                messagetype = responseModel.MessageType,
                message = responseModel.Message,
                data = result
            };

            return jsonObj;
        }

        public static Object Success(List<object> objList)
        {
            var responseModel = ResultApi.Ok();
            var jsonObj = new
            {
                statuscode = responseModel.StatusCode,
                success = responseModel.Success,
                messagetype = responseModel.MessageType,
                message = responseModel.Message,
                data = objList
            };

            return jsonObj;
        }

        public static Object Info(string message = null)
        {
            var responseModel = string.IsNullOrEmpty(message) ? ResultApi.Info() : ResultApi.Info(message);

            var jsonObj = new
            {
                statuscode = responseModel.StatusCode,
                success = responseModel.Success,
                messagetype = responseModel.MessageType,
                message = responseModel.Message,
                data = string.Empty
            };

            return jsonObj;
        }

        public static Object Warning(string message = null)
        {
            var responseModel = string.IsNullOrEmpty(message) ? ResultApi.Warning() : ResultApi.Warning(message);

            var jsonObj = new
            {
                statuscode = responseModel.StatusCode,
                success = responseModel.Success,
                messagetype = responseModel.MessageType,
                message = responseModel.Message,
                data = string.Empty
            };

            return jsonObj;
        }

        public static Object Error(string message = null)
        {
            var responseModel = string.IsNullOrEmpty(message) ? ResultApi.Fail() : ResultApi.Fail(message);

            var jsonObj = new
            {
                statuscode = responseModel.StatusCode,
                success = responseModel.Success,
                messagetype = responseModel.MessageType,
                message = responseModel.Message,
                data = string.Empty
            };

            return jsonObj;
        }

        public static Object Error(System.Exception ex)
        {
            ErrorViewModel errorViewModel = ExceptionHelper.ExceptionErrorMessageFormat(ex);
            string message = errorViewModel.ErrorMessage;

            var jsonObj = new
            {
                statuscode = (int)StatusCodeEnum.ServerError,
                success = false,
                messagetype = MessageHelper.MessageTypeDanger,
                message = message,
                data = string.Empty
            };

            return jsonObj;
        }

        public static Object Error(ErrorViewModel errorViewModel)
        {
            string message = errorViewModel.ErrorMessage;

            var jsonObj = new
            {
                statuscode = (int)StatusCodeEnum.ServerError,
                success = false,
                messagetype = MessageHelper.MessageTypeDanger,
                message = message,
                data = string.Empty
            };

            return jsonObj;
        }

        public static Object Error(ModelStateDictionary modelStateDictionary)
        {
            string message = ExceptionHelper.ModelStateErrorFirstFormat(modelStateDictionary);

            var jsonObj = new
            {
                statuscode = (int)StatusCodeEnum.ServerError,
                success = false,
                messagetype = MessageHelper.MessageTypeDanger,
                message = message,
                data = string.Empty
            };

            return jsonObj;
        }

        public static ObjectResult ObjectResult(int statusCode, string message)
        {
            return new ObjectResult(message) { StatusCode = statusCode };
        }

        public static ObjectResult ObjectResult(System.Exception ex)
        {
            ErrorViewModel errorViewModel = ExceptionHelper.ExceptionErrorMessageFormat(ex);
            string message = errorViewModel.ErrorMessage;
            return new ObjectResult(message) { StatusCode = (int)StatusCodeEnum.ServerError };
        }

        public static ObjectResult ObjectResult(ErrorViewModel errorViewModel)
        {
            return new ObjectResult(errorViewModel) { StatusCode = (int)StatusCodeEnum.ServerError };
        }

        public static ObjectResult ObjectResult(ModelStateDictionary modelStateDictionary)
        {
            string message = ExceptionHelper.ModelStateErrorFirstFormat(modelStateDictionary);
            return new ObjectResult(message) { StatusCode = (int)StatusCodeEnum.ServerError };
        }

        public static ObjectResult ObjectResult(ResultApi resultApi)
        {
            return new ObjectResult(resultApi) { StatusCode = (int)StatusCodeEnum.Successful };
        }

        public static ObjectResult ObjectResult(ResultApi resultApi, int statusCode)
        {
            return new ObjectResult(resultApi) { StatusCode = statusCode };
        }
    }
}
