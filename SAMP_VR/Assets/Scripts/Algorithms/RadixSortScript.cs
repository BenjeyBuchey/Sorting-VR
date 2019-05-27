using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class RadixSortScript : Algorithm {

	//private GameObject[] elementArray;
    private List<Vector3> bucket_positions = new List<Vector3> ();

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public RadixSortScript() : base (Algorithm.RADIXSORT)
	{
		
	}

	public List<SortingVisualItem> StartSort(GameObject[] array)
	{
		visualItems.Clear();
		elementArray = array;
		setBucketPositions();
		myRadixSort();

		return visualItems;
	}

	// Source: http://algorithmsandstuff.blogspot.co.at/2014/06/radix-sort-in-c-sharp.html
	private void myRadixSort()
	{
		bool isFinished = false;
		int digitPosition = 0;

		List<Queue<GameObject>> buckets = new List<Queue<GameObject>>();
		InitializeBuckets(buckets);

		while (!isFinished)
		{
			isFinished = true;

			foreach (GameObject go in elementArray)
			{
				int value = GetElementSize(go);
				//int value = GetElementValue(go);
				int bucketNumber = GetBucketNumber(value, digitPosition);
				if (bucketNumber > 0)
				{
					isFinished = false;
				}

				buckets[bucketNumber].Enqueue(go);
			}

			int i = 0;
			int bucket_number = 0;
			foreach (Queue<GameObject> bucket in buckets)
			{
				int bucket_size = bucket.Count;
				int bucket_position = 0;
				while (bucket.Count > 0)
				{
					elementArray[i] = bucket.Dequeue();
					ArrayList list = new ArrayList ();

					if (bucket_size != elementArray.Length)
					{
						visualItems.Add(new SortingVisualItem((int)SortingVisualType.Radix, elementArray[i], null, bucket_number, bucket_position));
					}
					bucket_position++;
					i++;
				}
				bucket_number++;
			}

			if(!isFinished)
				setToInitialPositions ();

			digitPosition++;
		}
	}

	private void setToInitialPositions()
	{
		for (int i = 0; i < elementArray.Length; i++) 
		{
			visualItems.Add(new SortingVisualItem((int)SortingVisualType.Radix, elementArray[i], null, -1, i));
		}
	}

	private int GetBucketNumber(int value, int digitPosition)
	{
		int bucketNumber = (value / (int)Math.Pow(10, digitPosition)) % 10;
		return bucketNumber;
	}

	private static void InitializeBuckets(List<Queue<GameObject>> buckets)
	{
		for (int i = 0; i < 10; i++)
		{
			Queue<GameObject> q = new Queue<GameObject>();
			buckets.Add(q);
		}
	}

	private void setBucketPositions()
	{
		for (int i = 0; i < elementArray.Length; i++) 
		{
			bucket_positions.Add (elementArray [i].transform.position);
		}
	}

	private int GetElementValue(GameObject go)
	{
		string text = go.GetComponentInChildren<TextMesh>().text;
		int value = 0;
		Int32.TryParse(text, out value);

		return value;
	}
}
