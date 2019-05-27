using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InsertionSortScript : Algorithm
{
	private List<Vector3> indexPositions = new List<Vector3>();

	public InsertionSortScript() : base(Algorithm.INSERTIONSORT)
	{

	}

	public List<SortingVisualItem> StartSort(GameObject[] array)
	{
		visualItems.Clear();
		elementArray = array;
		InitIndexPositions();
		InsertionSort();

		return visualItems;
	}

	private void InitIndexPositions()
	{
		foreach(GameObject element in elementArray)
		{
			indexPositions.Add(element.transform.localPosition);
		}
	}

	void InsertionSort()
	{
		for (int i = 1; i < elementArray.Length; ++i)
		{
			GameObject key = elementArray[i];
			visualItems.Add(new SortingVisualItem((int)SortingVisualType.MoveMemory, key, null, dest: GetMoveUpwardsPosition(key)));
			int j = i - 1;

			while (j >= 0 && Compare(elementArray[j], key) && GetElementSize(elementArray[j]) > GetElementSize(key))
			{
				visualItems.Add(new SortingVisualItem((int)SortingVisualType.MoveTo, elementArray[j], null, dest: indexPositions[j+1]));
				elementArray[j + 1] = elementArray[j];
				j = j - 1;
			}
			visualItems.Add(new SortingVisualItem((int)SortingVisualType.MoveTo, key, null, dest: indexPositions[j + 1]));
			elementArray[j + 1] = key;
		}
	}
}
