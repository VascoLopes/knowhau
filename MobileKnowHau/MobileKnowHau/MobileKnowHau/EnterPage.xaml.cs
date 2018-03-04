using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using MobileKnowHau.Models;
using MobileKnowHau.Service;


namespace MobileKnowHau
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EnterPage : ContentPage
    {
        DataService dataService;
        List<Utilizador> utilizador;

        public EnterPage()
        {
            InitializeComponent();
            dataService = new DataService();
            AtualizaDados();
        }
        async void AtualizaDados()
        {
            utilizador = await dataService.GetUtilizadorAsync();
            listaUtilizador.ItemsSource = utilizador.OrderBy(item => item.name).ToList();
        }
        private async void HistoryPage(Object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Historico(null));

        }

        private void lv_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {

        }
    }
}