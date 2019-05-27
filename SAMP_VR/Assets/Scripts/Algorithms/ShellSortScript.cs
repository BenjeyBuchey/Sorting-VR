using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShellSortScript : Algorithm
{
	private List<Vector3> indexPositions = new List<Vector3>();

	public ShellSortScript() : base(Algorithm.SHELLSORT)
	{

	}

	public List<SortingVisualItem> StartSort(GameObject[] array)
	{
		visualItems.Clear();
		elementArray = array;
		InitIndexPositions();
		ShellSort();

		return visualItems;
	}

	private void InitIndexPositions()
	{
		foreach (GameObject element in elementArray)
		{
			indexPositions.Add(element.transform.localPosition);
		}
	}

	void ShellSort()
	{
		for (int gap = elementArray.Length / 2; gap > 0; gap /= 2)
		{
			// Do a gapped insertion sort for this gap size. 
			// The first gap elements a[0..gap-1] are already 
			// in gapped order keep adding one more element 
			// until the entire array is gap sorted 
			for (int i = gap; i < elementArray.Length; i += 1)
			{
				// add a[i] to the elements that have 
				// been gap sorted save a[i] in temp and 
				// make a hole at position i 
				GameObject temp = elementArray[i];
				visualItems.Add(new SortingVisualItem((int)SortingVisualType.MoveMemory, temp, null, dest: GetMoveUpwardsPosition(indexPositions[i])));

				// shift earlier gap-sorted elements up until 
				// the correct location for a[i] is found 
				int j;
				for (j = i; j >= gap && Compare(elementArray[j - gap], temp) && GetElementSize(elementArray[j - gap]) > GetElementSize(temp); j -= gap)
				{
					visualItems.Add(new SortingVisualItem((int)SortingVisualType.MoveTo, elementArray[j - gap], null, dest: indexPositions[j]));
					elementArray[j] = elementArray[j - gap];
				}

				// put temp (the original a[i])  
				// in its correct location 
				visualItems.Add(new SortingVisualItem((int)SortingVisualType.MoveTo, temp, null, dest: indexPositions[j]));
				elementArray[j] = temp;
			}
		}
	}
}
