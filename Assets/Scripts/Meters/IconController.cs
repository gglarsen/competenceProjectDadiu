using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Gets the array with Sphere boolChecks from TextManager. determines if spheres own number = boolCheck in array.
 * sphereMarkers are shown on howering over left and right response.
 */

public class IconController : MonoBehaviour
{
	[SerializeField]
	private int sphereNumber; //start at 0 plz
	[SerializeField]
	private Vector3 upScale = new Vector3(1f, .2f, .5f);
	[SerializeField]
	private Vector3 zeroScale = new Vector3(0f, .0f, 0f);
	private bool[] checkArray, checkArrayRight, usedArray;
	private float[] scaleFactor;
	[SerializeField]
	private float sphereSizeFactor = 3;
	[SerializeField]
	private TextManager textManager;
	//[SerializeField]
	//private MeterController meterController;

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

	private void SphereGetBtnPress() // get what button is clicked
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
		scaleFactor = new float[5];
	}

	public void IncreaseScaleLeft()
	{
		transform.localScale = zeroScale;
		checkArray = textManager.GetComponent<TextManager>()._sphereCheckLeft;

		if (checkArray[sphereNumber])
		{
			StopAllCoroutines();
			for (int i = 0; i < 5; i++)
			{
				scaleFactor[i] = textManager._meterNumber[i];
			}
			upScale.x = 1f * Mathf.Log(Mathf.Abs(scaleFactor[sphereNumber])) / sphereSizeFactor;
			upScale.y = .2f * Mathf.Log(Mathf.Abs(scaleFactor[sphereNumber])) / sphereSizeFactor;
			upScale.z = .5f * Mathf.Log(Mathf.Abs(scaleFactor[sphereNumber])) / sphereSizeFactor;
			StartCoroutine(ScaleLerpSphere(upScale));
		}
	}

	public void IncreaseScaleRight()
	{
		transform.localScale = zeroScale;
		checkArrayRight = textManager.GetComponent<TextManager>()._sphereCheckRight;

		if (checkArrayRight[sphereNumber])
		{
			StopAllCoroutines();
			for (int i = 0; i < 5; i++)
			{
				scaleFactor[i] = textManager._meterNumberRight[i];
				//print("nr: " + sphereNumber + " number: " + textManager._meterNumberRight[i]);
			}
			upScale.x = 1f * Mathf.Log(Mathf.Abs(scaleFactor[sphereNumber]))/ sphereSizeFactor;
			upScale.y = .2f * Mathf.Log(Mathf.Abs(scaleFactor[sphereNumber]))/ sphereSizeFactor;
			upScale.z = .5f * Mathf.Log(Mathf.Abs(scaleFactor[sphereNumber]))/ sphereSizeFactor;
			StartCoroutine(ScaleLerpSphere(upScale));
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

	private IEnumerator ScaleLerpSphere(Vector3 targetScale) // Tween scale of sphere, 
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
		yield return null;
	}
}
