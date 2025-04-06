﻿using MauiAppTempoAgora_.Models;
using MauiAppTempoAgora_.Services;

namespace MauiAppTempoAgora_
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            InitializeComponent();
        }

        

        private async void Button_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txt_cidade.Text))
                {
                    Tempo? t = await DataService.GetPrevisao(txt_cidade.Text);

                    if(t != null)
                    {
                        string dados_previsao = "";

                        dados_previsao = $"Latitude: {t.lat} \n" +
                                        $"Longitude: {t.lon} \n" +
                                        $"Temperatura Minima: {t.temp_min} \n" +
                                        $"Temperatura Maxima: {t.temp_max} \n" +
                                        $"Descrição: {t.description} \n" +
                                        $"Main: {t.main} \n" +
                                        $"Velocidade do Vento: {t.speed} \n" +
                                        $"Visibilidade: {t.visibility} \n" +
                                        $"Nascer do Sol: {t.sunrise} \n" +
                                        $"Pôr do Sol: {t.sunset} \n";

                        lbl_res.Text = dados_previsao;


                    }
                    else
                    {
                        lbl_res.Text = "Sem dados de Previsão";
                    }

                }
                else
                {
                    lbl_res.Text = "Preencha a cidade.";
                }

            }
            catch(Exception ex)
            {
              await DisplayAlert("Ops", ex.Message, "OK");
            }
        }
    }

}
