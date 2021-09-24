using CoreShared.ModelsDto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Web.Views.Shared.Components.Calculator
{
    [ViewComponent(Name = "Calculator")]
    public class CalculatorViewComponent : ViewComponent
    {

        private readonly IConfiguration _configuration;
        private readonly string UrlApi;

        public CalculatorViewComponent(IConfiguration configuration)
        {
            _configuration = configuration;
            UrlApi = _configuration.GetSection("ApiUrl").Value;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            try
            {
                //Disable certificate validation
                HttpClientHandler clientHandler = new HttpClientHandler();
                clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

                //API request data
                using (HttpClient cli = new HttpClient(clientHandler))
                {
                    //API call
                    var jsonApiResponse = await cli.GetStringAsync(UrlApi + "/calculate");

                    //Json serializer options
                    JsonSerializerOptions options = new JsonSerializerOptions()
                    {
                        PropertyNameCaseInsensitive = true
                    };

                    //Deserialize the answer into a corresponding template list 
                    List<ResultDto> responseDto = JsonSerializer.Deserialize<List<ResultDto>>(jsonApiResponse, options);


                    //Return result
                    return View(responseDto);
                }
            }
            catch (Exception error)
            {
                throw new Exception("Not was possible connect on API: \"" + error.Message + "\"");
            }
        }
    }
}