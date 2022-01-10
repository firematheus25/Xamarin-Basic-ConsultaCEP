using ConsultaCEP.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace ConsultaCEP.Services
{
    public class ServiceAddressViaCep
    {
        private static string _addressURL = "https://viacep.com.br/ws/{0}/json/";

        public static Address searchAddressViaCep(string cep)
        {
            string UrlFormated = string.Format(_addressURL, cep);

            WebClient webClient = new WebClient();
            var response = webClient.DownloadString(UrlFormated);

            var address = JsonConvert.DeserializeObject<Address>(response);

            if (address.cep == null)
            {
                return null;
            }

            return address;
        }


    }
}
