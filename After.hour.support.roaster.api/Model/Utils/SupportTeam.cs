namespace After.hour.support.roaster.api.Model.Utils
{
    public static class SupportTeam
    {
        private static string _teamName;
        private static string _teamLead;

        public static string getTeamName
        {
            get { return _teamName; }
            set { _teamName = value; }

        }
        public static string getTeamLead
        {
            get { return _teamLead; }
            set { _teamLead = value; }

        }

    }
}
