﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounting.Utility.Convertor
{
    public static class DateConvertor
    {
        private static object dateTime;

        public static string ToShamsy(this DateTime value)
        {
            PersianCalendar pc = new PersianCalendar();
            return pc.GetYear(value) + "/" + pc.GetMonth(value).ToString("00") + "/" + pc.GetDayOfMonth(value).ToString("00");
        }
        public static DateTime ToMiladi(DateTime dateTime)
        {

            return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, new System.Globalization.PersianCalendar());
        }
    }
     
}
