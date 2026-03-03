using System.Text.Json;
using System.Text;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace API_Gateway;  
public class GeminiApiClient
{
    public static async Task<Tuple<int,string>> Do(string text)
    {
        string apiKey = File.ReadAllLines("GEMINI_API_KEY.txt")[0];
        string url = $"https://generativelanguage.googleapis.com/v1beta/models/gemini-3-flash-preview:generateContent?key={apiKey}";
        var requestBody = new
        {
            contents = new[]
            {
                new {
                    parts = new[]
                    {
                        new { text = $"Ordne bitte folgende Ticket-Nachricht eines Kunden einer der folgenden Priorität zu: sehr niedrig(1), niedrig(2), mittel(3), hoch(4), sehr hoch(5). 'sehr niedrig' kann eine Kundenfrage sein oder ähnliches. 'mittel' sind standartmäßige Anfragen, die im Laufe eines Arbeitstages bearbeitet werden sollten. 'Sehr hoch' bedeutet, dass der Kunde ein nicht funktionales Produkt hat, wo sich umgehend drum gekümmert werden soll. Bedenke bei deiner Auswahl auch noch die Zwischenprioritäten, die immer einen Mittelweg darstellen. Antworte bitte in folgender Syntax: Priorität(nur die Zahl (1-5), keine Sonderzeichen oder ähnliches) || Begründung. Jetzt folgt die Anfrage: {text}" }

                    }
                }
            }
        };
        
        var json = JsonSerializer.Serialize(requestBody);
        var content = new StringContent(json, Encoding.UTF8, "application/json");


        using var client = new HttpClient();
        var response = await client.PostAsync(url, content);
        Console.WriteLine(await response.Content.ReadAsStringAsync());
        GeminiResponseDto.Rootobject geminiResponseDto = JsonSerializer.Deserialize<GeminiResponseDto.Rootobject>(response.Content.ReadAsStreamAsync().Result);
        string[]responseText = geminiResponseDto?
            .candidates
            .FirstOrDefault()?
            .content?
            .parts?
            .FirstOrDefault()?
            .text
            .Split("||");
        Console.WriteLine("GEMINI EVAL:" + responseText[0]);
        return new Tuple<int,string>(Convert.ToInt32(responseText[0].ToString()), responseText[1]);
    }
}

