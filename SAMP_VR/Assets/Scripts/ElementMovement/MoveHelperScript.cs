using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MoveHelperScript {

	public static Color DEFAULT_COLOR = Color.red, SWAP_COLOR = Color.green, COMPARISON_COLOR = Color.blue;
	public static Color LEFTARRAY_COLOR = new Color(0.6f, 0.96f, 1.0f), RIGHTARRAY_COLOR = new Color(0.28f, 0.24f, 0.55f);

	public void SetColor(GameObject element, Color color)
	{
		foreach (Transform child in element.transform)
		{
			if (child.tag.Equals("BasicElement"))
			{
				child.GetComponent<Renderer>().material.color = color;
			}
		}
	}

	public void ChangeColor(GameObject element1, GameObject element2, int type, bool isDefaultColor)
	{
		HandleColor(element1, type, isDefaultColor);
		HandleColor(element2, type, isDefaultColor);
	}

	public void ChangeColor(GameObject[] array, int type, bool isLeftArray, bool isDefaultColor)
	{
		if (array == null) return;

		foreach(GameObject element in array)
		{
			HandleColor(element, type, isDefaultColor, isLeftArray);
		}
	}

	private void HandleColor(GameObject element, int type, bool isDefaultColor, bool isLeftArray = false)
	{
		if (element == null) return;

		foreach (Transform child in element.transform)
		{
			if (child.tag.Equals("BasicElement"))
			{
				switch(type)
				{
					case (int)SortingVisualType.MoveTo:
					case (int)SortingVisualType.MoveMemory:
					case (int)SortingVisualType.Swap:
					case (int)SortingVisualType.Radix:
					case (int)SortingVisualType.MergeMove:
						if (isDefaultColor)
							child.GetComponent<Renderer>().material.color = element.GetComponent<SingleElementScript>().DefaultColor;
						else
							child.GetComponent<Renderer>().material.color = SWAP_COLOR;
						break;
					case (int)SortingVisualType.Comparison:
						if (isDefaultColor)
							child.GetComponent<Renderer>().material.color = element.GetComponent<SingleElementScript>().DefaultColor;
						else
							child.GetComponent<Renderer>().material.color = COMPARISON_COLOR;
						break;
					case (int)SortingVisualType.MergeArray:
						if (isLeftArray)
							child.GetComponent<Renderer>().material.color = LEFTARRAY_COLOR;
						else
							child.GetComponent<Renderer>().material.color = RIGHTARRAY_COLOR;
						break;
					case (int)SortingVisualType.MergeComparison:
						if (!isDefaultColor)
							child.GetComponent<Renderer>().material.color = COMPARISON_COLOR;
						break;
				}
			}
		}
	}

	public void stopSortingboxUsage(GameObject go)
	{
		if (go == null) return;

		SortingBoxScript sbs = go.GetComponentInParent<SortingBoxScript>();
		if (sbs == null) return;

		sbs.setInUse(false);
	}

	public float GetSwapSpeed(int type)
	{
		GameObject go = GameObject.Find("SwapSpeedSlider");
		if (go == null) return 0.0f;

		Slider s = go.GetComponent<Slider>();
		if (s == null) return 0.0f;

		float modifier = (type == (int)SortingVisualType.Comparison) ? 2.0f : 1.0f;

		return s.value * -1 / modifier;
	}
}
