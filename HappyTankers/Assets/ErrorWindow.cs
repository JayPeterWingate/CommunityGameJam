using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ErrorWindow : MonoBehaviour
{
	[SerializeField] TMPro.TextMeshProUGUI m_text;

	static string[] errorMessages = new string[5]
	{
		"Player out of bounds",
		"Filter has broken",
		"Level system corrupted",
		"Player is aware",
		"Player is escaping"
	};
	
	// Start is called before the first frame update
    void Start()
    {
		m_text.text = errorMessages[Random.Range(0, errorMessages.Length)];
    }
}
