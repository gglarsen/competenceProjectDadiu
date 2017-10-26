using UnityEngine;
using System.Collections;

/*Scriptable Objects for events/cards, Cards have a name, event text, response text for left/right.
Information for how much they affect a meter, and a boolCheck to mark if they effect a meter.
BoolChekc is for SphereMarkers over Meters to signal MeterChange on mouseOver responseButton.
*/

[CreateAssetMenu(fileName = "Data", menuName = "EventCards", order = 1)]
public class EventCards : ScriptableObject
{
	public string _eventName;

	[SerializeField] [TextArea(3, 10)]
	public string _textForEvent;

	[Space(20), SerializeField]
	public string _responseLeft;

	public bool _checkEgo;
	public int _meterEgo;
	public bool _checkSwamp;
	public int _meterSwamp;
	public bool _checkInternational;
	public int _meterInternational;
	public bool _checkBudget;
	public int _meterBudget;
	public bool _checkEnergy;
	public int _meterEnergy;

	[Space(20), SerializeField]
	public string _responseRight;

	public bool _checkEgoRight;
	public int _meterEgoRight;
	public bool _checkSwampRight;
	public int _meterSwampRight;
	public bool _checkInternationalRight;
	public int _meterInternationalRight;
	public bool _checkBudgetRight;
	public int _meterBudgetRight;
	public bool _checkEnergyRight;
	public int _meterEnergyRight;
}