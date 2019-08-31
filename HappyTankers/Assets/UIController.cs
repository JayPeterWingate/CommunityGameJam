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
	[SerializeField] GameObject[] m_Errors;
	[SerializeField] Transform m_ContiniousErrorParent;
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
		
		score = Mathf.Max(score + amount,0);
		m_score.text = score.ToString();
	}

	public void AddErrorScreen()
	{

		if(errorCount >= 6) { return; }
		if (errorCount > 4) {
			StartCoroutine(FireContinious());
		}
		else
		{
			if(errorCount != 0)
			{
				m_Errors[errorCount].SetActive(true);
			}
			
			errorCount += 1;
		}
	}

	IEnumerator FireContinious()
	{
		for(int i = 0; i < m_ContiniousErrorParent.childCount; i++)
		{
			yield return new WaitForSeconds(0.05f);
			m_ContiniousErrorParent.GetChild(i).gameObject.SetActive(true);
		}
		yield return new WaitForSeconds(1);
		Application.Quit();
	}
}
