// <copyright file="PathProvider.cs" company="GosselCorp" author="gossel_c">
// Demandez-moi d'abord.
// </copyright>

namespace TP_algo_génétique.Classes
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using AlgoGenetiqueLibrairie;
    using Solution = System.Collections.Generic.List<System.Tuple<Classes.Ville, System.TimeSpan>>;

    // factory de Routes
    public static class PathProvider
    {
        public static readonly Festival[] Festivals =
{
            new Festival("Hellfest", 100, new DateTime(2016, 06, 17), new DateTime(2016, 06, 19)),
            new Festival("Coachella", 30, new DateTime(2016, 04, 15), new DateTime(2016, 04, 24)),
            new Festival("Pinkpop", 60, new DateTime(2016, 06, 10), new DateTime(2016, 06, 12)),
            new Festival("INmusic", 40, new DateTime(2016, 06, 20), new DateTime(2016, 06, 22)),
            new Festival("Tomorrowland", 70, new DateTime(2016, 07, 22), new DateTime(2016, 07, 24)),
        };

        public static readonly Ville[] Villes =
        {
            new Ville("Clisson", 15, 47.0936171d, -1.3003978d, Festivals[0]),
            new Ville("Indio", 3, 33.7388487d, -116.3002846d, Festivals[1]),
            new Ville("Landgraaf", 7, 50.902539d, 5.9724243d, Festivals[2]),
            new Ville("Jarun", 3, 45.7853941d, 15.9001215d, Festivals[3]),
            new Ville("Itu", 10, -23.2964131d, -47.4165817d, Festivals[4]),
            new Ville("Tokyo", 100, 35.6730185d, 139.4302008d),
            new Ville("New York", 80, 40.7053111d, -74.2581814d),
            new Ville("Montreal", 70, 45.5597832d, -73.9917053d),
        };

        private static readonly Random Rnd = new Random();

        public static Route MergeRoutes(Route cur, Route next)
        {
            var mergedRoute = new Route();

            // parcours les etapes du plus long chemin
            var longest = cur.Etapes.Count() < next.Etapes.Count() ? next : cur;
            for (int i = 0; i < longest.Etapes.Count(); i++)
            {
                var elem1 = cur.Etapes.ElementAtOrDefault(i);
                var elem2 = next.Etapes.ElementAtOrDefault(i);

                var interet1 = elem1 == null ? 0 : elem1.Item1.Interet * elem1.Item2.Days;
                var interet2 = elem2 == null ? 0 : elem2.Item1.Interet * elem2.Item2.Days;

                // ajoute l'etape la plus interessante des 2 chemins
                mergedRoute.AjouterEtape(interet1 > interet2 ? elem1 : elem2);
            }

            return mergedRoute;
        }

        public static List<Route> GenereRoutes(int quantité)
        {
            var routes = new List<Route>();

            while (quantité > 0)
            {
                routes.Add(PathProvider.GenereRoute());
                quantité--;
            }

            return routes;
        }

        private static Route GenereRoute()
        {
            var route = new Route();
            int daysCount = 365;

            // ajoute un nombre d'étapes entre 1 et 20
            for (int steps = 0; steps < Rnd.Next(1, 20); steps++)
            {
                // limite la durée du voyage à 365 jours
                if (daysCount == 0)
                {
                    break;
                }

                var durée = Rnd.Next(1, daysCount);
                daysCount -= durée;

                // choisi une etape et une durée random
                route.AjouterEtape(
                    Villes[Rnd.Next(0, Villes.Length)],
                    new TimeSpan(durée, 0, 0, 0));
            }

            return route;
        }
    }
}
