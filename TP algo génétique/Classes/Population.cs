// <copyright file="Population.cs" company="GosselCorp" author="gossel_c">
// Demandez-moi d'abord.
// </copyright>

namespace TP_algo_génétique.Classes
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class Population : IList<Individu>
    {
        public List<Individu> PopulationList { get; set; }

        #region Implémentation de propriétés de IList

        public int Count
        {
            get
            {
                return ((IList<Individu>)this.PopulationList).Count;
            }
        }

        public bool IsReadOnly
        {
            get
            {
                return ((IList<Individu>)this.PopulationList).IsReadOnly;
            }
        }

        public Individu this[int index]
        {
            get
            {
                return ((IList<Individu>)this.PopulationList)[index];
            }

            set
            {
                ((IList<Individu>)this.PopulationList)[index] = value;
            }
        }

        #endregion

        public void RePeuple()
        {
            // recupére les 2/3 meilleures valeures de la population
            var elite = this.PopulationList.OrderBy(individu => individu.Valeur).
                Take(this.PopulationList.Count / (2 / 3)).ToList();

            // fusionne le 1er et le dernier elite en un nouvel individu
            var lenght = elite.Count();
            var half = lenght / 2;
            this.PopulationList = new List<Individu>();

            for (int i = 0; i < half; i++)
            {
                // TODO implementer individu
                PopulationList.Add(new Individu(elite[i], elite[lenght - i]));
            }
        }

        public Individu EvalueChampion(AlgoGenetique.EvaluateurFunc evalue)
        {
            foreach (var individu in this.PopulationList)
            {
                individu.Valeur = evalue(individu);
            }
            return this.PopulationList.Max();
        }

        #region Implémentation de méthodes de IList

        public void Add(Individu item)
        {
            ((IList<Individu>)this.PopulationList).Add(item);
        }

        public void Clear()
        {
            ((IList<Individu>)this.PopulationList).Clear();
        }

        public bool Contains(Individu item)
        {
            return ((IList<Individu>)this.PopulationList).Contains(item);
        }

        public void CopyTo(Individu[] array, int arrayIndex)
        {
            ((IList<Individu>)this.PopulationList).CopyTo(array, arrayIndex);
        }

        public IEnumerator<Individu> GetEnumerator()
        {
            return ((IList<Individu>)this.PopulationList).GetEnumerator();
        }

        public int IndexOf(Individu item)
        {
            return ((IList<Individu>)this.PopulationList).IndexOf(item);
        }

        public void Insert(int index, Individu item)
        {
            ((IList<Individu>)this.PopulationList).Insert(index, item);
        }

        public bool Remove(Individu item)
        {
            return ((IList<Individu>)this.PopulationList).Remove(item);
        }

        public void RemoveAt(int index)
        {
            ((IList<Individu>)this.PopulationList).RemoveAt(index);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IList<Individu>)this.PopulationList).GetEnumerator();
        }
        #endregion
    }
}