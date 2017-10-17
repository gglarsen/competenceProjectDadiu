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

	[SerializeField]
	public bool[] _sphereCheck;
	[SerializeField]
	public int[] _meterNumber;
	private EventCards eventCardsBehind;
	//private bool firstTurnCheck;

	public delegate void ScaleAction();
	public static event ScaleAction MeterActive;

	public delegate void CheckAction();
	public static event CheckAction CheckActive;

	void Start()
	{
		_meterNumber = new int[5];
		_sphereCheck = new bool[5];
		eventNumber = 0;
		eventCards = _eventArray[eventNumber];
		//StartEvent();
		//print(_eventArray[eventNumber]);
		//print(_eventArray[eventNumber]._eventName);
	}

	public void StartEvent()    //starts event, gives event name and event text, 
								//scrolls throug event text.
	{
		if (waitCheck)  //check if scrolling is finished of current event.
		{
			return;
		}
		waitCheck = true;

		if (eventNumber == 0) //if first time, make sure to not go below arraySize;
		{
			MeterPass(eventCards); //track meter numbers;
		}
		else if (eventNumber > 0)
		{
			eventCardsBehind = _eventArray[eventNumber - 1];
			MeterPass(eventCardsBehind); //track meter numbers, -1 to not change before decision;
			MeterActive(); //Send scale event to Meters
		}

		TrackNumber();

		CheckPass(); // assign boolchecks for spheres to array;
		CheckActive(); // send new onEnter update to update spheres;

		nameText.text = eventCards._eventName;

		if (eventNumber > arraySize)
		{
			EndEvent();
			return;
		}
		StopAllCoroutines();
		StartCoroutine(LetterPopIn(eventCards._textForEvent, eventDisplayText, true));
		StartCoroutine(LetterPopIn(eventCards._responseLeft, leftResponse, false));
		StartCoroutine(LetterPopIn(eventCards._responseRight, rightResponse, false));
	}

	void TrackNumber()  //keeps track of what event is current
	{
		if (eventNumber < arraySize)
		{
			eventCards = _eventArray[eventNumber];
			eventNumber++;
		}
	}

	IEnumerator LetterPopIn(string scrollArray, Text locationText, bool check) //scroll text Coroutine
	{
		locationText.text = "";
		foreach (char letter in scrollArray.ToCharArray())
		{
			locationText.text += letter;
			yield return null;
		}
		if (check)
		{
			waitCheck = false;
		}
	}

	void EndEvent() //post that there is no more events.
	{
		Debug.Log("End of events");
	}

	void MeterPass(EventCards numberPass) //assigns the correct 
	{
		_meterNumber[0] = numberPass._meterEgo;
		_meterNumber[1] = numberPass._meterSwamp;
		_meterNumber[2] = numberPass._meterInternational;
		_meterNumber[3] = numberPass._meterBudget;
		_meterNumber[4] = numberPass._meterEnergy;
	}

	void CheckPass() //assigns the correct 
	{
		_sphereCheck[0] = eventCards._checkEgo;
		_sphereCheck[1] = eventCards._checkSwamp;
		_sphereCheck[2] = eventCards._checkInternational;
		_sphereCheck[3] = eventCards._checkBudget;
		_sphereCheck[4] = eventCards._checkEnergy;
	}
}
