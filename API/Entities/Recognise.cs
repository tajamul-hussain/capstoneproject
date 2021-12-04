using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entities
{
    public class Recognise
    {

      public int Id { get; set; }

     public int SourceID { get; set; }
     public int LoggedId { get; set; }
    public int Points { get; set; }
     public string Comments { get; set; }
        
    }
}