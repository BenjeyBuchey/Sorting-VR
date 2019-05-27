using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState {

	private readonly List<ElementState> _elementStates;
	private readonly SortingVisualItem _nextInstruction;

	public GameState(List<ElementState> elementStates, SortingVisualItem nextInstruction)
	{
		_elementStates = elementStates;
		_nextInstruction = nextInstruction;
	}

	public List<ElementState> ElementStates
	{
		get
		{
			return _elementStates;
		}
	}

	public SortingVisualItem NextInstruction
	{
		get
		{
			return _nextInstruction;
		}
	}
}
