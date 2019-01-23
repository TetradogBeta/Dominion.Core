using System;
using System.Collections.Generic;
using System.Text;

namespace Dominion.Core
{
    public class PartidaLan : Partida
    {
        public PartidaLan(string[] jugadors, string[] tipusCartesSubmnistraments) : base(jugadors, tipusCartesSubmnistraments)
        {
            JugadorsLan = new JugadorLan[jugadors.Length];
            for (int i = 0; i < Jugadors.Length; i++)
                JugadorsLan[i] = new JugadorLan(Jugadors[i]);
        }

        public JugadorLan[] JugadorsLan { get; private set; }

        public override int PreguntaAlJugador(Jugador jugador, string contingut, params string[] opcions)
        {
            JugadorLan jugadorLan = JugadorsLan[jugador.Posicio];
            for (int i = 0; i < JugadorsLan.Length; i++)
                if (jugador.Posicio != i)
                    JugadorsLan[i].InformaAccio(JugadorsLan[i], "@preguntant");
            return jugadorLan.Pregunta(contingut, opcions);
        }

        public override CartaDominion[] TriaCartes(Jugador jugador, string continugt, int minimCartes, int maximCartes, IList<CartaDominion> cartes)
        {
            JugadorLan jugadorLan = JugadorsLan[jugador.Posicio];
            for (int i = 0; i < JugadorsLan.Length; i++)
                if (jugador.Posicio != i)
                    JugadorsLan[i].InformaAccio(JugadorsLan[i], "@triaCartes");
            return jugadorLan.TriaCartes(continugt, minimCartes, maximCartes, cartes);
        }
    }
}
