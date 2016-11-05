namespace ABClient.AppControls
{
    using System;
    using System.Windows.Forms;

    public class ListBoxEx : ListBox
    {
        protected override void Sort()
        {
            QuickSort(0, Items.Count - 1);
        }

        public void ManualSort()
        {
            Sort();
        }

        private void QuickSort(int left, int right)
        {
            if (right <= left) return;
            var pivotIndex = left;
            var pivotNewIndex = QuickSortPartition(left, right, pivotIndex);

            QuickSort(left, pivotNewIndex - 1);
            QuickSort(pivotNewIndex + 1, right);
        }

        private int QuickSortPartition(int left, int right, int pivot)
        {
            var pivotValue = (IComparable)Items[pivot];
            Swap(pivot, right);

            var storeIndex = left;
            for (var i = left; i < right; ++i)
            {
                if (pivotValue.CompareTo(Items[i]) < 0) continue;
                Swap(i, storeIndex);
                ++storeIndex;
            }

            Swap(storeIndex, right);
            return storeIndex;
        }

        private void Swap(int left, int right)
        {
            var temp = Items[left];
            Items[left] = Items[right];
            Items[right] = temp;
        }
    }
}