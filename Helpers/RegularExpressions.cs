﻿using System.Text.RegularExpressions;

namespace Youtube_Viewers.Helpers
{
    internal static class RegularExpressions
    {
        public static Regex Viewers =
            new Regex(
                @"viewCount\"":{\""videoViewCountRenderer\"":{\""viewCount\"":{\""runs\"":\[{\""text\"":\""(.+?)\""}",
                RegexOptions.Compiled);

        public static Regex Title = new Regex(@"\""title\"":{\""runs\"":\[{\""text\"":\""(.+?)\""}",
     