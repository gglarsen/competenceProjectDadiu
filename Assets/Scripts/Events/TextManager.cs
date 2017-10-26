using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/* Gets the cards from the scriptableObjects and puts them into _eventArray, starts with shuffeling the array
 * and then assings the currentCard as the first in the Array. Increments cardNumber each at each Card to update the
 * currentCard. 
 * Handles all the systems of the cardText, resourceMeters and sphereMarkers.
 * Uses gameOver flag, btnPressed flags, duckClick flag
 */

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
    private EventCards[] shuffelArrayHolder;
    private EventCards currentCard;
    private int cardNumber = 0;

    [SerializeField]
    private int arraySize;
    [SerializeField]
    public static bool _waitCheck;

    [SerializeField]
    public bool[] _sphereCheckLeft;
    [SerializeField]
    public bool[] _sphereCheckRight;
    [SerializeField]
    public int[] _meterNumber;
    [SerializeField]
    public int[] _meterNumberRight;
    public int[] _resourceCount;
    private EventCards eventCardsBehind;

    private bool duckCardPlayed;

    public delegate void ScaleAction();
    public static event ScaleAction MeterActive;

    public delegate void CheckAction();
    public static event CheckAction CheckActive;

    public delegate void DuckAction();
    public static event DuckAction DuckSpawnActive;

    void OnEnable()
    {
        DuckMovement.DuckActive += DuckCardPlayed;
    }

    void OnDisable()
    {
        DuckMovement.DuckActive -= DuckCardPlayed;
    }

    void Start()
    {
        _meterNumber = new int[5];
        _meterNumberRight = new int[5];
        _sphereCheckLeft = new bool[5];
        _sphereCheckRight = new bool[5];
        _resourceCount = new int[5];

        ShuffelEvents();

        cardNumber = 0;
        currentCard = _eventArray[cardNumber];
    }

    public void GetButtonPressedLeft()
    {
        _LeftBtnPressed = true;
    }

    public void GetButtonPressedRight()
    {
        _RightBtnPressed = true;
    }

    void DuckCardPlayed()
    {
        duckCardPlayed = true;
        StartEvent();
        duckCardPlayed = false;
    }

    public void StartEvent()    //starts event, gives event name and event text, 
                                //scrolls throug event text.
    {
        bool gameOverFlag = gameManager.GetComponent<GameOver>()._gameOverFlag;
        if (_waitCheck || gameOverFlag)  //check if scrolling is finished of current event.
        {
            return;
        }
        _waitCheck = true;

        if (cardNumber == 0) //if first time, make sure to not go below arraySize;
        {
            MeterPass(currentCard); //track meter numbers;
        }
        else if (cardNumber > 0)
        {
            eventCardsBehind = _eventArray[cardNumber - 1];
            MeterPass(eventCardsBehind); //track meter numbers, -1 to not change before btnPress;

            if (!duckCardPlayed)
            {
                MeterActive(); //Send scale event to Meters
            }
        }

        TrackNumber();

        DuckSpawnActive(); // spawn and move ducks
        CheckPass(); // assign boolchecks for spheres to array;

        if (!duckCardPlayed)
        {
            CheckActive(); // send new onMouseOver update to update spheres;
        }

        gameOverFlag = gameManager.GetComponent<GameOver>()._gameOverFlag;
        if (!gameOverFlag)
        {
            nameText.text = currentCard._eventName;
        }

        if (cardNumber >= arraySize)
        {
            EndEvent();
            return;
        }

        _LeftBtnPressed = false;
        _RightBtnPressed = false;

        StopAllCoroutines();
        if (!gameManager.GetComponent<GameOver>()._gameOverFlag)
        {
            StartCoroutine(LetterPopIn(currentCard._textForEvent, eventDisplayText, true));
            StartCoroutine(LetterPopIn(currentCard._responseLeft, leftResponse, false));
            StartCoroutine(LetterPopIn(currentCard._responseRight, rightResponse, false));
        }
    }

    void TrackNumber()  //keeps track of what event is current
    {
        if (cardNumber < arraySize)
        {
            currentCard = _eventArray[cardNumber];
            cardNumber++;
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
            _waitCheck = false;
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
        _sphereCheckLeft[0] = currentCard._checkEgo;
        _sphereCheckLeft[1] = currentCard._checkSwamp;
        _sphereCheckLeft[2] = currentCard._checkInternational;
        _sphereCheckLeft[3] = currentCard._checkBudget;
        _sphereCheckLeft[4] = currentCard._checkEnergy;

        _sphereCheckRight[0] = currentCard._checkEgoRight;
        _sphereCheckRight[1] = currentCard._checkSwampRight;
        _sphereCheckRight[2] = currentCard._checkInternationalRight;
        _sphereCheckRight[3] = currentCard._checkBudgetRight;
        _sphereCheckRight[4] = currentCard._checkEnergyRight;

        _meterNumber[0] = currentCard._meterEgo;
        _meterNumber[1] = currentCard._meterSwamp;
        _meterNumber[2] = currentCard._meterInternational;
        _meterNumber[3] = currentCard._meterBudget;
        _meterNumber[4] = currentCard._meterEnergy;

        _meterNumberRight[0] = currentCard._meterEgoRight;
        _meterNumberRight[1] = currentCard._meterSwampRight;
        _meterNumberRight[2] = currentCard._meterInternationalRight;
        _meterNumberRight[3] = currentCard._meterBudgetRight;
        _meterNumberRight[4] = currentCard._meterEnergyRight;

    }

    private void ShuffelEvents()
    {
        for (int i = 0; i < arraySize; i++) // shuffels the events
        {
            EventCards shuffelArray = _eventArray[i];
            int randomIndex = Random.Range(i, arraySize);
            //print("randomIndex: " + randomIndex);
            _eventArray[i] = _eventArray[randomIndex];
            _eventArray[randomIndex] = shuffelArray;
        }
    }
}
