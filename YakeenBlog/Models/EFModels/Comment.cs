using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace YakeenBlog.Models.EFModels
{
    [Table("Comment")]
    public class Comment
    {
        public int Id { get; set; }
        [Required]
        public string Text { get; set; }
        public DateTime Time { get; set; }
        public virtual ApplicationUser User { get; set; }
        public Post Post { get; set; }
    }
}