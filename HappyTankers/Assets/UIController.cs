using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
	[SerializeField] TextMeshProUGUI m_scoreTitle;
	[SerializeField] TextMeshProUGUI m_score;
	[SerializeField] Image[] m_hearts;
	[SerializeField] Sprite m_fullHeart;
	[SerializeField] Sprite m_noHeart;
	[SerializeField] GameObject m_ErrorPopup;
	private int score;
	private int errorCount = 0;
	public static UIController instance;


	private void Start()
	{
		instance = this;
		HeavyHandedReveal(FilterManager.IsHappy);
	}
	public void HeavyHandedReveal(bool value)
	{
		if (value)
		{
			m_scoreTitle.text = "Score";
		}
		else
		{
			m_scoreTitle.text = "Casulties:";
		}
	}
	public void ShowLives(int number)
	{
		for(int i = m_hearts.Length - 1; i > 0; i--)
		{
			m_hearts[i].sprite = number > i ? m_fullHeart : m_noHeart;
		}
	}
	public void ChangeScore(int amount)
	{
		score += amount;
		m_score.text = score.ToString();
	}

	public void AddErrorScreen()
	{
		errorCount += 1;
		if (errorCount > 20) {
			Application.Quit();
		}
		GameObject error = Instantiate(m_ErrorPopup,transform);
		 
		((RectTransform)error.transform).position = new Vector3(0,0, 0);
	}
}
