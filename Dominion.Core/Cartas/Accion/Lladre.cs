using Gabriel.Cat.S.Extension;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dominion.Core
{
    public class Lladre : CartaAccio
    {
        public Lladre()
        {
            Cost = 4;
        }
        public override bool EsCartaAtac => true;
        public override void ExecutaAccio(Partida partida)
        {
            CartaDominion[] cartes = new CartaDominion[2];
            IList<CartaDominion> cartesTresor;
            List<CartaDominion> cartesTriades = new List<CartaDominion>();
            CartaDominion cartaTriada;
            IList<CartaDominion> cartesEliminades;
            for (int i = 0; i < partida.Jugadors.Length; i++)
                if (partida.Jugadors[i].Posicio != partida.JugadorActual.Posicio&&!partida.Jugadors[i].Protegit(partida))
                {
                    cartaTriada = null;
                    cartes[0] = partida.Jugadors[i].DonamCartaMazo();
                    if (cartes[0] != null)
                    {
                        cartes[1] = partida.Jugadors[i].DonamCartaMazo();
                        partida.Jugadors[i].CartesMostrades.Add(cartes[0]);
                        if (cartes[1] != null)
                            partida.Jugadors[i].CartesMostrades.Add(cartes[1]);
                    }
                    cartesTresor = cartes.Filtra((c) => c is CartaTresor);
                    if (cartesTresor.Count > 1)
                    {
                        cartaTriada = partida.TriaCartes(partida.JugadorActual, "Tria una carta per quedartela o eliminarla", 1, 1, cartesTresor)[0];


                    }
                    else if (cartesTresor.Count == 1)
                        cartaTriada = cartesTresor[0];

         
                        

                    if (cartaTriada != null)
                    {
                        partida.Jugadors[i].CartesMostrades.Remove(cartaTriada);
                        cartesTriades.Add(cartaTriada);

                    }

                    if(partida.Jugadors[i].CartesMostrades.Count>0)
                    {
                        partida.Jugadors[i].Descartades.Add(partida.Jugadors[i].CartesMostrades[0]);
                        partida.Jugadors[i].CartesMostrades.Clear();
                    }

                }

            cartesEliminades=partida.TriaCartes(partida.JugadorActual, "Elimina les cartes que no vulguis", 0, cartesTriades.Count);
            cartesTriades.RemoveRange(cartesEliminades);
            partida.JugadorActual.Ma.AddRange(cartesTriades);
            partida.EliminaCarta(cartesEliminades);
        }
    }
}
