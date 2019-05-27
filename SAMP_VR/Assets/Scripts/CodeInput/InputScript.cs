using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Jint;

public class InputScript : MonoBehaviour {

	private InterpreterInterfaceScript iis;
	private JintEngine e;
	//private List<List<GameObject>> queue;
	private List<List<SortingVisualItem>> queue;
	private List<KeyValuePair<MoveScript, List<SortingVisualItem>>> queueNew;

	// Use this for initialization
	void Start () {

		//iis = gameObject.AddComponent<InterpreterInterfaceScript>();

		iis = new InterpreterInterfaceScript();
		//queue = new List<List<GameObject>>();
		queue = new List<List<SortingVisualItem>>();
		queueNew = new List<KeyValuePair<MoveScript, List<SortingVisualItem>>>();
		e = new JintEngine();

		InitFunctions();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void exec(string command)
	{
        queue.Clear();
		e.Run (command);
        execQueue();
	}

	private void InitFunctions()
	{
		e.SetFunction("log",new Jint.Delegates.Action<object>((a) => Debug.Log(a)));

		e.SetFunction("add",new Jint.Delegates.Func<double, double, double>((a, b) => a + b));

		e.SetFunction("swap",new Jint.Delegates.Action<int, int>((a, b) => Swap(a, b)));

		e.SetFunction("size",new Jint.Delegates.Func<int, int>((a) => size(a)));

		e.SetFunction("test123",new Jint.Delegates.Action<int>((a) => test123(a)));

		e.SetFunction("mul",new Jint.Delegates.Action<object, object>((a, b) => mul(a, b)));

		e.SetFunction("elementCount", new Jint.Delegates.Func<int>(() => elementCount()));
	}

	// swaps array elements in positions a & b
	private void Swap(int a, int b)
	{
		List<List<SortingVisualItem>> temp = iis.Swap(a, b);
		if (queue.Count == 0)
		{
			queue = temp;
			return;
		}

		for (int i = 0; i < temp.Count; i++)
		{
			queue[i].AddRange(temp[i]);
		}
	}

	// returns the size of the element in array position a
	private int size(int a)
	{
		return iis.size (a);
	}

	// returns the length of the corresponding element array
    private int elementCount()
    {
        return iis.getElementCount();
    }

	private void mul(object a_, object b_)
	{
		int a = 0, b = 0;
		int.TryParse (a_.ToString(), out a);
		int.TryParse (b_.ToString(), out b);

		Debug.Log (a.ToString() +" * " + b.ToString() +" = "+(a*b).ToString());
	}

	private void test123(int a) 
	{
		Debug.Log (a);
	}

	private void execQueue()
	{
		if (queue != null && queue.Count == 0)
			return;

		foreach (List<SortingVisualItem> container_queue in queue)
		{
			MoveScript ms = GetMoveScriptByQueue(container_queue);
			if (ms == null) continue;

			ms.Swap(container_queue);
		}
	}

	private MoveScript GetMoveScriptByQueue(List<SortingVisualItem> visualItems)
	{
		foreach(SortingVisualItem item in visualItems)
		{
			if (item.Element1 == null) continue;

			return item.Element1.GetComponentInParent<MoveScript>();
		}
		return null;
	}
}
