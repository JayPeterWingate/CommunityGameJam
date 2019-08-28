using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FilterChangeEvent : UnityEvent<bool> { };
public class FilterManager : MonoBehaviour
{
	public static FilterChangeEvent OnChange = new FilterChangeEvent();

}
