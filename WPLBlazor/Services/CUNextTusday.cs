namespace WPLBlazor.Services
{
    public class CUNextTusday
    {
        public DateOnly GetNextTuesday(DateOnly date)
        {
            if (date.DayOfWeek.ToString() != "Tuesday")
            {
                switch (date.DayOfWeek.ToString())
                {
                    case "Monday":
                        date = date.AddDays(1);
                        break;
                    case "Wednesday":
                        date = date.AddDays(6);
                        break;
                    case "Thursday":
                        date = date.AddDays(5);
                        break;
                    case "Friday":
                        date = date.AddDays(4);
                        break;
                    case "Saturday":
                        date = date.AddDays(3);
                        break;
                    case "Sunday":
                        date =  date.AddDays(2);
                        break;
                }
            }
            return date;
        }
    }
}
