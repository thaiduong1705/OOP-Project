namespace JobRecommendationWeb.AddingClasses
{
    public class UsingAccount
    {
        private static UsingAccount _instance;
        public static UsingAccount Instance 
        { 
            get { 
                if (_instance == null)
                    _instance = new UsingAccount();
                return _instance;
            } 
            set { _instance = value; }
        }

        public Taikhoan Taikhoan { get; set; } = null;
    }
}
