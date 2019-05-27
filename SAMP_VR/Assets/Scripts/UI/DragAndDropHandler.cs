using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragAndDropHandler : MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler {

	private Vector3 position;
    private GraphicRaycaster m_Raycaster;

    public void OnDrag(PointerEventData eventData)
	{
		if (HelperScript.IsPaused()) return;

		transform.position = Input.mousePosition;
	}

	public void OnBeginDrag(PointerEventData eventData)
	{
		position = transform.position;
        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

	public void OnEndDrag(PointerEventData eventData)
	{
        // check if it was released over sorting box
        //RaycastHit[] hits;
        //Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        //hits = Physics.RaycastAll(camRay);
        //if (hits != null)
        //{
        //    foreach (RaycastHit hit in hits)
        //    {
        //        Debug.Log("HIT: " + hit.transform.gameObject.tag);
        //        if (hit.transform.gameObject.tag == "Container")
        //        {
        //            ApplyAlgorithmText(hit.transform.gameObject);
        //        }
        //    }
        //}

        //List<RaycastResult> results = new List<RaycastResult>();
        //m_Raycaster.Raycast(eventData, results);

        //foreach (RaycastResult result in results)
        //{
        //    Debug.Log("DROPPED ON: " + result.gameObject.tag);
        //    if (result.gameObject.tag == "Container")
        //    {
        //        ApplyAlgorithmText(result.gameObject);
        //    }
        //}

        transform.position = position;
		EventSystem.current.SetSelectedGameObject(null);
        GetComponent<CanvasGroup>().blocksRaycasts = true;
    }

	// Use this for initialization
	void Start () {

    }
	
	// Update is called once per frame
	void Update () {
		
	}

	// go is a container --> set algorithm text
	private void ApplyAlgorithmText(GameObject go)
	{
		if (HelperScript.IsPaused() ||go == null) return;

		SortingBoxScript sbs = go.GetComponentInParent<SortingBoxScript>();
		if (sbs == null || sbs.isInUse()) return;

		sbs.setAlgorithmText(transform.gameObject.tag);
	}
}