using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SortingBoxScript : MonoBehaviour {

    private GameObject[] elementArray;
    private float min_dist,max_dist,max_dist_diff;
    private float y_min_position, y_max_position;
	private List<Vector3> init_element_positions = new List<Vector3>();
	private bool inUse = false;
	private uint swapsCounter = 0, comparisonCounter = 0;
	public GameObject elementIndexText;
	private Vector3 indexPosition = Vector3.zero;

	// Use this for initialization
	void Start () {
        setAlgorithmText("RadixSort"); // TODO: DELETE
    }
	
	// Update is called once per frame
	void Update () {
		updateSwapsCounter();
	}

    private void OnCollisionEnter(Collision collision)
    {
        VRTK.Examples.GunShoot gunShootScript = collision.gameObject.GetComponentInParent<VRTK.Examples.GunShoot>();
        if (gunShootScript != null && gunShootScript.gameObject != null)
            TriggerAlgorithm(gunShootScript.gameObject);     
    }

    private void TriggerAlgorithm(GameObject go)
    {
        setAlgorithmText(go.tag);
        GameObject.Find("ElementSpawner").GetComponent<ElementScript>().StartSort();
    }

    public void setElementArray(GameObject[] array)
    {
        elementArray = array;
    }

    public GameObject[] getElementArray()
    {
        return elementArray;
    }

	public void setInitialPositions()
	{
		foreach(GameObject go in elementArray)
		{
			init_element_positions.Add(go.transform.localPosition);
		}
	}

	public List<Vector3> getInitialPositionList()
	{
		return init_element_positions;
	}

    // set min dist & max dist & max dist diff
    public void setDistances()
    {
        if (elementArray == null || elementArray.Length < 2)
            return;

        // at this point we assume that the elements are already properly placed
        min_dist = elementArray[1].transform.localPosition.x - elementArray[0].transform.localPosition.x;

        max_dist = elementArray[elementArray.Length-1].transform.localPosition.x - elementArray[0].transform.localPosition.x;

        max_dist_diff = max_dist - min_dist;

		//float container_size = this.GetComponentInChildren<ElementContainerScript>().gameObject.transform.localScale.y;
		RectTransform rt = this.GetComponentInChildren<ElementContainerScript>().gameObject.GetComponent<RectTransform>();
		float container_size = rt.sizeDelta.y;
		y_max_position = container_size / 2;
        y_min_position = min_dist;
        Debug.Log("Y MAX: " + y_max_position);
        Debug.Log("Y MIN: " + y_min_position);
    }

    public float getOffsetY(GameObject go1, GameObject go2)
    {
        if (go1 == null || go2 == null)
            return 0.0f;

        float distance = Mathf.Abs(go1.transform.localPosition.x - go2.transform.localPosition.x);
        float offset = y_min_position + ((y_max_position-y_min_position)*((distance-min_dist)/max_dist_diff));
        Debug.Log("OFFSET Y: " + offset);
        return offset;
    }

    public void print()
    {
        Debug.Log("MIN DISTANCE: " + min_dist);

        Debug.Log("MAX DISTANCE: " + max_dist);

        Debug.Log("MAX DISTANCE DIFFERENCE: " + max_dist_diff);
       
        Debug.Log("Y MIN POSITION: " +y_min_position);
        Debug.Log("Y MAX POSITION: " +y_max_position);
    }

    public GameObject getContainer()
    {
        return this.GetComponentInChildren<ElementContainerScript>().gameObject;
    }

	public float GetMaxObjectWidth()
	{
		float max = 0.0f;
		foreach(GameObject go in elementArray)
		{
			Transform[] child_transforms = go.GetComponentsInChildren<Transform>();
			foreach (Transform t in child_transforms)
			{
				if (t.gameObject.tag == "BasicElement" || t.localScale.z > max)
				{
					max = t.localScale.z;
				}
			}
		}

		return max;
	}

	public void setAlgorithmText(string text)
	{
        foreach (TMPro.TMP_Text tm in this.GetComponentsInChildren<TMPro.TMP_Text>())
        {
            if (tm.name.Equals("AlgorithmText"))
                tm.text = text;
        }
    }

	public string GetAlgorithmText()
	{
        foreach (TMPro.TMP_Text tm in this.GetComponentsInChildren<TMPro.TMP_Text>())
        {
            if (tm.name.Equals("AlgorithmText"))
                return tm.text;
        }

        return string.Empty;
	}

	public void incSwapsCounter()
	{
		swapsCounter++;
	}

	public void DecreaseSwapsCounter()
	{
		swapsCounter--;
	}

	public void IncComparisonCounter()
	{
		comparisonCounter++;
	}

	public void DecreaseComparisonCounter()
	{
		comparisonCounter--;
	}

	private void updateSwapsCounter()
	{
		//if (swapsCounter <= 0) return;
		updateSwapsCounterText();
	}

	private void updateSwapsCounterText()
	{
		foreach (TMPro.TMP_Text tm in this.GetComponentsInChildren<TMPro.TMP_Text>())
		{
			if (tm.name.Equals("SwapsCounter"))
				tm.text = "Swaps: " + swapsCounter + " / Operations: " +comparisonCounter;
		}
	}

	public void ActivateSwapsCounter()
	{
		swapsCounter = 0;
		comparisonCounter = 0;
		updateSwapsCounterText();
	}

	public void DeactivateSwapsCounter()
	{
		swapsCounter = 0;
		comparisonCounter = 0;
		foreach (TMPro.TMP_Text tm in this.GetComponentsInChildren<TMPro.TMP_Text>())
		{
			if (tm.name.Equals("SwapsCounter"))
				tm.text = string.Empty;
		}
	}

	public bool isInUse()
	{
		return inUse;
	}

	public void setInUse(bool value)
	{
		inUse = value;
	}
}
