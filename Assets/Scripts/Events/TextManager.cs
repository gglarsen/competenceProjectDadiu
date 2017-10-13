using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextManager : MonoBehaviour
{
	[SerializeField]
	private Text nameText;
	[SerializeField]
	private Text eventDisplayText;
	[SerializeField]
	private Text leftResponse;
	[SerializeField]
	private Text rightResponse;

	public EventCards[] _eventArray;
	private EventCards eventCards;
	private int eventNumber = 0;

	[SerializeField]
	private int arraySize;
	[SerializeField]
	private bool waitCheck;

	void Start()
	{
		eventNumber = 0;
		eventCards = _eventArray[eventNumber];
		StartEvent();
		//print(_eventArray[eventNumber]);
		//print(_eventArray[eventNumber]._eventName);
	}

	public void StartEvent()    //starts event, gives event name and event text, 
								//scrolls throug event text.
	{
		if (waitCheck)	//check if scrolling is finished of current event.
		{
			return;
		}

		waitCheck = true;
		TrackNumber();
		nameText.text = eventCards._eventName;

		if (eventNumber > arraySize)
		{
			EndEvent();
			return;
		}
		StopAllCoroutines();
		StartCoroutine(LetterPopIn(eventCards._textForEvent, eventDisplayText));
		StartCoroutine(LetterPopIn(eventCards._responseLeft, leftResponse));
		StartCoroutine(LetterPopIn(eventCards._responseRight, rightResponse));

	}

	void TrackNumber()  //keeps track of what event is current
	{
		if (eventNumber < arraySize)
		{
			eventCards = _eventArray[eventNumber];
			eventNumber++;
		}
	}

	IEnumerator LetterPopIn(string scrollArray, Text locationText) //scroll text Coroutine
	{
		locationText.text = "";
		foreach (char letter in scrollArray.ToCharArray())
		{
			locationText.text += letter;
			yield return null;
		}
		waitCheck = false;
	}

	void EndEvent() //post that there is no more events.
	{
		Debug.Log("End of events");
	}
}
