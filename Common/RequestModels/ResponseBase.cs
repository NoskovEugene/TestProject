using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.RequestModels
{
    public class ResponseBase<T> : IResponse<T>
    {
        public T Payload { get; set; }
    }
}
