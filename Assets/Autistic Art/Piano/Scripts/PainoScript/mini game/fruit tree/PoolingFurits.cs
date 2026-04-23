using System.Collections.Generic;
using UnityEngine;

public class PoolingFurits : MonoBehaviour
{
    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefeb;
        public int size;
    }

    public List<Pool> pools;
    public Dictionary<string, Queue<GameObject>> poolDictionary;
    public static PoolingFurits Instance;

    public Fruitdetection fruitdetection;

    private void Awake()
    {
        Instance = this;
    }
    private void OnEnable()
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        if(fruitdetection)
        {
            StartCoroutine(fruitdetection.Spawner);
        }

        foreach(Pool pool in pools)
        {
            Queue<GameObject> objectpool = new Queue<GameObject>();
            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefeb);
                obj.SetActive(false);
                objectpool.Enqueue(obj);
            }
            poolDictionary.Add(pool.tag, objectpool);
        }
    }

    public GameObject SpawnfromPool(string tag,Vector3 position,Quaternion quaternion)
    {
        if(!poolDictionary.ContainsKey(tag))
        {
            return null;
        }
        GameObject spawn=poolDictionary[tag].Dequeue();
        spawn.SetActive(true);
        if (spawn.GetComponent<SkinnedMeshRenderer>())
            spawn.GetComponent<SkinnedMeshRenderer>().enabled = true;
        else if (spawn.GetComponent<SpriteRenderer>())
            spawn.GetComponent<SpriteRenderer>().enabled = true;
        else
            spawn/*.transform.GetChild(1)*/.gameObject.SetActive(true);
        spawn.GetComponent<Collider2D>().enabled = true;
        spawn.transform.localPosition = position;
        spawn.transform.rotation = quaternion;
        poolDictionary[tag].Enqueue(spawn);
        return spawn;
    }
}
