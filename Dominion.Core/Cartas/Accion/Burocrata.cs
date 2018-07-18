using Gabriel.Cat.S.Extension;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dominion.Core
{
    public class Burocrata : CartaAccio
    {
        public Burocrata()
        {
            Cost = 4;
        }
        public override bool EsCartaAtac => true;
        public override void ExecutaAccio(Partida partida)
        {
            IList<CartaDominion> cartes;
            if (partida.AgafaCarta(typeof(Plata)))
                partida.JugadorActual.Mazo.Push(new Plata());
            for (int i = 0; i < partida.Jugadors.Length; i++)
            {
                if (partida.Jugadors[i].Posicio != partida.JugadorActual.Posicio&&!partida.Jugadors[i].Protegit(partida))
                {
                    cartes = partida.Jugadors[i].Ma.Filtra((carta) => carta.EsCartaDeVictoria);
                    if (cartes.Count > 0)
                    {
                        //si te una carta victoria demano que trii quina enseña
                        cartes = partida.TriaCartes(partida.Jugadors[i], "Tria una carta per mostrar", 1, 1, cartes);
                        partida.Jugadors[i].CartesMostrades.Add(cartes[0]);
                        partida.Jugadors[i].Ma.Remove(cartes[0]);
                    }
                    else
                    {
                        //si no en te enseña totes
                        partida.Jugadors[i].CartesMostrades.AddRange(partida.Jugadors[i].Ma);
                        partida.Jugadors[i].Ma.Clear();
                    }
                }
            }
            partida.PreguntaAlJugador(partida.JugadorActual, "Mira les cartes.", "Ja está");
            for (int i = 0; i < partida.Jugadors.Length; i++)
            {
                partida.Jugadors[i].Ma.AddRange(partida.Jugadors[i].CartesMostrades);
                partida.Jugadors[i].CartesMostrades.Clear();
            }
        }
    }
}
