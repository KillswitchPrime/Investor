namespace Investor
{
    public static class TickerSelector
    {
        public static string GetRandomTicker(Random random, string filePath)
        {
            var fileData = File.ReadLines(filePath);
            var tickers = new List<string>();
            foreach (var line in fileData)
            {
                var items = line.Split(" ");
                tickers.Add(items[0]);
            }

            var index = random.Next(tickers.Count);
            return tickers[index];
        }
    }
}
