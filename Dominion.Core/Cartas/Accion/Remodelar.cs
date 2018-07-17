using System;
using System.Collections.Generic;
using System.Text;

namespace Dominion.Core
{
    public class Remodelar : CartaAccio
    {
        public Remodelar()
        {
            Cost = 4;
        }
        public override void ExecutaAccio(Partida partida)
        {
            IList<CartaDominion> cartaARemodelar=null;
            if (partida.JugadorActual.Ma.Count > 1)
            {
                cartaARemodelar = partida.TriaCartes(partida.JugadorActual, "Elimina una carta i guanya una que costi 2 més que la eliminada.", 1, 1);

            }
            else if (partida.JugadorActual.Ma.Count == 1)
                cartaARemodelar = partida.JugadorActual.Ma;

            if(partida.JugadorActual.Ma.Count>0)
            {
                partida.GanaCarta(cartaARemodelar[0].Cost + 2);
                partida.EliminaCarta(cartaARemodelar);
                partida.JugadorActual.Ma.Remove(cartaARemodelar[0]);
            }
        }
    }
}
