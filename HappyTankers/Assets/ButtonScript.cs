using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonScript : MonoBehaviour
{
	[SerializeField] Sprite selected;
	[SerializeField] Sprite unselected;
	[SerializeField] TMPro.TextMeshProUGUI m_text;
	Color m_initColor;
	public void StartGame()
	{
		m_initColor = m_text.color;
		SceneManager.LoadScene(1);
	}
	public void ExitGame()
	{
		Application.Quit();
	}

    public void HoverButton()
	{
		Color newColor = Color.HSVToRGB(Random.value, 1, 1);
		GetComponent<Image>().color = newColor; 
		GetComponent<Image>().sprite = selected;
		m_text.color = newColor;
	}

	public void UnHover()
	{
		Color newColor = new Color(1, 1, 1);
		GetComponent<Image>().color = newColor;
		GetComponent<Image>().sprite = unselected;
		m_text.color = newColor;
	}
}
