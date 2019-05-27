using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class HelperScript
{
	public static T[] FindComponentsInChildrenWithTag<T>(this GameObject parent, string tag, bool forceActive = false) where T : Component
	{
		if (parent == null) { throw new System.ArgumentNullException(); }
		if (string.IsNullOrEmpty(tag) == true) { throw new System.ArgumentNullException(); }
		List<T> list = new List<T>(parent.GetComponentsInChildren<T>(forceActive));
		if (list.Count == 0) { return null; }

		for (int i = list.Count - 1; i >= 0; i--)
		{
			if (list[i].CompareTag(tag) == false)
			{
				list.RemoveAt(i);
			}
		}
		return list.ToArray();
	}

	public static T FindComponentInChildWithTag<T>(this GameObject parent, string tag, bool forceActive = false) where T : Component
	{
		if (parent == null) { throw new System.ArgumentNullException(); }
		if (string.IsNullOrEmpty(tag) == true) { throw new System.ArgumentNullException(); }

		T[] list = parent.GetComponentsInChildren<T>(forceActive);
		foreach (T t in list)
		{
			if (t.CompareTag(tag) == true)
			{
				return t;
			}
		}
		return null;
	}

	// gets the scale of a textmesh text
	public static float GetTextMeshWidth(TextMesh mesh)
	{
		float width = 0;
		foreach (char symbol in mesh.text)
		{
			CharacterInfo info;
			if (mesh.font.GetCharacterInfo(symbol, out info))
			{
				width += info.advance;
			}
		}
		return width * mesh.characterSize * 0.1f * mesh.transform.lossyScale.x;
	}

	// checks if program is paused
	public static bool IsPaused()
	{
		//SwapManagerScript sms = GameObject.Find("SwapManager").GetComponent<SwapManagerScript>();
		SwapManagerScript sms = GameObject.Find("Controls").GetComponent<SwapManagerScript>();
		if (sms == null) return false;

		return sms.isPaused;
	}

	// returns all sortingbox gameobjects on which code is getting executed
	public static GameObject[] GetSortingboxesToExecuteCode()
	{
		GameObject[] allSortingBoxes = GameObject.FindGameObjectsWithTag("SortingBoxes");
		if (allSortingBoxes == null || allSortingBoxes.Length == 0) return null;

		List<GameObject> sortingBoxes = new List<GameObject>();

		// add to list if highlighted(container) and not in use(sbs)
		foreach(GameObject sortingBox in allSortingBoxes)
		{
			if (sortingBox == null) continue;

			if (sortingBox.GetComponentInChildren<ElementContainerScript>().getHighlighted() && !sortingBox.GetComponent<SortingBoxScript>().isInUse())
				sortingBoxes.Add(sortingBox);
		}

		return sortingBoxes.ToArray();
	}

	public static int GetElementSize(GameObject go)
	{
		string text = go.GetComponentInChildren<TextMesh>().text;
		int value = 0;
		Int32.TryParse(text, out value);

		return value;
	}
}
