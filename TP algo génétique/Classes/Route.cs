// <copyright file="Route.cs" company="GosselCorp" author="gossel_c">
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
    using AlgoGenetiqueLibrairie;
    using Solution = System.Collections.Generic.List<System.Tuple<Classes.Ville, System.TimeSpan>>;

    [DataContract]
    public class Route : ISolutionGenetique<Solution>
    {
        public static readonly GeoCoordinate PointDeDepart = new GeoCoordinate(47.2382007d, -1.6310052d); // Nantes

        public Route()
        {
            this.Etapes = new Solution();
        }

        [DataMember]
        public List<Tuple<Ville, TimeSpan>> Etapes { get; set; }

        public int EvalueEfficacite()
        {
            var efficacité = 0;
            var distanceParcourue = 0d;
            GeoCoordinate précédentePosition = PointDeDepart;

            foreach (var etape in this.Etapes)
            {
                var villeEtape = etape.Item1;
                var duréeEtape = etape.Item2;

                efficacité += villeEtape.CalculeInteret() * duréeEtape.Days;
                distanceParcourue += villeEtape.CalculeDistance(précédentePosition);
            }

            var millierDeKM = distanceParcourue / 1000000d;
            var efficacitéPonderée = efficacité / (millierDeKM < 1 ? 1 : millierDeKM);

            return Convert.ToInt32(efficacitéPonderée);
        }

        public List<ISolutionGenetique<Solution>> GenereNouvellesSolutions(List<ISolutionGenetique<Solution>> solutions)
        {
            var solutiontriées = solutions.OrderByDescending(a => a.EvalueEfficacite());

            // prend le 1er tier des meilleures solution
            var elite = solutiontriées.Take(solutiontriées.Count() / 3);

            var nouvellesSolutions = new List<ISolutionGenetique<Solution>>();

            int i = 0;
            foreach (var element in elite)
            {
                var cur = element as Route;

                // recupere l'element suivant ou le premier si hors limite
                var next = (i > elite.Count() ? elite.First() : elite.ElementAt(i)) as Route;

                // crée une nouvelle route a partir des 2 autres
                nouvellesSolutions.Add(PathProvider.MergeRoutes(cur, next));

                // conserve la solution elite
                nouvellesSolutions.Add(cur);
                i++;
            }

            // ajoute des solutions aleatoires
            var randoms = PathProvider.GenereRoutes(solutions.Count() - nouvellesSolutions.Count());
            nouvellesSolutions.AddRange(randoms);

            return nouvellesSolutions;
        }

        public bool EstSatisfesante(ISolutionGenetique<Solution> sol)
        {
            return sol.EvalueEfficacite() > 50000;
        }

        public void AjouterEtape(Ville ville, TimeSpan durée)
        {
            this.Etapes.Add(new Tuple<Ville, TimeSpan>(ville, durée));
        }

        public void AjouterEtape(Tuple<Ville, TimeSpan> etape)
        {
            this.Etapes.Add(etape);
        }

        public override string ToString()
        {
            var txt = new StringBuilder(string.Format("interet: {0}, path: " + Environment.NewLine, this.EvalueEfficacite()));

            foreach (var etape in this.Etapes)
            {
                txt.AppendLine(string.Format("Arret pendant {0} jours à {1}", etape.Item2.Days, etape.Item1));
            }

            return txt.ToString();
        }

        private int CalculMoyenneInteret(List<ISolutionGenetique<Solution>> solutions)
        {
            var res = 0;
            foreach (var element in solutions)
            {
                res += element.EvalueEfficacite();
            }

            res /= solutions.Count();
            return res;
        }
    }
}
