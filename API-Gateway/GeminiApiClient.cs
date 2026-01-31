using System.Text.Json;
using System.Text;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace API_Gateway;  
public class GeminiApiClient
{
    public static async Task<string> Do(string text)
    {
        var apiKey = "AIzaSyD2d76MYsIsrlfXMDbVHxQui6pqJrj92qY";
        var url = $"https://generativelanguage.googleapis.com/v1beta/models/gemini-3-flash-preview:generateContent?key={apiKey}";
        var requestBody = new
        {
            contents = new[]
            {
                new {
                    parts = new[]
                    {
                        new { text = $"Ordne bitte folgende Ticket-Nachricht eines Kunden einer der folgenden Priorität zu: sehr niedrig(1), niedrig(2), mittel(3), hoch(4), sehr hoch(5). 'sehr niedrig' kann eine Kundenfrage sein oder ähnliches. 'mittel' sind standartmäßige Anfragen, die im Laufe eines Arbeitstages bearbeitet werden sollten. 'Sehr hoch' bedeutet, dass der Kunde ein nicht funktionales Produkt hat, wo sich umgehend drum gekümmert werden soll. Bedenke bei deiner Auswahl auch noch die Zwischenprioritäten, die immer einen Mittelweg darstellen. Antworte bitte in folgender Syntax: Priorität(nur die Zahl, keine Sonderzeichen oder ähnliches) || Begründung. Jetzt folgt die Anfrage: {text}" }

                    }
                }
            }
        };

        var json = JsonSerializer.Serialize(requestBody);
        var content = new StringContent(json, Encoding.UTF8, "application/json");


        using var client = new HttpClient();
        var response = await client.PostAsync(url, content);
        var responseText = await response.Content.ReadAsStringAsync();

        return responseText;
    }
}

