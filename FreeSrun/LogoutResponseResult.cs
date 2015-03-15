using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FreeSrun
{
    public class LogoutResponseResult:ResponseResult
    {
         public LogoutResponseResult(ResponseStatus status, string message)
        {
            Status = status;
            Message = message;
        }
        public LogoutResponseResult(string responseText)
        {
            // If error: message@timestamp
            ResponseText = responseText;
            if (responseText=="logout_ok")
            {
                Status = ResponseStatus.Success;
                Message = CodeInfo.InfoDict[responseText];
            }
            else
            {
                Status = ResponseStatus.Error;
                Message = responseText;
            }
            
        }
    }
}
