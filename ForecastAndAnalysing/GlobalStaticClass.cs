using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForecastAndAnalysing
{
    static class GlobalStaticClass
    {

        private static int iUserId;

        public static int commonUserId
        {
            get { return iUserId; }
            set { iUserId = value; }

        }
    }
}
