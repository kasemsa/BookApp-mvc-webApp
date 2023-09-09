

namespace BookApp.Models
{
    public class Subcategory
    {
        [Key]
        public int SubcategoryID { get; set; }
        public string SubcategoryName { get; set; }

        [ForeignKey("Category")]
        public int CategoryID { get; set; }
        public virtual Category Category { get; set; }
    }
}
