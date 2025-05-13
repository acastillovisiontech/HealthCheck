using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCheckTools
{
    public class WebhookService
    {
        private readonly HttpClient _httpClient;

        public WebhookService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task SendNotificationAsync(string message)
        {
            var webhookUrl = "https://testing12465.free.beeceptor.com"; // URL webhook
            var content = new StringContent(
                JsonConvert.SerializeObject(new { text = message }),
                Encoding.UTF8,
                "application/json"
            );

            var response = await _httpClient.PostAsync(webhookUrl, content);
            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException("Error al enviar el webhook");
            }
        }
    }
}
