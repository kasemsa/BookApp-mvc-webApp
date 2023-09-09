using System.ComponentModel.DataAnnotations;
namespace BookApp.Models
{
    public class BookViewModel
    {
        [Display(Name = "Category")]
        public int CategoryID { set; get; }

        public IEnumerable<Category>? Categories { set; get; }

        [Display(Name = "Subcategory")]
        public int SubcategoryID { set; get; }

        public IEnumerable<Subcategory>? Subcategories { set; get; } = new List<Subcategory>();
    }
}
