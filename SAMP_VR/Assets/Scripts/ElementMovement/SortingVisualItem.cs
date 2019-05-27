using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum SortingVisualType { Swap = 0, Comparison = 1, Radix = 2, MergeArray = 3, MergeMove = 4, MergeComparison = 5, MoveTo = 6, MoveMemory = 7 }

public class SortingVisualItem {

	private readonly GameObject _element1, _element2;
	private readonly int _type, _bucket, _bucketPosition, _mergePosition;
	private readonly GameObject[] _array;//, _rightArray;
	private readonly bool _isLeftArray;
	private readonly Vector3 _dest;

	public GameObject Element1
	{
		get
		{
			return _element1;
		}
	}

	public GameObject Element2
	{
		get
		{
			return _element2;
		}
	}

	public int Type
	{
		get
		{
			return _type;
		}
	}

	public int Bucket
	{
		get
		{
			return _bucket;
		}
	}

	public int BucketPosition
	{
		get
		{
			return _bucketPosition;
		}
	}

	public GameObject[] Array
	{
		get
		{
			return _array;
		}
	}

	public bool IsLeftArray
	{
		get
		{
			return _isLeftArray;
		}
	}

	public int MergePosition
	{
		get
		{
			return _mergePosition;
		}
	}

	public Vector3 Dest => _dest;

	public SortingVisualItem(int type, GameObject element1, GameObject element2, int bucket = -1, int bucketPosition = -1, GameObject[] array = null, bool isLeftArray = false, int mergePosition = -1,
		Vector3 dest = new Vector3())
	{
		_type = type;
		_element1 = element1;
		_element2 = element2;
		_bucket = bucket;
		_bucketPosition = bucketPosition;
		_array = array;
		_isLeftArray = isLeftArray;
		_mergePosition = mergePosition;
		_dest = dest;
	}
}
