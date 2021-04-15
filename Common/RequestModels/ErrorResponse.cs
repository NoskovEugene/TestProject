using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.RequestModels
{
    public class ErrorResponse<T> : SuccessResponse<T>
    {
        public Exception Exception { get; set; }
    }
}
