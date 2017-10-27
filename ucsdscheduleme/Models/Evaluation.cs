namespace ucsdscheduleme.Models
{
    public class Evaluation
    {
        public int Id { get; set; }

        public Course Course { get; set; }
        public Professor Professor { get; set; }
        public Cape Cape { get; set; }
        public RateMyProfessor RateMyProfessor { get; set; }
    }
}