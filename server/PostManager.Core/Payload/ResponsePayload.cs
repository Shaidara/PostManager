using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostManager.Core.Payload
{
    public class ResponsePayload<T> where T : class
    {
        public T Data { get; set; }
        public int Count { get; set; }
    }
}
