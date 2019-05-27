using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class MergeSortScript : Algorithm {

    private List<GameObject> swappingQueue = new List<GameObject> ();

	public MergeSortScript() : base (Algorithm.MERGESORT)
	{

	}

	public List<SortingVisualItem> StartSort(GameObject[] array)
	{
		visualItems.Clear();
		elementArray = array;
		myMergeSort(0, elementArray.Length - 1);

		return visualItems;
	}

	public void myMergeSort(int left, int right)
	{
		if (left < right)
		{
			int middle = (left + right) / 2;

			myMergeSort(left, middle);
			myMergeSort(middle + 1, right);

			//Merge
			GameObject[] leftArray = new GameObject[middle - left + 1];
			GameObject[] rightArray = new GameObject[right - middle];

			Array.Copy(elementArray, left, leftArray, 0, middle - left + 1);
			Array.Copy(elementArray, middle + 1, rightArray, 0, right - middle);

			visualItems.Add(new SortingVisualItem((int)SortingVisualType.MergeArray, null, null, array: leftArray, isLeftArray: true));
			visualItems.Add(new SortingVisualItem((int)SortingVisualType.MergeArray, null, null, array: rightArray, isLeftArray: false));

			int i = 0;
			int j = 0;
			Debug.Log ("NEW LOOP");
			printArray (leftArray, "LeftArray: ");
			printArray (rightArray, "RightArray: ");
			for (int k = left; k < right + 1; k++)
			{
				if (i == leftArray.Length)
				{
					visualItems.Add(new SortingVisualItem((int)SortingVisualType.MergeMove, rightArray[j], null, mergePosition: k));

					elementArray[k] = rightArray[j];
					j++;
				}
				else if (j == rightArray.Length)
				{
					visualItems.Add(new SortingVisualItem((int)SortingVisualType.MergeMove, leftArray[i], null, mergePosition: k));

					elementArray[k] = leftArray[i];
					i++;
				}
				else if (GetElementSize(leftArray[i]) <= GetElementSize(rightArray[j]))
				{
					visualItems.Add(new SortingVisualItem((int)SortingVisualType.MergeComparison, leftArray[i], rightArray[j]));
					visualItems.Add(new SortingVisualItem((int)SortingVisualType.MergeMove, leftArray[i], null, mergePosition: k));

					elementArray[k] = leftArray[i];
					i++;
				}
				else
				{
					visualItems.Add(new SortingVisualItem((int)SortingVisualType.MergeComparison, leftArray[i], rightArray[j]));
					visualItems.Add(new SortingVisualItem((int)SortingVisualType.MergeMove, rightArray[j], null, mergePosition: k));

					elementArray[k] = rightArray[j];
					j++;
				}
			}
		}
	}

	private void printArray(GameObject[] array, string name)
	{
		string s = String.Empty;
		for (int i = 0; i < array.Length; i++) 
		{
			s += array [i].GetComponent<SingleElementScript> ().getElementId ().ToString() + " ";
		}

		Debug.Log (name + s);
	}

	//private double getGameObjectSize(GameObject go)
	//{
	//	return go.GetComponentInChildren<Rigidbody> ().transform.localScale.x;
	//}
}
