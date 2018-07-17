using Gabriel.Cat.S.Extension;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dominion.Core
{
    public class SalaDelTro : CartaAccio
    {
        
        public SalaDelTro()
        {
            Cost = 4;
        }
        public override void ExecutaAccio(Partida partida)
        {
            CartaAccio carta;
            IList<CartaDominion> cartesAccio = partida.JugadorActual.Ma.Filtra((c) => c is CartaAccio);
            if (cartesAccio.Count > 0)
            {
                if (cartesAccio.Count > 1)
                     carta = (CartaAccio)partida.TriaCartes(partida.JugadorActual, "Tria la carta a repetir la acció", 1, 1, cartesAccio)[0];
                else carta = cartesAccio[0] as CartaAccio;

                carta.ExecutaAccio(partida);
                carta.ExecutaAccio(partida);
       
            }
        }
    }
}
