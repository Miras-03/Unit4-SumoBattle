using System.Collections.Generic;
using UnityEngine;

public sealed class ObjectsPooler : MonoBehaviour
{
    [SerializeField] private List<Pool> objectsList;
    private Dictionary<string, Queue<Transform>> poolDictionary = new Dictionary<string, Queue<Transform>>();

    public void CreateObjects(Transform parent)
    {
        foreach (var item in objectsList)
        {
            Queue<Transform> queue = new Queue<Transform>();
            for (int i = 0; i < item.quantity; i++)
            {
                Transform prefab = Instantiate(item.prefab, parent);
                prefab.gameObject.SetActive(false);
                queue.Enqueue(prefab);
            }
            poolDictionary.Add(item.tag, queue);
        }
    }

    public Transform GetObjects(string tag, Vector3 position, Quaternion rotation)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning($"There's no such object with tag {tag}");
            return null;
        }

        Transform obj = poolDictionary[tag].Dequeue();
        obj.position = position;
        obj.rotation = rotation;
        obj.gameObject.SetActive(true);

        return obj;
    }

    [System.Serializable]
    internal struct Pool
    {
        public Transform prefab;
        public string tag;
        public int quantity;
    }
}