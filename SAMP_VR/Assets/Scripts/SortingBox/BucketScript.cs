using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BucketScript : MonoBehaviour
{
    List<Transform> _bucketObjects = new List<Transform>();
    // Start is called before the first frame update
    void Start()
    {
        Init();
        DoActivate(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Init()
    {
        Transform[] childTransforms = gameObject.GetComponentsInChildren<Transform>();
        foreach (Transform child in childTransforms)
        {
            if (child.gameObject.tag == "Buckets")
            {
                _bucketObjects.Add(child);
            }
        }
    }

    public void DoActivate(bool doActivate)
    {
        //gameObject.SetActive(doActivate);
        foreach(Transform bucket in _bucketObjects)
        {
            bucket.gameObject.SetActive(doActivate);
        }
    }

    public List<Transform> GetBucketObjects()
    {
        return _bucketObjects;
    }
}
