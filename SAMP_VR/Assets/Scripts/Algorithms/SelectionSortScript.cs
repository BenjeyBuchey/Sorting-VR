using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionSortScript : Algorithm
{
	public SelectionSortScript() : base(Algorithm.SELECTIONSORT)
	{

	}

	public List<SortingVisualItem> StartSort(GameObject[] array)
	{
		visualItems.Clear();
		elementArray = array;
		SelectionSort();

		return visualItems;
	}

	void SelectionSort()
	{
		for (int i = 0; i < elementArray.Length - 1; i++)
		{
			// Find the min element in the unsorted array 
			int min_idx = i;
			for (int j = i + 1; j < elementArray.Length; j++)
			{
				visualItems.Add(new SortingVisualItem((int)SortingVisualType.Comparison, elementArray[j], elementArray[min_idx]));
				if (GetElementSize(elementArray[j]) < GetElementSize(elementArray[min_idx]))
					min_idx = j;
			}
			// Swap the found min element with the first element
			Swap(min_idx, i);
		}
	}
}
