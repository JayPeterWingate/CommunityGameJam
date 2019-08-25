using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateLevel : MonoBehaviour
{
    private LevelManager m_manager;
    [SerializeField] private Texture2D m_levelMap;
    private int xSize = 24;
    private int ySize = 14;
    private List<GameObject> m_levelObjects;

    // Start is called before the first frame update
    void Start()
    {
        m_manager = transform.root.GetComponent<LevelManager>();
        m_levelObjects = new List<GameObject>();
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
        Destroy(GetComponent<MeshRenderer>());
        //SetActiveLevel(false);
    }

    private void SpawnObject(int x, int y)
    {
        GameObject prefab = m_manager.GetPrefabFromColor(m_levelMap.GetPixel(x, y));
        if (prefab != null)
        {
            m_levelObjects.Add(Instantiate(prefab, transform.position + new Vector3(x - xSize / 2, 0, y - ySize / 2), transform.rotation));
        }
    }

    public void SetActiveLevel(bool setting)
    {
        for (int i = 0; i < m_levelObjects.Count; i++)
        {
            m_levelObjects[i].SetActive(setting);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
