using Gabriel.Cat.S.Extension;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dominion.Core
{
    public class Enviat : CartaAccio
    {
        public Enviat()
        {
            Cost = 4;
        }
        public override void ExecutaAccio(Partida partida)
        {
            IList<CartaDominion> cartes = new CartaDominion[5];
            CartaDominion cartaDescartada;
            for (int i = 0; i < cartes.Count; i++)
                cartes[i] = partida.JugadorActual.DonamCartaMazo();
            cartes = cartes.Filtra((c) => c != null);
            cartaDescartada= partida.TriaCartes(partida.JugadorEsquerra, string.Format("Tria una carta de {0} per descartarla, les altres aniran a la seva má", partida.JugadorActual.Nom), 1, 1, cartes)[0];
            partida.JugadorActual.Ma.AddRange(cartes.Filtra((c)=>c != cartaDescartada));
            partida.JugadorActual.Descartades.Add(cartaDescartada);
        }
    }
}
