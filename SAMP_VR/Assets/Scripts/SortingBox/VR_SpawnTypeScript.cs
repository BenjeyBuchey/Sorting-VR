using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VRTK.Controllables;

public class VR_SpawnTypeScript : MonoBehaviour
{
    public VRTK_BaseControllable controllable;
    public Text displayText;
    public string outputOnMax = "Maximum Reached";
    public string outputOnMin = "Minimum Reached";
    private string[] spawnTypes = { "Random", "Nearly sorted", "Reversed" };
    private int leverValue = 0;

    protected virtual void OnEnable()
    {
        controllable = (controllable == null ? GetComponent<VRTK_BaseControllable>() : controllable);
        controllable.ValueChanged += ValueChanged;
        controllable.MaxLimitReached += MaxLimitReached;
        controllable.MinLimitReached += MinLimitReached;
    }

    protected virtual void ValueChanged(object sender, ControllableEventArgs e)
    {
        if (displayText != null && e.value >= 0 && e.value < spawnTypes.Length)
        {
            leverValue = (int)e.value;
            displayText.text = spawnTypes[(int)e.value];
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
        if (outputOnMin != "")
        {
            Debug.Log(outputOnMin);
        }
    }

    public int GetLeverValue()
    {
        return leverValue;
    }
}
