using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ExecuteButtonScript : MonoBehaviour {

	public GameObject execute_button;
	public TMP_InputField inputField;
	// Use this for initialization
	void Start () {
		//setPosition();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void setPosition()
	{
		Vector2 pos = gameObject.GetComponent<RectTransform>().anchoredPosition;
		RectTransform rt = gameObject.GetComponent<RectTransform>();

		RectTransform[] parent_rts = gameObject.GetComponentsInParent<RectTransform>();

		foreach (RectTransform parent_rt in parent_rts)
		{
			if(parent_rt.transform.name.Equals("CodeField"))
			{
				pos.x -= parent_rt.rect.width / 2 + rt.rect.width/2;
				pos.y -= parent_rt.rect.height / 2 - rt.rect.height/2;
				gameObject.GetComponent<RectTransform>().anchoredPosition = pos;
			}
		}
	}

	public void exec()
	{
		if (HelperScript.IsPaused()) return;
		if (inputField == null) return;

		gameObject.GetComponent<InputScript>().exec(inputField.text);
	}
}