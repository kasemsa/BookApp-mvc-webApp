namespace BookApp.Models
{
    public class Question
    {
       
        public int Id { get; set; }

        [NotMapped]
        public string[] tags { get; set; }

        [NotMapped]
        public Owner? owner { get; set; }
        public bool is_answered { get; set; }
        public int view_count { get; set; }
        public int answer_count { get; set; }
        public int score { get; set; }
        public int last_activity_date { get; set; }
        public int creation_date { get; set; }
        public int question_id { get; set; }
        public string? content_license { get; set; }
        public string? Link { get; set; }
        public string? Title { get; set; }

    }
}

