namespace WPLBlazor.API.Models
{
    public class WeeksView
    {
        public int Week_Id { get; set; }
        public int Home_Team {  get; set; }
        public int Away_Team { get; set; }
        public bool Forfeit { get; set; }
        public bool Home_Won { get; set; }
        public bool Playoff { get; set; }
    }
}
