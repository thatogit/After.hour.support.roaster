namespace After.hour.support.roaster.api.Model.Utils
{
    public static class LineSupport
    {
        private static string _firstLine;
        private static string _secondLine;
        private static DateTime _supportDate;
        public static string FirstLine
        {
            get { return _firstLine; }
            set { _firstLine = value; }

        }
        public static string SecondLine
        {
            get { return _secondLine; }
            set { _secondLine = value; }

        }
        public static DateTime SupportDate
        {
            get { return _supportDate; }
            set { _supportDate = value; }

        }
    }
}
