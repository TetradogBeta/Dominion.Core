﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Dominion.Core
{
    public class CartaVictoria:CartaDominion
    {
        public override bool EsCartaDeVictoria => true;
        public int PuntsDeVictoria
        {
            get;
            protected set;

        }
    }
}
