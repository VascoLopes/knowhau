using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WebAppKnowHau.Models;

namespace WebAppKnowHau.Service
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

        public List<Beacon> GetBeaconAsync()
        {
            try
            {
                string url = "http://knowhau.somee.com/api/Beacon";
                var response =  client.GetStringAsync(url).Result;



                var beacon = JsonConvert.DeserializeObject<List<Beacon>>(response);
                return beacon;


            }
            catch (Exception e)
            {
                throw e;
            }

        }

        public async Task<List<Content>> GetContentAsync()
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


        public async Task<List<Admin>> GetAdminAsync()
        {
            try
            {
                string url = "http://knowhau.somee.com/api/Admin";
                var response = await client.GetStringAsync(url);



                var admin = JsonConvert.DeserializeObject<List<Admin>>(response);
                return admin;


            }
            catch (Exception e)
            {
                throw e;
            }

        }

        public  List<BA> GetBAAsync()
        {
            try
            {
                string url = "http://knowhau.somee.com/api/BA";
                var response =  client.GetStringAsync(url).Result;



                var ba = JsonConvert.DeserializeObject<List<BA>>(response);
                return ba;


            }
            catch (Exception e)
            {
                throw e;
            }

        }


        public async Task<List<LogMobileApp>> GetLogMobileAppAsync()
        {
            try
            {
                string url = "http://knowhau.somee.com/api/LogMobileApp";
                var response = await client.GetStringAsync(url);

                var admin = JsonConvert.DeserializeObject<List<LogMobileApp>>(response);
                return admin;


            }
            catch (Exception e)
            {
                throw e;
            }

        }

        public async Task<List<LogWebbApp>> GetLogWebbAppAsync()
        {
            try
            {
                string url = "http://knowhau.somee.com/api/LogWebbApp";
                var response = await client.GetStringAsync(url);

                var admin = JsonConvert.DeserializeObject<List<LogWebbApp>>(response);
                return admin;


            }
            catch (Exception e)
            {
                throw e;
            }

        }


        public async Task<Beacon> GetBeaconByIDAsync(String id)
        {
            try
            {
                string url = "http://knowhau.somee.com/api/Beacon/{0}";
                var uri = new Uri(string.Format(url, id));
                var response = await client.GetStringAsync(uri);
                var beacon = JsonConvert.DeserializeObject<Beacon>(response);
                return beacon;

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
                string url = "http://knowhau.somee.com/api/Admin?name={0}&passwd={1}";
                var uri = new Uri(string.Format(url, user, pass));
                var response = await client.GetStringAsync(uri);
                var res = Convert.ToBoolean(response);
                if (res == false)
                {
                    string url2 = "http://knowhau.somee.com/api/SuperAdmin?name={0}&passwd={1}";
                    var uri2 = new Uri(string.Format(url2, user, pass));
                    var response2 = await client.GetStringAsync(uri2);
                    var res2 = Convert.ToBoolean(response2);
                    return res2;
                }
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


        public async Task<Admin> GetAdminByIdAsync(String id)
        {
            try
            {
                string url = "http://knowhau.somee.com/api/Admin?name={0}";
                var uri = new Uri(string.Format(url, id));
                var response = await client.GetStringAsync(uri);
                var content = JsonConvert.DeserializeObject<Admin>(response);
                return content;

            }
            catch (Exception e)
            {
                throw e;
            }

        }

        public Admin GetAdminEmailByIdAsync(String id)
        {
            try
            {
                string url = "http://knowhau.somee.com/api/Admin?email={0}";
                var uri = new Uri(string.Format(url, id));
                var response = client.GetStringAsync(uri).Result;
                var content = JsonConvert.DeserializeObject<Admin>(response);
                return content;

            }
            catch (Exception e)
            {
                throw e;
            }

        }

        public async Task<SuperAdmin> GetSuperAdminByIdAsync(String id)
        {
            try
            {
                string url = "http://knowhau.somee.com/api/SuperAdmin?username={0}";
                var uri = new Uri(string.Format(url, id));
                var response = await client.GetStringAsync(uri);
                var content = JsonConvert.DeserializeObject<SuperAdmin>(response);
                return content;

            }
            catch (Exception e)
            {
                throw e;
            }

        }

        public async Task<SuperAdmin> GetSuperAdminEmailByIdAsync(String id)
        {
            try
            {
                string url = "http://knowhau.somee.com/api/SuperAdmin?email={0}";
                var uri = new Uri(string.Format(url, id));
                var response = await client.GetStringAsync(uri);
                var content = JsonConvert.DeserializeObject<SuperAdmin>(response);
                return content;

            }
            catch (Exception e)
            {
                throw e;
            }

        }


        public async Task<bool> AddUtilizadorAsync(Utilizador utilizador)
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
                    return false;
                    throw new Exception("Erro ao incluir utilizador");
                    
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
                throw ex;
            }
        }


        public async Task<bool> AddAdminAsync(Admin admin)
        {
            try
            {
                string url = "http://knowhau.somee.com/api/Admin";
                var uri = new Uri(string.Format(url, admin.email));
                var data = JsonConvert.SerializeObject(admin);
                var content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = null;
                response = await client.PostAsync(uri, content);

                if (!response.IsSuccessStatusCode)
                {
                    return false;
                    throw new Exception("Erro ao incluir utilizador");

                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
                throw ex;
            }
        }


        public bool AddBaAsync(BA ba)
        {
            try
            {
                string url = "http://knowhau.somee.com/api/BA";
                var uri = new Uri(string.Format(url, ba.baID));
                var data = JsonConvert.SerializeObject(ba);
                var content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = null;
                response =  client.PostAsync(uri, content).Result;

                if (!response.IsSuccessStatusCode)
                {
                    return false;
                    throw new Exception("Erro ao incluir utilizador");

                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
                throw ex;
            }
        }


        public async Task<bool> AddBeaconAsync(Beacon beacon, String admin, String mensagem, int super)
        {
            try
            {
                string url = "http://knowhau.somee.com/api/Beacon?admin={0}&mensagem={1}&super={2}";
                var uri = new Uri(string.Format(url, admin,mensagem,super));
                var data = JsonConvert.SerializeObject(beacon);
                var content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = null;
                response = await client.PostAsync(uri, content);

                if (!response.IsSuccessStatusCode)
                {
                    return false;
                    throw new Exception("Erro ao incluir Beacon");

                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
                throw ex;
            }
        }



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
                throw new Exception("Erro ao atualizar produto");
            }
        }

        public async Task<bool> UpdateBeacoon(Beacon beacon, string admin)
        {
            string url = "http://knowhau.somee.com/api/Beacon?beaconID={0}&admin={1}";
            var uri = new Uri(string.Format(url, beacon.beaconID, admin));
            var data = JsonConvert.SerializeObject(beacon);
            var content = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage response = null;
            response = await client.PutAsync(uri, content);
            
            if (!response.IsSuccessStatusCode)
            {
                return false;
                throw new Exception("Erro ao atualizar beacon");
                
            }
            return true;
        }

        public async Task<bool> UpdateContent(Content cont)
        {
            string url = "http://knowhau.somee.com/api/Content?contentID={0}";
            var uri = new Uri(string.Format(url, cont.contentID));
            var data = JsonConvert.SerializeObject(cont);
            var content = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage response = null;
            response = await client.PutAsync(uri, content);

            if (!response.IsSuccessStatusCode)
            {
                return false;
                throw new Exception("Erro ao atualizar beacon");

            }
            return true;
        }



        public async Task<bool> UpdateAdmin(Admin admin)
        {
            string url = "http://knowhau.somee.com/api/Admin?email={0}";
            var uri = new Uri(string.Format(url, admin.email));
            var data = JsonConvert.SerializeObject(admin);
            var content = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage response = null;
            response = await client.PutAsync(uri, content);

            if (!response.IsSuccessStatusCode)
            {
                return false;
                throw new Exception("Erro ao atualizar admin");
            }
            return true;
        }

        public async Task<bool> UpdateSuperAdmin(SuperAdmin superadmin)
        {
            string url = "http://knowhau.somee.com/api/SuperAdmin?email={0}";
            var uri = new Uri(string.Format(url, superadmin.email));
            var data = JsonConvert.SerializeObject(superadmin);
            var content = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage response = null;
            response = await client.PutAsync(uri, content);

            if (!response.IsSuccessStatusCode)
            {
                return false;
                throw new Exception("Erro ao atualizar superadmin");
            }
            return true;
        }

        public async Task DeletaUtilizadorAsync(Utilizador utilizador)
        {
            string url = "http://knowhau.somee.com/api/User?email={0}";
            var uri = new Uri(string.Format(url, utilizador.email));
            await client.DeleteAsync(uri);
        }

        public async Task DeletaHitoricAsync(HistoricoModel historico)
        {
            string url = "http://knowhau.somee.com/api/Historic?historicID={0}";
            var uri = new Uri(string.Format(url, historico.historicID));
            await client.DeleteAsync(uri);
        }

        public async Task DeletaBeaconAsync(String beaconID, String admin)
        {
            string url = "http://knowhau.somee.com/api/Beacon?beaconID={0}&admin={1}";
            var uri = new Uri(string.Format(url, beaconID, admin));
            HttpResponseMessage response = null;
            response = await client.DeleteAsync(uri);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Erro ao eliminar beacon");
            }
        }

        public async Task DeletaAdminAsync(String email)
        {
            string url = "http://knowhau.somee.com/api/Admin?email={0}";
            var uri = new Uri(string.Format(url, email));
            HttpResponseMessage response = null;
            response = await client.DeleteAsync(uri);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Erro ao eliminar admin");
            }
        }

        public  void DeletaBAAsync(int id)
        {
            string url = "http://knowhau.somee.com/api/BA?baID={0}";
            var uri = new Uri(string.Format(url, id));
            HttpResponseMessage response = null;
            response = client.DeleteAsync(uri).Result;
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Erro ao eliminar BA");
            }
        }
    }

}