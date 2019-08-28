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
    //Orientation
    [HideInInspector] public Vector3 worldCentre;
    [HideInInspector] public CamPos camPos;
    //Levels
    [HideInInspector] public GameObject LevelCurrent;
    [HideInInspector] public GameObject LevelNext;
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

    public int m_levelW = 26;
    public int m_levelH = 16;

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

    public void RoomEntryDetected(GameObject room)
    {
        if (m_gridElements.camPos == CamPos.InRoom) { return; } //Return if invalid transition request
        UnfilteredTransition(room == m_gridElements.LevelCurrent ? CamTransitionType.HW_Old : CamTransitionType.HW_New);
    }

    public void UnfilteredTransition(CamTransitionType type)
    {
        Debug.Log("UnfilteredTransition - " + type);

        if ((type == CamTransitionType.Room_Down || type == CamTransitionType.Room_Left || type == CamTransitionType.Room_Right || type == CamTransitionType.Room_Up)
            && (m_gridElements.camPos != CamPos.InRoom))
        {
            return; //Return if invalid transition request
        }

        switch (type)
        {
            case CamTransitionType.HW_New:
                {
                    m_gridElements.camPos = CamPos.InRoom;
                    
                    //TODO

                    break;
                }
            case CamTransitionType.HW_Old:
                {
                    m_gridElements.camPos = CamPos.InRoom;
                    m_camera.LerpMoveFocus(m_gridElements.worldCentre);
                    break;
                }
            case CamTransitionType.Room_Left:
                {
                    m_gridElements.camPos = CamPos.LeftHW;
                    m_camera.LerpMoveFocus(m_gridElements.worldCentre - new Vector3(m_levelW / 2 + 3,0,0));
                    m_gridElements.LevelNext.transform.position = m_gridElements.worldCentre - new Vector3(m_levelW + 6, 0, 0);
                    break;
                }
            case CamTransitionType.Room_Right:
                {
                    m_gridElements.camPos = CamPos.RightHW;
                    m_camera.LerpMoveFocus(m_gridElements.worldCentre + new Vector3(m_levelW / 2 + 3, 0, 0));
                    m_gridElements.LevelNext.transform.position = m_gridElements.worldCentre + new Vector3(m_levelW + 6, 0, 0);
                    break;
                }
            case CamTransitionType.Room_Up:
                {
                    m_gridElements.camPos = CamPos.TopHW;
                    m_camera.LerpMoveFocus(m_gridElements.worldCentre + new Vector3(m_levelH / 2 + 3, 0, 0));
                    m_gridElements.LevelNext.transform.position = m_gridElements.worldCentre + new Vector3(m_levelH + 6, 0, 0);
                    break;
                }
            case CamTransitionType.Room_Down:
                {
                    m_gridElements.camPos = CamPos.BotHW;
                    m_camera.LerpMoveFocus(m_gridElements.worldCentre - new Vector3(m_levelH / 2 + 3, 0, 0));
                    m_gridElements.LevelNext.transform.position = m_gridElements.worldCentre - new Vector3(m_levelH + 6, 0, 0);
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
