using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebAppKnowHau.Models
{
    public class ListaAdmin
    {

        public string Nome { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }


    }




    public class EditaAdminViewModel
    {

        [Required]
        [Display(Name = "Name")]
        public string Nome { get; set; }

        [Required]
        [Display(Name = "Username")]
        public string Username { get; set; }

        [Required]
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must have atleast {2} characters.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "Passwords don't match.")]
        public string ConfirmPassword { get; set; }
    }

    public class EditaSuperAdminViewModel
    {

        [Required]
        [Display(Name = "Name")]
        public string Nome { get; set; }

        [Required]
        [Display(Name = "Username")]
        public string Username { get; set; }

        [Required]
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must have atleast {2} characters.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "Passwords don't match.")]
        public string ConfirmPassword { get; set; }
    }




  



    public class ListaBeaconD
    {
        public string beaconID { get; set; }
        public int majorvalue { get; set; }
        public int minorvalue { get; set; }
        public string name { get; set; }
        public string model { get; set; }


        public bool addAdmin { get; set; }
    }

  public class AdminListDetailsBeacon
    {
        //use CheckBoxModel class as list   
        public List<ListaBeaconD> listaDetailsBeacon
        {
            get;
            set;
        }

        public EditaAdminViewModel EditaAdminViewModel { get; set; }

    }



    public class AdminListDetails
    {
        //use CheckBoxModel class as list   
        public List<ListaBeaconD> listaDetailsBeacon
        {
            get;
            set;
        }

        public ListaAdmin ListaAdmin { get; set; }

    }
}
