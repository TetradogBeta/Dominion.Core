using System;
using System.Collections.Generic;
using System.Text;

namespace Dominion.Core
{
    public class Bruixa : CartaAccio
    {
        public Bruixa()
        {
            CartesAdicionals = 2;
            Cost = 5;
        }
        public override void ExecutaAccio(Partida partida)
        {

            for (int i = 0; i < partida.Jugadors.Length; i++)
                if (partida.Jugadors[i].Posicio != partida.JugadorActual.Posicio)
                    if (partida.AgafaCarta(typeof(Malediccio)))
                        partida.Jugadors[i].Descartades.Add(new Malediccio());
        }
    }
}
