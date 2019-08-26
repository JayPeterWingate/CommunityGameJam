using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateLevel : MonoBehaviour
{
    private LevelManager m_manager;
    [SerializeField] private Texture2D m_levelMap;
    private int xSize = 26;
    private int ySize = 16;
    private List<BlockScript> m_levelBlocks;

    // Start is called before the first frame update
    void Start()
    {
        m_manager = transform.root.GetComponent<LevelManager>();
        m_levelBlocks = new List<BlockScript>();
        if (m_levelMap != null)
        {
            for (int x = 0; x < xSize; x++)
            {
                for (int y = 0; y < ySize; y++)
                {
                    SpawnObject(x, y);
                }
            }
        }
        //SetActiveLevel(false);
        SetFilterMode(true, true);
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
            m_levelBlocks.Add(Instantiate(prefab, transform.position + new Vector3(x - xSize / 2, 0, y - ySize / 2), transform.rotation, transform).GetComponent<BlockScript>());
			
		}
    }

    public void SetActiveLevel(bool setting)
    {
        for (int i = 0; i < m_levelBlocks.Count; i++)
        {
            m_levelBlocks[i].gameObject.SetActive(setting);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
