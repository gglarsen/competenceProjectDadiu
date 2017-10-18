using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
	public bool _gameOverFlag;
	private int gameOverFlag = 0;
	[SerializeField]
	public EndEventCards[] _endEvents;
	private EndEventCards endEvents;
	[SerializeField]
	private MeterController[] meterStatus;

	[SerializeField]
	private Text endName;
	[SerializeField]
	private Text endDisplayText;
	[SerializeField]
	private GameObject leftBtn;
	[SerializeField]
	private GameObject RightBtn;
	[SerializeField]
	private GameObject restartBtn;
	[SerializeField]
	private Text textLeftBtn;
	[SerializeField]
	private Text textRightBtn;

	void OnEnable()
	{
		MeterController.GameOverStatus += EndGameScreen;
	}

	void OnDisable()
	{
		MeterController.GameOverStatus -= EndGameScreen;
	}

	void EndGameScreen()
	{
		if (gameOverFlag == 0)
		{
			_gameOverFlag = true;
			gameOverFlag++;
			EndReason();
		}
	}

	void EndReason()
	{
		for (int i = 0; i < 4; i++)
		{
			print(meterStatus[i]._resourceMeter);
			if (meterStatus[i]._resourceMeter >= 100)
			{
				endEvents = _endEvents[i];
				endName.text = endEvents._eventName;
				endDisplayText.text = endEvents._textForEvent;
				leftBtn.SetActive(false);
				RightBtn.SetActive(false);
				restartBtn.SetActive(true);
				//textLeftBtn.text = "";
				//leftBtn.interactable = false;
				//textRightBtn.text = "";
				//RightBtn.interactable = false;
				//restartBtn.interactable = true;
			}
			else if(meterStatus[i]._resourceMeter <= 0)
			{
				endEvents = _endEvents[i + 5];
				endName.text = endEvents._eventName;
			}
		}
	}

}
