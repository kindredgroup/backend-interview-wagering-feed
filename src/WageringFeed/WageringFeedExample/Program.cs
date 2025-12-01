using System.Net.WebSockets;
using System.Text;

class Program
{
    static async Task Main(string[] args)
    {
        //TODO - insert your provided candidateId here
        var candidateId = "";

        using var websocket = new ClientWebSocket();
        var cts = new CancellationTokenSource();
        await websocket.ConnectAsync(
            new Uri(
                $"ws://betfeedfdj25-dkfbamhzh5cdhxfs.australiaeast-01.azurewebsites.net/ws?candidateId={candidateId}"),
            cts.Token);

        var buffer = new byte[1024];

        while (!cts.Token.IsCancellationRequested && websocket.State == WebSocketState.Open)
        {
            var result = await websocket.ReceiveAsync(new ArraySegment<byte>(buffer), cts.Token);
            if (result.MessageType == WebSocketMessageType.Close)
            {
                // stop consuming
                break;
            }

            var baseMessageJson = Encoding.UTF8.GetString(buffer, 0, result.Count);
            Console.WriteLine(baseMessageJson);
        }
    }
}