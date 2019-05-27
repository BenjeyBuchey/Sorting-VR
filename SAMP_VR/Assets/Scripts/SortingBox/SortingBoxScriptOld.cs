using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SortingBoxScript1 : MonoBehaviour {

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
		SetAlgorithmTextPosition();
		SetSwapsCounterPosition();
		SetIndexTextPosition();
		//SetIndexesTexts();
	}
	
	// Update is called once per frame
	void Update () {
		updateSwapsCounter();
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
        min_dist = elementArray[1].transform.position.z - elementArray[0].transform.position.z;

        max_dist = elementArray[elementArray.Length-1].transform.position.z - elementArray[0].transform.position.z;

        max_dist_diff = max_dist - min_dist;

        float container_size = this.GetComponentInChildren<ElementContainerScript>().gameObject.transform.localScale.y;
        y_max_position = container_size / 2;
        y_min_position = min_dist;
    }

    public float getOffsetY(GameObject go1, GameObject go2)
    {
        if (go1 == null || go2 == null)
            return 0.0f;

        float distance = Mathf.Abs(go1.transform.position.z - go2.transform.position.z);
        float offset = y_min_position + ((y_max_position-y_min_position)*((distance-min_dist)/max_dist_diff));
        return y_min_position + (y_max_position-y_min_position)*((distance-min_dist)/max_dist_diff);
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

	private void SetAlgorithmTextPosition()
	{
		// should be set on upper right corner of container
		foreach (TextMesh tm in this.GetComponentsInChildren<TextMesh>())
		{
			if (tm == null ||!tm.name.Equals("AlgorithmText"))
				continue;

			GameObject container = getContainer();
			if (container == null) return;

			Vector3 tm_position = container.transform.position;
			tm_position.z = tm_position.z + container.transform.localScale.z / 2;
			tm_position.y = tm_position.y + container.transform.localScale.y / 2;
			tm.transform.position = tm_position;
		}
	}

	private void SetIndexTextPosition()
	{
		// set on lower left corner of container
		// set gameobject inactive till called
		foreach (TextMesh tm in this.GetComponentsInChildren<TextMesh>())
		{
			if (tm == null || !tm.name.Equals("IndexText"))
				continue;

			GameObject container = getContainer();
			if (container == null) return;

			//tm.text = "Index: ";
			tm.text = string.Empty;
			float zOffset = 7.5f;
			float yOffset = tm.transform.localScale.z / 2;
			Vector3 tm_position = container.transform.position;
			tm_position.z = tm_position.z - container.transform.localScale.z / 2 + zOffset;
			tm_position.y = tm_position.y - container.transform.localScale.y / 2 + yOffset;
			tm_position.x = 0.0f; //0.0f;
			tm.transform.position = tm_position;
			indexPosition = tm_position;
		}
	}

	private void SetIndexesTexts()
	{
		// create text for every element
		for(int i = 0; i < elementArray.Length; i++)
		{
			var elementIndex = Instantiate(elementIndexText, this.transform);

			// set z coordinate to go
			indexPosition.z = elementArray[i].transform.position.z;
			elementIndex.transform.position = indexPosition;
			elementIndex.GetComponent<TextMesh>().text = i.ToString();
		}
	}

	private void SetSwapsCounterPosition()
	{
		// set on upper left corner of container
		// set gameobject inactive till called
		foreach (TextMesh tm in this.GetComponentsInChildren<TextMesh>())
		{
			if (tm == null || !tm.name.Equals("SwapsCounter"))
				continue;

			GameObject container = getContainer();
			if (container == null) return;

			tm.text = "Swaps: " + swapsCounter;
			Vector3 tm_position = container.transform.position;
			tm_position.z = tm_position.z - container.transform.localScale.z / 2;// + HelperScript.GetTextMeshWidth(tm);
			tm_position.y = tm_position.y + container.transform.localScale.y / 2;
			tm.transform.position = tm_position;

			tm.text = string.Empty;
		}
	}

	public void setAlgorithmText(string text)
	{
		foreach (TextMesh tm in this.GetComponentsInChildren<TextMesh>())
		{
			if (tm.name.Equals("AlgorithmText"))
				tm.text = text;
		}

        foreach (TMPro.TMP_Text tm in this.GetComponentsInChildren<TMPro.TMP_Text>())
        {
            if (tm.name.Equals("AlgorithmText"))
                tm.text = text;
        }
    }

	public string GetAlgorithmText()
	{
		foreach (TextMesh tm in this.GetComponentsInChildren<TextMesh>())
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
		foreach (TextMesh tm in this.GetComponentsInChildren<TextMesh>())
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
		foreach (TextMesh tm in this.GetComponentsInChildren<TextMesh>())
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
