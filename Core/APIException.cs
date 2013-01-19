using DDnsPod.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDnsPod.Core
{
    public class APIException:Exception
    {
        public APIException(CallbackStatus status)
            :base(status.Message)
        {
            ErrorCode = status.Code;
        }

        public APIException(int errorCode, string message)
            : base(message)
        {
            ErrorCode = errorCode;
        }

        public int ErrorCode { get; private set; }
    }
}
