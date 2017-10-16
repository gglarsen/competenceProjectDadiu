using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "Data", menuName = "EventCards", order = 1)]
public class EventCards : ScriptableObject
{
	public string _eventName;

	[SerializeField] [TextArea(3, 10)]
	public string _textForEvent;

	[SerializeField]
	public string _responseLeft;

	[SerializeField]
	public string _responseRight;

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
}