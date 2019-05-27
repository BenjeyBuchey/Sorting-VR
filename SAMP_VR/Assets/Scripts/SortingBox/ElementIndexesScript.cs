using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementIndexesScript : MonoBehaviour
{
	public TMPro.TMP_Text elementIndex;
    // Start is called before the first frame update
    void Start()
    {

	}

    // Update is called once per frame
    void Update()
    {
        
    }

	public void SpawnIndexes(GameObject[] elements)
	{
		GameObject parent = gameObject.transform.parent.gameObject;
		float containerHeight = parent.GetComponent<RectTransform>().rect.height;
		for (int i = 0; i < elements.Length; i++)
		{
			TMPro.TMP_Text index = Instantiate(elementIndex, gameObject.transform);
			index.text = i.ToString();
			RectTransform rt = index.rectTransform;
			Vector3 pos = new Vector3(elements[i].transform.localPosition.x, -containerHeight / 2+ rt.rect.height);
			rt.localPosition = pos;
		}
	}
}
