using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ManagementStore.Extensions
{
    public static class Utils
    {
        public static string GetVideoId(string url)
        {
            var yMatch = new Regex(@"youtu(?:\be|be\.com)/(?:.*v(?:/|=)|(?:.*/)?)([a-zA-Z0-9-_]+)").Match(url);
            return yMatch.Success ? yMatch.Groups[1].Value : string.Empty;
        }
    }
}
