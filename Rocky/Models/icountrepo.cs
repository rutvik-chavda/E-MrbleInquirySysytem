using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rocky.Models
{
   public interface icountrepo
    {
        bool addcount(int i);
        int getcount();
    }
}
