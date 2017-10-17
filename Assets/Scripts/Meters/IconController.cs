using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IconController : MonoBehaviour
{
	[SerializeField]
	private int sphereNumber; //start at 0 plz

	private Vector3 fullScale = new Vector3(1f, .2f, .5f);
	private Vector3 zeroScale = new Vector3(0f, .0f, 0f);
	private bool[] checkArray;
	[SerializeField]
	private TextManager textManager;

	private Vector3 currentScale;
	private float time = 1f;

	void OnEnable()
	{
		TextManager.CheckActive += DeselectOldSpheres;
		TextManager.CheckActive += IncreaseScale;
	}

	void OnDisable()
	{
		TextManager.CheckActive -= DeselectOldSpheres;
		TextManager.CheckActive -= IncreaseScale;
	}

	private void Start()
	{
		checkArray = new bool[5];
	}

	public void IncreaseScale()
	{
		transform.localScale = zeroScale;
		checkArray = textManager.GetComponent<TextManager>()._sphereCheck;
		//print("array: " + checkArray[0] + checkArray[1] + checkArray[2] +
		//	checkArray[3] + checkArray[4]);

		if (checkArray[sphereNumber])
		{
			StopAllCoroutines();
			StartCoroutine(ScaleLerpSphere(fullScale));
			//transform.localScale = fullScale;
		}
	}

	public void DecreaseScale()
	{
		StopAllCoroutines();
		StartCoroutine(ScaleLerpSphere(zeroScale));
	}

	public void DeselectOldSpheres() //Scale down old spheres, prevents coroutines to override scale
	{
		if (!checkArray[sphereNumber])
		{
			StopAllCoroutines();
			StartCoroutine(ScaleLerpSphere(zeroScale));
		}
	}

	private IEnumerator ScaleLerpSphere(Vector3 targetScale) // transfer this event to the next 
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
		//waitCheck = false;
		yield return null;
	}
}
