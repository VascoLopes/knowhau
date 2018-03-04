using MobileKnowHau.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileKnowHau
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Favoritos : ContentPage
    {
        String ficheiro;
        public Favoritos(object txtUsername)
        {
            InitializeComponent();
            ficheiro = txtUsername.ToString();
            LeInfoUsuario111(ficheiro);

        }
        
        string[] words;
        public async void LeInfoUsuario111(string arquivo)
        {
            string conteudo = await PCLHelper.ReadAllTextAsync(arquivo);
            if (conteudo.Equals(""))
            {
                EmtHist.IsVisible = true;
            }
            else
            {
                EmtHist.IsVisible = false;
            }
            Historico2.Text = conteudo.ToString();
            words = Historico2.Text.Split('-');
            teste.ItemsSource = words;

            //await DisplayAlert("KSADJF", "asd:> " + conteudo.ToString(), "okok");
        }
        private void Search_CLicked(object sender, EventArgs e)
        {
            var keyword = MainSearchBar.Text;
            try
            {
                //listaUtilizador.ItemsSource = historico.Where(historico => historico.userMAIL.Contains(keyword));      
                teste.ItemsSource = words.Where(words => words.ToLower().Contains(keyword.ToLower()));
            }
            catch (Exception e5)
            {
                LeInfoUsuario111(ficheiro);
            }
        }
        private async void Lixo(object sender, EventArgs e)
        {
            try
            {
                if (Historico2.Text != "")
                {
                    Device.BeginInvokeOnMainThread(async () => {
                        var result = await this.DisplayAlert("Delete!", "Are you sure you want to delete this registry?", "Yes", "No");
                        if (result)
                        { await PCLHelper.WriteTextAllAsync(ficheiro, ""); Historico2.Text = ""; LeInfoUsuario111(ficheiro);await PCLHelper.DeleteFile(ficheiro); }
                    });
                    
                    

                }

                LeInfoUsuario111(ficheiro);

            }
            catch (Exception ola)
            {

            }
            // await PCLHelper.WriteTextAllAsync(ficheiro, "");
        }
        private async void Lixo2(object sender, EventArgs e)
        {
            //DisplayAlert("OK", "ENJREi", "ok");
            var mi = ((MenuItem)sender);

            String ver = (String)mi.CommandParameter; //Tem o do historico
            string[] words = Historico2.Text.Split('-');
            int conta = 0;
            String texto = "";
            for (int i = 0; i < words.Length; i++)
            {
                if (words[i].Equals(ver))
                {
                    continue;
                }
                else
                {
                    if (conta == 0)
                    {
                        texto = words[i];
                        conta++;
                    }
                    else
                    {
                        if (!texto.Contains(words[i]))
                        {
                            texto = texto + "-" + words[i];
                            conta++;
                        }

                    }
                }

            }
            Historico2.Text = "";
            await PCLHelper.WriteTextAllAsync(ficheiro, texto);
            //  DisplayAlert("OK", "tenho " + ver.ToString(), "OK");
            LeInfoUsuario111(ficheiro);

        }
    }
}