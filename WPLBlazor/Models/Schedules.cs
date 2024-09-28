namespace WPLBlazor.Models
{
    public class Schedules
    {
        public int Week_Id { get; set; }
        public DateOnly Date { get; set; }
        public int Home_Team { get; set; }
        public int Away_Team { get; set; }
        public int? Table_Number { get; set; }
    }
}