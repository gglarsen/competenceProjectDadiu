using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IconController : MonoBehaviour {

	private Vector3 fullScale = new Vector3(1f, .2f, .5f);
	private Vector3 zeroScale = new Vector3(0f, .0f, 0f);

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void IncreaseScale()
	{
		transform.localScale = fullScale;
	}

	public void DecreaseScale()
	{
		transform.localScale = zeroScale;
	}
}
