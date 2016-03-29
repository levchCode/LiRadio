using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadioListener
{
   public class Station
    {
      public string Name {get; set;}
      public string URL {get; set;}

       public override string ToString()
       {
           return Name;
       }
       
    }
}
