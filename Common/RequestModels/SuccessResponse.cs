using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.RequestModels
{
    public class SuccessResponse<T> : IResponse<T>
    {
        public bool Success { get; set; }
        public T Payload { get; set; }
    }
}
