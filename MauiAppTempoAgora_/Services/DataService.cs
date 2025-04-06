using MauiAppTempoAgora_.Models;
using Newtonsoft.Json.Linq;
using System.Net;

namespace MauiAppTempoAgora_.Services
{
    public class DataService
    {
        public static async Task<Tempo?> GetPrevisao(string cidade)
        {
            Tempo? t = null;

            string chave = "d9f149a1a4e169be767f3fdaeb133070";
            string url = $"https://api.openweathermap.org/data/2.5/weather?" +
                $"q={cidade}&units=metric&appid={chave}";

            using (HttpClient client = new HttpClient())
            {
                try
                {

                    HttpResponseMessage resp = await client.GetAsync(url);

                    if (resp.StatusCode == HttpStatusCode.NotFound)
                    {
                        throw new Exception("Cidade não encontrada, digite novamente.");
                        // Mensagem específica para quando a cidade não for encontrada.

                    }

                    else if (resp.IsSuccessStatusCode)
                    {
                        string json = await resp.Content.ReadAsStringAsync();

                        var rascunho = JObject.Parse(json);

                        DateTime time = new();
                        DateTime sunrise = time.AddSeconds((double)rascunho["sys"]["sunrise"]).ToLocalTime();
                        DateTime sunset = time.AddSeconds((double)rascunho["sys"]["sunset"]).ToLocalTime();


                        t = new()
                        {
                            lat = (double)rascunho["coord"]["lat"],
                            lon = (double)rascunho["coord"]["lon"],
                            description = (string)rascunho["weather"][0]["description"],
                            main = (string)rascunho["weather"][0]["main"],
                            temp_min = (double)rascunho["main"]["temp_min"],
                            temp_max = (double)rascunho["main"]["temp_max"],
                            speed = (double)rascunho["wind"]["speed"],
                            visibility = (int)rascunho["visibility"],
                            sunrise = sunrise.ToString(),
                            sunset = sunset.ToString(),
                        }; // Fecha obj do Tempo.
                    } // Fecha if se o status do servidor foi de sucesso.
                }
                catch (HttpRequestException ex)
                {
                    throw new Exception("Erro ao acessar o servidor, tente novamente mais tarde.");
                    // Mensagem específica para quando houver erro de conexão com o servidor.
                }
                catch (Exception ex)
                {
                    throw new Exception("Erro inesperado: " + ex.Message);
                    // Mensagem genérica para outros erros.
                }

            } // Fecha o using.
                return t;
        }
    }
}
