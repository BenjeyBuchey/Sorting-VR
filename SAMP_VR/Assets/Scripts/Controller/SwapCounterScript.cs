using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SwapCounterScript : MonoBehaviour {

	public Text swap_counter_text;
	private int counter = 0;
	private static string swap_string = "Swaps: ";
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		swap_counter_text.text = swap_string + counter;
	}

	public int getCounter()
	{
		return counter;
	}

	public void incCounter()
	{
		counter++;
	}
}
