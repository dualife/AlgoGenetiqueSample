// <copyright file="Ville.cs" company="GosselCorp" author="gossel_c">
// Demandez-moi d'abord.
// </copyright>

namespace TP_algo_génétique.Classes
{
    using System;
    using System.Collections.Generic;
    using System.Device.Location;
    using System.Linq;
    using System.Runtime.Serialization;
    using System.Text;
    using System.Threading.Tasks;

    [DataContract]
    public class Ville
    {
        public Ville(string nom, int interet, double latitude, double longitude, Festival festival = default(Festival))
        {
            this.Nom = nom;
            this.Interet = interet;
            this.Localisation = new GeoCoordinate(latitude, longitude);
            this.Festival = festival;
        }

        [DataMember]
        public GeoCoordinate Localisation { get; private set; }

        [DataMember]
        public string Nom { get; private set; }

        [DataMember]
        public Festival Festival { get; private set; }

        [DataMember]
        public int Interet { get; private set; }

        public int CalculeInteret()
        {
            return this.Festival == null ? this.Interet : this.Interet + Festival.Interet;
        }

        public double CalculeDistance(GeoCoordinate oth)
        {
            return this.Localisation.GetDistanceTo(oth);
        }

        public override string ToString()
        {
            if (this.Festival != null)
            {
                return string.Format(
                    "{0} avec le festival {1}",
                    this.Nom,
                    this.Festival.Nom);
            }

            return string.Format("{0} sans festival", this.Nom);
        }
    }
}
