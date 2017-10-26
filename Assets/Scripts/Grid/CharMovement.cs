using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Handles the roating "characters"/blocks that stands in a quoue.
 * 
 */ 
public class CharMovement : MonoBehaviour
{
	[SerializeField]
	private int startCharPosition;
	private Vector3 currentPosition, axisOffset;
	private int numberOfSpots = 6;
	private int mySpot = 0;
	private float randomOffset = .3f;
	private bool gm, timerCheck, getManagerCheck;

	public GameObject getManager = null;

	void Start()
	{
		currentPosition = transform.position;
		axisOffset.x = Random.Range(-randomOffset, randomOffset);

		switch (startCharPosition) // set start chars to their spot, fly in.
		{
			case 1:
				mySpot = 5;
				break;
			case 2:
				mySpot = 4;
				break;
			case 3:
				mySpot = 3;
				break;
			case 4:
				mySpot = 2;
				break;
			case 5:
				mySpot = 1;
				break;
			default:
				mySpot = 0;
				break;
		}
	}

	void Update()
	{
		currentPosition = transform.position;
		transform.position = Vector3.Lerp(currentPosition,
				LineGrid.positionArray[mySpot] + axisOffset, 0.03f); // Get pos of characters spot, then lerp.
		DestroyChar();

		if (!getManagerCheck)
		{
			getManager = GameObject.Find("LineManager");
			getManagerCheck = true;
		}
		gm = getManager.GetComponent<BtnGetChar>().triggerMovement;

		if (gm && !timerCheck) // Get button click and check timer
		{
			timerCheck = true;
			StartCoroutine(waitTimer());
			TriggerCharacters();
		}
	}

	void DestroyChar() // Destroy char if at final spot
	{
		if (mySpot == numberOfSpots)
		{
			Destroy(gameObject, 1.5f);
		}
	}

	public void TriggerCharacters() // Increase spot nr, new random offset;
	{
		if (mySpot < numberOfSpots)
		{
			axisOffset.x = Random.Range(-randomOffset, randomOffset);
			mySpot++;
		}
	}

	private IEnumerator waitTimer()
	{
		yield return new WaitForSeconds(1f);
		timerCheck = false;
	}
}

