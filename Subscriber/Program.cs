using System.Net.Http.Json;
using Subscriber.Models;

var httpClient = new HttpClient();

Console.WriteLine("Press ESC to stop");
Console.WriteLine("Listening...");

do
{
    while (!Console.KeyAvailable)
    {
        var ackIds = (await GetMessagesAsyn(httpClient)).ToList();

        Thread.Sleep(2000);

        if (ackIds.Count > 0)
        {
            await AckMessagesAsync(httpClient, ackIds);
        }
    }

} while (Console.ReadKey(true).Key != ConsoleKey.Escape);

const string MessagesUri = "https://localhost:7173/api/subscriptions/{0}/messages";

static async Task<IEnumerable<int>> GetMessagesAsyn(HttpClient httpClient)
{
    var ackIds = new List<int>();
    var newMessages = new List<MessageReadModel>();

    try
    {
        newMessages = (await httpClient
            .GetFromJsonAsync<IEnumerable<MessageReadModel>>(
                string.Format(MessagesUri, 2)))
                .ToList();
    }
    catch (Exception)
    {
        return ackIds;
    }

    newMessages.ForEach(m =>
    {
        Console.WriteLine($"{m.Id} - {m.TopicMessage} - {m.MessageStatus}");
        ackIds.Add(m.Id);
    });

    return ackIds;
}

static async Task AckMessagesAsync(HttpClient httpClient, IEnumerable<int> ackIds)
{
    var response = await httpClient
        .PostAsJsonAsync(string.Format(MessagesUri, 2), ackIds);

    var returnMessage = await response.Content.ReadAsStringAsync();

    Console.WriteLine(returnMessage);
}