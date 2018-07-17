using Gabriel.Cat.S.Utilitats;
using System;
using System.Collections.Generic;

namespace Dominion.Core
{
    public class Partida
    {
        Jugador[] jugadors;
        int jugadorActual;
        LlistaOrdenada<string, int> subministraments;
        public Llista<CartaDominion> Eliminades
        {
            get;private set;
        }

        public Jugador JugadorActual
        {
            get
            {
                return jugadors[jugadorActual];
            }
        }
        public Jugador JugadorEsquerra
        {
            get
            {
                int posJugadorEsquerra = jugadorActual - 1;
                if (posJugadorEsquerra < 0)
                    posJugadorEsquerra += jugadors.Length;
                return jugadors[posJugadorEsquerra];
            }
        }
        public Jugador JugadorDreta
        {
            get
            {
                int posJugadorDreta = jugadorActual + 1;
                if (posJugadorDreta == jugadors.Length)
                    posJugadorDreta = 0;

                return jugadors[posJugadorDreta];
            }
        }

        public void AvançaJugador()
        {
            jugadorActual = jugadorActual + 1 % jugadors.Length;
        }
        public void EliminaCarta(IList<CartaDominion> cartes)
        {
            for (int i = 0; i < cartes.Count; i++)
                EliminaCarta(cartes[i]);
        }
        public void EliminaCarta(CartaDominion carta)
        {
            //posa la carta al pilo de eliminades
            Eliminades.Insert(0, carta);
        }
        public void GanaCarta(int costMaxim,bool vaADescartadas=true)
        {
            //demana al jugador actual que trii una carta del pilo de subministraments amb un cost maxim
            List<CartaDominion> cartesQuePotAgafar = new List<CartaDominion>();
            KeyValuePair<string, int> aux;
            CartaDominion carta;
            for (int i = 0; i < subministraments.Count; i++)
            {
                aux = subministraments[i];
                if (aux.Value > 0)
                {
                    carta = CartaDominion.DonamCarta(aux.Key);
                    if (carta.Cost <= costMaxim)
                        cartesQuePotAgafar.Add(carta);
                }
            }
            carta = TriaCartes(JugadorActual, "Has guanyat una carta tria una", 1, 1, cartesQuePotAgafar)[0];
            subministraments[carta.NomCarta]--;
            if (vaADescartadas)
                 JugadorActual.Descartades.Insert(0, carta);
            else JugadorActual.Ma.Add(carta);
        }
        public bool AgafaCarta(Type carta)
        {
            //agafa la carta que es demana del pilo de subministraments (si no es pot retorna false)
            bool agafada = subministraments[carta.Name] > 0;
            if (agafada)
                subministraments[carta.Name]--;
            return agafada;
        }
        public int PreguntaAlJugador(Jugador jugador, string contingut, params string[] opcions)
        {
            //pregunta al jugador i retorna la posició en la array d'opcions triada
            throw new NotImplementedException();
        }
        /// <summary>
        /// demana al jugador que trii unes cartes de la seva ma
        /// </summary>
        /// <param name="jugador"></param>
        /// <param name="contingut"></param>
        /// <param name="maximCartes"></param>
        /// <returns></returns>
        public CartaDominion[] TriaCartes(Jugador jugador, string contingut, int minimCartes, int maximCartes)
        {
            return TriaCartes(jugador, contingut, minimCartes, maximCartes, jugador.Ma);
        }

        public CartaDominion[] TriaCartes(Jugador jugador, string continugt, int minimCartes, int maximCartes, IList<CartaDominion> cartes)
        {
            //demana al jugador que que trii entre les cartes
            throw new NotImplementedException();
        }
        public void RevelaCartesMazo(Jugador jugador, int numCartes)
        {
            //enseña a tots el jugadors les cartes que surten del mazo del jugador
            jugador.EnseñaCartesMazo(numCartes);
        }

    }
}