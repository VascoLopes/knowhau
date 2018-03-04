using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebAppKnowHau.Models
{

    public class AdminEditModel
    {
        [StringLength(50)]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Username")]
        public string Username { get; set; }

        [StringLength(100, ErrorMessage = "The {0} must have atleast {2} characters.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "Passwords don't match.")]
        public string ConfirmPassword { get; set; }

        internal bool isValid()
        {
            throw new NotImplementedException();
        }
    }

    public class EditBeaconViewModel
    {
        [Required]
        [Display(Name = "BeaconID")]
        public string BeaconID { get; set; }

        [Required]
        [Display(Name = "Name")]
        public string Nome { get; set; }

        [Required]
        [Display(Name = "Model")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "MajorValue")]
        public int MajorValue { get; set; }

        [Required]
        [Display(Name = "MinorValue")]
        public int MinorValue { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Message")]
        public string Mensagem { get; set; }

        [Required]
        [Display(Name = "Activate Beacon?")]
        public bool ActiveBeacon { get; set; }

        
        public string Hide { get; set; }



    }

    public class ListaAdminBEdit
    {
        public string NomeAdmin { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }


        public bool addAdmin { get; set; }
    }
    public class CityListEdit
    {
        //use CheckBoxModel class as list   
        public List<ListaAdminBEdit> listaa
        {
            get;
            set;
        }

        public EditBeaconViewModel EditBeaconViewModel { get; set; }

    }

}