﻿using System;
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
        public new int PuntsDeVictoria(IList<CartaDominion> mazoJugador)
        {
            return mazoJugador.Count / 10;
        }
    }
}
