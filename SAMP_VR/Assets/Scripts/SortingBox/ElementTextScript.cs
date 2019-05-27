using UnityEngine;
using System.Collections;

public class ElementTextScript : MonoBehaviour {

	Quaternion rotation;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void Awake()
	{
		rotation = transform.rotation;
	}
	void LateUpdate()
	{
		transform.rotation = rotation;
	}

    public void setPosition(float element_size)
    {
        // change gameobjects y position to -element_size/2
        Vector3 pos = new Vector3(gameObject.transform.localPosition.x,element_size/-2.0f,gameObject.transform.localPosition.z);
        gameObject.transform.localPosition = pos;
    }
}
