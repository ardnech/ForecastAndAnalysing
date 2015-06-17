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
        private static int iProductId;
        private static int iScenarioId;

        public static int commonUserId
        {
            get { return iUserId; }
            set { iUserId = value; }

        }

        public static int commonProductId
        {
            get { return iProductId; }
            set { iProductId = value; }

        }

        public static int commonScenarioId
        {
            get { return iScenarioId; }
            set { iScenarioId = value; }

        }

    }
}
