namespace IronSourceAnalyticsSDK
{
    public class ISAnalyticsUserProgress
    {
        private string appLevel1;
        private string appLevel2;
        private string appLevel3;
        private ISAnalyticsProgressState progressState;
        private int? userScore;
        private int? userAttempt;

        public ISAnalyticsUserProgress(string level1)
        {
            this.appLevel1 = level1;
        }

        public ISAnalyticsUserProgress level2(string value)
        {
            appLevel2 = value;
            return this;
        }

        public ISAnalyticsUserProgress level3(string value)
        {
            appLevel3 = value;
            return this;
        }

        public ISAnalyticsUserProgress score(int amount)
        {
            userScore = amount;
            return this;
        }

        public ISAnalyticsUserProgress attempt(int amount)
        {
            userAttempt = amount;
            return this;
        }

        public ISAnalyticsUserProgress state(ISAnalyticsProgressState state)
        {
            progressState = state;
            return this;
        }

        internal int? getScore()
        {
            return userScore;
        }

        internal string getLevel1()
        {
            return appLevel1;
        }

        internal int? getAttempt()
        {
            return userAttempt;
        }

        internal string getLevel2()
        {
            return appLevel2;
        }

        internal string getLevel3()
        {
            return appLevel3;
        }

        internal ISAnalyticsProgressState getState()
        {
            return progressState;
        }
    }
}