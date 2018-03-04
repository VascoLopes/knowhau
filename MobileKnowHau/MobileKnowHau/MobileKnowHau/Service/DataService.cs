using MobileKnowHau.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;


namespace MobileKnowHau.Service
{

    public class DataService
    {

        HttpClient client = new HttpClient();

        public async Task<Utilizador> GetUtilizadorByusernameAsync(String user)
        {
            try
            {
                string url = "http://knowhau.somee.com/api/User?name={0}";
              var uri = new Uri(string.Format(url, user));
                 var response = await client.GetStringAsync(uri);

                var utilizador = JsonConvert.DeserializeObject<Utilizador>(response);
                return utilizador;

            }
            catch (Exception e)
            {
                throw e;
            }

        }
        public async Task<Beacon> GetBeacon(String id)
        {
            try
            {
                string url = "http://knowhau.somee.com/api/Beacon/{0}";
                var uri = new Uri(string.Format(url, id));
                var response = await client.GetStringAsync(uri);
                var content = JsonConvert.DeserializeObject<Beacon>(response);
                return content;

            }
            catch (Exception e)
            {
                throw e;
            }

        }

        public async Task<List<Utilizador>> GetUtilizadorAsync()
        {
            try
            {
                string url = "http://knowhau.somee.com/api/User";
                var response = await client.GetStringAsync(url);



                var utilizador = JsonConvert.DeserializeObject<List<Utilizador>>(response);
                return utilizador;


            }
            catch (Exception e)
            {
                throw e;
            }

        }
        public async Task<List<Content>> GetListContent()
        {
            try
            {
                string url = "http://knowhau.somee.com/api/Content";
                var response = await client.GetStringAsync(url);



                var content = JsonConvert.DeserializeObject<List<Content>>(response);
                return content;


            }
            catch (Exception e)
            {
                throw e;
            }

        }

        public async Task<List<HistoricoModel>> GetHistoricoByUserAsync(String user)
        {
            try
            {
                string url = "http://knowhau.somee.com/api/Historic?user={0}";
                var uri = new Uri(string.Format(url, user));
                var response = await client.GetStringAsync(uri);
                var historico = JsonConvert.DeserializeObject<List<HistoricoModel>>(response);
                return historico;

            }
            catch (Exception e)
            {
                throw e;
            }

        }

        public async Task<bool> GetLogin(String user, String pass)
        {
            try
            {
                string url = "http://knowhau.somee.com/api/User?name={0}&passwd={1}";
                var uri = new Uri(string.Format(url, user, pass));
                var response = await client.GetStringAsync(uri);
                var res = Convert.ToBoolean(response);
                return res;

            }
            catch (Exception e)
            {
                throw e;
            }

        }

        public async Task<Content> GetContentByIdAsync(int id)
        {
            try
            {
                string url = "http://knowhau.somee.com/api/Content/{0}";
                var uri = new Uri(string.Format(url, id));
                var response = await client.GetStringAsync(uri);
                var content = JsonConvert.DeserializeObject<Content>(response);
                return content;

            }
            catch (Exception e)
            {
                throw e;
            }

        }

  
        public async Task AddUtilizadorAsync(Utilizador utilizador)
        {
            try
            {
                string url = "http://knowhau.somee.com/api/User";
                var uri = new Uri(string.Format(url, utilizador.email));

                var data = JsonConvert.SerializeObject(utilizador);
                var content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = null;
                response = await client.PostAsync(uri, content);

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception("error adding user");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //Teste
        public async Task addHistorico(HistoricoModel a)
        {
            try
            {
                string url = "http://knowhau.somee.com/api/Historic";
                var uri = new Uri(string.Format(url, a.contentID));

                var data = JsonConvert.SerializeObject(a);
                var content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = null;
                response = await client.PostAsync(uri, content);

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception("Error while adding History");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //acaba o teste 
        public async Task UpdateUtilizadorAsync(Utilizador utilizador)
        {
            string url = "http://knowhau.somee.com/api/User?email={0}";
            var uri = new Uri(string.Format(url, utilizador.email));
            var data = JsonConvert.SerializeObject(utilizador);
            var content = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage response = null;
            response = await client.PutAsync(uri, content);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Error updating product");
            }
        }

        public async Task DeletaUtilizadorAsync(Utilizador utilizador)
        {
            string url = "http://knowhau.somee.com/api/User?email={0}";
            var uri = new Uri(string.Format(url, utilizador.email));
            await client.DeleteAsync(uri);
        }
        
        public async Task DeletaHitoricAsync(int historico)
        {
            string url = "http://knowhau.somee.com/api/Historic?historicID={0}";
            var uri = new Uri(string.Format(url, historico));
            await client.DeleteAsync(uri);
        }

        public async Task<bool> UpdateBeaconSemLog(Beacon beacon)
        {
            string url = "http://knowhau.somee.com/api/Beacon?beaconID={0}";
            var uri = new Uri(string.Format(url, beacon.beaconID));
            var data = JsonConvert.SerializeObject(beacon);
            var content = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage response = null;
            response = await client.PutAsync(uri, content);

            if (!response.IsSuccessStatusCode)
            {
                return false;
                throw new Exception("Error updating beacon");
            }
            return true;
        }
    }
}