using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Text;
using Gabriel.Cat.S.Utilitats;
namespace Dominion.Core
{
    public delegate void CartaDominionEventHanlder(CartaDominion carta);
    public abstract class CartaDominion:IComparable<CartaDominion>
    {
        public static readonly string[] Cartes;
        public static readonly LlistaOrdenada<string, ResourceImage> DicImatges;
        static ResourceImage imgRevers;



        string nomCarta;
        string nomCartaFinMazo;
        private bool finMazo;
        private bool seleccionada;
        private bool caraAmunt;

        public event CartaDominionEventHanlder UpdateImg;
        public event CartaDominionEventHanlder CanviSeleccio;

        static CartaDominion()
        {
            Type tipoArray = typeof(byte[]);
           
            Type tipoResources = typeof(Dominion.Core.Properties.Resources);
            PropertyInfo[] imgsRecursos = tipoResources.GetProperties();
            string nomRevers = nameof(Dominion.Core.Properties.Resources.Revers);
            Cartes = new string[imgsRecursos.Length-2];
            DicImatges = new LlistaOrdenada<string, ResourceImage>();
            //si esta ordenado empezaré por 2 asi quito el primer if :)
            for (int i = 0,j=0; i < imgsRecursos.Length; i++)
                if (imgsRecursos[i].PropertyType.Equals(tipoArray)) {
                    DicImatges.Add(imgsRecursos[i].Name, new ResourceImage(tipoResources, imgsRecursos[i].Name));
                    if (!imgsRecursos[i].Name.Contains("Final"))
                        Cartes[j++] = imgsRecursos[i].Name;
                }
            imgRevers = DicImatges[nomRevers];

        }

        public string NomCarta
        {

            get
            {
                string[] camps;

                if (nomCarta == null)
                {
                    camps = ToString().Split('.');
                    nomCarta = camps[camps.Length - 1];
                }
                return nomCarta;
            }
        }
        public string NomCartaFinMazo
        {

            get
            {
                const string FINMAZO = "Final";
                if (nomCartaFinMazo == null)
                    nomCartaFinMazo = NomCarta + FINMAZO;
                return nomCartaFinMazo;
            }
        }
        public bool CaraAmunt { get => caraAmunt; set { caraAmunt = value; Refresh(); } }

        private void Refresh()
        {
            if (UpdateImg != null)
                UpdateImg(this);
        }

        public bool Seleccionada
        {
            get => seleccionada;
            set
            {
                bool aux = seleccionada;
                seleccionada = value;
                if (value != aux && CanviSeleccio != null)
                    CanviSeleccio(this);
            }
        }
        public bool FinMazo { get => finMazo; set { finMazo = value; Refresh(); } }
        public ResourceImage ImgRevers => imgRevers;
        public ResourceImage ImgAnvers => DicImatges[NomCarta];
        public ResourceImage ImgFinMazo => DicImatges[NomCartaFinMazo];
        public Bitmap ImagenActual
        {
            get
            {
                Bitmap img;
                if (!FinMazo)
                {
                    if (CaraAmunt)
                        img = ImgAnvers.Image;
                    else img = ImgRevers.Image;
                }
                else img = ImgFinMazo.Image;

                return img;
            }
        }

        public virtual bool EsCartaDeTresor { get => false;  }
        public virtual bool EsCartaDeVictoria { get => false;  }
        public virtual bool EsCartaDeAccio { get => false;  }
        public int Cost { get; protected set; }
        public int Valor { get; protected set; }

        public void Dispose()
        {
            ImgAnvers.Dispose();
            ImgRevers.Dispose();
            ImgFinMazo.Dispose();
        }
        public int CompareTo(CartaDominion other)
        {
            //victoria,accio,tresor
            //de mes a menys punts de cost
            int valor;
            if (other.EsCartaDeVictoria)
            {
                if (EsCartaDeVictoria)
                    valor = Cost - other.Cost;
                else
                    valor = -1;

            }
            else if (other.EsCartaDeAccio)
            {
                if (EsCartaDeVictoria)
                    valor = 1;
                else if (EsCartaDeAccio)
                    valor = Cost - other.Cost;
                else
                    valor = -1;
            }
            else
            {
                if (EsCartaDeVictoria)
                    valor = 1;
                else if (EsCartaDeAccio)
                    valor = -1;
                else
                    valor = Cost - other.Cost;
            }
            return valor;

        }

        public static CartaDominion DonamCarta(string nomCarta)
        {
            Type tipus;
            CartaDominion carta;
            string tipusCarta = "Dominion.Core";
            switch (nomCarta)
            {
                case "Aldea":
                case "Aventurer":
                case "Banquet":
                case "Biblioteca":
                case "Bruixa":
                case "Burocrata":
                case "Canceller":
                case "Capella":
                case "Enviat":
                case "Espia":
                case "Ferreria":
                case "Festival":
                case "Fossat":
                case "Laboratori":
                case "Lladre":
                case "Llenyataires":
                case "Mercat":
                case "Milicia":
                case "Mina":
                case "Prestador":
                case "Remodelar":
                case "SalaDelConsell":
                case "SalaDelTron":
                case "Soterrani":
                case "Taller":
                    tipusCarta += ".CartaAccio." + nomCarta ; break;
                case "Coure":
                case "Or":
                case "Plata":
                    tipusCarta += ".CartaTresor." + nomCarta; break;
                case "Ducat":
                case "Finca":
                case "Jardins":
                case "Malediccio":
                case "Provincia":
                    tipusCarta += ".CartaVictoria." + nomCarta ; break;
            }


             tipus = Type.GetType(tipusCarta);
             carta = Activator.CreateInstance(tipus) as CartaDominion;

            return carta;
        }
    }
}
