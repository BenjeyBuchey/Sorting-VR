using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GnomeSortScript : Algorithm {

	public GnomeSortScript() : base (Algorithm.GNOMESORT)
	{

	}

	public List<SortingVisualItem> StartSort(GameObject[] array)
	{
		visualItems.Clear();
		elementArray = array;
		myGnomeSort();

		return visualItems;
	}

	private void myGnomeSort() 
	{ 
		for(int i = 1;i < elementArray.Length;)
		{
			visualItems.Add(new SortingVisualItem((int)SortingVisualType.Comparison, elementArray[i - 1], elementArray[i]));
			if (GetElementSize(elementArray[i-1]) <= GetElementSize(elementArray[i])) //if (getSize(i-1)<=getSize(i)) 
				i += 1;
			else
			{
				Swap (i-1,i);

				i-=1; 
				if(i==0) 
					i=1;
			}
		} 
	}

	private double getSize(int index)
	{
		return elementArray [index].GetComponentInChildren<Rigidbody> ().transform.localScale.x;
	}
}
