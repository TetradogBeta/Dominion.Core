using Gabriel.Cat.S.Utilitats;
using System;
using System.Collections.Generic;
using System.Linq;
using Gabriel.Cat.S.Extension;
namespace Dominion.Core
{
    public abstract class Partida
    {
        static readonly int[] QuatreJugadors = new int[] { 0, 1, 2, 3 };
        static readonly int[] TresJugadors = new int[] { 0, 1, 2 };

        List<int> guanyadorsPartidaAnterior;
        int jugadorActual;
        LlistaOrdenada<string, int> subministraments;

        public int DinersExtra { get; set; }
        public Llista<CartaDominion> Eliminades { get; private set; }
        public Llista<CartaDominion> Jugada { get; private set; }
        public Jugador[] Jugadors { get; private set; }

        public Partida(string[] jugadors, string[] tipusCartesSubmnistraments)
        {
            if (jugadors.Length < 2 || jugadors.Length > 4)
                throw new ArgumentOutOfRangeException("jugador");
            if (tipusCartesSubmnistraments.Length != 10)
                throw new ArgumentException("Han d'haver 10 tipus de subministraments");

            Jugadors = new Jugador[jugadors.Length];
            for (int i = 0; i < jugadors.Length; i++)
                Jugadors[i] = new Jugador(i, jugadors[i]);
            Eliminades = new Llista<CartaDominion>();
            Jugada = new Llista<CartaDominion>();
            subministraments = new LlistaOrdenada<string, int>();
            for (int i = 0; i < tipusCartesSubmnistraments.Length; i++)
            {
                subministraments.Add(tipusCartesSubmnistraments[i], 0);
            }

            subministraments.Add(nameof(Malediccio), 0);
            subministraments.Add(nameof(Ducat), 0);
            subministraments.Add(nameof(Provincia), 0);
            subministraments.Add(nameof(Finca), 0);

            subministraments.Add(nameof(Or), 0);
            subministraments.Add(nameof(Plata), 0);
            subministraments.Add(nameof(Coure), 0);
            guanyadorsPartidaAnterior = new List<int>();
        }
        /// <summary>
        /// Es el jugador que reacciona actualment al atac
        /// </summary>
        public Jugador JugadorReactiu { get; private set; }
        public Jugador JugadorActual
        {
            get
            {
                return Jugadors[jugadorActual];
            }
        }
        public Jugador JugadorEsquerra
        {
            get
            {
                int posJugadorEsquerra = jugadorActual - 1;
                if (posJugadorEsquerra < 0)
                    posJugadorEsquerra += Jugadors.Length;
                return Jugadors[posJugadorEsquerra];
            }
        }
        public Jugador JugadorDreta
        {
            get
            {
                int posJugadorDreta = jugadorActual + 1;
                if (posJugadorDreta == Jugadors.Length)
                    posJugadorDreta = 0;

                return Jugadors[posJugadorDreta];
            }
        }
        /// <summary>
        /// Prepara la partida per començar
        /// </summary>
        public void PartidaNova()
        {
            int[] aux=null;
            for (int i = 0; i < subministraments.Count; i++)
                if (subministraments.GetKey(i) != nameof(Jardins))
                    subministraments.SetValueAt(i, 10);
                else
                    subministraments[nameof(Jardins)] = Jugadors.Length > 2 ? 12 : 8;

            subministraments[nameof(Malediccio)] = Jugadors.Length == 2 ? 10 : Jugadors.Length == 3 ? 20 : 30;
            subministraments[nameof(Provincia)] = Jugadors.Length > 2 ? 12 : 8;
            subministraments[nameof(Ducat)] = Jugadors.Length > 2 ? 12 : 8;
            subministraments[nameof(Finca)] = Jugadors.Length > 2 ? 12 : 8;

            subministraments[nameof(Or)] = 30;
            subministraments[nameof(Plata)] = 40;
            subministraments[nameof(Coure)] = 60;

            for (int i = 0; i < Jugadors.Length; i++)
            {
                Jugadors[i].Reset();
                jugadorActual = i;
                for (int j = 0; j < 7; j++)
                    AgafaCarta(typeof(Coure));
                for (int j = 0; j < 3; j++)
                    AgafaCarta(typeof(Finca));

            }
            Eliminades.Clear();
            Jugada.Clear();
            DinersExtra = 0;
            if (guanyadorsPartidaAnterior.Count == 0 || guanyadorsPartidaAnterior.Count == Jugadors.Length)
            {
                jugadorActual = MiRandom.Next(Jugadors.Length);
            }
            else if (guanyadorsPartidaAnterior.Count == 1)
            {

                jugadorActual = guanyadorsPartidaAnterior[0];
                AvançaJugador();
            }
            else if (guanyadorsPartidaAnterior.Count == 3 && Jugadors.Length == 4)
            {
                jugadorActual = QuatreJugadors.Except(guanyadorsPartidaAnterior).First();
            }
            else
            {
                if (Jugadors.Length == 4)
                    aux = QuatreJugadors;
                else if (Jugadors.Length == 3)
                    aux = TresJugadors;

                aux = aux.Except(guanyadorsPartidaAnterior).ToArray();
                aux.Desordena();
                jugadorActual = aux[0];
            }

        }

        private void FiPartida()
        {
            int[] puntuacions = new int[Jugadors.Length];
            int puntuacioJugadorActual;
            int puntuacioMax=0;

            guanyadorsPartidaAnterior.Clear();
            for (int i = 0; i < Jugadors.Length; i++)
            {
                puntuacioJugadorActual = Jugadors[i].PuntuacioFinal();
                puntuacions[i] = puntuacioJugadorActual;
                if (puntuacioJugadorActual > puntuacioMax)
                    puntuacioMax = puntuacioJugadorActual;
                PreguntaAlJugador(Jugadors[i], string.Format("Tens un total de {0}", puntuacioJugadorActual), "OK");
            }
            for(int i=0;i<Jugadors.Length;i++)
            {
                if (puntuacions[i] == puntuacioMax)
                {
                    PreguntaAlJugador(Jugadors[i], "Has Guanyat el joc!", "OK");//hay que mirar los turnos jugados...el que tenga menos gana sino comparten la victoria
                    guanyadorsPartidaAnterior.Add(i);
                }
                else
                {
                    PreguntaAlJugador(Jugadors[i], "Has Perdut!", "OK");
                }
            }
        }
        public void AvançaJugador()
        {
            jugadorActual = jugadorActual + 1 % Jugadors.Length;
        }
        public void EliminaCarta(IList<CartaDominion> cartes)
        {
            for (int i = 0; i < cartes.Count; i++)
                EliminaCarta(cartes[i]);
        }
        public void EliminaCarta(CartaDominion carta)
        {
            //posa la carta al pilo de eliminades
            Eliminades.Push(carta);
        }
        public void GuanyaCarta(int costMaxim, bool vaADescartadas = true, Type tipusCarta = null)
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
                    if (tipusCarta == null || tipusCarta.IsAssignableFrom(carta.GetType()))
                        if (carta.Cost <= costMaxim)
                            cartesQuePotAgafar.Add(carta);
                }
            }
            carta = TriaCartes(JugadorActual, "Has guanyat una carta tria una", 1, 1, cartesQuePotAgafar)[0];
            subministraments[carta.NomCarta]--;
            if (vaADescartadas)
                JugadorActual.Descartades.Push(carta);
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
        //pregunta al jugador i retorna la posició en la array d'opcions triada
        public abstract int PreguntaAlJugador(Jugador jugador, string contingut, params string[] opcions);
        
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
        //demana al jugador que que trii entre les cartes
        public abstract CartaDominion[] TriaCartes(Jugador jugador, string continugt, int minimCartes, int maximCartes, IList<CartaDominion> cartes);
        
        public void RevelaCartesMazo(Jugador jugador, int numCartes)
        {
            //enseña a tots el jugadors les cartes que surten del mazo del jugador
            jugador.EnseñaCartesMazo(numCartes);
        }

    }
}