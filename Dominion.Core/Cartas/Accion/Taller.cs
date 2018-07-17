using System;
using System.Collections.Generic;
using System.Text;

namespace Dominion.Core
{
    public class Taller : CartaAccio
    {
        public Taller()
        {
            Cost = 3;
        }
        public override void ExecutaAccio(Partida partida)
        {
            partida.GanaCarta(4);
        }
    }
}
