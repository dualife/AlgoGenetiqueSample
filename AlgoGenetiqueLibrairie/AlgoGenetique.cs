// <copyright file="AlgoGenetique.cs" company="GosselCorp" author="gossel_c">
// Demandez-moi d'abord.
// </copyright>

namespace AlgoGenetiqueLibrairie
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class AlgoGenetique<T>
    {
        public AlgoGenetique(bool debug = false)
        {
            this.DebugEnabled = debug;
            this.IndividuList = new List<ISolutionGenetique<T>>();
        }

        public bool DebugEnabled { get; private set; }

        public List<ISolutionGenetique<T>> IndividuList { get; set; }

        public void AddIndividu(ISolutionGenetique<T> individu)
        {
            this.IndividuList.Add(individu);
        }

        public void AddIndividus(List<ISolutionGenetique<T>> individuList)
        {
            this.IndividuList.AddRange(individuList);
        }

        public object Evolution(int maxGeneration = 600)
        {
            var champion = default(ISolutionGenetique<T>);
            int generation = 0;

            do
            {
                this.IndividuList = this.IndividuList.First().GenereNouvellesSolutions(this.IndividuList);

                champion = this.IndividuList.OrderByDescending(individu => individu.EvalueEfficacite()).First();
                generation++;

                if (this.DebugEnabled)
                {
                    Console.WriteLine(
                        "AlgoGénétique: génération {0}, resultat: {1}",
                        generation,
                        champion.EvalueEfficacite());
                }
            }
            while (!this.IndividuList.First().EstSatisfesante(champion) && generation < maxGeneration);

            return champion;
        }
    }
}