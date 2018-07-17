using System;
using System.Collections.Generic;
using System.Text;

namespace Dominion.Core
{
    public class Aventurer : CartaAccio
    {
        public Aventurer()
        {
            Cost = 6;
        }
        public override void ExecutaAccio(Partida partida)
        {
            const int TOTAL = 2;
            CartaDominion aux;
            int numCartesTrobades=0;
            //voy cogiendo cartas del mazo hasta que descubra dos cartas tesoro las demás se van a descartadas.
            //si acabo todas las cartas es que no hay más de tesoro disponibles entonces solo cojo la encontrada o ninguna.
            for(int i=0,iF=partida.JugadorActual.Mazo.Count+partida.JugadorActual.Descartades.Count;i<iF&&numCartesTrobades<TOTAL;i++)
            {
                aux = partida.JugadorActual.DonamCartaMazo();
                if (aux.EsCartaDeTresor)
                {
                    numCartesTrobades++;
                    partida.JugadorActual.Ma.Add(aux);
                }
                else partida.JugadorActual.Descartades.Push(aux);
            }
        }
    }
}
