using CoreShared.ModelsDto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Net.Http;
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

        public async Task<IViewComponentResult> InvokeAsync(CalculatorInput input)
        {
            try
            {
                //Disable ssl certificate validation (for linux environtments)
                HttpClientHandler clientHandler = new HttpClientHandler();
                if (_env.IsDevelopment() && Environment.OSVersion.Platform == PlatformID.Unix)
                {
                    clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
                }

                //API request call (with resource auto dispose)
                using (HttpClient cli = new HttpClient(clientHandler))
                {
                    //API request already answered in json string format
                    var queryString = @Url.ActionLink(null, null, input);
                    var url = UrlApi + queryString.Substring(queryString.IndexOf("?"));

                    var jsonApiResponse = await cli.GetStringAsync(url);

                    //Json serializer options (disable case sensitive deserialization)
                    JsonSerializerOptions options = new JsonSerializerOptions()
                    {
                        PropertyNameCaseInsensitive = true
                    };

                    //Deserialize the answer into a corresponding template list 
                    CalculatorResultDto responseDto = JsonSerializer.Deserialize<CalculatorResultDto>(jsonApiResponse, options);

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