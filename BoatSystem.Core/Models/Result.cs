using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoatSystem.Core.Models
{
    public class Result
    {
        public bool IsSuccess { get; private set; }
        public string Message { get; private set; }
        public object Data { get; private set; }

        private Result(bool isSuccess, string message, object data = null)
        {
            IsSuccess = isSuccess;
            Message = message;
            Data = data;
        }

        public static Result Success(object data = null) => new Result(true, "Operation successful", data);
        public static Result Failure(string message) => new Result(false, message);
    }
}

