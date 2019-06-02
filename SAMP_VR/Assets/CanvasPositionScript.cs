using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class CanvasPositionScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Camera.main == null) return;
        // canvas should always be at player x position
        gameObject.transform.position = new Vector3(Camera.main.transform.position.x,
                                                    gameObject.transform.position.y,
                                                    gameObject.transform.position.z);
    }
}
