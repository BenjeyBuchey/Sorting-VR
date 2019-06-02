using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System;

public class MoveScript : MonoBehaviour {

	private float swapSpeed = 1.5f, mergeDiffY = 10.0f;
	private List<SortingVisualItem> _visualItems;
	private GameObject sortingBox = null;
	private int initCounter = 0, _visualizationCounter = 0;
	private string _algorithm = null;
	private List<Vector3> initPositions = new List<Vector3>();
	private List<GameState> _gameStates = new List<GameState>();
	private bool isBusy = false, isMainVisualizationRunning = false;

	public GameObject SortingBox
	{
		get
		{
			return sortingBox;
		}

		set
		{
			sortingBox = value;
		}
	}

	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{

	}

	public void StepBackwards()
	{
		if (isBusy) return;

		if (_visualizationCounter <= _visualItems.Count && _visualizationCounter>0)
			StartCoroutine(DoStepBackwards());
	}

	public void StepBegin()
	{
		if (isBusy) return;

		if (_visualizationCounter <= _visualItems.Count && _visualizationCounter > 0)
			StartCoroutine(DoStepBegin());
	}

	public void StepForward()
	{
		if (isBusy) return;

		if(_visualizationCounter < _visualItems.Count)
			StartCoroutine(DoStepForward());
	}

	public void StepEnd()
	{
		if (isBusy) return;

		if (_visualizationCounter < _visualItems.Count)
			StartCoroutine(DoStepEnd());
	}

	public void Swap(List<SortingVisualItem> visualItems, string algorithm = null)
	{
		_visualItems = visualItems;
		_algorithm = algorithm;
		_visualizationCounter = 0;
		_gameStates.Clear();
		if (_visualItems == null || _visualItems.Count == 0) return;

		if (sortingBox == null)
			SetSortingBox();

		SetSortingboxUsage(true);
		InitByAlgorithm();
		StartCoroutine(DoSwap());
	}

	private void InitByAlgorithm()
	{
		if (_algorithm == null) return;

		switch(_algorithm)
		{
			case Algorithm.RADIXSORT:
			case Algorithm.MERGESORT:
				DeactivateSwapsCounter();
				SetInitialPositions();
				break;
			default:
				ActivateSwapCounter();
				break;
		}
	}

	public void ResumeVisualization()
	{
		if (_visualItems == null || _visualItems.Count == 0) return;

		if (!isMainVisualizationRunning)
			StartCoroutine(DoSwap());
	}

	IEnumerator DoSwap()
	{
		isMainVisualizationRunning = true;
		for (; _visualizationCounter < _visualItems.Count; _visualizationCounter++)
		{
			while (IsPaused() || isBusy) // completely break here? when resume is pressed start this coroutine ??
				yield return null;

            if (_visualizationCounter > 0)
                ChangeColors(_visualItems[_visualizationCounter - 1], true);

            if (_visualizationCounter >= _visualItems.Count)
			{
				Exit();
				isMainVisualizationRunning = false;
				yield break;
			}

			UpdateSwapSpeed(_visualItems[_visualizationCounter].Type);

			HandleVisualizationItem(_visualItems[_visualizationCounter]);

			yield return new WaitForSeconds(swapSpeed);
		}

        if (_visualizationCounter <= _visualItems.Count)
            ChangeColors(_visualItems[_visualizationCounter - 1], true);

        Exit();
		isMainVisualizationRunning = false;
	}

	IEnumerator DoStepForward()
	{
		isBusy = true;
		UpdateSwapSpeed(_visualItems[_visualizationCounter].Type);

		if (_visualizationCounter > 0)
			ChangeColors(_visualItems[_visualizationCounter - 1], true);

		if (_visualizationCounter >= _visualItems.Count) yield break;
		HandleVisualizationItem(_visualItems[_visualizationCounter]);

		yield return new WaitForSeconds(swapSpeed);

		_visualizationCounter++;

		// DO LAST ITEM HERE !?
		if (_visualizationCounter == _visualItems.Count)
			ChangeColors(_visualItems[_visualizationCounter - 1], true);

		isBusy = false;
	}

	IEnumerator DoStepEnd()
	{
		isBusy = true;
		swapSpeed = 0.1f;

		for (; _visualizationCounter < _visualItems.Count; _visualizationCounter++)
		{
			if (_visualizationCounter > 0)
				ChangeColors(_visualItems[_visualizationCounter - 1], true);

			if (_visualizationCounter >= _visualItems.Count)
			{
				Exit();
				yield break;
			}

			HandleVisualizationItem(_visualItems[_visualizationCounter]);

			yield return new WaitForSeconds(swapSpeed);
		}

		if (_visualizationCounter <= _visualItems.Count)
			ChangeColors(_visualItems[_visualizationCounter - 1], true);

		Exit();

		isBusy = false;
	}

	IEnumerator DoStepBackwards()
	{
		isBusy = true;

		if(_visualizationCounter < _gameStates.Count)
			SetColor(_gameStates[_visualizationCounter].ElementStates);

		GameState gameState = _gameStates[_visualizationCounter - 1];
		UpdateSwapSpeed(gameState.NextInstruction.Type);

		HandleVisualizationItemBackwards(gameState.NextInstruction, gameState.ElementStates);

		yield return new WaitForSeconds(swapSpeed);

		_visualizationCounter--;

		isBusy = false;
	}

	IEnumerator DoStepBegin()
	{
		isBusy = true;
		swapSpeed = 0.1f;

		for(;_visualizationCounter > 0; _visualizationCounter--)
		{
			if (_visualizationCounter < _gameStates.Count)
				SetColor(_gameStates[_visualizationCounter].ElementStates);

			if (_visualizationCounter <= 0)
			{
				Exit();
				yield break;
			}

			GameState gameState = _gameStates[_visualizationCounter - 1];

			HandleVisualizationItemBackwards(gameState.NextInstruction, gameState.ElementStates);

			yield return new WaitForSeconds(swapSpeed);
		}

		Exit();
		isBusy = false;
	}

	void HandleVisualizationItem(SortingVisualItem item)
	{
		switch (item.Type)
		{
			case (int)SortingVisualType.Swap:
				Debug.Log("FORWARD SWAP " + _visualizationCounter + " - GameStates Count: " +_gameStates.Count);
				HandleSwap(item);
				break;
			case (int)SortingVisualType.Comparison:
			case (int)SortingVisualType.MergeComparison:
				Debug.Log("FORWARD COMPARISON " + _visualizationCounter + " - GameStates Count: " + _gameStates.Count);
				HandleComparison(item);
				break;
			case (int)SortingVisualType.Radix:
				HandleRadix(item);
				break;
			case (int)SortingVisualType.MergeArray:
				HandleMergeArray(item);
				break;
			case (int)SortingVisualType.MergeMove:
				HandleMergeMove(item);
				break;
			case (int)SortingVisualType.MoveTo:
			case (int)SortingVisualType.MoveMemory:
				HandleMoveTo(item);
				break;
		}
	}

	void HandleVisualizationItemBackwards(SortingVisualItem item, List<ElementState> elementStates)
	{
		switch (item.Type)
		{
			case (int)SortingVisualType.Swap:
				Debug.Log("BACKWARDS SWAP " +(_visualizationCounter-1) + " - GameStates Count: " + _gameStates.Count);
				HandleSwapBackwards(item);
				break;
			case (int)SortingVisualType.Comparison:
			case (int)SortingVisualType.MergeComparison:
				Debug.Log("BACKWARDS COMPARISON " + (_visualizationCounter-1) + " - GameStates Count: " + _gameStates.Count);
				HandleComparisonBackwards(item);
				break;
			case (int)SortingVisualType.Radix:
                HandleRadixMoveBackwards(item, elementStates);
                break;
            case (int)SortingVisualType.MergeMove:
				HandleMoveBackwards(item, elementStates);
				break;
			case (int)SortingVisualType.MergeArray:
				HandleMergeArrayBackwards(item, elementStates);
				break;
			case (int)SortingVisualType.MoveTo:
			case (int)SortingVisualType.MoveMemory:
				HandleMoveToBackwards(item, elementStates);
				break;
		}
	}

	private void HandleMoveTo(SortingVisualItem item)
	{
		if (_gameStates.Count <= _visualizationCounter)
		{
			ElementState elementState1 = new ElementState(item.Element1, item.Element1.GetComponent<SingleElementScript>().GetColor(), item.Element1.transform.position);
			List<ElementState> elementStates = new List<ElementState> { elementState1 };
			_gameStates.Add(new GameState(elementStates, item));
		}

		ChangeColors(item, false);
		MoveElement(item.Element1, item.Dest);
		if (item.Type == (int)SortingVisualType.MoveTo)
			IncreaseSwapCounter();
		else
			IncreaseComparisonCounter();
	}

	private void HandleMoveToBackwards(SortingVisualItem item, List<ElementState> elementStates)
	{
		ChangeColors(item, false);
        MoveElementBackwards(item.Element1, elementStates[0].Position);
		if (item.Type == (int)SortingVisualType.MoveTo)
			DecreaseSwapCounter();
		else
			DecreaseComparisonCounter();
	}

	private void HandleMoveBackwards(SortingVisualItem item, List<ElementState> elementStates)
	{
		ChangeColors(item, false);
        MoveElementBackwards(item.Element1, elementStates[0].Position);
	}

    private void HandleRadixMoveBackwards(SortingVisualItem item, List<ElementState> elementStates)
    {
        ChangeColors(item, false);
        MoveElementBackwards(item.Element1, elementStates[0].Position);
    }

    private void MoveElement(GameObject element, Vector3 dest)
	{
		LeanTween.moveLocal(element, dest, swapSpeed);
	}

    private void MoveElementBackwards(GameObject element, Vector3 dest)
    {
        LeanTween.move(element, dest, swapSpeed);
    }

	private void HandleMergeMove(SortingVisualItem item)
	{
		if (_gameStates.Count <= _visualizationCounter)
		{
			ElementState elementState1 = new ElementState(item.Element1, item.Element1.GetComponent<SingleElementScript>().GetColor(), item.Element1.transform.position);
			List<ElementState> elementStates = new List<ElementState> { elementState1 };
			_gameStates.Add(new GameState(elementStates, item));
		}

		ChangeColors(item, false);
		MoveMerge(item.Element1, item.MergePosition);
	}

	private void MoveMerge(GameObject element1, int mergePosition)
	{
		Vector3 dest = GetMergeDestination(mergePosition);
		LeanTween.moveLocal(element1, dest, swapSpeed);
	}

	private void HandleMergeArrayBackwards(SortingVisualItem item, List<ElementState> elementStates)
	{
		ChangeColors(item, false);
		foreach(ElementState elementState in elementStates)
		{
            MoveElementBackwards(elementState.Element, elementState.Position);
		}
	}

	private void HandleMergeArray(SortingVisualItem item)
	{
		if (_gameStates.Count <= _visualizationCounter)
		{
			List<ElementState> elementStates = new List<ElementState>();
			foreach (GameObject element in item.Array)
			{
				elementStates.Add(new ElementState(element, element.GetComponent<SingleElementScript>().GetColor(), element.transform.position));
			}
			_gameStates.Add(new GameState(elementStates, item));
		}

		ChangeColors(item, false);
		MoveMergeArray(item.Array);
	}

	private void MoveMergeArray(GameObject[] array)
	{
		if (array == null) return;

		foreach(GameObject element in array)
		{
			Vector3 dest = element.transform.localPosition;
			dest.y += mergeDiffY;
			LeanTween.moveLocal(element, dest, swapSpeed);
		}
	}

	private void HandleRadix(SortingVisualItem item)
	{
		if (_gameStates.Count <= _visualizationCounter)
		{
			ElementState elementState1 = new ElementState(item.Element1, item.Element1.GetComponent<SingleElementScript>().GetColor(), item.Element1.transform.position);
			List<ElementState> elementStates = new List<ElementState> { elementState1 };
			_gameStates.Add(new GameState(elementStates, item));
		}

		ChangeColors(item, false);
		MoveRadix(item.Element1, item.Bucket, item.BucketPosition);
	}

	private void MoveRadix(GameObject element1, int bucket, int bucketPosition)
	{
		Vector3 dest = GetRadixDestination(bucket, bucketPosition, element1);
        Debug.Log("ELEMENT POSITION: " + element1.transform.position);
        if (bucket == -1)
            LeanTween.moveLocal(element1, dest, swapSpeed);
        else
            LeanTween.move(element1, dest, swapSpeed);
    }

	private void HandleComparison(SortingVisualItem item)
	{
		if (_gameStates.Count <= _visualizationCounter)
		{
			ElementState elementState1 = new ElementState(item.Element1, item.Element1.GetComponent<SingleElementScript>().GetColor(), item.Element1.transform.position);
			ElementState elementState2 = new ElementState(item.Element2, item.Element2.GetComponent<SingleElementScript>().GetColor(), item.Element2.transform.position);
			List<ElementState> elementStates = new List<ElementState> { elementState1, elementState2 };
			_gameStates.Add(new GameState(elementStates, item));
		}

		ChangeColors(item, false);
        IncreaseComparisonCounter();
	}

	private void HandleComparisonBackwards(SortingVisualItem item)
	{
		ChangeColors(item, false);
		DecreaseComparisonCounter();
	}

	private void HandleSwap(SortingVisualItem item)
	{
		if (_gameStates.Count <= _visualizationCounter)
		{
			ElementState elementState1 = new ElementState(item.Element1, item.Element1.GetComponent<SingleElementScript>().GetColor(), item.Element1.transform.position);
			ElementState elementState2 = new ElementState(item.Element2, item.Element2.GetComponent<SingleElementScript>().GetColor(), item.Element2.transform.position);
			List<ElementState> elementStates = new List<ElementState> { elementState1, elementState2 };
			_gameStates.Add(new GameState(elementStates, item));
		}

		ChangeColors(item, false);
		SwapElements(item.Element1, item.Element2);
		IncreaseSwapCounter();
	}

	private void SwapElements(GameObject element1, GameObject element2)
	{
		Vector3 dest1 = element2.transform.localPosition;
		Vector3 dest2 = element1.transform.localPosition;
		Vector3 rotationPoint = GetRotationPoint(element1, element2);

		float yOffset = GetOffsetY(element1, element2);
		Vector3 temp1 = rotationPoint;
		temp1.y = temp1.y + yOffset;

		Vector3 temp2 = rotationPoint;
		temp2.y = temp2.y - yOffset;

        LeanTween.moveLocal(element1, new Vector3[] { dest2, temp1, temp1, dest1 }, swapSpeed).setEaseInOutCubic();
		LeanTween.moveLocal(element2, new Vector3[] { dest1, temp2, temp2, dest2 }, swapSpeed).setEaseInOutCubic();
	}

	private void HandleSwapBackwards(SortingVisualItem item)
	{
		ChangeColors(item, false);
		SwapElements(item.Element2, item.Element1); // TODO: delete linerenderer
		DecreaseSwapCounter();
	}

	private void increaseCounter()
	{
		Text score = GameObject.Find ("SwapCounter").GetComponent<Text> ();

		if (score == null)
			return;

		score.GetComponent<SwapCounterScript> ().incCounter ();
	}

	private void SetSortingBox()
	{
		sortingBox = gameObject;

		// set in use
		SortingBoxScript sbs = sortingBox.GetComponent<SortingBoxScript>();
		sbs.setInUse(true);
	}

	private void DeactivateSwapsCounter()
	{
		SortingBoxScript sbs = sortingBox.GetComponentInParent<SortingBoxScript>();
		if (sbs == null) return;

		sbs.DeactivateSwapsCounter();
	}

	private void ActivateSwapCounter()
	{
		SortingBoxScript sbs = sortingBox.GetComponentInParent<SortingBoxScript>();
		if (sbs == null) return;

		sbs.ActivateSwapsCounter();
	}

	private void IncreaseSwapCounter()
	{
		SortingBoxScript sbs = sortingBox.GetComponentInParent<SortingBoxScript>();
		if (sbs == null) return;

		sbs.incSwapsCounter();
	}

	private void DecreaseSwapCounter()
	{
		SortingBoxScript sbs = sortingBox.GetComponentInParent<SortingBoxScript>();
		if (sbs == null) return;

		sbs.DecreaseSwapsCounter();
	}

	private void IncreaseComparisonCounter()
	{
		if (sortingBox == null) return;

		SortingBoxScript sbs = sortingBox.GetComponentInParent<SortingBoxScript>();
		if (sbs == null) return;

		sbs.IncComparisonCounter();
	}

	private void DecreaseComparisonCounter()
	{
		if (sortingBox == null) return;

		SortingBoxScript sbs = sortingBox.GetComponentInParent<SortingBoxScript>();
		if (sbs == null) return;

		sbs.DecreaseComparisonCounter();
	}

	private void ChangeColors(SortingVisualItem item, bool isDefaultColor)
	{
		MoveHelperScript mhs = new MoveHelperScript();
		switch(item.Type)
		{
			case (int)SortingVisualType.MergeArray:
				mhs.ChangeColor(item.Array, item.Type, item.IsLeftArray, isDefaultColor);
				break;
			default:
				mhs.ChangeColor(item.Element1, item.Element2, item.Type, isDefaultColor);
				break;
		}
	}

	private void SetColor(List<ElementState> elementStates)
	{
		MoveHelperScript mhs = new MoveHelperScript();
		foreach(ElementState elementState in elementStates)
		{
			mhs.SetColor(elementState.Element, elementState.Color);
		}
	}

	private Vector3 GetRotationPoint(GameObject element1, GameObject element2)
	{
		float distance = Mathf.Abs(element1.transform.localPosition.x - element2.transform.localPosition.x);
		float x = 0.0f;
		if (element1.transform.localPosition.x > element2.transform.localPosition.x)
			x = element1.transform.localPosition.x - distance / 2;
		else
			x = element1.transform.localPosition.x + distance / 2;

		return new Vector3(x, element1.transform.localPosition.y, element1.transform.localPosition.z);
	}

	private float GetOffsetY(GameObject element1, GameObject element2)
	{
		if (element1 == null || element2 == null) return 0.0f;

		return sortingBox.GetComponent<SortingBoxScript>().getOffsetY(element1, element2);
	}

	private void SetSortingboxUsage(bool isInUse)
	{
		if (sortingBox == null) return;

		SortingBoxScript sbs = sortingBox.GetComponent<SortingBoxScript>();
		if (sbs == null) return;

		sbs.setInUse(isInUse);
	}

	private void UpdateSwapSpeed(int type)
	{
		MoveHelperScript mhs = new MoveHelperScript();
		swapSpeed = mhs.GetSwapSpeed(type);
	}

	private bool IsPaused()
	{
		return HelperScript.IsPaused();
	}

	private Vector3 GetRadixDestination(int bucket, int position, GameObject go)
	{
        if (initCounter >= initPositions.Count)
            initCounter = 0;

        float object_width = GetObjectWidth();
        BucketScript bs = gameObject.GetComponentInChildren<BucketScript>();
        if (bs == null) return Vector3.zero;

        List<Transform> bucketObjects = bs.GetBucketObjects();
        if (bucketObjects == null || bucketObjects.Count < bucket)
            return Vector3.zero;

        Vector3 dest = Vector3.zero;
        // bucket -1 --> move to init positions
        if (bucket == -1)
        {
            if (initCounter > initPositions.Count || initCounter < 0)
                Debug.Log("INIT COUNTER: " + initCounter);
            dest = initPositions[initCounter];
            initCounter++;
        }
        else
        {
            float xDiff = 5f * sortingBox.transform.localScale.x;
            float xOffset = xDiff + xDiff * position; // +5f
            dest = bucketObjects[bucket].position; // position
            //dest.y = dest.y - bucketObjects[bucket].localScale.y / 2; //half bucket text size
            dest.x = dest.x + xOffset;
            Debug.Log("DEST: " + dest);
        }

        return dest;
	}

	private Vector3 GetMergeDestination(int mergePosition)
	{
		if (initPositions == null || initPositions.Count < (mergePosition + 1)) return Vector3.zero;

		return initPositions[mergePosition];
	}

	private float GetObjectWidth()
	{
		if (sortingBox == null) return 1.0f;

		SortingBoxScript sbs = sortingBox.GetComponent<SortingBoxScript>();
		if (sbs == null) return 1.0f;

		return sbs.GetMaxObjectWidth();
	}

	private void SetInitialPositions()
	{
		if (sortingBox == null) return;

		SortingBoxScript sbs = sortingBox.GetComponent<SortingBoxScript>();
		if (sbs == null) return;

		//initPositions.Clear();
		initPositions = sbs.getInitialPositionList();
	}

	private void Exit()
	{
		Debug.Log("EXITED DoSwap! visualizationCounter: " + _visualizationCounter + " - Items: " + _visualItems.Count);
		SetSortingboxUsage(false);
		//Destroy(this);
	}
}
