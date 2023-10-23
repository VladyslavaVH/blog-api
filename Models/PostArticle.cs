using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogWebAPI.Models
{
    public class PostArticle
    {
        public string Name { get; set; }
        public string NickName { get; set; }
        public string Theme { get; set; }
        public string ImgUrl { get; set; }
        public string Tags { get; set; }
        public string Text { get; set; }
    }
}
