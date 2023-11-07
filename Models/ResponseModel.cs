using System;
namespace SignalR101.Models
{
	public class ResponseModel<T>
	{
        public bool IsSuccessful { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
    }
}

