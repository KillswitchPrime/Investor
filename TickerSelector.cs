namespace Investor
{
    public static class TickerSelector
    {
        public static Ticker GetRandomTicker(Random random, string filePath)
        {
            var fileData = File.ReadLines(filePath).ToList();
            var index = random.Next(fileData.Count);
            var data = fileData[index].Split(" ");

            return new Ticker
            {
                Symbol = data[0].Trim(),
                Name = data[1].Trim(),
                Sector = data[2].Trim()
            };
        }
    }
}
