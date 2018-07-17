using System;
using System.Collections.Generic;
using System.Text;

namespace Dominion.Core
{
    public class Canceller : CartaAccio
    {
        public Canceller()
        {
            Cost = 3;
            Valor = 2;
        }
        public override void ExecutaAccio(Partida partida)
        {
            const int SI = 0;
            string[] opcions = { "Si", "No" };
            if (partida.PreguntaAlJugador(partida.JugadorActual, "Vols descartar el mazo?", opcions) == SI)
            {
                partida.JugadorActual.Descartades.AddRange(partida.JugadorActual.Mazo);
                partida.JugadorActual.Mazo.Clear();
                partida.JugadorActual.PosaCartesDescartadesAlMazo();
            }
        }
    }
}
