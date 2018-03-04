using MobileKnowHau.Models;
using MobileKnowHau.Service;
using Plugin.BLE;
using Plugin.BLE.Abstractions.Contracts;
using System;

using System.Diagnostics;
using Xamarin.Forms;


using PCLStorage;
using System.Net;
using Windows.Networking.Connectivity;
using Plugin.Connectivity;
using Windows.Devices.Bluetooth;
using Xamarin.Forms.PlatformConfiguration;
using LocalNotifications.Plugin;
using LocalNotifications.Plugin.Abstractions;
using Plugin.Toasts;
using System.Threading.Tasks;
using System.Text;
using Windows.UI;
using Windows.UI.ViewManagement;
using Windows.UI.Notifications;
using Windows.Data.Xml.Dom;
using System.Collections.Generic;
using Windows.Networking.PushNotifications;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml;

namespace MobileKnowHau
{
    
    public partial class MainPage : ContentPage
    {
        
        DataService dataService;
        //List<Utilizador> utilizador;
        public object listaUtilizador;
        

        public MainPage()
        {
            Guardar();
            InitializeComponent();
            
            dataService = new DataService();

           

            //Creating TapGestureRecognizers  
            var tapImage = new TapGestureRecognizer();
            //Binding events  
            tapImage.Tapped += tapImage_Tapped;
            //Associating tap events to the image buttons  
            // img.GestureRecognizers.Add(tapImage) 

            // var image = ImageSource.FromResource("KnowSFundo.png");
            // AtualizaDados();
            
        }
        private void OnToggled(object sender, ToggledEventArgs e)
        {
            bool isToggled = e.Value;
           // DisplayAlert("ok", "asd " + isToggled.ToString(), "ok");
        }
        /*  private async void lv_ItemSelected(object sender, SelectedItemChangedEventArgs e)
          {
              if (lv.SelectedItem == null)
              {
                  return;
              }
              device = lv.SelectedItem as IDevice;
          }*/


        /* private async void btnConnect_Clicked(Object sender, EventArgs lo)
         {
             try
             {
                 await adapter.ConnectToDeviceAsync(device);
             }
             catch(Exception e3)
             {
                 DisplayAlert("OK", "ERRO -> " + e3.ToString(), "OK");
             }
         }*/


        private bool verificaInter()
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                // your logic...  
                return true;
            }
            else
            {
                return false;
                // write your code if there is no Internet available  
            }
        }

        private bool VerEstado()
         {
            var state = ble.State;
         
             //IDevice device;
             if (ble.State== BluetoothState.Off)
             {
              
               // adapter = BluetoothState.On();
                 //state = BluetoothState.On;
                 //state = ble.State;
                this.DisplayAlert("Bluetooth","Bluetooth is: "+ state.ToString(), "OK");
                return false;

             }
             else //if state on
             {

                 state = BluetoothState.On;
                 state = ble.State;
                 //this.DisplayAlert("Warning else", state.ToString(), "OK");
                return true;
             }
           // await adapter.StartScanningForDevicesAsync();

        }

        void tapImage_Tapped(object sender, EventArgs e)
        {
            // handle the tap  
            //DisplayAlert("Alert", "This is an image button", "OK");
            DisplayAlert("Help", "Register new user \n- This option allows the registration of a user in form that he can enter the aplication.Some personal data are required in the registration \nLogin - Entry form on the application.It's necessary to enter registered credentials \nHistory- Here you can see your content history, and you can select a certain content as favorite or visualize it's informations or delete it from the history \nFavorites- Check the information of the contents selected as favorites","OK");
        }
        private async void botaoTeste(object sender, EventArgs e)
        {
            //await Navigation.PushAsync(new Historico(null));
            
        }

        private async void RegistarUser(object sender, EventArgs e)
        {

            bool a = verificaInter();
            if (a == true)
            {
                try
                {
                  
                    MyProgressBar.IsVisible = true;
                    Regis.IsEnabled = false;
                    Log.IsEnabled = false;
                    txtPassword.IsEnabled = false;
                    txtUsername.IsEnabled = false;
                    await MyProgressBar.ProgressTo(0.9, 500, Easing.SpringIn);

                    await Navigation.PushAsync(new Registar());
                    Regis.IsEnabled = true;
                    Log.IsEnabled = true;
                    txtPassword.IsEnabled = true;
                    txtUsername.IsEnabled = true;
                    MyProgressBar.IsVisible = false;
                    //await Navigation.PushAsync(new Registar());
                }
                finally
                {
                    Regis.IsEnabled = true;
                    Log.IsEnabled = true;
                    txtPassword.IsEnabled = true;
                    txtUsername.IsEnabled = true;
                    MyProgressBar.IsVisible = false;
                    MyProgressBar.Progress = 0;
                    //MainProgressBar.IsVisible = false;
                    //Stackla.IsVisible = true;

                }

                //await Navigation.PushAsync(new Registar());

            }
            else
            {
                DisplayAlert("Internet", "Turn On Internet", "OK");
            }

        }

        /* private async void Gosto(object sender, EventArgs e)
         {
             var notification = new LocalNotification
             {
                 Text = "Hello from Plugin",
                 Title = "Notification Plugin",
                 Id = 2,

                 NotifyTime = DateTime.Now.AddSeconds(10)
             };
             var notifier = CrossLocalNotifications.CreateLocalNotifier();
             notifier.Notify(notification);
             //A213FD51   historico = await dataService.GetHistoricoByUserAsync(userHist.ToString()); 
             Content a = await dataService.GetContentByIdAsync(36);
             HistoricoModel h = new HistoricoModel();
             h.contentID= a.contentID;
             h.date =DateTime.Now;
             h.userMAIL =;

             await dataService.addHistorico(h);




             //notifier.Cancel(notification.Id);

             DisplayAlert("OK", "OK"+a.beaconID.ToString(), "OK");
         }
         */
        public static string sha256_hash(string value)
        {
            string finalKey = string.Empty;
            byte[] encode = new byte[value.Length];
            encode = Encoding.UTF8.GetBytes(value);
            finalKey = Convert.ToBase64String(encode);
            return finalKey;

        }
        IBluetoothLE ble;
        IAdapter adapter;
        bool flag = false;
        private async void Guardar()
        {
            bool existe = await PCLHelper.ArquivoExisteAsync("User.txt");
            if (existe != true)
            {
                flag = true;
                //await PCLHelper.CriarArquivo("User.txt");
                //await PCLHelper.WriteTextAllAsync(fichPermi, aux);
            }
            else
            {
                String utilizadorRegistado=await PCLHelper.ReadAllTextAsync("User.txt");
                await Navigation.PushAsync(new Historico(utilizadorRegistado));
            }
            
            //DisplayAlert("ok", "mandie " + isToggled.ToString(), "ok");
        }
        private async void Login(object sender, EventArgs e)
        {
            bool a = verificaInter();
            bool testar = true;
            if (txtPassword.Text==null || txtUsername.Text==null)
            {
                if (txtPassword.Text == null && txtUsername.Text == null)
                {
                    await DisplayAlert("Error", "Please enter the username and password", "Ok");
                }
                else if (txtUsername.Text == null)
                {
                    await DisplayAlert("Error", "Please enter the username or email", "Ok");
                }
                else if (txtPassword.Text == null)
                {
                    await DisplayAlert("Error", "Please enter the password", "Ok");

                }
                a = false;
                testar = false;
            }
            
            
            if (a == true)
            {
                String auxilia = sha256_hash(txtPassword.Text);
                var res = await dataService.GetLogin(txtUsername.Text, auxilia);
                

                // DisplayAlert("Error3", "entrou  " + txtUsername.Text, "Ok");
                try
                {
                    switch (Device.RuntimePlatform)
                    {
                        case Device.iOS:
                            //padding = 10;
                            break;
                        case Device.Android:
                            //padding = 0;
                           
                            ble = CrossBluetoothLE.Current;
                            adapter = CrossBluetoothLE.Current.Adapter;

                            VerEstado();
                            break;
                        case Device.UWP:
                            //padding = 0;
                            //DisplayAlert("OIOI","OLA N","ASD");
                            break;
                        default:
                            //DisplayAlert("DEF", "FED", "ASD");
                            break; 
                    }
                    if (res == true)
                    {
                        String a1 = txtUsername.Text;
                        if (a1.Contains("@") == true)
                        {
                            //DisplayAlert("Error", "entrou  "+txtUsername.ToString(), "Ok");
                            try
                            {
                                MyProgressBar.IsVisible = true;
                                Regis.IsEnabled = false;
                                Log.IsEnabled = false;
                                txtUsername.IsEnabled = false;
                                txtPassword.IsEnabled = false;
                                await MyProgressBar.ProgressTo(0.9, 500, Easing.SpringIn);
                                if (flag == true)
                                {
                                    await PCLHelper.CriarArquivo("User.txt");

                                }
                                await PCLHelper.WriteTextAllAsync("User.txt", a1);
                                await Navigation.PushAsync(new Historico(a1));
                                
 
                                txtPassword.Text = "";

                            }
                            finally
                            {
                                Regis.IsEnabled = true;
                                Log.IsEnabled = true;
                                txtUsername.IsEnabled = true;
                                txtPassword.IsEnabled = true;
                                MyProgressBar.IsVisible = false;
                                MyProgressBar.Progress = 0;
                            }
                            

                        }
                        else
                        {
                            Utilizador auxiliar = await dataService.GetUtilizadorByusernameAsync(a1);

                            try
                            {
                                MyProgressBar.IsVisible = true;
                                Regis.IsEnabled = false;
                                Log.IsEnabled = false;
                                await MyProgressBar.ProgressTo(0.9, 500, Easing.SpringIn);
                                if (flag == true)
                                {
                                    await PCLHelper.CriarArquivo("User.txt");

                                }
                                await PCLHelper.WriteTextAllAsync("User.txt", auxiliar.email);
                                await Navigation.PushAsync(new Historico(auxiliar.email));
                                txtPassword.Text = "";
                            }
                            finally
                            {
                                Regis.IsEnabled = true;
                                Log.IsEnabled = true;
                                MyProgressBar.IsVisible = false;
                                MyProgressBar.Progress = 0;
                            }
                            
                        }

                    }
                    else
                    {
                        await DisplayAlert("Error", "User or Password Wrong", "Ok");
                    }
                }
                catch (Exception ex)
                {
                    //DisplayAlert("Error2", "User or Password Wrong", "Ok");
                    //Debug.WriteLine("Answer: " + ex);
                    // DisplayAlert("Alert", "User or password wrong", "Ok");
                }
            }
            else
            {
                if (testar == true)
                {
                    DisplayAlert("Internet", "Connect your device to the internet", "OK");
                }

                

            }


        }
        
    }



}
