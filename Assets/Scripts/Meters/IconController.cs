using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IconController : MonoBehaviour
{
	[SerializeField]
	private int sphereNumber; //start at 0 plz
	[SerializeField]
	private Vector3 fullScale = new Vector3(1f, .2f, .5f);
	[SerializeField]
	private Vector3 zeroScale = new Vector3(0f, .0f, 0f);
	private bool[] checkArray, checkArrayRight, usedArray;
	[SerializeField]
	private TextManager textManager;

	private Vector3 currentScale;
	[SerializeField]
	private float time = 1f;

	private bool LeftBtnPressed;
	private bool RightBtnPressed;

	void OnEnable()
	{
		TextManager.CheckActive += DeselectOldSpheres;
		TextManager.CheckActive += SphereGetBtnPress;
	}

	void OnDisable()
	{
		TextManager.CheckActive -= DeselectOldSpheres;
		TextManager.CheckActive -= SphereGetBtnPress;
	}

	private void SphereGetBtnPress()
	{
		LeftBtnPressed = textManager._LeftBtnPressed;
		RightBtnPressed = textManager._RightBtnPressed;

		if (LeftBtnPressed)
		{
			IncreaseScaleLeft();
			LeftBtnPressed = false;
		}
		if (RightBtnPressed)
		{
			IncreaseScaleRight();
			RightBtnPressed = false;
		}
	}

	private void Start()
	{
		transform.localScale = zeroScale;
		checkArray = new bool[5];
		checkArrayRight = new bool[5];
	}

	public void IncreaseScaleLeft()
	{
		transform.localScale = zeroScale;
		checkArray = textManager.GetComponent<TextManager>()._sphereCheck;

		if (checkArray[sphereNumber])
		{
			StopAllCoroutines();
			StartCoroutine(ScaleLerpSphere(fullScale));
		}
	}

	public void IncreaseScaleRight()
	{
		transform.localScale = zeroScale;
		checkArrayRight = textManager.GetComponent<TextManager>()._sphereCheckRight;

		if (checkArrayRight[sphereNumber])
		{
			StopAllCoroutines();
			StartCoroutine(ScaleLerpSphere(fullScale));
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
			transform.localScale = Vector3.Lerp(currentScale, targetScale, .2f);
			elapsedTime += Time.deltaTime;
			yield return null;
		}
		transform.localScale = targetScale;
		//waitCheck = false;
		yield return null;
	}
}
