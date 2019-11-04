using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace yeopjipRandom
{
    public class Constants
    {
        public static string APP_TITLE = "랜덤 팀짜기";
        private static string APP_DOMAIN = AppDomain.CurrentDomain.BaseDirectory;
        public static string MEMBER_LIST_PATH = APP_DOMAIN + "randomteam.records";
        public static string FAVORITE_LIST_PATH = APP_DOMAIN + "randomteam.favorite";
        public static int WIDTH = 850;
        public static int HEIGHT = 650;
    }
}
