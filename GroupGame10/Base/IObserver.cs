using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace GroupGame10.Base
{
    interface IObserver
    {
        void OnNotify( string file);
    }
}
