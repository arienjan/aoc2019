using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;

namespace Shared
{
    public static partial class InputGetter
    {
        private static readonly HttpClient httpClient;
        
        static InputGetter() {
            var sessionId = session;
            httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("Cookie", sessionId);
        }
        
        public static async Task<string> ReadInputAsString(int jaar, int dag) {
            var input = await httpClient.GetAsync($"http://adventofcode.com/{jaar}/day/{dag}/input");
            var bla = await input.Content.ReadAsStringAsync();
            return bla;
        }
        
        public static async Task<List<string>> ReadInputAsLines(int jaar, int dag) {
            var input = await httpClient.GetAsync($"http://adventofcode.com/{jaar}/day/{dag}/input");
            var bla = await input.Content.ReadAsStringAsync();
            var lines = bla.Split(
                new[] { "\r\n", "\r", "\n" },
                StringSplitOptions.None);
            var listOfLines = lines.ToList();
            listOfLines.RemoveAt(listOfLines.Count() - 1);
            return listOfLines;
        }
    }
}
