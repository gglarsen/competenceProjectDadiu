using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NameBounce : MonoBehaviour
{
	private float elapsedTime;
	private Vector3 bouncePosition, currentPosition;
	[SerializeField]
	private float bounceSize = 3;

	[SerializeField]
	private MeterController meterStatus;
	private int currentStatus;
	[SerializeField]
	private int dangerMax = 80, dangerMin = 20;

	void Start()
	{
		currentPosition = transform.position;
	}

	// Update is called once per frame
	void Update()
	{
		currentStatus = meterStatus._resourceMeter;
		if (currentStatus >= dangerMax || currentStatus <= dangerMin) // activate bounce if meter is close to end;
		{
			AnimateBounce();
		}
	}

	void AnimateBounce()
	{
		elapsedTime += Time.deltaTime;
		float cycles = .5f;
		float bounce = (Mathf.Sin(2 * cycles * Mathf.PI * elapsedTime) / 4);
		bouncePosition = new Vector3(currentPosition.x + (bounce * bounceSize),
			currentPosition.y, currentPosition.z);

		if (bounce > 0)
		{
			transform.position = bouncePosition;
		}
		//print("elapsedTime" + elapsedTime);
		//print("bounce: " + bounce);
	}
}
