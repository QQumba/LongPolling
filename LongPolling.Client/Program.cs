var client = new HttpClient();
client.BaseAddress = new Uri("https://localhost:7023/");
client.Timeout = TimeSpan.FromMilliseconds(Timeout.Infinite);
var response = await client.GetAsync("poll");
var message = await response.Content.ReadAsStringAsync();

Console.WriteLine(message);