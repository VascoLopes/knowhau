using MobileKnowHau.Service;
using MobileKnowHau.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using LocalNotifications.Plugin.Abstractions;
using Xamarin.Forms.PlatformConfiguration;
using LocalNotifications.Plugin;
using LocalNotifications.Plugin.Abstractions;
using Plugin.Toasts;
using System.Threading.Tasks;
using Plugin.Connectivity;
using Windows.UI.Notifications;

namespace MobileKnowHau
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Historico : ContentPage
    {
        DataService dataService;
        List<HistoricoModel> historico;
        Content Conteste;
        object a;
        String ficheiro = "fich.txt";
        String nomeUtilizador;
        public Historico(object txtUsername)
        {
            InitializeComponent();
            dataService = new DataService();
            try
            {
                ficheiro = txtUsername + ficheiro;
                nomeUtilizador = ficheiro;
                AtualizaDados(txtUsername);
                a = txtUsername;
                

            } catch (Exception e2)
            {
               // DisplayAlert("OK", "eero " + e2.ToString(), "OK");
            }

            //MandaCli = a.ToString();

        }

   
        private async void IrDefinicao()
        {
           await Navigation.PushAsync(new Definicoes());
        }
        HistoricoModel historicoUtilizador;
        private async void VeCont(object sender, EventArgs e)
        {

            try
            {
                var mi = ((MenuItem)sender);
                Tuple<String, String, int, int> behis = (Tuple<String, String, int, int>)mi.CommandParameter;
                // historicoUtilizador = (HistoricoModel)mi.CommandParameter;
                int aux2 = behis.Item3;
                Conteste = await dataService.GetContentByIdAsync(aux2);
                String todos = Conteste.contentmsg.ToString(); //"Beacon ID: " + Conteste.beaconID.ToString() + "\nBeacon: " + Conteste.BEACON.ToString() + "\nContent id: " + Conteste.contentID.ToString() + "\nConteudo: " + Conteste.contentmsg.ToString();
                await DisplayAlert("Historic ", "" + todos, "OK");

            }
            catch (Exception ex)
            {
                DisplayAlert("ERROR", "ERRROR" + ex.ToString(), "OK");
            }
        }
        private void Search_CLicked(object sender, EventArgs e)
        {
            var keyword = MainSearchBar.Text;
            try
            {

                //listaUtilizador.ItemsSource = historico.Where(historico => historico.userMAIL.Contains(keyword));
               // listaUtilizador.ItemsSource = historico.Where(historico => historico.historicID.ToString().Contains(keyword)); //historicoUtilizador.contentID.CompareTo(keyword);  // .Where(historicout => historico.ToString().Contains(keyword));
                listaUtilizador.ItemsSource = historico.Where(historico => historico.contentID.ToString().Contains(keyword) ||  historico.userMAIL.ToString().ToLower().Contains(keyword.ToLower()) || historico.date.ToString().Contains(keyword));
                //listaUtilizador.ItemsSource = historico.Where(historico=> historico.userMAIL.ToString().ToLower().Contains(keyword.ToLower()));
                //listaUtilizador.ItemsSource = historico.Where(historico => historico.date.ToString().Contains(keyword));
                //teste.ItemsSource = words.Where(words => words.Contains(keyword));
            }
            catch (Exception e5)
            {
                LeInfoUsuario111(ficheiro);
            }
        }
        private async void Gosto(object sender, EventArgs e)
        {
            String textoCont = "", beaconName = "";
            bool flagA = false;
            try
            {
                List<Content> lia = await dataService.GetListContent();
                Random rnd = new Random();

                while (flagA != true)
                {
                    int random = rnd.Next(lia.Count);
                    // Content a = await dataService.GetContentByIdAsync(random);
                    HistoricoModel h = new HistoricoModel();
                    h.contentID = lia[random].contentID;
                    h.date = DateTime.Now;
                    h.userMAIL = a.ToString();

                    textoCont = lia[random].contentmsg.ToString();
                    Beacon querBe = await dataService.GetBeacon(lia[random].beaconID);

                    String[] state = querBe.name.Split('-');

                    if (state[0].Equals("inactive"))
                    {
                        flagA = false;
                    }
                    else
                    {
                        flagA = true;
                        beaconName = state[1];
                        int cont = Int32.Parse(state[2]);
                        cont++;
                        String nome = state[0] + "-" + state[1] + "-" + cont;
                        Beacon update = new Beacon();
                        update.beaconID = querBe.beaconID;
                        update.majorvalue = querBe.majorvalue;
                        update.minorvalue = querBe.minorvalue;
                        update.model = querBe.model;
                        update.name = nome;
                        await dataService.UpdateBeaconSemLog(update);
                        await dataService.addHistorico(h);
                        AtualizaDados(a.ToString());
                    }
                }
            }
            finally
            {
    
                switch (Device.RuntimePlatform)
                {
                    case Device.iOS:

                        break;
                    case Device.Android:

                            var notification = new LocalNotification
                            {
                                Text = textoCont,
                                Title = beaconName,
                                Id = 2,

                                NotifyTime = DateTime.Now.AddSeconds(5)
                            };
                            var notifier = CrossLocalNotifications.CreateLocalNotifier();
                            notifier.Notify(notification);
                   
                        
                        
                        break;
                    case Device.UWP:
                             String titulo = beaconName;
                               string mensagem = textoCont;
                               string imagem = @"Assets/aaaa.PNG";
                               var notificationXml = ToastNotificationManager.GetTemplateContent(ToastTemplateType.ToastImageAndText02);
                               var toastElements = notificationXml.GetElementsByTagName("text");
                               toastElements[0].AppendChild(notificationXml.CreateTextNode(titulo));
                               var toastElementss = notificationXml.GetElementsByTagName("text");
                               toastElements[1].AppendChild(notificationXml.CreateTextNode(mensagem));

                               var imageElement = notificationXml.GetElementsByTagName("image");
                               imageElement[0].Attributes[1].NodeValue = imagem;
                               var toastNotification = new ToastNotification(notificationXml);
                               ToastNotificationManager.CreateToastNotifier().Show(toastNotification);

                        break;
                    default:

                        break;
                }
            }
            //A213FD51   historico = await dataService.GetHistoricoByUserAsync(userHist.ToString()); 


            //notifier.Cancel(notification.Id);

        }
        private  void Windowspopup(){


        }
        private async void teste22(object sender, EventArgs e)
        {

            try
            {
                var mi = ((MenuItem)sender);
                Tuple<String, String, int, int> behis = (Tuple<String, String, int, int>)mi.CommandParameter;

                int aux2 = behis.Item3;
                Conteste = await dataService.GetContentByIdAsync(aux2);
                // DisplayAlert("OK", "ve " + Conteste.contentmsg.ToString(), "Ok");
                
                String aux = Historico2.Text;
                String[] auxNelson = Historico2.Text.Split('-');


                bool existe = await PCLHelper.ArquivoExisteAsync(ficheiro);
                if (existe != true)
                {
                    await PCLHelper.CriarArquivo(ficheiro);
                    Historico2.Text = "";
                    aux = Conteste.contentmsg.ToString();

                    await PCLHelper.WriteTextAllAsync(ficheiro, aux);
                    LeInfoUsuario111(ficheiro);
                }
                else
                {

                    if (!auxNelson.Contains(Conteste.contentmsg.ToString()))
                    {
                        if (aux == "")
                        {
                            aux = Conteste.contentmsg.ToString();
                        }
                        else
                        {
                            aux = aux + "-" + Conteste.contentmsg.ToString();
                        }
                    }
                    await PCLHelper.WriteTextAllAsync(ficheiro, aux);


                    //await DisplayAlert("Login", "já existe", "OK");

                    LeInfoUsuario111(ficheiro);
                    
                }
                auxNelson = null;
                Historico2.Text = "";
                AtualizaDados(a);
            }
            catch (Exception ex)
            {
                await DisplayAlert("Erro", ex.Message, "OK");
            }
        }
        string[] words;
        public async void LeInfoUsuario111(string arquivo)
        {
            Historico2.Text = "";
            string conteudo = await PCLHelper.ReadAllTextAsync(arquivo);
            Historico2.Text = conteudo.ToString();
            words = Historico2.Text.Split('-');

            teste.ItemsSource = conteudo.ToString().Split('-');
            //DisplayAlert("tps", "todos " + conteudo.ToString(), "asd");


            AtualizaDados(a);

            //await DisplayAlert("KSADJF", "asd:> " + conteudo.ToString(), "okok");
        }
        /*private async void Lixo(object sender, EventArgs e)
        {
            try
            {
                if (Historico2.Text != "")
                {
                    await PCLHelper.WriteTextAllAsync(ficheiro, "");
                }
                
                LeInfoUsuario111(ficheiro);

            }
            catch (Exception ola)
            {

            }
           // await PCLHelper.WriteTextAllAsync(ficheiro, "");
        }*/
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
                        texto = texto + "-" + words[i];
                        conta++;
                    }
                }

            }
            Historico2.Text = "";
            await PCLHelper.WriteTextAllAsync(ficheiro, texto);
          //  DisplayAlert("OK", "tenho " + ver.ToString(), "OK");
            LeInfoUsuario111(ficheiro);
            AtualizaDados(a);

        }

        async void AtualizaDados(object userHist)
        {
            historico = await dataService.GetHistoricoByUserAsync(userHist.ToString());
            //List<Beacon> bec = new List<Beacon>();
            if (historico==null)
            {
                HistE.IsVisible = true;

            }
            else
            {
                //HistE.IsVisible = false;
            }
            List<Tuple<String, String, int, int>> behi = new List<Tuple<String, String, int, int>>();
            for (int i = 0; i < historico.Count; i++)
            {
                Content con = await dataService.GetContentByIdAsync(historico[i].contentID);
                Beacon be = await dataService.GetBeacon(con.beaconID);
                String[] substrings = be.name.Split('-');
                behi.Add(Tuple.Create(substrings[1], historico[i].date.ToString(), historico[i].contentID, historico[i].historicID));
                HistE.IsVisible = false;
            }
            behi.Reverse();
            
            listaUtilizador.ItemsSource = behi;
        }


        private async void OnDeletar(object sender, EventArgs e)
        {
            try
            {
                Device.BeginInvokeOnMainThread(async () => {
                    var result = await this.DisplayAlert("Delete!", "Are you sure you want to delete this registry?", "Yes", "No");
                    if (result)
                    {
                        var mi = ((MenuItem)sender);
                        Tuple<String, String, int, int> behis = (Tuple<String, String, int, int>)mi.CommandParameter;
                        //  HistoricoModel historicoUtilizador = (HistoricoModel)mi.CommandParameter;

                        int aux2 = behis.Item3; //historicoUtilizador.contentID;

                        Conteste = await dataService.GetContentByIdAsync(aux2);
                        string[] words = Historico2.Text.Split('-');
                        int conta = 0;
                        String texto = "";
                        for (int i = 0; i < words.Length; i++)
                        {
                            if (words[i].Equals(Conteste.contentmsg.ToString()))
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
                                    texto = texto + "-" + words[i];
                                    conta++;
                                }
                            }

                        }
                        Historico2.Text = "";
                        await PCLHelper.WriteTextAllAsync(ficheiro, texto);

                        await dataService.DeletaHitoricAsync(behis.Item4);
                        LeInfoUsuario111(ficheiro);
                    }
                });
            }

            catch (Exception ex)
            {
                await DisplayAlert("Erro", ex.Message, "OK");
            }
        }

        public async void GoFav(object sender, EventArgs e)
        {
            try
            {
                try
                {
                    MyProgressBar.IsVisible = true;
                    await Navigation.PushAsync(new Favoritos(nomeUtilizador));
                    MyProgressBar.IsVisible = false;
                }
                finally
                {
                    MyProgressBar.IsVisible = false;
                }
               // await Navigation.PushAsync(new Favoritos());
            }
            catch
            {

            }
        }
      /*  protected override bool OnBackButtonPressed() //funciona com o botao back do telemovel
        {
            // If you want to continue going back
            DisplayAlert("OK", "ver se funca", "ok");
            return true;

        }*/ 
 
    }
}