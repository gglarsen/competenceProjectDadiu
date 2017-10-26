using UnityEngine;
using System.Collections;

/*Scriptable Objects for end events/cards, Cards have a name and text.
*/

[CreateAssetMenu(fileName = "Data", menuName = "EndEventCards", order = 2)]
public class EndEventCards : ScriptableObject
{
	public string _eventName;

	[TextArea(3, 10)]
	public string _textForEvent;
}