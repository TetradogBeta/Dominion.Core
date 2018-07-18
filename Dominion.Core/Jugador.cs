using Gabriel.Cat.S.Extension;
using Gabriel.Cat.S.Utilitats;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Dominion.Core
{
    public class Jugador
    {
        public Jugador(int posicio, string nom)
        {
            Posicio = posicio;
            Nom = nom;
            Ma = new Llista<CartaDominion>();
            Descartades = new Llista<CartaDominion>();
            Mazo = new Llista<CartaDominion>();
            CartesMostrades = new Llista<CartaDominion>();
        }

        public int Posicio { get; set; }



        public string Nom { get; set; }
        public Llista<CartaDominion> Ma { get; private set; }

       

        public Llista<CartaDominion> Descartades { get; private set; }
        public Llista<CartaDominion> Mazo { get; private set; }
        //las cartas mostradas a veces vuelven al mazo otras van a otro jugador y tambien van a descartadas
        /// <summary>
        /// Son cartes que s'ensenyen a tots el jugadors durant un temps(mentres dura la carta que fa l'acció)
        /// </summary>
        public Llista<CartaDominion> CartesMostrades { get;private set; }

        public void EnseñaCartesMazo(int num)
        {
            CartaDominion carta;
            for (int i = 0; i < num; i++)
            {
                carta = DonamCartaMazo();
                if(carta!=null)
                  CartesMostrades.Add(carta);
            }
        }

        public CartaDominion DonamCartaMazo()
        {
            CartaDominion carta=null;

            if (Mazo.Count == 0)
                PosaCartesDescartadesAlMazo();

            if(Mazo.Count>0)
            {
                carta = Mazo.Pop();
            }

            return carta;
        }
        public bool Protegit(Partida partida)
        {
            Fossat fossat = Ma.Filtra((c) => c is Fossat).FirstOrDefault() as Fossat;
            bool protegit = fossat != null;
            if (protegit)
                protegit = fossat.Reaccio(partida, this);
            return protegit;
        }
        public bool Roba()
        {
            CartaDominion carta = DonamCartaMazo();

            if (carta != null)
                Ma.Add(carta);

            return carta != null;
        }
        public void PosaCartesDescartadesAlMazo()
        {
            Descartades.Desordena();
            Mazo.AddRange(Descartades);
            Descartades.Clear();
        }
        public void PosarTotesLesCartesAlMazo()
        {
            Mazo.AddRange(Ma);
            Ma.Clear();
            Mazo.AddRange(CartesMostrades);
            CartesMostrades.Clear();
            Mazo.AddRange(Descartades);
            Descartades.Clear();
        }
        public int PuntuacioFinal()
        {
            int puntuacio = 0;
            IList<CartaVictoria> cartaVictorias;
            PosarTotesLesCartesAlMazo();
            cartaVictorias = Mazo.Filtra((c) => c is CartaVictoria).Casting<CartaVictoria>();
            for(int i=0;i<cartaVictorias.Count;i++)
                if(!(cartaVictorias[i] is Jardins))
                {
                    puntuacio += cartaVictorias[i].Valor;
                }
                else
                {
                    puntuacio += (cartaVictorias[i] as Jardins).PuntsDeVictoria(Mazo);
                }

            return puntuacio;
        }

        public void Reset()
        {
            CartesMostrades.Clear();
            Ma.Clear();
            Mazo.Clear();
            Descartades.Clear();
        }
    }
}