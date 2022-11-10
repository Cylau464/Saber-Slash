namespace IronSourceAnalyticsSDK
{
    public class ISAnalyticsUserActivity
    {
        private string activityName;
        private string activityProprety;

        public ISAnalyticsUserActivity(string name)
        {
            activityName = name;
        }

        public ISAnalyticsUserActivity attribute(string name)
        {
            activityProprety = name;
            return this;
        }

        internal string getName()
        {
            return activityName;
        }

        internal string getProperty()
        {
            return activityProprety;
        }
    }

}