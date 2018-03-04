using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebAppKnowHau.Models;
using WebAppKnowHau.Service;
using PagedList;
using System.Text;

namespace WebAppKnowHau.Controllers
{

    [Authorize]
    [HandleError]
    public class HomeController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        [Authorize]
        public ActionResult Index()
        {

            return View();
        }

        public ActionResult Help()
        {

            return View();
        }
        public static string sha256_hash(string value)
        {
            string finalKey = string.Empty;
            byte[] encode = new byte[value.Length];
            encode = Encoding.UTF8.GetBytes(value);
            finalKey = Convert.ToBase64String(encode);
            return finalKey;
        }


        public void Success(string message, bool dismissable = false)
        {
            AddAlert(AlertStyles.Success, message, dismissable);
        }

        public void Information(string message, bool dismissable = false)
        {
            AddAlert(AlertStyles.Information, message, dismissable);
        }

        public void Warning(string message, bool dismissable = false)
        {
            AddAlert(AlertStyles.Warning, message, dismissable);
        }

        public void Danger(string message, bool dismissable = false)
        {
            AddAlert(AlertStyles.Danger, message, dismissable);
        }

        private void AddAlert(string alertStyle, string message, bool dismissable)
        {
            var alerts = TempData.ContainsKey(Alert.TempDataKey)
                ? (List<Alert>)TempData[Alert.TempDataKey]
                : new List<Alert>();

            alerts.Add(new Alert
            {
                AlertStyle = alertStyle,
                Message = message,
                Dismissable = dismissable
            });

            TempData[Alert.TempDataKey] = alerts;
        }


        [Authorize]
        [HttpGet]
        public async Task<ActionResult> EditBeacon(String id)
        {

            EditBeaconViewModel be = new EditBeaconViewModel();
            DataService dataService = new DataService();
            List<Content> con = new List<Content>();
            con = await dataService.GetContentAsync();
            Content cont = new Content();

            Beacon beacon = new Beacon();
            beacon = await dataService.GetBeaconByIDAsync(id);
            cont = con.FirstOrDefault(x => x.beaconID == beacon.beaconID);
            be.MajorValue = beacon.majorvalue;
            be.Mensagem = cont.contentmsg;
            be.MinorValue = beacon.minorvalue;
            be.Name = beacon.model;
            String[] state = beacon.name.Split('-');
            be.Nome = state[1];
            
            be.BeaconID = beacon.beaconID;
            if (state[0] == "active")
                be.ActiveBeacon = true;
            else
                be.ActiveBeacon = false;

            be.Hide = state[2];

            List<Admin> admin = new List<Admin>();
            admin = await dataService.GetAdminAsync();
            List<ListaAdminBEdit> ad = new List<ListaAdminBEdit>();
            ListaAdminBEdit adm;
            for (int i = 0; i < admin.Count; i++)
            {

                adm = new ListaAdminBEdit();
                adm.Email = admin[i].email;
                adm.NomeAdmin = admin[i].name;
                adm.Username = admin[i].username;

                List<BA> ba = new List<BA>();
                ba = dataService.GetBAAsync();
                for (int j = 0; j < ba.Count; j++)
                {
                    if (ba[j].adminemail == admin[i].email && ba[j].beaconID == beacon.beaconID)
                    {
                        adm.addAdmin = true;
                         break;
                    }
     
                    else
                        adm.addAdmin = false;
                }

                ad.Add(adm);
            }
            
            
            CityListEdit objBind = new CityListEdit();
            objBind.listaa= ad ;
            objBind.EditBeaconViewModel = be;

            return View(objBind);
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> EditBeacon(CityListEdit model)
        {
            Uri aaaa = Request.UrlReferrer;
            DataService dataService = new DataService();
            if (!ModelState.IsValid)
            {
                return Redirect(aaaa.ToString());
            }


            string uri = aaaa.ToString();
            string param = string.Join(string.Empty, uri.Split('/').Skip(5));
            List<Content> con = new List<Content>();
            con = await dataService.GetContentAsync();
            Content cont = new Content();

            Beacon alterabeacon = new Beacon();
            cont = con.FirstOrDefault(x => x.beaconID == model.EditBeaconViewModel.BeaconID);
            alterabeacon.beaconID = param;
            alterabeacon.majorvalue = model.EditBeaconViewModel.MajorValue;
            alterabeacon.minorvalue = model.EditBeaconViewModel.MinorValue;
            alterabeacon.model = model.EditBeaconViewModel.Name;
            
            if (model.EditBeaconViewModel.ActiveBeacon == true)
                alterabeacon.name = "active-" + model.EditBeaconViewModel.Nome+"-"+ model.EditBeaconViewModel.Hide;
            else
                alterabeacon.name = "inactive-" + model.EditBeaconViewModel.Nome + "-" + model.EditBeaconViewModel.Hide;
            cont.contentmsg = model.EditBeaconViewModel.Mensagem;
            string username = User.Identity.GetUserName();
            Admin admin = new Admin();
            admin = await dataService.GetAdminByIdAsync(username);
            bool a = false;
            if (admin == null)
            {
                
                SuperAdmin superadmin = new SuperAdmin();
                superadmin = await dataService.GetSuperAdminByIdAsync(username);
                a = await dataService.UpdateBeacoon(alterabeacon, superadmin.email);
            }
            else
            {
                a = await dataService.UpdateBeacoon(alterabeacon, admin.email);
            }
            
            bool b = await dataService.UpdateContent(cont);


            List<BA> baaa = new List<BA>();
            baaa =  dataService.GetBAAsync();
            int max = 0;
            int i = 0;
            for (i = 0; i < baaa.Count(); i++)
            {
                if (max < baaa[i].baID)
                {
                    max = baaa[i].baID;
                }

            }
            for (i = 0; i < model.listaa.Count(); i++)
            {
                if (model.listaa[i].addAdmin == true && baaa.FirstOrDefault(x => x.beaconID == model.EditBeaconViewModel.BeaconID && x.adminemail == model.listaa[i].Email) ==null)
                {
                    max = max + 1;
                    BA ba = new BA();
                    ba.baID = max;
                    ba.adminemail = model.listaa[i].Email;
                    ba.beaconID = model.EditBeaconViewModel.BeaconID;
                    bool verdade = dataService.AddBaAsync(ba);
                }
                else if(baaa.FirstOrDefault(x => model.listaa[i].addAdmin == true && x.beaconID == model.EditBeaconViewModel.BeaconID && x.adminemail == model.listaa[i].Email) != null)
                {
                    continue;
                }
                else
                {
                    for (int j= 0; j < baaa.Count(); j++)
                    {
                        if (baaa[j].adminemail == model.listaa[i].Email && baaa[j].beaconID == model.EditBeaconViewModel.BeaconID)
                        {
                            dataService.DeletaBAAsync(baaa[j].baID);
                            break;
                        }
                    }
                }

            }


            if (a == true && b == true)
            {
                Success(string.Format("<b>{0}</b> was successfully edited", alterabeacon.beaconID), true);
                return RedirectToAction("Index", "Home");
            }
            ModelState.AddModelError("", "A problem occurred while saving beacon. Please try again later.");

            Danger(string.Format("<b>{0}</b> could not be edited", alterabeacon.beaconID), true);
            return View(aaaa);

        }



        [Authorize]
        [HttpGet]
        public async Task<ActionResult> CriaBeaconAsync()
        {
            DataService dataService = new DataService();
            List<Admin> admin = new List<Admin>();
            admin = await dataService.GetAdminAsync();
            List<ListaAdminB> ad = new List<ListaAdminB>();
            ListaAdminB adm;
            for (int i = 0; i < admin.Count; i++)
            {

                adm = new ListaAdminB();
                adm.Email = admin[i].email;
                adm.NomeAdmin = admin[i].name;
                adm.Username = admin[i].username;

                ad.Add(adm);
            }
            CityList objBind = new CityList();
            objBind.list = ad;
            // IEnumerable<ListaAdminB> myEnumerable = (IEnumerable<ListaAdminB>)ad;

            return View(objBind);

        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> CriaBeaconAsync(CityList model)
        {

            string men = model.CreateBeacon.Mesage;
            string username = User.Identity.GetUserName();
            DataService dataService = new DataService();
            Admin ad = new Admin();
            ad = await dataService.GetAdminByIdAsync(username);
            string admin = "";
            int flag = 0;
            if (ad == null)
            {

                SuperAdmin superadmin = new SuperAdmin();
                superadmin = await dataService.GetSuperAdminByIdAsync(username);
                admin = superadmin.email;
                flag = 1;
            }
            else
            {
                admin = ad.email;
                flag = 0;
            }
            
            Beacon beacon = new Beacon();
            beacon.beaconID = model.CreateBeacon.BeaconId;
            beacon.majorvalue = model.CreateBeacon.MajorValue;
            beacon.minorvalue = model.CreateBeacon.MinorValue;
            beacon.model = model.CreateBeacon.Model;
            if (model.CreateBeacon.Nome.Contains("-") || model.CreateBeacon.Mesage.Contains("-")) {
                ModelState.AddModelError("", "Beacon should not contain - in the name and in the message.");
                 return View(model);
            }
            if (model.CreateBeacon.ActiveBeacon == true)
                beacon.name = "active-" + model.CreateBeacon.Nome+"-0";
            else
                beacon.name = "inactive-" + model.CreateBeacon.Nome+"-0";
            List<ListaAdminB> lista = new List<ListaAdminB>();
            List<BA> baaa = new List<BA>();
            baaa =  dataService.GetBAAsync();
            int max = 0;
            int i = 0;
            for (i = 0; i < baaa.Count(); i++)
            {
                if (max < baaa[i].baID)
                {
                    max = baaa[i].baID;
                }

            }
            //lista = (List<ListaAdminB>)model2;
            bool a = await dataService.AddBeaconAsync(beacon, admin, men,flag);
            for (i = 0; i < model.list.Count(); i++)
            {
                if (model.list[i].addAdmin == true)
                {
                    max = max + 1;
                    BA ba = new BA();
                    ba.baID = max;
                    ba.adminemail = model.list[i].Email;
                    ba.beaconID = model.CreateBeacon.BeaconId;
                    bool verdade = dataService.AddBaAsync(ba);
                }

            }


            if (a == true)
            {
                Success(string.Format("<b>{0}</b> created with success!", beacon.beaconID), true);
                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("", "Beacon already registered.");


            return View(model);
        }

        [Authorize]
        public async Task<ActionResult> Details(String id)
        {
            DataService dataService = new DataService();
            Beacon beacon = new Beacon();
            beacon = await dataService.GetBeaconByIDAsync(id);


            BeaconsDetails bea;

            List<Content> con = new List<Content>();
            con = await dataService.GetContentAsync();
            Content cont = new Content();
            cont = con.FirstOrDefault(x => x.beaconID == beacon.beaconID);
            bea = new BeaconsDetails();
            bea.BeaconID = beacon.beaconID;
            bea.MajorValue = beacon.majorvalue;
            bea.MinorValue = beacon.minorvalue;
            bea.Name = beacon.model;
            String[] state = beacon.name.Split('-');
            bea.Nome = state[1];
            bea.Message = cont.contentmsg;
            bea.Cont= state[2];
            BeaconsDetails be = new BeaconsDetails();
          
          
            List<Admin> admin = new List<Admin>();
            admin = await dataService.GetAdminAsync();
            List<ListaAdminD> ad = new List<ListaAdminD>();
            ListaAdminD adm;
            for (int i = 0; i < admin.Count; i++)
            {

                adm = new ListaAdminD();
                adm.Email = admin[i].email;
                adm.NomeAdmin = admin[i].name;
                adm.Username = admin[i].username;

                List<BA> ba = new List<BA>();
                ba = dataService.GetBAAsync();
                for (int j = 0; j < ba.Count; j++)
                {
                    if (ba[j].adminemail == admin[i].email && ba[j].beaconID == beacon.beaconID)
                    {
                        adm.addAdmin = true;
                        break;
                    }

                    else
                        adm.addAdmin = false;
                }

                ad.Add(adm);
            }







            List<BA> baaa = new List<BA>();
            baaa = dataService.GetBAAsync();
            List<ListaBa> b = new List<ListaBa>();

            ListaBa baa;
            for (int i = 0; i < baaa.Count; i++)
            {

                baa = new ListaBa();
                Admin adi = new Admin();
                adi = dataService.GetAdminEmailByIdAsync(baaa[i].adminemail);
                baa.adminemail = adi.username;
                baa.beaconID = baaa[i].beaconID;
                b.Add(baa);

            }
           









            CityListDetails objBind = new CityListDetails();
            objBind.listaDetails = ad;
            objBind.BeaconsDetails = bea;
            objBind.listaBA = b;

            return View(objBind);

        }


        [Authorize]
        public async Task<ActionResult> Delete(String id)
        {
            Uri aaaa = Request.UrlReferrer;

            string username = User.Identity.GetUserName();
            DataService dataService = new DataService();
            Admin ad = new Admin();
            ad = await dataService.GetAdminByIdAsync(username);
            string admin;
            if (ad == null)
            {
                SuperAdmin superad = new SuperAdmin();
                superad = await dataService.GetSuperAdminByIdAsync(username);
                admin = superad.email;
            }
            else
            {
                admin = ad.email;
            }

            /* string uri = aaaa.ToString();
             string param = string.Join(string.Empty, uri.Split('/').Skip(5));*/

            await dataService.DeletaBeaconAsync(id, admin);
            Success(string.Format("'<b>{0}</b>' deleted!", id), true);
            return RedirectToAction("Index", "Home");


        }

        public IEnumerable GetBeacons()
        {


            DataService dataService = new DataService();
            List<Beacon> beacon = new List<Beacon>();
            beacon = dataService.GetBeaconAsync();

            List<BeaconsM> be = new List<BeaconsM>();

            BeaconsM bea;
            for (int i = 0; i < beacon.Count; i++)
            {
                bea = new BeaconsM();
                bea.BeaconID = beacon[i].beaconID;
                bea.MajorValue = beacon[i].majorvalue;
                bea.MinorValue = beacon[i].minorvalue;
                bea.Name = beacon[i].model;
                String[] state = beacon[i].name.Split('-');
                bea.Nome = state[1];
                be.Add(bea);
            }
            IEnumerable myEnumerable = (IEnumerable)be;
            return myEnumerable;
        }

        public IEnumerable GetInaticeBeacons()
        {


            DataService dataService = new DataService();
            List<Beacon> beacon = new List<Beacon>();
            beacon = dataService.GetBeaconAsync();

            List<BeaconsM> be = new List<BeaconsM>();

            BeaconsM bea;
            for (int i = 0; i < beacon.Count; i++)
            {
                String[] state = beacon[i].name.Split('-');
                if (state[0] == "inactive")
                {
                    bea = new BeaconsM();
                    bea.BeaconID = beacon[i].beaconID;
                    bea.MajorValue = beacon[i].majorvalue;
                    bea.MinorValue = beacon[i].minorvalue;
                    bea.Name = beacon[i].model;
                    bea.Nome = state[1];
                    be.Add(bea);
                }
            }
            IEnumerable myEnumerable = (IEnumerable)be;
            return myEnumerable;
        }

        public IEnumerable GetAtiveBeacons()
        {


            DataService dataService = new DataService();
            List<Beacon> beacon = new List<Beacon>();
            beacon = dataService.GetBeaconAsync();

            List<BeaconsM> be = new List<BeaconsM>();

            BeaconsM bea;
            for (int i = 0; i < beacon.Count; i++)
            {
                String[] state = beacon[i].name.Split('-');
                if (state[0] == "active")
                {
                    bea = new BeaconsM();
                    bea.BeaconID = beacon[i].beaconID;
                    bea.MajorValue = beacon[i].majorvalue;
                    bea.MinorValue = beacon[i].minorvalue;
                    bea.Name = beacon[i].model;
                    bea.Nome = state[1];
                    be.Add(bea);
                }
            }
            IEnumerable myEnumerable = (IEnumerable)be;
            return myEnumerable;
        }


        public IEnumerable GetBA()
        {


            DataService dataService = new DataService();
            List<BA> ba = new List<BA>();
            ba = dataService.GetBAAsync();

            List<BeaconsM> be = new List<BeaconsM>();
            
            BeaconsM bea;
            for (int i = 0; i < ba.Count; i++)
            {
                
                    bea = new BeaconsM();
                    Admin ad = new Admin();
                    ad = dataService.GetAdminEmailByIdAsync(ba[i].adminemail);
                    bea.adminemail = ad.username;
                    bea.beaconID = ba[i].beaconID;
                    be.Add(bea);

            }
            IEnumerable myEnumerable = (IEnumerable)be;
            return myEnumerable;
        }

        [Authorize]
        [HttpGet]
        public ActionResult Index(String pa)
        {
            dynamic mymodel = new ExpandoObject();
            mymodel.ABeacons = GetAtiveBeacons();
            mymodel.IBeacons = GetInaticeBeacons();
            mymodel.Beacons = GetBeacons();
            mymodel.BA = GetBA();
            return View(mymodel);
        }
        [Authorize]
        public ActionResult About()
        {
            ViewBag.Message = "Administrators";

            return View();
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        //
        // GET: /Account/Register
        [Authorize]
        public ActionResult Register()
        {
            return View();
        }

        //
        // POST: /Account/Register
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            DataService dataService = new DataService();
            if (ModelState.IsValid)
            {
                string pass = sha256_hash(model.Password);
                Admin novoAdmin = new Admin
                {
                    name = model.Nome,
                    username = model.Username,
                    email = model.Email,
                    password = pass

                };
                ApplicationDbContext context = new ApplicationDbContext();
                var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
                if (!roleManager.RoleExists("Admin"))
                {
                    var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                    role.Name = "Admin";
                    roleManager.Create(role);

                }
                var user = new ApplicationUser { UserName = model.Username, Email = model.Email };
                var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await this.UserManager.AddToRoleAsync(user.Id, "Admin");
                }
                Admin aaaa = new Admin();
                aaaa = await dataService.GetAdminByIdAsync(model.Username);
                bool a = false;
                if (aaaa == null && novoAdmin.username!="toze")
                    a = await dataService.AddAdminAsync(novoAdmin);

                //await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);

                // Para obter mais informações sobre como habilitar a confirmação da conta e redefinição de senha, visite https://go.microsoft.com/fwlink/?LinkID=320771
                // Enviar um email com este link
                // string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                // await UserManager.SendEmailAsync(user.Id, "Confirmar sua conta", "Confirme sua conta clicando <a href=\"" + callbackUrl + "\">aqui</a>");

                if (a == true)
                {
                    Success(string.Format("'<b>{0}</b>' created with success!", novoAdmin.username), true);
                    return RedirectToAction("ListaAdmin", "Home");
                }
                ModelState.AddModelError("", "Admin already registered.");
            }

            // Se chegamos até aqui e houver alguma falha, exiba novamente o formulário
            return View(model);
        }


        [Authorize]
        public async Task<ActionResult> DetailsAdmin(String id)
        {
            DataService dataService = new DataService();
            Admin admin = new Admin();
            admin = await dataService.GetAdminByIdAsync(id);


            ListaAdmin ad;

            ad = new ListaAdmin();
            ad.Email = admin.email;
            ad.Nome = admin.name;
            ad.Username = admin.username;

            List<Beacon> beacon = new List<Beacon>();
            beacon = dataService.GetBeaconAsync();

            List<ListaBeaconD> be = new List<ListaBeaconD>();
            ListaBeaconD bea;
            for (int i = 0; i < beacon.Count; i++)
            {

                bea = new ListaBeaconD();
                bea.beaconID = beacon[i].beaconID;
                bea.model = beacon[i].model;
                String[] state = beacon[i].name.Split('-');
                bea.name = state[1];

                List<BA> ba = new List<BA>();
                ba = dataService.GetBAAsync();
                for (int j = 0; j < ba.Count; j++)
                {
                    if (ba[j].beaconID == beacon[i].beaconID && ba[j].adminemail == admin.email)
                    {
                        bea.addAdmin = true;
                        break;
                    }

                    else
                        bea.addAdmin = false;
                }

                be.Add(bea);
            }


            AdminListDetails objBind = new AdminListDetails();
            objBind.listaDetailsBeacon = be;
            objBind.ListaAdmin = ad;

            return View(objBind);


        }


        [Authorize]
        //[Authorize(Roles = "SuperAdmin")]
        [HttpGet]
        public async Task<ActionResult> EditaAdmin(String id)
        {
            Admin admin = new Admin();
            EditaAdminViewModel ad = new EditaAdminViewModel();
            DataService dataService = new DataService();
            if (User.Identity.Name == "toze")
            {
                
                admin = await dataService.GetAdminByIdAsync(id);
                ad.Username = admin.username;
                ad.Email = admin.email;
                ad.Nome = admin.name;
                ad.ConfirmPassword = admin.password;
                ad.Password = admin.password;

            }
            else
            {
                
                admin = await dataService.GetAdminByIdAsync(id);
                ad.Username = admin.username;
                ad.Email = admin.email;
                ad.Nome = admin.name;
                //ad.ConfirmPassword = admin.password;
                //ad.Password = admin.password;
            }



            List<Beacon> beacon = new List<Beacon>();
            beacon =  dataService.GetBeaconAsync();
            List<ListaBeaconD> be = new List<ListaBeaconD>();
            ListaBeaconD bea;
            for (int i = 0; i < beacon.Count; i++)
            {

                bea = new ListaBeaconD();
              
                    bea.beaconID = beacon[i].beaconID;
                bea.majorvalue = beacon[i].majorvalue;
                bea.minorvalue = beacon[i].minorvalue;
                bea.model = beacon[i].model;
                String[] state = beacon[i].name.Split('-');
                bea.name = state[1];
           

                List<BA> ba = new List<BA>();
                ba = dataService.GetBAAsync();
                for (int j = 0; j < ba.Count; j++)
                {
                    if (ba[j].adminemail == admin.email && ba[j].beaconID == beacon[i].beaconID)
                    {
                        bea.addAdmin = true;
                        break;
                    }

                    else
                        bea.addAdmin = false;
                }

                be.Add(bea);
            }


            AdminListDetailsBeacon objBind = new AdminListDetailsBeacon();
            objBind.EditaAdminViewModel = ad;
            objBind.listaDetailsBeacon = be;

            return View(objBind);
        }


        [Authorize]
        //[Authorize(Roles = "SuperAdmin")]
        [HttpGet]
        public async Task<ActionResult> EditaSuperAdmin(String id)
        {

            EditaSuperAdminViewModel ad = new EditaSuperAdminViewModel();
            DataService dataService = new DataService();

            SuperAdmin superadmin = new SuperAdmin();
            superadmin = await dataService.GetSuperAdminByIdAsync(id);
            ad.Username = superadmin.username;
            ad.Email = superadmin.email;
            ad.Nome = superadmin.name;
            

            return View(ad);

        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditaSuperAdmin(EditaSuperAdminViewModel model)
        {
            DataService dataService = new DataService();
            if (ModelState.IsValid)
            {
                string pass = sha256_hash(model.Password);

                SuperAdmin novoSuperAdmin = new SuperAdmin
                {
                    name = model.Nome,
                    username = model.Username,
                    email = model.Email,
                    password = pass

                };
                bool a = await dataService.UpdateSuperAdmin(novoSuperAdmin);

                if (a == true)
                {
                    Success(string.Format("Your account was successfully updated"), true);
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "User already registered");
            }


            return View(model);
        }




        [Authorize]
        public async Task<ActionResult> DeleteAdmin(String id)
        {

            DataService dataService = new DataService();

            Admin ad = new Admin();
            ad = await dataService.GetAdminByIdAsync(id);
            string admin = ad.email;

            await dataService.DeletaAdminAsync(admin);
            Success(string.Format("'<b>{0}</b>' deleted", ad.name), true);
            return RedirectToAction("ListaAdmin", "Home");


        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditaAdmin(AdminListDetailsBeacon model)
        {
            DataService dataService = new DataService();
            if (ModelState.IsValid)
            {
                Admin novoAdmin = new Admin();
                if(User.Identity.Name == "toze")
                {


                    novoAdmin.name = model.EditaAdminViewModel.Nome;
                    novoAdmin.username = model.EditaAdminViewModel.Username;
                    novoAdmin.email = model.EditaAdminViewModel.Email;
                    novoAdmin.password = model.EditaAdminViewModel.Password;

                    
                }
                else
                {

               
                string pass = sha256_hash(model.EditaAdminViewModel.Password);
                    novoAdmin.name = model.EditaAdminViewModel.Nome;
                    novoAdmin.username = model.EditaAdminViewModel.Username;
                    novoAdmin.email = model.EditaAdminViewModel.Email;
                    novoAdmin.password = pass;

 
 }

                if(User.Identity.Name == "toze")
{

                    List<BA> baaa = new List<BA>();
                    baaa = dataService.GetBAAsync();
                    int max = 0;
                    int i = 0;
                    for (i = 0; i < baaa.Count(); i++)
                    {
                        if (max < baaa[i].baID)
                        {
                            max = baaa[i].baID;
                        }

                    }
                    for (i = 0; i < model.listaDetailsBeacon.Count(); i++)
                    {
                        if (model.listaDetailsBeacon[i].addAdmin == true && baaa.FirstOrDefault(x => x.beaconID == model.listaDetailsBeacon[i].beaconID && x.adminemail == model.EditaAdminViewModel.Email) == null)
                        {
                            max = max + 1;
                            BA ba = new BA();
                            ba.baID = max;
                            ba.adminemail = model.EditaAdminViewModel.Email;
                            ba.beaconID = model.listaDetailsBeacon[i].beaconID;
                            bool verdade = dataService.AddBaAsync(ba);
                        }
                        else if (baaa.FirstOrDefault(x => model.listaDetailsBeacon[i].addAdmin == true && x.beaconID == model.listaDetailsBeacon[i].beaconID && x.adminemail == model.EditaAdminViewModel.Email) != null)
                        {
                            continue;
                        }
                        else
                        {
                            for (int j = 0; j < baaa.Count(); j++)
                            {
                                if (baaa[j].adminemail == model.EditaAdminViewModel.Email && baaa[j].beaconID == model.listaDetailsBeacon[i].beaconID)
                                {
                                    dataService.DeletaBAAsync(baaa[j].baID);
                                    break;
                                }
                            }
                        }

                    }
                }

               bool a = await dataService.UpdateAdmin(novoAdmin);

                if (a == true)
                {
                    Success(string.Format("'<b>{0}</b>' edited with success!", model.EditaAdminViewModel.Nome), true);
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "User already registered");
            }


            return View(model);
        }

        [Authorize]
        public async Task<ActionResult> ListaAdmin(int? pagina)
        {
            DataService dataService = new DataService();
            List<Admin> admin = new List<Admin>();
            admin = await dataService.GetAdminAsync();
            List<ListaAdmin> ad = new List<ListaAdmin>();

            ListaAdmin adm;
            for (int i = 0; i < admin.Count; i++)
            {
                adm = new ListaAdmin();
                adm.Email = admin[i].email;
                adm.Nome = admin[i].name;
                adm.Username = admin[i].username;
                ad.Add(adm);
            }
            IEnumerable myEnumerable = (IEnumerable)ad;
            int paginaTamanho = 10;
            int paginaNumero = (pagina ?? 1);
            return View(ad.ToPagedList(paginaNumero, paginaTamanho));
            

        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////
        [Authorize]
        public async Task<ActionResult> ListaLogMobApp(int? pagina)
        {
            DataService dataService = new DataService();
            List<LogMobileApp> logmob = new List<LogMobileApp>();
            logmob = await dataService.GetLogMobileAppAsync();
            List<LogModel> lo = new List<LogModel>();

            LogModel log;
            for (int i = 0; i < logmob.Count; i++)
            {
                log = new LogModel();
                log.Date = logmob[i].date;
                log.EventType = logmob[i].eventtype;
                log.LogmaID = logmob[i].logmaID;
                log.Username = logmob[i].username;
                lo.Add(log);
            }
            IEnumerable myEnumerable = (IEnumerable)lo;
            int paginaTamanho = 10;
            int paginaNumero = (pagina ?? 1);
            return View(lo.ToPagedList(paginaNumero, paginaTamanho));

        }
        [Authorize]
        public async Task<ActionResult> ListaLogWebbApp(int? pagina)
        {
            DataService dataService = new DataService();
            List<LogWebbApp> logweb = new List<LogWebbApp>();
            logweb = await dataService.GetLogWebbAppAsync();
            List<LogModelWeb> lo = new List<LogModelWeb>();
            logweb.Reverse();
            LogModelWeb log;
            for (int i = logweb.Count-1; i >= 0; i--)
            {
                log = new LogModelWeb();
                log.Date = logweb[i].date;
                log.EventType = logweb[i].eventtype;
                log.LogweID = logweb[i].logwaID;
                log.Username = logweb[i].username;
                lo.Add(log);
            }
            IEnumerable myEnumerable = (IEnumerable)logweb;
            int paginaTamanho = 10;
            int paginaNumero = (pagina ?? 1);
            return View(logweb.ToPagedList(paginaNumero, paginaTamanho));

        }
        [Authorize]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }


        [Authorize]
        [HttpPost]
        public ActionResult EditAdminSubmit(AdminEditModel model)
        {
            if (model.isValid())
                return View(model);
            return View(model);
        }
    }
}