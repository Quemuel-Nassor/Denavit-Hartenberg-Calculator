using CoreShared.ModelsDto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Mime;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Web.Views.Shared.Components.GetCalculatorResult
{
    [ViewComponent(Name = "GetCalculatorResult")]
    public class GetCalculatorResultViewComponent : ViewComponent
    {
        /// Dependencies
        private readonly IHostEnvironment _env;
        private readonly IConfiguration _configuration;
        private readonly string UrlApi;

        /// <summary>
        /// Dependencies injection
        /// </summary>
        /// <param name="configuration"> Interface for manipulating appSettings.json file </param>
        /// <param name="env"> Interface to access environment variables </param>
        public GetCalculatorResultViewComponent(
            IConfiguration configuration,
            IHostEnvironment env)
        {
            _env = env;
            _configuration = configuration;
            UrlApi = _configuration.GetSection("ApiUrl").Value + "/calculate";
        }

        public async Task<IViewComponentResult> InvokeAsync(ApiInput input)
        {
            try
            {
                List<CalculatorResultDto> responseDto = new List<CalculatorResultDto>();

                //Disable ssl certificate validation (for linux environtments)
                HttpClientHandler clientHandler = new HttpClientHandler();
                if (_env.IsDevelopment() && Environment.OSVersion.Platform == PlatformID.Unix)
                {
                    clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
                }

                //API request call (with resource auto dispose)
                using (HttpClient cli = new HttpClient(clientHandler))
                {
                    //Json serializer options (disable case sensitive deserialization)
                    JsonSerializerOptions options = new JsonSerializerOptions()
                    {
                        PropertyNameCaseInsensitive = true
                    };

                    //Serialize input model
                    var jsonInputString = JsonSerializer.Serialize(input);
                    var content = new StringContent(jsonInputString, Encoding.UTF8, MediaTypeNames.Application.Json);

                    //Post request on API
                    var jsonApiResponse = await cli.PostAsync(UrlApi, content);

                    //Check if response return SUCCESS
                    jsonApiResponse.EnsureSuccessStatusCode();

                    //Deserialize response
                    var responseContent = await jsonApiResponse.Content.ReadAsStringAsync();
                    responseDto = JsonSerializer.Deserialize<List<CalculatorResultDto>>(responseContent, options);

                    //Return result
                    return View(responseDto);
                }
            }
            catch (Exception error)
            {
                throw new Exception("An error occurred when try to connect on API: \"" + error.Message + "\"");
            }
        }
    }
}