using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour
{
	public event Action<Piece> PieceClickedEvent;

	public int i;
	public int j;

	public void Initialize(int i, int j)
	{
		this.i = i;
		this.j = j;
	}

	public void ChangeY(int j, Vector2 localPosition)
	{
		this.j = j;
		transform.localPosition = localPosition;
	}

	public void OnMouseDown()
	{
		if(PieceClickedEvent != null)
		{
			PieceClickedEvent(this);
		}
	}
}
