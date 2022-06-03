using static lab.WebApi19Sample.Utility.Enums;

namespace lab.WebApi19Sample.Helpers
{
    public class ResultApi
    {
        public int StatusCode { get; }

        public bool Success { get; }

        public string Message { get; }

        public string MessageType { get; }

        public int ParentId { get; }

        public string ParentName { get; }

        public object Data { get; }

        public ResultApi()
        {
        }

        private ResultApi(int statusCode, bool success, string message, string messageType)
        {
            StatusCode = statusCode;
            Success = success;
            Message = message;
            MessageType = messageType;
        }

        private ResultApi(int statusCode, bool success, string message, string messageType, object data)
        {
            StatusCode = statusCode;
            Success = success;
            Message = message;
            MessageType = messageType;
            Data = data;
        }

        private ResultApi(int statusCode, bool success, string message, string messageType, int parentId, string parentName)
        {
            StatusCode = statusCode;
            Success = success;
            Message = message;
            MessageType = messageType;
            ParentId = parentId;
            ParentName = parentName;
        }

        public static ResultApi Info()
        {
            return new ResultApi((int)StatusCodeEnum.Information, false, MessageHelper.Info, MessageHelper.MessageTypeInfo);
        }

        public static ResultApi Info(string message)
        {
            return new ResultApi((int)StatusCodeEnum.Information, false, message, MessageHelper.MessageTypeInfo);
        }

        public static ResultApi Warning()
        {
            return new ResultApi((int)StatusCodeEnum.ServerError, false, MessageHelper.Warning, MessageHelper.MessageTypeWarning);
        }

        public static ResultApi Warning(string message)
        {
            return new ResultApi((int)StatusCodeEnum.ServerError, false, message, MessageHelper.MessageTypeWarning);
        }

        public static ResultApi Fail()
        {
            return new ResultApi((int)StatusCodeEnum.ServerError, false, MessageHelper.Error, MessageHelper.MessageTypeDanger);
        }

        public static ResultApi Fail(string message)
        {
            return new ResultApi((int)StatusCodeEnum.ServerError, false, message, MessageHelper.MessageTypeDanger);
        }

        public static ResultApi Ok()
        {
            return new ResultApi((int)StatusCodeEnum.Successful, true, MessageHelper.Success, MessageHelper.MessageTypeSuccess);
        }

        public static ResultApi Ok(string message)
        {
            return new ResultApi((int)StatusCodeEnum.Successful, true, message, MessageHelper.MessageTypeSuccess);
        }

        public static ResultApi Ok(string message, int parentId, string parentName)
        {
            return new ResultApi((int)StatusCodeEnum.Successful, true, message, MessageHelper.MessageTypeSuccess, parentId, parentName);
        }

        public static ResultApi Ok(string message, object data)
        {
            return new ResultApi((int)StatusCodeEnum.Successful, true, message, MessageHelper.MessageTypeSuccess, data);
        }

        public static ResultApi Ok(object data)
        {
            return new ResultApi((int)StatusCodeEnum.Successful, true, MessageHelper.Success, MessageHelper.MessageTypeSuccess, data);
        }
    }
}
