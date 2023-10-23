using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlogWebAPI.Models
{
    public class Article
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime Date { get; set; }

        [Column("AuthorFK")]
        public int AuthorFK { get; set; }

        [ForeignKey("AuthorFK")]
        public virtual Author Author { get; set; }// Навигационное свойство

        [Column("ThemeFK")]
        public int ThemeFK { get; set; }

        [ForeignKey("ThemeFK")]
        public virtual Theme Theme { get; set; }// Навигационное свойство

        [Required]
        [StringLength(150)]
        public string ImgUrl { get; set; }

        [Required]
        public string Text { get; set; }

        [Required]
        public ICollection<Tag> Tags { get; set; }
        public List<ArticlesTags> ArticleTag { get; set; }
    }
}
