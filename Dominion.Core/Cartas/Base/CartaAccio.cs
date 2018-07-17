using System;
using System.Collections.Generic;
using System.Text;

namespace Dominion.Core
{

    public class CartaAccio:CartaDominion
    {
        public override bool EsCartaDeAccio => true;
        protected int CompresAdicionals
        {
            get;
            set;
        }
        protected int CartesAdicionals
        {
            get;
            set;
        }
        protected int AccionsAdicionals
        {
            get;
            set;
        }
        public virtual bool EsCartaAtac => true;
        public virtual bool EsCartaReaccio => false;

        public virtual void ExecutaAccio(Partida partida)
        {
            /*Roba del piló de cartes per robar tantes cartes como indiqui la i les afegeix
             * a la del jugador actual Després executa l'acció específica de la carta. I finalment executa el final de fase.*/
        }

    }
}
