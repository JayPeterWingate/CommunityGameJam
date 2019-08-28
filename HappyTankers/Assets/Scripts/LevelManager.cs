using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum CamPos
{
    InRoom,
    LeftHW,
    RightHW,
    TopHW,
    BotHW,
}

public enum CamTransitionType
{
    HW_New,
    HW_Old,
    Room_Up,
    Room_Down,
    Room_Left,
    Room_Right,
}

[Serializable]
public struct LevelGridElements
{
    [HideInInspector] public Vector3 worldCentre;
    [HideInInspector] public CamPos camPos;
    //Vertical Highways
    public GameObject HW_V11;
    public GameObject HW_V21;
    public GameObject HW_V31;
    public GameObject HW_V12;
    public GameObject HW_V22;
    public GameObject HW_V32;
    //Horizontal Highways
    public GameObject HW_H11;
    public GameObject HW_H21;
    public GameObject HW_H31;
    public GameObject HW_H12;
    public GameObject HW_H22;
    public GameObject HW_H32;
    //Nodes
    public GameObject NodeTL;
    public GameObject NodeTR;
    public GameObject NodeBL;
    public GameObject NodeBR;
    //Levels
    public GameObject LevelCurrent;
    public GameObject LevelNext;
    public GameObject LevelVoid;
}

public class LevelManager : MonoBehaviour
{
    [Serializable]
    private class levelObjectColorCoded
    {
        public Color colorCode;
        public GameObject prefab;
    }
	public static bool IsHappy { get; private set; }
    [SerializeField] private levelObjectColorCoded[] m_levelObjectCodedPrefabs;
	[SerializeField] private LevelProgression[] m_levels;
	[SerializeField] private CameraScript m_camera;
    
    [SerializeField] private LevelGridElements m_gridElements;

    public GameObject GetPrefabFromColor(Color col)
    {
        if (col.a == 0) { return null; }
        for (int i = 0; i < m_levelObjectCodedPrefabs.Length; i++)
        {
            if (col == m_levelObjectCodedPrefabs[i].colorCode)
            {
                return m_levelObjectCodedPrefabs[i].prefab;
            }
        }
        return null;
    }

	private void Start()
	{
		IsHappy = true;

        m_gridElements.LevelCurrent = m_levels[0].gameObject;
        m_gridElements.LevelNext = m_levels[1].gameObject;
        m_gridElements.LevelCurrent.transform.position = new Vector3(0, 0, 0);
        m_gridElements.camPos = CamPos.InRoom;
        m_gridElements.worldCentre = new Vector3(0,0,0);
        FilteredTransition(0)();

		for (int i = 0; i < m_levels.Length; i++)
		{
			m_levels[i].OnLevelComplete.AddListener(FilteredTransition(i + 1));
		}
        
    }

    private void UnfilteredTransition(CamTransitionType type)
    {
        switch (type)
        {
            case CamTransitionType.HW_New:
                {
                    m_gridElements.camPos = CamPos.InRoom;

                    break;
                }
            case CamTransitionType.HW_Old:
                {
                    m_gridElements.camPos = CamPos.InRoom;
                    break;
                }
            case CamTransitionType.Room_Left:
                {
                    m_gridElements.camPos = CamPos.LeftHW;
                    break;
                }
            case CamTransitionType.Room_Right:
                {
                    m_gridElements.camPos = CamPos.RightHW;
                    break;
                }
            case CamTransitionType.Room_Up:
                {
                    m_gridElements.camPos = CamPos.TopHW;
                    break;
                }
            case CamTransitionType.Room_Down:
                {
                    m_gridElements.camPos = CamPos.BotHW;
                    break;
                }
            default: break;
        }
    }


    private UnityAction FilteredTransition(int i)
	{
		return () =>
		{
            m_levels[i].transform.position = m_gridElements.LevelCurrent.transform.position;
            if (i > 0)
            {
                m_levels[i - 1].GetComponent<GenerateLevel>().SetActiveLevel(false);
            }
            m_gridElements.LevelCurrent = m_levels[i].gameObject;
            m_gridElements.LevelNext = m_levels[i+1].gameObject;

            GenerateLevel nextLevel = m_levels[i].GetComponent<GenerateLevel>();
            nextLevel.SetActiveLevel(true);
			//m_camera.transform.position = m_levels[i].transform.position + new Vector3(0,0,0.5f);
			TankScript.TankList.ForEach((TankScript tank) => tank.DestroyBullets());
		};
	}
}
