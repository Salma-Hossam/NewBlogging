using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace YakeenBlog.Models.EFModels
{
    [Table("Post")]
    public class Post
    {
        public Post()
        {
            Comments = new HashSet<Comment>();
        }
        public int Id { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        [MinLength(300)]
        public string Text { get; set; }
        public virtual Category Category { get; set; }
        public DateTime Time { get; set; }
        public virtual ApplicationUser User { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
    }
}