using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NameBounce : MonoBehaviour
{
	private float elapsedTime;
	private Vector3 bouncePosition, currentPosition;
	[SerializeField]
	private int bounceSize = 5;

	void Start()
	{
		currentPosition = transform.position;
	}

	// Update is called once per frame
	void Update()
	{
		elapsedTime += Time.deltaTime;
		float cycles = .5f;
		float bounce = (Mathf.Sin(2 * cycles * Mathf.PI * elapsedTime) / 4);
		bouncePosition = new Vector3(currentPosition.x + (bounce * bounceSize), currentPosition.y, currentPosition.z);
		if (bounce > 0)
		{
			transform.position = bouncePosition;
		}
		print("elapsedTime" + elapsedTime);
		print("bounce: " + bounce);
	}
}
