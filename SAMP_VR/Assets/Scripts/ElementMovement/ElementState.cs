using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementState {

	private readonly GameObject _element;
	private readonly Color _color;
	private readonly Vector3 _position;

	public ElementState(GameObject element, Color color, Vector3 position)
	{
		_element = element;
		_color = color;
		_position = position;
	}

	public GameObject Element
	{
		get
		{
			return _element;
		}
	}

	public Color Color
	{
		get
		{
			return _color;
		}
	}

	public Vector3 Position
	{
		get
		{
			return _position;
		}
	}
}
