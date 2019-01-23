using System;
using System.Collections.Generic;
using System.Text;

namespace Dominion.Core
{
    public class JugadorLan
    {
        public JugadorLan(Jugador jugador)
        {
            this.Jugador = jugador;
        }

        public Jugador Jugador { get; private set; }

        public int Pregunta(string contingut,string[] opcions)
        {
            throw new NotImplementedException();
        }
        public CartaDominion[] TriaCartes(string continugt, int minimCartes, int maximCartes, IList<CartaDominion> cartes)
        {
            throw new NotImplementedException();
        }
        public void InformaAccio(JugadorLan jugadorAccio,string accio)
        {
            throw new NotImplementedException();
        }
        public void ActualitzaTaulell(PartidaLan partida)
        {//els canvis fets pel jugador s'han de reflectir als altres i ho han de veure (les cartes cara amunt o no)//mirar que editant el navegador no facin trampes
            throw new NotImplementedException();
        }
    }
}
