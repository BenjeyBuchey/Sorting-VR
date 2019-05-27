using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BucketElementObject {

	public GameObject go { get; private set; }
	public int bucket { get; private set; }
	public int position { get; private set; }

	public BucketElementObject(GameObject go_, int bucket_, int position_)
	{
		go = go_;
		bucket = bucket_;
		position = position_;
	}
}
