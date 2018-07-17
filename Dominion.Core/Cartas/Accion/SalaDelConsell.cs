using System;
using System.Collections.Generic;
using System.Text;

namespace Dominion.Core
{
    public class SalaDelConsell : CartaAccio
    {
        public SalaDelConsell()
        {
            CartesAdicionals = 4;
            CompresAdicionals = 1;
            Cost = 5;
        }
        public override void ExecutaAccio(Partida partida)
        {
            for (int i = 0; i < partida.Jugadors.Length; i++)
                if (partida.Jugadors[i].Posicio != partida.JugadorActual.Posicio)
                    partida.Jugadors[i].Roba();

        }
    }
}
