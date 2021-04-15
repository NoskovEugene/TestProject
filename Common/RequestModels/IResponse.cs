using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.RequestModels
{
    public interface IResponse<T>
    {
        T Payload { get; set; }
    }
}
