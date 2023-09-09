
namespace BookApp.Models
{
    public class Owner
    {
       

        public int account_id { get; set; }
        public int reputation    { get; set; }
        public int user_id { get; set; }
        public String user_type { get; set; }
        public String profile_image { get; set; }

        public String display_name { get; set;}

        public String link { get; set;}

       
    }
}
