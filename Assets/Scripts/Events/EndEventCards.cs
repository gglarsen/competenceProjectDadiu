using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "Data", menuName = "EndEventCards", order = 2)]
public class EndEventCards : ScriptableObject
{
	public string _eventName;

	[TextArea(3, 10)]
	public string _textForEvent;
}