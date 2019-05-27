using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InterpreterInterfaceScript {

	public List<List<SortingVisualItem>> Swap(int x, int y)
	{
		List<GameObject[]> elementArrays = GetElementArrays();
		if (elementArrays == null || elementArrays.Count == 0) return null;

		List<List<SortingVisualItem>> queue = new List<List<SortingVisualItem>>();
		foreach (GameObject[] elementArray in elementArrays)
		{
			queue.Add(FillQueue(x, y, elementArray));
		}

		return queue;
	}

	public int size(int index)
	{
		List<GameObject[]> elementArrays = GetElementArrays();
		if (elementArrays == null || elementArrays.Count == 0) return -1;

		int size = -1;

		foreach (GameObject[] elementArray in elementArrays)
		{
			if (elementArray.Length <= index || elementArray[index] == null) return size;

			size = HelperScript.GetElementSize(elementArray[index]);
			break;
		}
		return size;
	}

	public int getElementCount()
    {
		List<GameObject[]> elementArrays = GetElementArrays();
		if (elementArrays == null || elementArrays.Count == 0) return 0;

		foreach (GameObject[] elementArray in elementArrays)
        {
            return elementArray.Length;
        }

        return 0;
    }

	private List<SortingVisualItem> FillQueue(int x, int y, GameObject[] elementArray)
	{
		if (x > elementArray.Length || y > elementArray.Length || x < 0 || y < 0)
		{
			Debug.Log("Out of range! Can't swap " + x + " and " + y);
			return null;
		}

		List<SortingVisualItem> queue = new List<SortingVisualItem>();
		queue.Add(new SortingVisualItem((int)SortingVisualType.Swap, elementArray[x], elementArray[y]));

		// swap in array
		GameObject tmp = elementArray[x];
		elementArray[x] = elementArray[y];
		elementArray[y] = tmp;

		return queue;
	}

	private List<GameObject[]> GetElementArrays()
	{
		GameObject[] sortingBoxes = HelperScript.GetSortingboxesToExecuteCode();
		List<GameObject[]> elementArrays = new List<GameObject[]>();
		foreach (GameObject sortingBox in sortingBoxes)
		{
			if (sortingBox == null) continue;

			elementArrays.Add(sortingBox.GetComponent<SortingBoxScript>().getElementArray());
		}
		return elementArrays;
	}
}
