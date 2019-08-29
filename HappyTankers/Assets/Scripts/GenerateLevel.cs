using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GenerateLevel : MonoBehaviour
{
    private LevelManager m_manager;
    [SerializeField] private Texture2D m_levelMap;
    private List<BlockScript> m_levelBlocks;

    [SerializeField] LevelTriggerScript m_triggerMiddle;

    [SerializeField] LevelTriggerScript m_triggerLeft;
    [SerializeField] LevelTriggerScript m_triggerRight;
    [SerializeField] LevelTriggerScript m_triggerTop;
    [SerializeField] LevelTriggerScript m_triggerBot;

    // Start is called before the first frame update
    void Start()
    {
        m_manager = transform.root.GetComponent<LevelManager>();
        m_levelBlocks = new List<BlockScript>();
        Generate();

        m_triggerMiddle.m_action = () => m_manager.RoomEntryDetected(this.gameObject);

        m_triggerLeft.m_action = LeaveLevelBounds(CamTransitionType.Room_Left);
        m_triggerRight.m_action = LeaveLevelBounds(CamTransitionType.Room_Right);
        m_triggerTop.m_action = LeaveLevelBounds(CamTransitionType.Room_Up);
        m_triggerBot.m_action = LeaveLevelBounds(CamTransitionType.Room_Down);
    }

    public UnityAction LeaveLevelBounds(CamTransitionType ctt)
    {
        return () =>
        {
            m_manager.UnfilteredTransition(ctt);
        };
    }

    public void Generate()
    {
        if (m_levelMap != null)
        {
            for (int x = 0; x < m_manager.m_levelW; x++)
            {
                for (int y = 0; y < m_manager.m_levelH; y++)
                {
                    SpawnObject(x, y);
                }
            }
        }
        SetActiveLevel(false);
        SetPauseMode(true);
        SetShadowMode(true);
        //SetFilterMode(true, true);
    }

    public void SetFilterMode(bool happy, bool dark)
    {
        for (int i = 0; i < m_levelBlocks.Count; i++)
        {
            m_levelBlocks[i].SetFilterModes(happy, dark);
        }
    }

    private void SpawnObject(int x, int y)
    {
        GameObject prefab = m_manager.GetPrefabFromColor(m_levelMap.GetPixel(x, y));
        if (prefab != null)
        {
            prefab.GetComponent<BlockScript>().m_level = gameObject;
            m_levelBlocks.Add(Instantiate(prefab, transform.position + new Vector3(x - m_manager.m_levelW / 2, 0, y - m_manager.m_levelH / 2), transform.rotation, transform).GetComponent<BlockScript>());

        }
    }

    public void SetActiveLevel(bool setting)
    {
        try
        {
            for (int i = 0; i < m_levelBlocks.Count; i++)
            {
                m_levelBlocks[i].gameObject.SetActive(setting);
            }
            transform.position = new Vector3(transform.position.x, setting ? 0 : -20, transform.position.z);
        }
        catch
        {

        }
    }

    public void SetPauseMode(bool setting)
    {
        //TODO area pause
        Debug.Log("Area Pause: " + setting);
    }

    public void SetShadowMode(bool setting)
    {
        //TODO area shadows
        Debug.Log("Area shadow mode: " + setting);
    }
}
