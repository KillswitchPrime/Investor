using Alpaca.Markets;
using Investor;
using Microsoft.Extensions.Configuration;

//var paperUrl = "https://paper-api.alpaca.markets/v2";
//var liveUrl = "https://api.alpaca.markets/v2";

var secrets = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .Build();

var key = secrets.GetSection("Secrets").GetSection("ApiKey").Value ?? "";
var secret = secrets.GetSection("Secrets").GetSection("ApiSecret").Value ?? "";
var filePath = secrets.GetSection("Secrets").GetSection("FilePath").Value ?? "";


var alpacaClient = Environments.Paper.GetAlpacaTradingClient(new SecretKey(key, secret));
var random = new Random();


var tradeExecuted = false;
while (tradeExecuted is false)
{
    var ticker = TickerSelector.GetRandomTicker(random, filePath);

    var result = await alpacaClient.PostOrderAsync(new NewOrderRequest(
            ticker,
            25,
            OrderSide.Buy,
            OrderType.Market,
            TimeInForce.Day
        )
    );

    if(result.OrderStatus is OrderStatus.Canceled || result.OrderStatus is OrderStatus.Rejected || result.OrderStatus is OrderStatus.Stopped)
    {
        continue;
    }

    tradeExecuted = true;
    Console.WriteLine($"Created buy order for ticker: {ticker}, of quantity: 25.");
}