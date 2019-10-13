using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace YakeenBlog.Models.EFModels
{
    [Table("Category")]
    public class Category
    {
        public Category()
        {
            Posts = new HashSet<Post>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Post> Posts { get; set; }
    }
}