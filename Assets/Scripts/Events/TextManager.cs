using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextManager : MonoBehaviour
{
	[SerializeField]
	public bool _LeftBtnPressed;
	[SerializeField]
	public bool _RightBtnPressed;

	[SerializeField]
	private Text nameText;
	[SerializeField]
	private Text eventDisplayText;
	[SerializeField]
	private Text leftResponse;
	[SerializeField]
	private Text rightResponse;
	[SerializeField]
	private GameObject gameManager;

	public EventCards[] _eventArray;
	private EventCards[] shuffelArray;
	private EventCards eventCards;
	private int eventNumber = 0;

	[SerializeField]
	private int arraySize;
	[SerializeField]
	private bool waitCheck;

	[SerializeField]
	public bool[] _sphereCheck;
	[SerializeField]
	public bool[] _sphereCheckRight;
	[SerializeField]
	public int[] _meterNumber;
	[SerializeField]
	public int[] _meterNumberRight;
	private EventCards eventCardsBehind;
	//private bool firstTurnCheck;

	public delegate void ScaleAction();
	public static event ScaleAction MeterActive;

	public delegate void CheckAction();
	public static event CheckAction CheckActive;

	void Start()
	{
		_meterNumber = new int[5];
		_meterNumberRight = new int[5];
		_sphereCheck = new bool[5];
		_sphereCheckRight = new bool[5];

		ShuffelEvents();

		eventNumber = 0;
		eventCards = _eventArray[eventNumber];

		//StartEvent();
		//print(_eventArray[eventNumber]);
		//print(_eventArray[eventNumber]._eventName);
	}

	public void GetButtonPressedLeft()
	{
		_LeftBtnPressed = true;
	}

	public void GetButtonPressedRight()
	{
		_RightBtnPressed = true;
	}

	public void StartEvent()    //starts event, gives event name and event text, 
								//scrolls throug event text.
	{
		bool gameOverFlag = gameManager.GetComponent<GameOver>()._gameOverFlag;
		if (waitCheck || gameOverFlag)  //check if scrolling is finished of current event.
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
			MeterPass(eventCardsBehind); //track meter numbers, -1 to not change before btnPress;
			MeterActive(); //Send scale event to Meters
		}

		TrackNumber();

		CheckPass(); // assign boolchecks for spheres to array;
		CheckActive(); // send new onEnter update to update spheres;

		gameOverFlag = gameManager.GetComponent<GameOver>()._gameOverFlag;
		if (!gameOverFlag)
		{
			nameText.text = eventCards._eventName;
		}

		if (eventNumber > arraySize)
		{
			EndEvent();
			return;
		}

		_LeftBtnPressed = false;
		_RightBtnPressed = false;

		StopAllCoroutines();
		if (!gameManager.GetComponent<GameOver>()._gameOverFlag)
		{
			StartCoroutine(LetterPopIn(eventCards._textForEvent, eventDisplayText, true));
			StartCoroutine(LetterPopIn(eventCards._responseLeft, leftResponse, false));
			StartCoroutine(LetterPopIn(eventCards._responseRight, rightResponse, false));
		}
	}

	void TrackNumber()  //keeps track of what event is current
	{
		if (eventNumber < arraySize)
		{
			eventCards = _eventArray[eventNumber];
			eventNumber++;
		}
	}

	IEnumerator LetterPopIn(string scrollArray, Text locationText, bool waitForTextCheck) //scroll text Coroutine
	{
		locationText.text = "";
		foreach (char letter in scrollArray.ToCharArray())
		{
			locationText.text += letter;
			yield return null;
		}
		if (waitForTextCheck)
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

		_meterNumberRight[0] = numberPass._meterEgoRight;
		_meterNumberRight[1] = numberPass._meterSwampRight;
		_meterNumberRight[2] = numberPass._meterInternationalRight;
		_meterNumberRight[3] = numberPass._meterBudgetRight;
		_meterNumberRight[4] = numberPass._meterEnergyRight;
	}

	void CheckPass() //assigns the correct 
	{
		_sphereCheck[0] = eventCards._checkEgo;
		_sphereCheck[1] = eventCards._checkSwamp;
		_sphereCheck[2] = eventCards._checkInternational;
		_sphereCheck[3] = eventCards._checkBudget;
		_sphereCheck[4] = eventCards._checkEnergy;

		_sphereCheckRight[0] = eventCards._checkEgoRight;
		_sphereCheckRight[1] = eventCards._checkSwampRight;
		_sphereCheckRight[2] = eventCards._checkInternationalRight;
		_sphereCheckRight[3] = eventCards._checkBudgetRight;
		_sphereCheckRight[4] = eventCards._checkEnergyRight;
	}

	private void ShuffelEvents()
	{
		for (int i = 0; i < arraySize; i++) // shuffels the events
		{
			EventCards shuffelArray = _eventArray[i];
			int randomIndex = Random.Range(i, arraySize);
			_eventArray[i] = _eventArray[randomIndex];
			_eventArray[randomIndex] = shuffelArray;
		}
	}
}
