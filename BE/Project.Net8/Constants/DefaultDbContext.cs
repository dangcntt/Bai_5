namespace NKTM.Constants
{
    public static class DefaultNameCollection
    {
        public const string USERS = "CR_USERS";
        public const string REFRESHTOKEN = "CR_REFRESHTOKEN";
        
        public const string DIAGIOIHANHCHINH = "CR_DIAGIOIHANHCHINH";
        public const string LOGGING = "CR_LOGGING";
        public const string MENU = "CR_MENU";
      
        public const string API = "CR_API";
        public const string UNIT_ROLE = "CR_UNIT_ROLE";
      

        public const string FILES = "CR_FILES";
       

        
        
        
        
        
        
        public const string MENU_CITIZEN = "NV_MENU_CITIZEN";
        
        public const string NEWS = "NV_NEWS";
        
        public const string BAITHI = "NV_BAITHI";
        public const string THEDIEM = "NV_THEDIEM";

        public const string UNIT_GROUP = "NV_UNIT_GROUP";
        
        public const string DEPARTMENT = "NV_DEPARTMENT";
        public const string PAGE_INFO = "NV_PAGE_INFO";
        public const string ROOM = "NV_ROOM";
        public const string DOCTOR = "NV_DOCTOR";
        //public const string DICHVU = "NV_DOCTOR";
        
        #region 
        public const string CAROUSELSLIDER = "NV_CAROUSELSLIDER";
        
       
        
        public const string CONSTRUCTORCALENDAR = "NV_CONSTRUCTORCALENDAR";


        public const string HOLIDAY = "NV_HOLIDAY";
        public const string BOOKINGDOCTOR = "NV_BOOKINGDOCTOR";
        public const string CITIZEN = "NV_CITIZEN";


        #endregion
        
        
        #region ĐIỀU TRỊ
        public const string NEWSSECTIONMOBI = "NV_NEWSSECTIONMOBI";

        public const string SERVICE = "NV_SERVICE";
        public const string DICHVUMENU = "NV_SERVICEMENU";
        #endregion
    }

    public static class ConfigurationDb
    {
        #region MONGODB 
        public const string MONGO_CONNECTION_STRING = "DbSettings:ConnectionString";
        public const string MONGO_DATABASE_NAME = "DbSettings:DatabaseName";
        #endregion

        #region POSTGRE
        public const string POSTGRE_CONNECTION = "DbSettings:PostgreConnection";
        #endregion                                                                                                      
    }
}