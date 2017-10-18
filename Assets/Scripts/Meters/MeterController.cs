using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MeterController : MonoBehaviour
{
	private bool LeftBtnPressed;
	private bool RightBtnPressed;

	private int[] meterArray;
	private int[] meterArrayRight;

	private Vector3 currentScale;
	private Vector3 targetScale;
	[SerializeField]
	private float time = 2f;

	[SerializeField]
	private float newScale;
	[SerializeField]
	public int _resourceMeter;
	[SerializeField]
	private int blockNumber;
	private TextManager tM;
	[SerializeField]
	private bool waitCheck, fullMeterCheck;

	public delegate void GameOver();
	public static event GameOver GameOverStatus;

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
		_resourceMeter = 50;
		meterArray = new int[5];
	}

	void ScaleMeter()
	{
		currentScale = transform.localScale;

		TextManager tM = GameObject.Find("TextManager").GetComponent<TextManager>();
		meterArray = tM._meterNumber;
		meterArrayRight = tM._meterNumberRight;
		LeftBtnPressed = tM._LeftBtnPressed;
		RightBtnPressed = tM._RightBtnPressed;

		if (LeftBtnPressed)
		{
			AssignMeter(meterArray);
			LeftBtnPressed = false;
		}
		else if (RightBtnPressed)
		{
			AssignMeter(meterArrayRight);
			RightBtnPressed = false;
		}

		targetScale = currentScale + new Vector3(0, newScale, 0);

		if (!fullMeterCheck)
		{
			StopAllCoroutines();
			StartCoroutine(ScaleLerp());
		}

		FullMeter();
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

	void FullMeter()
	{
		if (_resourceMeter >= 100)
		{
			fullMeterCheck = true;
			targetScale = new Vector3(1f, 1f, 1f);
			StartCoroutine(ScaleLerp());
			GameOverStatus();
		}
		else if(_resourceMeter <= 0)
		{
			fullMeterCheck = true;
			targetScale = new Vector3(1f, 0.01f, 1f);
			StartCoroutine(ScaleLerp());
			GameOverStatus();
		}
	}

	void AssignMeter(int[] getArray)
	{
		switch (blockNumber) //Assigns the Right Variable to the right Meter
		{
			case 1:
				//print("case1: " + getArray[0]);
				newScale = ((float)getArray[0] / 100);
				_resourceMeter += getArray[0];
				break;
			case 2:
				//print("case2: " + getArray[1]);
				newScale = ((float)getArray[1] / 100);
				_resourceMeter += getArray[1];
				break;
			case 3:
				//print("case3: " + getArray[2]);
				newScale = ((float)getArray[2] / 100);
				_resourceMeter += getArray[2];
				break;
			case 4:
				//print("case4: " + getArray[3]);
				newScale = ((float)getArray[3] / 100);
				_resourceMeter += getArray[3];
				break;
			case 5:
				//print("case5: " + getArray[4]);
				newScale = ((float)getArray[4] / 100);
				_resourceMeter += getArray[4];
				break;
			default:
				Debug.Log("Meter Assignment Failed");
				break;
		}
	}
}
