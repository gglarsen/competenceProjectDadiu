using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextManager : MonoBehaviour {

	private Queue<string> events;

	void Start()
	{
		events = new Queue<string>();
	}

	public void StartEvent(EventText eventText)
	{
		Debug.Log("Starting Conversation with " + eventText.name);
	}
}
