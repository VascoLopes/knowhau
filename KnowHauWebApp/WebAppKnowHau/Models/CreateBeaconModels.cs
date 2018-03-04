using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebAppKnowHau.Models
{
    public class CreateBeacon
    {

[Required]
        [Display(Name = "BeaconID")]
        public string BeaconId { get; set; }

        [Required]
        [Display(Name = "Name")]
        public string Nome { get; set; }

        [Required]
        [Display(Name = "Model")]
        public string Model { get; set; }

        [Required]
        [Display(Name = "MajorValue")]
        public int MajorValue { get; set; }

        [Required]
        [Display(Name = "MinorValue")]
        public int MinorValue { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Message")]
        public string Mesage { get; set; }

        [Required]
        [Display(Name = "Activate Beacon?")]
        public bool ActiveBeacon { get; set; }

    }

    /* public class ListaAdminB
     {


         public string NomeAdmin { get; set; }

         public string Username { get; set; }

         public string Email { get; set; }

         public bool addAdmin { get; set; }


     }*/

    public class ListaAdminB
    {
       public string NomeAdmin { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }

       
        public bool addAdmin { get; set; }
    }
    public class CityList
    {
        //use CheckBoxModel class as list   
        public List<ListaAdminB> list
        {
            get;
            set;
        }

        public CreateBeacon CreateBeacon { get; set; }

    }

     

    


}