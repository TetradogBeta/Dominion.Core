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
        public override bool EsCartaAtac => false;
        public override bool EsCartaReaccio => true;
        public override void ExecutaAccio(Partida partida)
        {
            bool quiereEnseñarla;
            quiereEnseñarla = partida.PreguntaAlJugador(partida.JugadorReactiu,"Vols protegirte de l'atac?","Si","No")==0;
            if(quiereEnseñarla)
            {
                partida.JugadorReactiu.CartesMostrades.Add(partida.JugadorReactiu.Ma.Filtra((c) => c is Fossat)[0]);
                partida.JugadorReactiu.Ma.RemoveRange(partida.JugadorReactiu.CartesMostrades);

            }
            partida.PreguntaAlJugador(partida.JugadorActual, string.Format("{0} s'ha protegit", partida.JugadorReactiu.Nom), "Ok");
            //lo que no se ahora si el jugador pierde la carta o no...


        }
    }
}
