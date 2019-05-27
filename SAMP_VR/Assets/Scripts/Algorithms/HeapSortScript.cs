using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HeapSortScript : Algorithm {

	public HeapSortScript() : base (Algorithm.HEAPSORT)
	{ }

	public List<SortingVisualItem> StartSort(GameObject[] array)
	{
		visualItems.Clear();
		elementArray = array;
		myHeapSort();

		return visualItems;
	}

	private void myHeapSort()
	{
		//Build-Max-Heap
		int heapSize = elementArray.Length;
		for (int p = heapSize / 2; p >= 0; p--)
			maxHeapify(heapSize, p);

		for (int i = elementArray.Length - 1; i >= 0; i--)
		{
			//Swap
			Swap(i,0);

			heapSize--;
			maxHeapify(heapSize, 0);
		}
	}

	private void maxHeapify(int heapSize, int index)
	{
		int left = 2 * index;
		int right = 2 * index + 1;
		int largest = index;

		//if (left < heapSize && GetElementSize(elementArray[left]) > GetElementSize(elementArray[index]))
		//	largest = left;
		//else
		//	largest = index;

		if (left < heapSize)
		{
			visualItems.Add(new SortingVisualItem((int)SortingVisualType.Comparison, elementArray[left], elementArray[index]));
			if (GetElementSize(elementArray[left]) > GetElementSize(elementArray[index]))
				largest = left;
			else
				largest = index;
		}
		else
			largest = index;

		if(right < heapSize)
		{
			visualItems.Add(new SortingVisualItem((int)SortingVisualType.Comparison, elementArray[right], elementArray[largest]));
			if (GetElementSize(elementArray[right]) > GetElementSize(elementArray[largest]))
				largest = right;
		}

		if (largest != index) 
		{
			Swap (index, largest);

			maxHeapify (heapSize, largest);
		}
	}

	private double getSize(int index)
	{
		return elementArray [index].GetComponentInChildren<Rigidbody> ().transform.localScale.x;
	}
}
