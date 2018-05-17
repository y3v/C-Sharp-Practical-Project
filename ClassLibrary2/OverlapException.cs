using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class OverlapException : Exception
    {
       public OverlapException() : base() {
       }

       public OverlapException(String message) : base(message) {
       }
    }
}
