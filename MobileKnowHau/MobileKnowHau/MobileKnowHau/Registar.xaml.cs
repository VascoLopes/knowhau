using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MobileKnowHau.Models;
using MobileKnowHau.Service;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Globalization;

namespace MobileKnowHau
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Registar : ContentPage
    {
        DataService dataService;
        String teste; private bool excepcao= true; private bool excepcao2 = true;

        //List<Utilizador> utilizador;
        public Registar()
        {
            InitializeComponent();
            dataService = new DataService();
            picker.Items.Add("F");
            picker.Items.Add("M");
        }


        
        private bool Valida()
        {

           

            if (string.IsNullOrEmpty(txtName.Text) && string.IsNullOrEmpty(txtUsername.Text) && string.IsNullOrEmpty(txtGenre.ToString()) && string.IsNullOrEmpty(txtBirth.Text) && string.IsNullOrEmpty(txtEmail.Text) && string.IsNullOrEmpty(txtPassword.Text)&& txtPassword.Text== txtPasswordConfirm.Text)
            {
               
                return false;
            }
            else
            {
                String dataAux = Convert.ToDateTime(txtBirth.Text.Trim()).ToString("yyyy-MM-dd");
                txtBirth.Text = dataAux;
                //DisplayAlert("aFA", "safasf   " + Convert.ToDateTime(txtBirth.Text), "asfdas");
                bool validaP = ValidatePassword(txtPassword.Text);
                bool ValidaE = ValidarEmail(txtEmail.Text);
               // DisplayAlert("OKOKOK", "VALOR  " + ValidaE.ToString(), "OKOK");

                if (ValidaE != true)
                {
                    txtEmail.Text = "";
                   // excepcao2 = false;
                    DisplayAlert("Error", "Invalid email, this must be of the type example@servico.com", "OK");

                    return false;
                }
                if (validaP!=true )
                {

                    //DisplayAlert("Password invalida", "Password 6-15caracteres", "OK");
                    txtPassword.Text = "";
                    txtPasswordConfirm.Text = "";
                    DisplayAlert("Error", "Password invalid, this must contain a capital, lowercase and numbers with a size of 6 to 20", "OK");
                    //Navigation.PopAsync();//   PopAsync(new MainPage());
                    //excepcao = false;

                    return false;
                }
                else
                {
                    //DisplayAlert("Todos", " pass " + txtPassword.Text + " usern " + txtUsername.Text + " nome " + txtName.Text+" genre "+txtGenre.Text, "ok");
                    return true;
                }

                //return true;
                
            }
        }
        DateTime myDate;
        private void LimpaProduto()
        {
            txtName.Text = "";
            txtUsername.Text = "";
            txtGenre.Text = "";
            txtBirth.Text = "";
            txtEmail.Text = "";
            txtPassword.Text = "";
            txtPasswordConfirm.Text = "";
            picker.SelectedIndex = -1;
        }

        private void onDateSelect(object sender, DateChangedEventArgs e)
        {
            txtBirth.Text = e.NewDate.ToString();
        }
        private void olatestar1(object sender, EventArgs e)
        {
            var name = picker.Items[picker.SelectedIndex];
            txtUsername.Text = name.ToString();
            //DisplayAlert("ola", "temos o valor"+name.ToString(), "OK");
        }
   
        private async void btnAdicionar_Clicked(object sender, EventArgs e)
        {
            try
            {
                var name = picker.Items[picker.SelectedIndex];
                teste = name;
                txtGenre.Text = name;

                if (Valida())
                {
                    String auxilia = sha256_hash(txtPassword.Text);
                    Utilizador novoUtiliazador = new Utilizador
                    {
                        name = txtName.Text.Trim(),
                        username = txtUsername.Text.Trim(),
                        genre = txtGenre.Text.Trim(),
                        birthdate = Convert.ToDateTime(txtBirth.Text.Trim()),// Convert.ToDateTime(txtBirth.Text), //DateTime.ParseExact(txtBirth.Text.Trim(), "yy/MM/dd h:mm:ss tt", CultureInfo.InvariantCulture),
                        email = txtEmail.Text.Trim(),
                        password = auxilia.Trim()

                    };
                    //DateTime a = Convert.ToDateTime(txtBirth.Text);
                    // DisplayAlert("dados", "name-> " + txtName.Text.Trim() + " username-> " + txtName.Text.Trim() + " genre-> " + txtGenre.Text.Trim() + " birthdate-> "+ DateTime.ParseExact(txtBirth.Text.Trim(), "yy/MM/dd h:mm:ss tt", CultureInfo.InvariantCulture)+" email-> " + txtEmail.Text.Trim() + "password->" + txtPassword.Text.Trim(), "OKOK");
                    try
                    {
                        await dataService.AddUtilizadorAsync(novoUtiliazador);
                        LimpaProduto();
                        await DisplayAlert("Good", "Registed successfully.", "OK");
                        await Navigation.PopAsync();
                    }
                    catch (Exception ex)
                    {
                        await DisplayAlert("Error", ex.Message, "OK");
                    }
                }
                else
                {
                   // if (excepcao == true)
                        //await DisplayAlert("Erro", "Dados inválidos...", "OK");
                    if (excepcao != false)
                    {
                        //await DisplayAlert("Error", "Password invalid, this must contain a capital, lowercase and numbers with a size of 6 to 20", "OK");
                        excepcao = true;
                    }
                    if (excepcao2 == false)
                    {
                       // await DisplayAlert("Error", "Invalid email, this must be of the type example@servico.com", "OK");
                        excepcao2 = true;
                    }
                    else
                    {
                       // await DisplayAlert("Erro", "ver...", "OK");
                    }
                }
            }
            catch(Exception)
            {
                //if (excepcao == true)
                   // await DisplayAlert("Erro", "Dados inválidos.22222..", "OK");
                if (excepcao != false)
                {
                  //  await DisplayAlert("Error", "Password invalid, this must contain a capital, lowercase and numbers with a size of 6 to 20", "OK");
                    excepcao = true;
                }
                if (excepcao2 == false)
                {
                  //  await DisplayAlert("Error", "Invalid email, this must be of the type example@servico.com", "OK");
                    excepcao2 = true;
                }
                else
                {
                    await DisplayAlert("Error", "Enter data in all fields", "OK");
                    //LimpaProduto();
                }
                
            }
            
            // txtGenre.Text = name.ToString();
            //  picker.Items.Clear();
            
            //DisplayAlert("ola", "piker txt " + teste.ToString() + "  name " + name.ToString(), "ok");

            
        }
        public static string sha256_hash(string value)
        {
             string finalKey = string.Empty;
            byte[] encode = new byte[value.Length];
            encode = Encoding.UTF8.GetBytes(value);
            finalKey = Convert.ToBase64String(encode);
            return finalKey;

        }
        public static bool ValidarEmail(string email)
        {
            bool validEmail = false;
            int indexArr = email.IndexOf('@');
            if (indexArr > 0)
            {
                int indexDot = email.IndexOf('.', indexArr);
                if (indexDot - 1 > indexArr)
                {
                    if (indexDot + 1 < email.Length)
                    {
                        string indexDot2 = email.Substring(indexDot + 1, 1);
                        if (indexDot2 != ".")
                        {
                            validEmail = true;
                        }
                    }
                }
            }
            return validEmail;
        }


        private bool ValidatePassword(string password)
        {
            const int MIN_LENGTH = 6;
            const int MAX_LENGTH = 20;

            if (password == null) throw new ArgumentNullException();

            bool meetsLengthRequirements = password.Length >= MIN_LENGTH && password.Length <= MAX_LENGTH;
            bool hasUpperCaseLetter = false;
            bool hasLowerCaseLetter = false;
            bool hasDecimalDigit = false;

            if (meetsLengthRequirements)
            {
                foreach (char c in password)
                {
                    if (char.IsUpper(c)) hasUpperCaseLetter = true;
                    else if (char.IsLower(c)) hasLowerCaseLetter = true;
                    else if (char.IsDigit(c)) hasDecimalDigit = true;
                }
            }

            bool isValid = meetsLengthRequirements
                        && hasUpperCaseLetter
                        && hasLowerCaseLetter
                        && hasDecimalDigit
                        ;
            return isValid;

        }
    }
}