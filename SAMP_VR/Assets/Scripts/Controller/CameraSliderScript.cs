using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CameraSliderScript : MonoBehaviour {

    public Slider slider;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        UpdateZoom();
	}

    private void UpdateZoom()
    {
        Camera camera = gameObject.GetComponent<Camera>();
		camera.fieldOfView = slider.value;
    }
}
