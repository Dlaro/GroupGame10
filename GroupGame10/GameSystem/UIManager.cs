using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

using GroupGame10.Base;

namespace GroupGame10.GameSystem
{
    class UIManager : DrawableGameComponent, IManager
    {
        public UIManager(Game game) : base(game)
        {
        }

        public void ClearList()
        {
            throw new NotImplementedException();
        }
    }
}
