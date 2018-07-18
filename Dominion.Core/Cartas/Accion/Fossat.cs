using Gabriel.Cat.S.Extension;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dominion.Core
{
    public class Fossat : CartaAccio
    {
        public Fossat()
        {
            Cost = 2;
        }

        public override bool EsCartaReaccio => true;
        public  bool Reaccio(Partida partida,Jugador jugador)
        {
            bool quiereEnseñarla;
            quiereEnseñarla = partida.PreguntaAlJugador(jugador,"Vols protegirte de l'atac?","Si","No")==0;
            if(quiereEnseñarla)
            {
                jugador.CartesMostrades.Add(partida.JugadorReactiu.Ma.Filtra((c) => c is Fossat)[0]);
                partida.PreguntaAlJugador(partida.JugadorActual, string.Format("{0} s'ha protegit", partida.JugadorReactiu.Nom), "Ok");
                jugador.CartesMostrades.Clear();

            }
            return quiereEnseñarla;


        }
    }
}
