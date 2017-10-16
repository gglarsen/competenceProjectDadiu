using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class BtnGetChar : MonoBehaviour
{
	public bool triggerMovement;

	public void ClickMovement()
	{
		//print(triggerMovement);
		if (!triggerMovement)
		{
			StartCoroutine(MovementTriggerIE());
		}
	}

	private IEnumerator MovementTriggerIE()
	{
		triggerMovement = true;
		//print("triggerMovement " + triggerMovement);
		yield return new WaitForSeconds(1f);
		triggerMovement = false;
	}
}