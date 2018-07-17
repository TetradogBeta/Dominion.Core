using System;
using System.Collections.Generic;
using System.Text;

namespace Dominion.Core
{
    public class Banquet : CartaAccio
    {
        public Banquet()
        {
            Cost = 4;
        }
        public override void ExecutaAccio(Partida partida)
        {
            partida.EliminaCarta(this);
            partida.GanaCarta(5);
        }
    }
}
