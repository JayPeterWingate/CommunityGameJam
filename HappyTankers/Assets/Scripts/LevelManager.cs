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
    [HideInInspector] public GenerateLevel[,] usedSlots;
    //Levels
    [HideInInspector] public GenerateLevel LevelLast;
    [HideInInspector] public GenerateLevel LevelCurrent;
    [HideInInspector] public GenerateLevel LevelNext;

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
    [SerializeField] private GenerateLevel[] m_unfilteredLevelGenerations;
    private int m_currentUFLIndex = 0;
	[SerializeField] private CameraScript m_camera;
    
    [SerializeField] private LevelGridElements m_gridElements;

    public int m_levelW = 26;
    public int m_levelH = 16;
    public int m_worldSize = 200;

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


    private void InLevelSlot(GenerateLevel inputObj)
    {
        m_gridElements.usedSlots[(m_worldSize / 2) + (int)Mathf.Round(inputObj.transform.position.x / (m_levelW + 6))
                               , (m_worldSize / 2) + (int)Mathf.Round(inputObj.transform.position.z / (m_levelH + 6))] = inputObj;
    }

    private GenerateLevel OutLevelSlot(float x, float y)
    {
        return m_gridElements.usedSlots[(m_worldSize / 2) + (int)Mathf.Round(x / (m_levelW + 6)), (m_worldSize / 2) + (int)Mathf.Round(y / (m_levelH + 6))];
    }

	private void Start()
	{
		IsHappy = true;

        m_gridElements.LevelCurrent = m_levels[0].GetComponent<GenerateLevel>();
        m_gridElements.LevelNext = m_unfilteredLevelGenerations[0];
        m_gridElements.LevelCurrent.transform.position = new Vector3(0, 0, 0);
        m_gridElements.camPos = CamPos.InRoom;
        m_gridElements.usedSlots = new GenerateLevel[m_worldSize, m_worldSize];
        InLevelSlot(m_gridElements.LevelCurrent);
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
        if ((type == CamTransitionType.Room_Down || type == CamTransitionType.Room_Left || type == CamTransitionType.Room_Right || type == CamTransitionType.Room_Up)
            && (m_gridElements.camPos != CamPos.InRoom))
        {
            return; //Return if invalid transition request
        }

        Debug.Log("UnfilteredTransition - " + type + " from " + m_gridElements.camPos);
        switch (type)
        {
            case CamTransitionType.HW_New:
                {
                    FilterManager.IsHappy = false;

                    GameObject temp;
                    Vector3 shiftVal = new Vector3(0,0,0);
                    switch (m_gridElements.camPos)
                    {
                        case CamPos.BotHW:
                            {
                                shiftVal = -(new Vector3(0,0,m_levelH + 6));
                                //Vx1
                                temp = m_gridElements.HW_V11; m_gridElements.HW_V11 = m_gridElements.HW_V21; m_gridElements.HW_V21 = m_gridElements.HW_V31;
                                temp.transform.position += 3*shiftVal; m_gridElements.HW_V31 = temp;
                                //Vx2
                                temp = m_gridElements.HW_V12; m_gridElements.HW_V12 = m_gridElements.HW_V22; m_gridElements.HW_V22 = m_gridElements.HW_V32;
                                temp.transform.position += 3*shiftVal; m_gridElements.HW_V32 = temp;
                                //H1x
                                temp = m_gridElements.HW_H11; m_gridElements.HW_H11 = m_gridElements.HW_H12;
                                temp.transform.position += 2*shiftVal; m_gridElements.HW_H12 = temp;
                                //H2x
                                temp = m_gridElements.HW_H21; m_gridElements.HW_H21 = m_gridElements.HW_H22;
                                temp.transform.position += 2*shiftVal; m_gridElements.HW_H22 = temp;
                                //H3x
                                temp = m_gridElements.HW_H31; m_gridElements.HW_H31 = m_gridElements.HW_H32;
                                temp.transform.position += 2*shiftVal; m_gridElements.HW_H32 = temp;
                                //NxL
                                temp = m_gridElements.NodeTL; m_gridElements.NodeTL = m_gridElements.NodeBL;
                                temp.transform.position += 2*shiftVal; m_gridElements.NodeBL = temp;
                                //NxR
                                temp = m_gridElements.NodeTR; m_gridElements.NodeTR = m_gridElements.NodeBR;
                                temp.transform.position += 2*shiftVal; m_gridElements.NodeBR = temp;
                                break;
                            }
                        case CamPos.TopHW:
                            {
                                shiftVal = (new Vector3(0, 0, m_levelH + 6));
                                //Vx1
                                temp = m_gridElements.HW_V31; m_gridElements.HW_V31 = m_gridElements.HW_V21; m_gridElements.HW_V21 = m_gridElements.HW_V11;
                                temp.transform.position += 3 * shiftVal; m_gridElements.HW_V11 = temp;
                                //Vx2
                                temp = m_gridElements.HW_V32; m_gridElements.HW_V32 = m_gridElements.HW_V22; m_gridElements.HW_V22 = m_gridElements.HW_V12;
                                temp.transform.position += 3 * shiftVal; m_gridElements.HW_V12 = temp;
                                //H1x
                                temp = m_gridElements.HW_H12; m_gridElements.HW_H12 = m_gridElements.HW_H11;
                                temp.transform.position += 2 * shiftVal; m_gridElements.HW_H11 = temp;
                                //H2x
                                temp = m_gridElements.HW_H22; m_gridElements.HW_H22 = m_gridElements.HW_H21;
                                temp.transform.position += 2 * shiftVal; m_gridElements.HW_H21 = temp;
                                //H3x
                                temp = m_gridElements.HW_H32; m_gridElements.HW_H32 = m_gridElements.HW_H31;
                                temp.transform.position += 2 * shiftVal; m_gridElements.HW_H31 = temp;
                                //NxL
                                temp = m_gridElements.NodeBL; m_gridElements.NodeBL = m_gridElements.NodeTL;
                                temp.transform.position += 2 * shiftVal; m_gridElements.NodeTL = temp;
                                //NxR
                                temp = m_gridElements.NodeBR; m_gridElements.NodeBR = m_gridElements.NodeTR;
                                temp.transform.position += 2 * shiftVal; m_gridElements.NodeTR = temp;
                                break;
                            }
                        case CamPos.LeftHW:
                            {
                                shiftVal = -(new Vector3(m_levelW + 6, 0, 0));
                                //Hx1
                                temp = m_gridElements.HW_H31; m_gridElements.HW_H31 = m_gridElements.HW_H21; m_gridElements.HW_H21 = m_gridElements.HW_H11;
                                temp.transform.position += 3 * shiftVal; m_gridElements.HW_H11 = temp;
                                //Hx2
                                temp = m_gridElements.HW_H32; m_gridElements.HW_H32 = m_gridElements.HW_H22; m_gridElements.HW_H22 = m_gridElements.HW_H12;
                                temp.transform.position += 3 * shiftVal; m_gridElements.HW_H12 = temp;
                                //V1x
                                temp = m_gridElements.HW_V12; m_gridElements.HW_V12 = m_gridElements.HW_V11;
                                temp.transform.position += 2 * shiftVal; m_gridElements.HW_V11 = temp;
                                //V2x
                                temp = m_gridElements.HW_V22; m_gridElements.HW_V22 = m_gridElements.HW_V21;
                                temp.transform.position += 2 * shiftVal; m_gridElements.HW_V21 = temp;
                                //V3x
                                temp = m_gridElements.HW_V32; m_gridElements.HW_V32 = m_gridElements.HW_V31;
                                temp.transform.position += 2 * shiftVal; m_gridElements.HW_V31 = temp;
                                //NTx
                                temp = m_gridElements.NodeTR; m_gridElements.NodeTR = m_gridElements.NodeTL;
                                temp.transform.position += 2 * shiftVal; m_gridElements.NodeTL = temp;
                                //NBx
                                temp = m_gridElements.NodeBR; m_gridElements.NodeBR = m_gridElements.NodeBL;
                                temp.transform.position += 2 * shiftVal; m_gridElements.NodeBL = temp;
                                break;
                            }
                        case CamPos.RightHW:
                            {
                                shiftVal = (new Vector3(m_levelW + 6, 0, 0));
                                //Hx1
                                temp = m_gridElements.HW_H11; m_gridElements.HW_H11 = m_gridElements.HW_H21; m_gridElements.HW_H21 = m_gridElements.HW_H31;
                                temp.transform.position += 3 * shiftVal; m_gridElements.HW_H31 = temp;
                                //Hx2
                                temp = m_gridElements.HW_H12; m_gridElements.HW_H12 = m_gridElements.HW_H22; m_gridElements.HW_H22 = m_gridElements.HW_H32;
                                temp.transform.position += 3 * shiftVal; m_gridElements.HW_H32 = temp;
                                //V1x
                                temp = m_gridElements.HW_V11; m_gridElements.HW_V11 = m_gridElements.HW_V12;
                                temp.transform.position += 2 * shiftVal; m_gridElements.HW_V12 = temp;
                                //V2x
                                temp = m_gridElements.HW_V21; m_gridElements.HW_V21 = m_gridElements.HW_V22;
                                temp.transform.position += 2 * shiftVal; m_gridElements.HW_V22 = temp;
                                //V3x
                                temp = m_gridElements.HW_V31; m_gridElements.HW_V31 = m_gridElements.HW_V32;
                                temp.transform.position += 2 * shiftVal; m_gridElements.HW_V32 = temp;
                                //NTx
                                temp = m_gridElements.NodeTL; m_gridElements.NodeTL = m_gridElements.NodeTR;
                                temp.transform.position += 2 * shiftVal; m_gridElements.NodeTR = temp;
                                //NBx
                                temp = m_gridElements.NodeBL; m_gridElements.NodeBL = m_gridElements.NodeBR;
                                temp.transform.position += 2 * shiftVal; m_gridElements.NodeBR = temp;
                                break;
                            }
                        case CamPos.InRoom:
                            {
                                Debug.Log("Attempted room switch whilst midroom");
                                break;
                            }
                        default: break;
                    }
                    m_gridElements.worldCentre += shiftVal;
                    m_gridElements.camPos = CamPos.InRoom;
                    m_camera.LerpMoveFocus(m_gridElements.worldCentre);

                    GenerateLevel newLevel = OutLevelSlot(m_gridElements.worldCentre.x, m_gridElements.worldCentre.z);
                    if (m_gridElements.LevelLast != null && newLevel != m_gridElements.LevelLast) { m_gridElements.LevelLast.SetActiveLevel(false); }
                    m_gridElements.LevelLast = m_gridElements.LevelCurrent;
                    m_gridElements.LevelCurrent = newLevel;
                    m_gridElements.LevelCurrent.SetShadowMode(false);
                    m_gridElements.LevelCurrent.SetPauseMode(false);
                    //TODO Unhardcode this level 3 spot

                    //TODO activate and darken next level
                    DebugGridArrayPrint();
                    break;
                }
            case CamTransitionType.HW_Old:
                {
                    m_gridElements.camPos = CamPos.InRoom;
                    m_camera.LerpMoveFocus(m_gridElements.worldCentre);
                    m_gridElements.LevelCurrent.SetShadowMode(false);
                    break;
                }
            case CamTransitionType.Room_Left:
                {
                    if (FilterManager.IsHappy) { FilterManager.IsAlmostDark = true; }
                    m_gridElements.camPos = CamPos.LeftHW;
                    m_camera.LerpMoveFocus(m_gridElements.worldCentre - new Vector3(m_levelW / 2 + 3,0,0));

                    HighwayEntryAddedCode(-new Vector3(m_levelW + 6, 0, 0));
                    break;
                }
            case CamTransitionType.Room_Right:
                {
                    if (FilterManager.IsHappy) { FilterManager.IsAlmostDark = true; }
                    m_gridElements.camPos = CamPos.RightHW;
                    m_camera.LerpMoveFocus(m_gridElements.worldCentre + new Vector3(m_levelW / 2 + 3, 0, 0));

                    HighwayEntryAddedCode(new Vector3(m_levelW + 6, 0, 0));
                    break;
                }
            case CamTransitionType.Room_Up:
                {
                    if (FilterManager.IsHappy) { FilterManager.IsAlmostDark = true; }
                    m_gridElements.camPos = CamPos.TopHW;
                    m_camera.LerpMoveFocus(m_gridElements.worldCentre + new Vector3(0, 0, m_levelH / 2 + 3));

                    HighwayEntryAddedCode(new Vector3(0, 0, m_levelH + 6));
                    break;
                }
            case CamTransitionType.Room_Down:
                {
                    if (FilterManager.IsHappy) { FilterManager.IsAlmostDark = true; }
                    m_gridElements.camPos = CamPos.BotHW;
                    m_camera.LerpMoveFocus(m_gridElements.worldCentre - new Vector3(0, 0, m_levelH / 2 + 3));

                    HighwayEntryAddedCode(-new Vector3(0, 0, m_levelH + 6));
                    break;
                }
            default: break;
        }
        //DebugGridChecker();
    }

    private void HighwayEntryAddedCode(Vector3 offsetNL)
    {
        Vector3 nextLevelPos = m_gridElements.worldCentre + offsetNL;
        GenerateLevel nextLevelSlot = OutLevelSlot(nextLevelPos.x, nextLevelPos.z);
        if (nextLevelSlot == null)
        {
            m_gridElements.LevelNext.transform.position = nextLevelPos;
            InLevelSlot(m_gridElements.LevelNext);
            m_gridElements.LevelNext.SetActiveLevel(true);
            m_currentUFLIndex++;
            Debug.Log("NextLevelIndex = " + m_currentUFLIndex);
            m_gridElements.LevelNext = m_unfilteredLevelGenerations[m_currentUFLIndex];
        }
        else
        {
            nextLevelSlot.SetActiveLevel(true);
        }
        m_gridElements.LevelCurrent.SetShadowMode(true);
    }

    private void DebugGridArrayPrint()
    {
        int minIdx = (m_worldSize / 2) - 5;
        int maxIdx = (m_worldSize / 2) + 5;
        string debugString = "";
        for (int y = maxIdx - 1; y > minIdx - 1; y--)
        {
            for (int x = minIdx; x < maxIdx; x++)
            {
                debugString += DGAP_Aux(m_gridElements.usedSlots[x,y]);
            }
            debugString += "\n";
        }
        Debug.Log(debugString);
    }
    private string DGAP_Aux(GenerateLevel gl)
    {
        if (gl == null)
        {
            return "_ ";
        }
        if (gl == m_gridElements.LevelCurrent)
        {
            return "C ";
        }
        if (gl == m_gridElements.LevelLast)
        {
            return "L ";
        }
        if (gl == m_gridElements.LevelNext)
        {
            return "N ";
        }
        return "X ";
    }


    private void DebugGridChecker()
    {
        Vector3 H11 = m_gridElements.HW_H11.transform.GetChild(0).position;
        Vector3 H21 = m_gridElements.HW_H21.transform.GetChild(0).position;
        Vector3 H31 = m_gridElements.HW_H31.transform.GetChild(0).position;
        Vector3 H12 = m_gridElements.HW_H12.transform.GetChild(0).position;
        Vector3 H22 = m_gridElements.HW_H22.transform.GetChild(0).position;
        Vector3 H32 = m_gridElements.HW_H32.transform.GetChild(0).position;

        Vector3 V11 = m_gridElements.HW_V11.transform.GetChild(0).position;
        Vector3 V21 = m_gridElements.HW_V21.transform.GetChild(0).position;
        Vector3 V31 = m_gridElements.HW_V31.transform.GetChild(0).position;
        Vector3 V12 = m_gridElements.HW_V12.transform.GetChild(0).position;
        Vector3 V22 = m_gridElements.HW_V22.transform.GetChild(0).position;
        Vector3 V32 = m_gridElements.HW_V32.transform.GetChild(0).position;

        Vector3 NTL = m_gridElements.NodeTL.transform.GetChild(0).position;
        Vector3 NBL = m_gridElements.NodeBL.transform.GetChild(0).position;
        Vector3 NTR = m_gridElements.NodeTR.transform.GetChild(0).position;
        Vector3 NBR = m_gridElements.NodeBR.transform.GetChild(0).position;

        Vector3 WP = m_gridElements.worldCentre;

        if (H11.x >= H21.x) { Debug.Log("H-Fail1"); } if (H21.x >= H31.x) { Debug.Log("H-Fail2"); } if (H12.x >= H22.x) { Debug.Log("H-Fail3"); } if (H22.x >= H32.x) { Debug.Log("H-Fail4"); }
        if (H11.z != H21.z || H11.z != H31.z || H21.z != H31.z) { Debug.Log("H-Fail5"); } if (H12.z != H22.z || H12.z != H32.z || H22.z != H32.z) { Debug.Log("H-Fail6"); }
        if (V11.z <= V21.z) { Debug.Log("V-Fail1"); } if (V21.z <= V31.z) { Debug.Log("V-Fail2"); } if (V12.z <= V22.z) { Debug.Log("V-Fail3"); } if (V22.z <= V32.z) { Debug.Log("V-Fail4"); }
        if (V11.x != V21.x || V11.x != V31.x || V21.x != V31.x) { Debug.Log("V-Fail5"); } if (V12.x != V22.x || V12.x != V32.x || V22.x != V32.x) { Debug.Log("V-Fail6"); }
        if (WP.x >= NBR.x || WP.x >= NTR.x) { Debug.Log("N-Fail1"); } if (WP.x <= NBL.x || WP.x <= NTL.x) { Debug.Log("N-Fail2"); }
        if (WP.z >= NTR.z || WP.z >= NTL.z) { Debug.Log("N-Fail3"); } if (WP.z <= NBR.z || WP.z <= NBL.z) { Debug.Log("N-Fail4"); }
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

            GenerateLevel nextLevel = m_levels[i].GetComponent<GenerateLevel>();
            m_gridElements.LevelCurrent = nextLevel;
            InLevelSlot(m_gridElements.LevelCurrent);
            nextLevel.SetActiveLevel(true);
			//m_camera.transform.position = m_levels[i].transform.position + new Vector3(0,0,0.5f);
			TankScript.TankList.ForEach((TankScript tank) => tank.DestroyBullets());
		};
	}
}
