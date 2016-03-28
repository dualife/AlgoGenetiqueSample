// <copyright file="Program.cs" company="GosselCorp" author="gossel_c">
// Demandez-moi d'abord.
// </copyright>

namespace TP_algo_génétique
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using AlgoGenetiqueLibrairie;
    using Classes;
    using Solution = System.Collections.Generic.List<System.Tuple<Classes.Ville, System.TimeSpan>>;

    public class Program
    {
        public static void Main(string[] args)
        {
            var algoGenetique = new AlgoGenetique<Solution>(debug: true);

            List<ISolutionGenetique<Solution>> solutionListe = new List<ISolutionGenetique<Solution>>();
            solutionListe.AddRange(PathProvider.GenereRoutes(30));

            algoGenetique.AddIndividus(solutionListe);

            var resultat = algoGenetique.Evolution();

            Console.WriteLine("programme: resultat: {0}", resultat);
            Console.ReadKey();
        }
    }
}
