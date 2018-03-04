using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebAppKnowHau.Models
{

    /*public class ViewModelTab
    {
        public IEnumerable<BeaconsM> ABeacons { get; set; }
        public IEnumerable<BeaconsM> IBeacons { get; set; }
        public IEnumerable<BeaconsM> Beacons { get; set; }
    }*/
    public class BeaconsM
    {
        public string BeaconID { get; set; }
        public int MajorValue { get; set; }

        public int MinorValue { get; set; }


        public string Nome { get; set; }

        public string Name { get; set; }

        public int baID { get; set; }
        public string adminemail { get; set; }
        public string beaconID { get; set; }



    }

    public class BeaconsDetails
    {
        public string BeaconID { get; set; }
        public int MajorValue { get; set; }

        public int MinorValue { get; set; }


        public string Nome { get; set; }

        public string Name { get; set; }

        public string Message { get; set; }

        public string Cont { get; set; }




    }


    public class ListaAdminD
    {
        public string NomeAdmin { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }


        public bool addAdmin { get; set; }
    }
    public class ListaBa
    {
        public int baID { get; set; }
        public string adminemail { get; set; }
        public string beaconID { get; set; }
    }
    public class CityListDetails
    {
        //use CheckBoxModel class as list   
        public List<ListaAdminD> listaDetails
        {
            get;
            set;
        }

        public List<ListaBa> listaBA
        {
            get;
            set;
        }

        public BeaconsDetails BeaconsDetails { get; set; }

    }

}