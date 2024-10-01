namespace WPLBlazor.App.Models
{
    public class WeekGroup : List<WeekFullInfo>
    {
        public string GroupName { get; private set; }

        public WeekGroup(string groupName, List<WeekFullInfo> weeks) : base(weeks)
        {
            GroupName = groupName;
        }
    }
}
