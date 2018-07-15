using System;
using System.Collections.Generic;
using System.Text;

namespace Dominion.Core
{
    public class Jardins : CartaVictoria
    {
        public Jardins()
        {
            Cost = 4;
          
        }
        public int PuntsDeVictoria(PiloJugador mazoJugador)
        {
            //conta la de jardins??
            return mazoJugador.Count / 10;
        }
    }
}
