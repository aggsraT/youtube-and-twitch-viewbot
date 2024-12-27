using System.Text.RegularExpressions;

namespace Youtube_Viewers.Helpers
{
    internal static class RegularExpressions
    {
        public static Regex Viewers =
            new Regex(
                @"viewCount\"":{\""videoViewCountRenderer\"":{\""viewCount\"":{\""runs\"":\[{\""text\"":\""(.+?)\""}",
                RegexOptions.Compil