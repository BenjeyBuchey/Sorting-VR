using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class Algorithm {

	public const string QUICKSORT = "QuickSort", RADIXSORT = "RadixSort", GNOMESORT = "GnomeSort", MERGESORT = "MergeSort", HEAPSORT = "HeapSort", BUBBLESORT = "BubbleSort", 
		SELECTIONSORT = "SelectionSort", SHELLSORT = "ShellSort", INSERTIONSORT = "InsertionSort";
	private readonly string name;
	protected List<SortingVisualItem> visualItems = new List<SortingVisualItem>();
	protected GameObject[] elementArray;

	protected Algorithm(string name)
	{
		this.name = name;
	}

	public string getName()
	{
		return name;
	}

	protected void Swap(int i, int j)
	{
		if (i == j)
			return;

		// add to queue and swap element array position
		visualItems.Add(new SortingVisualItem((int)SortingVisualType.Swap, elementArray[i], elementArray[j]));

		GameObject tmp = elementArray[i];
		elementArray[i] = elementArray[j];
		elementArray[j] = tmp;
	}

	protected int GetElementSize(GameObject go)
	{
		return HelperScript.GetElementSize(go);
	}

	protected bool Compare(GameObject element1, GameObject element2)
	{
		visualItems.Add(new SortingVisualItem((int)SortingVisualType.Comparison, element1, element2));
		return true;
	}

	protected Vector3 GetMoveUpwardsPosition(GameObject element)
	{
		Vector3 dest = element.transform.localPosition;
		dest.y += 10f;
		return dest;
	}

	protected Vector3 GetMoveUpwardsPosition(Vector3 initPos)
	{
		Vector3 dest = initPos;
		dest.y += 10f;
		return dest;
	}
}
