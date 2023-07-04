using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cine
{
    using System;

    internal class QuickSortAlgorithm
    {
        internal void Sort<T>(List<T> list, IComparer<T> comparer)
        {
            Sort(list, comparer, 0, list.Count - 1);
        }

        internal void Sort<T>(List<T> list, IComparer<T> comparer, int left, int right)
        {
            if (left < right)
            {
                int pivotIndex = Partition(list, comparer, left, right);
                Sort(list, comparer, left, pivotIndex - 1);
                Sort(list, comparer, pivotIndex + 1, right);
            }
        }
        internal int Partition<T>(List<T> list, IComparer<T> comparer, int left, int right)
        {
            T pivot = list[right];
            int i = left - 1;

            for (int j = left; j <= right - 1; j++)
            {
                if (comparer.Compare(list[j], pivot) <= 0)
                {
                    i++;
                    Swap(list, i, j);
                }
            }

            Swap(list, i + 1, right);
            return i + 1;
        }
        internal void Swap<T>(List<T> list, int i, int j)
        {
            T temp = list[i];
            list[i] = list[j];
            list[j] = temp;
        }

    }
}
