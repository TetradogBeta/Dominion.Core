using System;
using System.Collections.Generic;
using System.Text;

namespace Dominion.Core
{
    public class Biblioteca : CartaAccio
    {
        public Biblioteca()
        {
            Cost = 5;
        }
        public override void ExecutaAccio(Partida partida)
        {
            CartaDominion carta;
            for(int i=0,iF=partida.JugadorActual.Mazo.Count+partida.JugadorActual.Descartades.Count;i<iF&&partida.JugadorActual.Ma.Count<7;i++)
            {
                //si es accion pregunto si la quiere sino la descarto
                carta = partida.JugadorActual.DonamCartaMazo();
                if (carta != null)
                {
                    if (!carta.EsCartaDeAccio||partida.TriaCartes(partida.JugadorActual,"La vols?",1,1,new CartaDominion[] { carta}).Length>0)
                    {
                        partida.JugadorActual.Ma.Add(carta);
                    }else if(carta.EsCartaDeAccio)
                    {
                        partida.JugadorActual.Descartades.Push(carta);
                    }
                }
            }
        }
    }
}
