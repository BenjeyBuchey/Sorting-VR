using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ElementContainerScript : MonoBehaviour, IDropHandler, IPointerClickHandler {

	// Use this for initialization
    Color color_default = new Color (0.0f, 0.0f, 0.0f, 0.0f);
    Color defaultColor = new Color(0.2924528f, 0.2910733f, 0.2910733f, 1.0f);
    Color highlightColor = new Color(0.4901961f, 0.5803922f, 0.6156863f, 1.0f);
    private bool isHighlighted = false;

    void Start () {
        //gameObject.GetComponent<Renderer>().material.color = color_default;
    }
	
	// Update is called once per frame
	void Update () 
    {
        
	}

    public void setHighlighted(bool value)
    {
        isHighlighted = value;
    }

    public bool getHighlighted()
    {
        return isHighlighted;
    }

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("SELECTED: " + eventData.selectedObject.tag);
        string algorithmText = eventData.selectedObject.tag;
        gameObject.transform.Find("AlgorithmText").GetComponent<TMPro.TMP_Text>().text = eventData.selectedObject.tag;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        isHighlighted = !isHighlighted;
        SetContainerColor();
    }

    private void SetContainerColor()
    {
        Image img = gameObject.transform.Find("Panel").GetComponent<Image>();
        if (isHighlighted)
            img.color = highlightColor;
        else
            img.color = defaultColor;
    }
}
