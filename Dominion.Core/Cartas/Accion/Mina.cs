using Gabriel.Cat.S.Extension;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dominion.Core
{
    public class Mina : CartaAccio
    {
        public Mina()
        {
            Cost = 5;
        }
        public override void ExecutaAccio(Partida partida)
        {
            IList<CartaDominion> cartesTresor = partida.JugadorActual.Ma.Filtra((c) => c is CartaTresor);
            if(cartesTresor.Count>0)
            {
                cartesTresor = partida.TriaCartes(partida.JugadorActual, "Tria una carta per eliminar, a canvi guanyaràs una que costi 3 més", 0, 1);
                if(cartesTresor.Count>0)
                {
                    partida.EliminaCarta(cartesTresor);
                    partida.JugadorActual.Ma.RemoveRange(cartesTresor);
                    partida.GanaCarta(cartesTresor[0].Cost + 3, false, typeof(CartaTresor));

                }
            }
        }
    }
}
