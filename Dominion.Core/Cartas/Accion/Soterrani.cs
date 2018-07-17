using System;
using System.Collections.Generic;
using System.Text;

namespace Dominion.Core
{
    public class Soterrani : CartaAccio
    {
        public Soterrani()
        {
            Cost = 2;
            AccionsAdicionals = 1;
        }
        public override void ExecutaAccio(Partida partida)
        {
            IList<CartaDominion> cartesDescartades = partida.TriaCartes(partida.JugadorActual, "Descarta totes les cartes que vulguis i roba el mateix nombre", 0, partida.JugadorActual.Ma.Count);
            partida.JugadorActual.Descartades.AddRange(cartesDescartades);
            for (int i = 0; i < cartesDescartades.Count; i++)
            {
                partida.JugadorActual.Ma.Remove(cartesDescartades[i]);
                partida.JugadorActual.Roba();
            }
        }
    }
}
