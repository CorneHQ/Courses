using System;
using System.Threading;

namespace Exercise
{
    public class SequentialMergeSort
    {
        int[] input;

        public SequentialMergeSort(int[] data)
        {
            input = new int[data.Length];
            Array.Copy(data, input, data.Length);
        }

        public void printContent(String msg)
        {
            Console.WriteLine(msg+"Content of the array is:");
            for (int i = 0; i < input.Length; i++)
                Console.Write("data[{0}]={1} ;",i,input[i]);
        }
 
        public void sortSeq(int left, int right)
        {
            if (left < right)
            {
                int middle = (left + right) / 2;

                sortSeq(left, middle);
                sortSeq(middle + 1, right);

                merge(left, middle, right);
            }
        }

        /** merges two subarrays: left-middle and middle-right.
         */
        public void merge(int left, int middle, int right)
        {
            int[] leftArray = new int[middle - left + 1];
            int[] rightArray = new int[right - middle];

            Array.Copy(input, left, leftArray, 0, middle - left + 1);
            Array.Copy(input, middle + 1, rightArray, 0, right - middle);

            // in the merge process apply right order of the elements
            int i = 0;
            int j = 0;
            for (int k = left; k < right + 1; k++)
            {
                if (i == leftArray.Length)
                {
                    input[k] = rightArray[j];
                    j++;
                }
                else if (j == rightArray.Length)
                {
                    input[k] = leftArray[i];
                    i++;
                }
                else if (leftArray[i] <= rightArray[j])
                {
                    input[k] = leftArray[i];
                    i++;
                }
                else
                {
                    input[k] = rightArray[j];
                    j++;
                }
            }
        }

    }
    public class ConcurrentMergeSort
    {
        // Implements concurrent version of MergeSort.
        public virtual void sortCon(int[] d)
        {
            SequentialMergeSort sort = new SequentialMergeSort(d);

            int arrayMidPosition = d.Length / 2;

            Thread leftSideT = new Thread(() => sort.sortSeq(0, arrayMidPosition));
            Thread rightSideT = new Thread(() => sort.sortSeq(arrayMidPosition+1, d.Length-1));

            leftSideT.Start();
            rightSideT.Start();

            leftSideT.Join();
            rightSideT.Join();

            sort.merge(0, arrayMidPosition, d.Length-1);
            sort.printContent("Concurrent sort results");
        }
    }
}
