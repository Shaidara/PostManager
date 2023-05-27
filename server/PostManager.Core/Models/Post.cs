using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostManager.Core.Models
{
    public class Post
    {
        public int Id { get; set; }
        public string Author { get; set; } = string.Empty;
        public int AuthorId { get; set; }
        public int Likes { get; set; }
        public decimal Popularity { get; set; }

        public int Reads { get; set; }

        public string[] Tags { get; set; }

    }
}