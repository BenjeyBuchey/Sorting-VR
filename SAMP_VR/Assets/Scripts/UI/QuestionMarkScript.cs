using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class QuestionMarkScript : MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler
{
	public GameObject panel;

	private void Start()
	{
		setPanel();
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		panel.SetActive(!panel.activeSelf);
	}

	public void OnPointerDown(PointerEventData eventData)
	{
	}

	public void OnPointerUp(PointerEventData eventData)
	{
	}

	public void hidePanel()
	{
		panel.SetActive(false);
	}

	private void setPanel()
	{
		//panel = this.transform.Find("PopupWindowPanel").gameObject;
		panel.SetActive(false);
	}
}
