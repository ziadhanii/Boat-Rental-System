using System;

namespace BoatSystem.Core.Models
{
    public class OperationResult
    {
        public bool Success { get; set; }
        public string Message { get; set; }

        // Parameterless constructor
        public OperationResult()
        {
        }

        // Constructor with parameters
        public OperationResult(bool success, string message)
        {
            Success = success;
            Message = message;
        }
    }
}
