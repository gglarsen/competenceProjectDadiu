using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextManager : MonoBehaviour
{
	public Text nameText;
	public Text eventDisplayText;

	private Queue<string> events;

	void Start()
	{
		events = new Queue<string>();
	}

	public void StartEvent(EventText eventText)
	{
		nameText.text = eventText.name;

		events.Clear();

		foreach (string nrEvent in eventText.textForEvent)
		{
			events.Enqueue(nrEvent);
		}

		DisplayNextEventText();
	}

	public void DisplayNextEventText()
	{
		if (events.Count == 0)
		{
			EndEvent();
			return;
		}

		string nrEvent = events.Dequeue();
		//eventDisplayText.text = nrEvent;
		StopCoroutine(LetterPopIn(nrEvent));
		StartCoroutine(LetterPopIn(nrEvent));
	}

	IEnumerator LetterPopIn (string nrEvent)
	{
		eventDisplayText.text = "";
		foreach(char letter in nrEvent.ToCharArray())
		{
			eventDisplayText.text += letter;
			yield return null;
		}
	}

	void EndEvent()
	{
		Debug.Log("End of events");
	}
}
