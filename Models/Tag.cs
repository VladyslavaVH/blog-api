using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BlogWebAPI.Models
{
    public class Tag
    {
        public int Id { get; set; }

        [Required]
        [StringLength(30)]
        public string Name { get; set; }

        [Required]
        public ICollection<Article> Articles { get; set; }
        public List<ArticlesTags> ArticleTag { get; set; }
    }
}
