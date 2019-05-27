using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SliderUpdateScript : MonoBehaviour {

	public Text slidertext;
	public Slider slider;
	private int element_size;
	// Use this for initialization
	void Start () {
		element_size = (int)slider.value;
		slidertext.text = slider.value.ToString ();
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void updateSlider(float value)
	{
		element_size = (int)value;
		slidertext.text = value.ToString ();
	}

	void Awake()
	{
		DontDestroyOnLoad(gameObject);
	}

	public int getElementSize()
	{
		return element_size;
	}
}
