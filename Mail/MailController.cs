using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Backend.Mail
{
    public class MailController
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;

        public MailController(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _apiKey = "mlsn.680a03e7648ce10e791b45d82798606f5407132ec38ac70eb9a45983ed1405cc";
        }

        public async Task EnviarCorreo(string email, string name, string password)
        {
            try
            {
                string url = "https://api.mailersend.com/v1/email";

                var emailMessage = new Email
                {
                    from = new From { email = "your_email@your_verified_domain.com" },
                    to = new[] { new To { email = email } },
                    subject = "Registration Confirmation",
                    text = $"Dear {name},\n\nThank you for registering. Your password is: {password}\n\nBest regards,\nYour Company",
                    html = $"<p>Dear {name},</p><p>Thank you for registering. Your password is: {password}</p><p>Best regards,<br>Your Company</p>"
                };

                var jsonRequest = JsonSerializer.Serialize(emailMessage);
                var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

                _httpClient.DefaultRequestHeaders.Clear();
                _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {_apiKey}");

                var response = await _httpClient.PostAsync(url, content);

                response.EnsureSuccessStatusCode();
                Console.WriteLine($"Correo enviado exitosamente a {email}");
            }
            catch (HttpRequestException ex)
            {   
                Console.WriteLine($"Error al enviar correo: {ex.Message}");
                throw new Exception("Error sending email", ex);
            }
        }
    }

    public class Email
    {
        public From from { get; set; }
        public To[] to { get; set; }
        public string subject { get; set; }
        public string text { get; set; }
        public string html { get; set; }
    }

    public class From
    {
        public string email { get; set; }
    }

    public class To
    {
        public string email { get; set; }
    }
}
