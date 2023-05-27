using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostManager.Core.Models
{
    public class PostApiResponse
    {
        public IEnumerable<Post> Posts { get; set; }
    }
}
