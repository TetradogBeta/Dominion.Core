using System;
using System.Collections.Generic;
using System.Text;

namespace Dominion.Core
{
    public class Capella : CartaAccio
    {
        public Capella()
        {
            Cost = 2;
        }
        public override void ExecutaAccio(Partida partida)
        {
            partida.EliminaCarta(partida.TriaCartes(partida.JugadorActual, "Elimina fins a 4 cartes", 0, 4));
        }
    }
}
