using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuckGrid : MonoBehaviour
{
	[SerializeField]
	static public Vector3 ds, d1, d2, d3;
	static public Vector3[] _duckPositionArray;
    static public bool[] _gridFull;
	[SerializeField]
	private Vector3 start, line1, line2, line3;

	private void Awake()
	{
        _gridFull = new bool[4];

		ds = start;
		d1 = line1;
		d2 = line2;
		d3 = line3;
		_duckPositionArray = new[] {ds, d1, d2, d3};
	}
}
//Spot Coordinates
//0: -1, 18, -9,24
//1: 6.43
//2: 4.08
//3: 1.41
