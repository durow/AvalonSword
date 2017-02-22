using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WpfSample.Infrastructure
{
    public static class Extensions
    {
        public static string ToStandard(this DateTime dt)
        {
            return dt.ToString("yyyy-MM-dd HH:mm:ss");
        }
    }
}
