using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using Leaf.xNet;

namespace Youtube_Viewers.Helpers
{
    internal class ProxyQueue
    {
        private readonly object locker = new object();
        private ProxyClient[] plist;
        private ConcurrentQueue<ProxyClient> proxies;

        public ProxyQueue(string pr, ProxyType type)
        {
            Type = type;
            SafeUpdate(pr);
            proxies = new ConcurrentQueue<ProxyClient>(plist);
        }

        public int Count => proxies.Count;
        public int Length => plist.Length;

        public ProxyType Type { get; }

        private List<string> GetProxies(string str)
        {
            var res = new HashSet<string>();

            foreach (var proxy in MatchAndFormatProxies(str))
                try
                {
                    if (!res.Contains(proxy))
                        res.Add(proxy);
                }
                catch
                {
                    // ignored
                }

            return new List<string>(res);
        }

        private List<string> MatchAndFormatProxies(string str)
        {
            var res = new List<string>();

            var list = str.Split(new[] {"\n", "\r\n"}, StringSplitOptions.None);

            foreach (var lineStock in list)
            {
                var line = lineStock.Trim();

                try
                {
                    var formatted = FormatLine(line);
                    if (!string.IsNullOrEmpty(formatted))
                        res.Add(formatted);
                }
                catch
                {
                    // ignored
                }
            }

            return res;
        }

        private string FormatLine(string line)
        {
            var lineSplit = line.Split(':');
            if (lineSplit.Length < 2 || lineSplit.Length > 4) return string.Empty;

            var formatted = string.Empty;

            if (line.Contains("@") && lineSplit.Length == 3)
            {
                lineSplit = line