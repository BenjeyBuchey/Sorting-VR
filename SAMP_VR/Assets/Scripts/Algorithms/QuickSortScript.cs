using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class QuickSortScript : Algorithm {

	public QuickSortScript() : base (Algorithm.QUICKSORT)
	{

	}

	public List<SortingVisualItem> StartSort(GameObject[] array, int left, int right)
	{
		visualItems.Clear();
		elementArray = array;
		myQuickSort(left, right);

		return visualItems;
	}

	private void myQuickSort(int left, int right)
	{
		if (elementArray == null || elementArray.Length <= 1)
			return;
		
		if (left < right)
		{
			int pivot_id = myPartition(left,right);
			myQuickSort(left,pivot_id-1);
			myQuickSort(pivot_id, right);
		}
	}
	
	private int myPartition(int left, int right)
	{
		int start = left;
		GameObject pivot = elementArray [start];
		left++;
		right--;
		
		while (true) 
		{
			while (left <= right)
			{
				visualItems.Add(new SortingVisualItem((int)SortingVisualType.Comparison, elementArray[left], pivot));
				if (GetElementSize(elementArray[left]) <= GetElementSize(pivot))
					left++;
				else
					break;
			}

			//while (left <= right && elementArray[left].GetComponentInChildren<Rigidbody>().transform.localScale.x <= pivot.GetComponentInChildren<Rigidbody>().transform.localScale.x)
			//{
			//	visualItems.Add(new SortingVisualItem((int)SortingVisualType.Comparison, elementArray[left], pivot));
			//	left++;
			//}

			while (left <= right)
			{
				visualItems.Add(new SortingVisualItem((int)SortingVisualType.Comparison, elementArray[right], pivot));
				if (GetElementSize(elementArray[right]) > GetElementSize(pivot))
					right--;
				else
					break;
			}

			//while (left <= right && elementArray[right].GetComponentInChildren<Rigidbody>().transform.localScale.x > pivot.GetComponentInChildren<Rigidbody>().transform.localScale.x)
			//{
			//	visualItems.Add(new SortingVisualItem((int)SortingVisualType.Comparison, elementArray[right], pivot));
			//	right--;
			//}

			if (left > right)
			{
				Swap (start,left-1);
				return left;
			}
			
			Swap (left,right);
		}
	}
}
