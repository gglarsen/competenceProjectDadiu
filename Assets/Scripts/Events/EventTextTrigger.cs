using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventTextTrigger : MonoBehaviour
{

	public EventText eventText;

	public void TriggerEvents()
	{
		FindObjectOfType<TextManager>().StartEvent(eventText);
	}
}
