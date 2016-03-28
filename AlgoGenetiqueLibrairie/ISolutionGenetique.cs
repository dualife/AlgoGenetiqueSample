// <copyright file="ISolutionGenetique.cs" company="GosselCorp" author="gossel_c">
// Demandez-moi d'abord.
// </copyright>

namespace AlgoGenetiqueLibrairie
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// interface d'inversion de dépendance pour l'utiliser dans l'algo genetique
    /// </summary>
    /// <typeparam name="T">type interne de representation de la solution</typeparam>
    public interface ISolutionGenetique<T>
    {
        List<ISolutionGenetique<T>> GenereNouvellesSolutions(List<ISolutionGenetique<T>> individuList);

        int EvalueEfficacite();

        bool EstSatisfesante(ISolutionGenetique<T> sol);
    }
}
