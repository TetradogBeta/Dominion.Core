using Gabriel.Cat.S.Extension;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dominion.Core
{
    public class Prestamista : CartaAccio
    {
        public Prestamista()
        {
            Cost = 4;
        }
        public override void ExecutaAccio(Partida partida)
        {
            IList<CartaDominion> cartesTresor = partida.JugadorActual.Ma.Filtra((c) => c is Coure);
            if(cartesTresor.Count>0)
            {
                cartesTresor = partida.TriaCartes(partida.JugadorActual, "Elimina una carta si ho fas tindràs 3 monedes extra", 0, 1);
                if(cartesTresor.Count>0)
                {
                    partida.JugadorActual.Ma.RemoveRange(cartesTresor);
                    partida.EliminaCarta(cartesTresor);
                    partida.DinersExtra += 3;
                }
            }
        }
    }
}
