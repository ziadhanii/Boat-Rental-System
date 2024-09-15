using System;

namespace BoatSystem.Core.Models
{
    public class OperationResult
    {
        public bool Success { get; set; }
        public string Message { get; set; }

        public OperationResult()
        {
        }

        public OperationResult(bool success, string message)
        {
            Success = success;
            Message = message;
        }
    }
}
