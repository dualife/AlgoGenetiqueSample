// <copyright file="Festival.cs" company="GosselCorp" author="gossel_c">
// Demandez-moi d'abord.
// </copyright>

namespace TP_algo_génétique.Classes
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.Serialization;
    using System.Text;
    using System.Threading.Tasks;

    [DataContract]
    public class Festival
    {
        public Festival(string nom, int interet, DateTime debut, DateTime fin)
        {
            this.Nom = nom;
            this.Interet = interet;
            this.Debut = debut;
            this.Fin = fin;
        }

        [DataMember]
        public string Nom { get; private set; }

        // non utilisé
        [DataMember]
        public DateTime Debut { get; private set; }

        // non utilisé
        [DataMember]
        public DateTime Fin { get; private set; }

        [DataMember]
        public int Interet { get; private set; }
    }
}
