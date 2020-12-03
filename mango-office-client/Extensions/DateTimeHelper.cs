using System;
using System.Collections.Generic;
using System.Text;

namespace MangoOfficeClient.Extensions
{
    public static class DateTimeHelper
    {
        /// <summary>
        /// Convert to UNIX datetime
        /// </summary>
        /// <param name="unixTimeStamp"></param>
        /// <returns></returns>
        public static int ToUnixTimeStamp(this DateTime date)
        {
            int unixTimestamp = (Int32)(date.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
            return unixTimestamp;
        }
    }
}
