using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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
	[SerializeField] private GameObject m_camera;

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
		LegitTransition(0)();
		for (int i = 0; i < m_levels.Length; i++)
		{
			m_levels[i].OnLevelComplete.AddListener(LegitTransition(i + 1));
		}
	}

	private UnityAction LegitTransition(int i)
	{
		return () =>
		{
			print(i);
            m_levels[i - 1].GetComponent<GenerateLevel>().SetActiveLevel(false);
            GenerateLevel nextLevel = m_levels[i].GetComponent<GenerateLevel>();
            nextLevel.SetActiveLevel(false);
			m_camera.transform.position = m_levels[i].transform.position + new Vector3(0,0,0.5f);
			TankScript.TankList.ForEach((TankScript tank) => tank.DestroyBullets());
		};
	}
}
