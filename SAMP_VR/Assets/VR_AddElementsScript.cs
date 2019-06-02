using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VRTK.Controllables;

public class VR_AddElementsScript : MonoBehaviour
{
    public VRTK_BaseControllable controllable;
    public Text displayText;
    public Text elementSizeText;
    public string outputOnMax = "Maximum Reached";
    public string outputOnMin = "Minimum Reached";
    private float lastSpawnTime = 0f, timeDiff = 5f;

    protected virtual void OnEnable()
    {
        controllable = (controllable == null ? GetComponent<VRTK_BaseControllable>() : controllable);
        controllable.ValueChanged += ValueChanged;
        controllable.MaxLimitReached += MaxLimitReached;
        controllable.MinLimitReached += MinLimitReached;
    }

    protected virtual void ValueChanged(object sender, ControllableEventArgs e)
    {
        if (displayText != null)
        {
            displayText.text = e.value.ToString("F1");
        }
    }

    protected virtual void MaxLimitReached(object sender, ControllableEventArgs e)
    {        
        if (outputOnMax != "")
        {
            Debug.Log(outputOnMax);
        }
    }

    protected virtual void MinLimitReached(object sender, ControllableEventArgs e)
    {
        // prevent firing on start
        if (Time.time < timeDiff) return;

        // here we spawn the new SortingBox
        float elementSize = 0;
        if (elementSizeText != null && float.TryParse(elementSizeText.text, out elementSize))
        {
            if (lastSpawnTime == 0f || (lastSpawnTime + timeDiff < Time.time))
            {
                lastSpawnTime = Time.time;
                GameObject.Find("ElementSpawner").GetComponent<ElementScript>().spawnElements((int)elementSize);
            }
        }
    }
}
