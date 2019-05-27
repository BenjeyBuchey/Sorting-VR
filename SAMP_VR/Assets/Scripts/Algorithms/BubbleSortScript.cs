using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleSortScript : Algorithm
{
	public BubbleSortScript() : base(Algorithm.BUBBLESORT)
	{

	}

	public List<SortingVisualItem> StartSort(GameObject[] array)
	{
		visualItems.Clear();
		elementArray = array;
		BubbleSort();

		return visualItems;
	}

	void BubbleSort()
	{
		int i, j;
		for (i = 0; i < elementArray.Length - 1; i++)
		{
			for (j = 0; j < elementArray.Length - i - 1; j++)
			{
				visualItems.Add(new SortingVisualItem((int)SortingVisualType.Comparison, elementArray[j], elementArray[j+1]));
				if (GetElementSize(elementArray[j]) > GetElementSize(elementArray[j + 1]))
					Swap(j, j + 1);
			}
		}
	}
}
