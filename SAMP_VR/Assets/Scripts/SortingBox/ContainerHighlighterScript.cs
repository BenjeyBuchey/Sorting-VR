using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerHighlighterScript : MonoBehaviour {

    Color color_default = new Color (0.0f, 0.0f, 0.0f, 0.0f);
    Color color_highlighted = new Color (0.0f, 0.0f, 0.5f, 0.2f);

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () 
    {
        //if (Input.GetMouseButtonDown(0))
        //{
        //    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //    RaycastHit hit;

        //    if (Physics.Raycast(ray, out hit))
        //    {
        //        if (hit.transform.tag == "Container")
        //        {
        //            setColor(hit.transform.gameObject);
        //        }
        //    }   
        //}
	}

    private void setColor(GameObject go)
    {
        if (go.GetComponent<Renderer>().material.color == color_default)
        {
            go.GetComponent<Renderer>().material.color = color_highlighted;
            go.GetComponent<ElementContainerScript>().setHighlighted(true);
        }
        else
        {
            go.GetComponent<Renderer>().material.color = color_default;
            go.GetComponent<ElementContainerScript>().setHighlighted(false);
        }
    }
}
