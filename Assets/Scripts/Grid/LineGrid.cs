using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*Defines the grid that is used by the Characters/blocks
*/
public class LineGrid : MonoBehaviour
{
	[SerializeField]
	static public Vector3 sp, l1, l2, l3, l4, l5, ep;
	static public Vector3[] positionArray;
	[SerializeField]
	private Vector3 start, line1, line2, line3, line4, line5, end;

	private void Awake()
	{
		sp = start;
		l1 = line1;
		l2 = line2;
		l3 = line3;
		l4 = line4;
		l5 = line5;
		ep = end;
		positionArray = new[] {sp, l1, l2, l3, l4, l5, end};
	}
}
//Spot Coordinates
//0: 1, 1, 30
//1: 1, 1, 11
//2: 1, 1, 9.5
//3: 1, 1, 8
//4: 1, 1, 6.5
//5: 1, 1, 5
//6: -30, 1, 5 