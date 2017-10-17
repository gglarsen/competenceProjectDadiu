using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MeterController : MonoBehaviour
{
	private int[] meterArray;

	private Vector3 currentScale;
	private Vector3 targetScale;
	[SerializeField]
	private float time = 1f;


	[SerializeField]
	private float newScale;
	[SerializeField]
	private int blockNumber;
	private TextManager tM;
	[SerializeField]
	private bool waitCheck;

	void OnEnable()
	{
		TextManager.MeterActive += ScaleMeter;
	}

	void OnDisable()
	{
		TextManager.MeterActive -= ScaleMeter;
	}

	void Start()
	{
		meterArray = new int[5];
		AssignMeter();
	}

	void ScaleMeter()
	{
		TextManager tM = GameObject.Find("TextManager").GetComponent<TextManager>();
		meterArray = tM._meterNumber;
		AssignMeter();

		currentScale = transform.localScale;
		targetScale = currentScale + new Vector3(0, newScale, 0);
		StartCoroutine(ScaleLerp());
	}

	private IEnumerator ScaleLerp() // transfer this event to the next 
	{
		float elapsedTime = 0;
		while (elapsedTime < time)
		{
			currentScale = transform.localScale;
			transform.localScale = Vector3.Lerp(currentScale, targetScale, .1f);
			elapsedTime += Time.deltaTime;
			yield return null;
		}
		transform.localScale = targetScale;
		waitCheck = false;
		yield return null;
	}

	void AssignMeter()
	{
		switch(blockNumber) //Assigns the Right Variable to the right Meter
		{
			case 1:
				newScale = ((float)meterArray[0] / 100);
				break;
			case 2:
				newScale = ((float)meterArray[1] / 100);
				break;
			case 3:
				newScale = ((float)meterArray[2] / 100);
				break;
			case 4:
				newScale = ((float)meterArray[3] / 100);
				break;
			case 5:
				newScale = ((float)meterArray[4] / 100);
				break;
			default:
				Debug.Log("Meter Assignment Failed");
				break;
		}
	}
}
