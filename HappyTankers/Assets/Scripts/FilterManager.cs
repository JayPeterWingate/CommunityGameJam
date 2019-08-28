using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FilterChangeEvent : UnityEvent<bool> { };
public class FilterManager : MonoBehaviour
{
	public static FilterChangeEvent OnChange = new FilterChangeEvent();
	private static bool m_isHappy = true;
	public static bool IsAlmostDark = false;
	public static bool IsHappy{
		get { return m_isHappy; }
		set {
			OnChange.Invoke(value);
			m_isHappy = value;
		}
	}
	

}
