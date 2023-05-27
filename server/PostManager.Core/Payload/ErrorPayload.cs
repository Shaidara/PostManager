using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostManager.Core.Payload
{
    public class ErrorPayload
    {
        public string ErrorMessage { get; set; } = string.Empty;
        public int Status { get; set; }
    }
}
