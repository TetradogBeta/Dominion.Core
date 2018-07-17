using System;
using System.Collections.Generic;
using System.Text;

namespace Dominion.Core
{
    public class Milicia : CartaAccio
    {
        public Milicia()
        {
            Valor = 2;
            Cost = 4;
        }
        public override void ExecutaAccio(Partida partida)
        {
            IList<CartaDominion> descartades;
            for (int i = 0; i < partida.Jugadors.Length; i++)
                if (partida.Jugadors[i].Posicio != partida.JugadorActual.Posicio)
                {
                    descartades = partida.TriaCartes(partida.Jugadors[i], "Tria les cartes a descartar", partida.Jugadors[i].Ma.Count - 3, partida.Jugadors[i].Ma.Count - 3);
                    partida.Jugadors[i].Descartades.AddRange(descartades);
                    for (int j = 0; j < descartades.Count; j++)
                        partida.Jugadors[i].Ma.Remove(descartades[j]);
                }
        }
    }
}
