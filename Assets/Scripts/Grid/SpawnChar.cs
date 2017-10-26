using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/* spawns the characters
 */
public class SpawnChar : MonoBehaviour
{
	[SerializeField]
	private Transform charPrefab;
	private bool timerCheck;

	public void InstantiateChar() // instantiate new char at start pos.
	{
		if (!timerCheck)
		{
			timerCheck = true;
			StartCoroutine(waitTimer());
			Instantiate(charPrefab, LineGrid.positionArray[0], Quaternion.identity);
		}
	}

	private IEnumerator waitTimer()
	{
		yield return new WaitForSeconds(1f);
		timerCheck = false;
	}
}
