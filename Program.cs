using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace RequestHttp
{
    class Program
    {
       
        static async Task Main(string[] args)
        {
            
            
                Console.WriteLine("Hello World!\n");

                string url = "https://jsonplaceholder.typicode.com/posts";

                HttpClient client = new HttpClient();


                var response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    File.WriteAllText("Object.txt", content);

                    List<Models.Post> ListPost = 
                    JsonSerializer.Deserialize<List<Models.Post>>(content);

                    foreach(var item in ListPost)
                    {
                        Console.WriteLine($"userId:{Convert.ToString(item.userId)}\nId: {item.id}\nTitle: {item.title}\nBody: {item.body}\n");
                    }
                }

            Models.Post oPost = new Models.Post()
            {
                userId = 101,
                title = "I am Title",
                body = "I am Body"
            };

            var data = JsonSerializer.Serialize(oPost);
            HttpContent Httpcontent = new StringContent(data, System.Text.Encoding.UTF8, "application/json");
            var ResponsePost = await client.PostAsync(url, Httpcontent);

            if (ResponsePost.IsSuccessStatusCode)
            {
                var result = await ResponsePost.Content.ReadAsStringAsync();
                var ReadResult = JsonSerializer.Deserialize<Models.Post>(result);
                Console.WriteLine(ReadResult);
            }
            




            Console.ReadKey();

        }
    }
}
