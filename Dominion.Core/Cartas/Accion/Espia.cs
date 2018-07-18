using System;
using System.Collections.Generic;
using System.Text;

namespace Dominion.Core
{
    public class Espia : CartaAccio
    {
        public Espia()
        {
            CartesAdicionals = 1;
            AccionsAdicionals = 1;
        }
        public override bool EsCartaAtac => true;
        public override void ExecutaAccio(Partida partida)
        {
            bool tornarAlMazo;
            for(int i=0;i<partida.Jugadors.Length;i++)
            {
                if (!partida.Jugadors[i].Protegit(partida))
                {
                    partida.Jugadors[i].EnseñaCartesMazo(1);
                    tornarAlMazo = partida.PreguntaAlJugador(partida.JugadorActual, string.Format("Descartar o volver a su sitio la carta de {0}", partida.Jugadors[i]), "Descartar", "Tornar al Mazo") == 1;
                    if (tornarAlMazo)
                    {
                        partida.Jugadors[i].Mazo.Push(partida.Jugadors[i].CartesMostrades.Pop());

                    }
                    else
                    {
                        partida.Jugadors[i].Descartades.Push(partida.Jugadors[i].CartesMostrades.Pop());
                    }
                }
            }
        }
    }
}
