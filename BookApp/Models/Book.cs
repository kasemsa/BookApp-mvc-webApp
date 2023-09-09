
namespace BookApp.Models
{
    public class Book
    {
        [Key]
        public int BookID { get; set; }
        public string Title { get; set; }
        public int AuthorID { get; set; }
        public Author Author { get; set; }

        [ForeignKey("Category")]
        public int CategoryID { get; set; }
        public virtual Category Category { get; set; }

        [ForeignKey("Subcategory")]
        public int SubcategoryID { get; set; }
        public virtual Subcategory Subcategory { get; set; }
    }
}
