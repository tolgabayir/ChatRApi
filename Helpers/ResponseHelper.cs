using System;
using SignalR101.Models;

namespace SignalR101.Helpers
{
    public static class ResponseHelper
    {
        public static ResponseModel<T> Success<T>(T data)
        {
            return new ResponseModel<T>
            {
                IsSuccessful = true,
                Data = data,
                Message = "Success"
            };
        }

        public static ResponseModel<T> Fail<T>(string message)
        {
            return new ResponseModel<T>
            {
                IsSuccessful = false,
                Message = message
            };
        }
    }
}

