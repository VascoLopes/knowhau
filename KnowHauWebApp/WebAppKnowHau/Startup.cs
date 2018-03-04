using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;
using WebAppKnowHau.Models;

[assembly: OwinStartupAttribute(typeof(WebAppKnowHau.Startup))]
namespace WebAppKnowHau
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            //createRolesandUsers();
        }
        private void createRolesandUsers()
        {
            ApplicationDbContext context = new ApplicationDbContext();

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));


            // In Startup iam creating first Admin Role and creating a default Admin User    
            if (!roleManager.RoleExists("SuperAdmin"))
            {

                // first we create Admin rool   
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "SuperAdmin";
                roleManager.Create(role);

                //Here we create a Admin super user who will maintain the website                  

                var user = new ApplicationUser();
                user.UserName = "toze";
                user.Email = "zeto@olaola.com";

                string userPWD = "lol";

                var chkUser = UserManager.Create(user, userPWD);

                //Add default User to Role SuperAdmin   
                if (chkUser.Succeeded)
                {
                    var result1 = UserManager.AddToRole(user.Id, "SuperAdmin");

                }
            }

            // creating Creating Admin role    
            if (!roleManager.RoleExists("Admin"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Admin";
                roleManager.Create(role);

            }


        }
    }
}
