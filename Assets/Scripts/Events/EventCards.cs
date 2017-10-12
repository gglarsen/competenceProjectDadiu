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
}