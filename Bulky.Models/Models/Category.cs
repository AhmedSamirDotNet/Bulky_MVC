using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Bulky.DataAccess.Models
{
    public class Category
    {
        public int Id { get; set; }
        [MaxLength(30)]
        [Required]
        public string Name { get; set; }

        [DisplayName("Display Order")]
        [Range(1,100,ErrorMessage ="Rage must be between 1-100")]
        public int DisplayOrder { get; set; }
    }
}
