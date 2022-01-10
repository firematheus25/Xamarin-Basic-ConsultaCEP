using ConsultaCEP.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using ConsultaCEP.Services;
using ConsultaCEP.Models;

namespace ConsultaCEP
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            BOTAO.Clicked += SearchCEP;
        }

        private void SearchCEP(object sender, EventArgs args)
        {
            try
            {
                var Address = CEP.Text.Trim();
                if (IsValidCep(Address))
                {
                    var response = ServiceAddressViaCep.searchAddressViaCep(Address);
                    if (response != null)
                        RESULTADO.Text = string.Format("Endereço: {0}, {1} - {2} ({3} - {4})", response.logradouro, response.complemento, response.bairro, response.localidade, response.uf);
                    else
                    {
                        DisplayAlert("Erro", "Endereço não encontrado para o cep informado: " + Address, "Ok");
                    }
                }
            }
            catch (Exception e)
            {

                DisplayAlert("Erro Crítico", e.Message, "Ok");
            }                   
        }


        private bool IsValidCep(string address)
        {
            string message = "";
            bool valid = true;
            int newCep= 0;

            if (address.Length < 8)
            {
                message += "Cep deve conter 8 caracteres.\n";
                valid = false;   
            }

            if (!int.TryParse(address, out newCep))
            {
                message += "Cep deve conter somente números.";
                valid=false;
            }

            if (!valid)
            {
                DisplayAlert("Cep Ínvalido", message, "Ok");
            }

            return valid;

        }

    }
}
