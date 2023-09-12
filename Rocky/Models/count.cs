using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Rocky.Models;

namespace Rocky.Models
{
    public class count : icountrepo
    {
        public int a = 0;
        public bool addcount(int i)
        {
            a = i;
            return true;
        }
        public int getcount()
        {
            return a;
        }

    }

   
}
