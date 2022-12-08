using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Pool;

public class ObjectPoolManager : Singleton<ObjectPoolManager>
{
    [SerializeField] List<MonoBehaviour> prefabs = new();

    private readonly List<object> pools = new();

    public T GetObject<T>() where T : MonoBehaviour
    {
        if (GetPool<T>() == null)
        {
            var newPool = new ObjectPool<T>(() => Spawn<T>(), OnGetFromPool, OnReleaseToPool, OnDestroyAtPool);
            pools.Add(newPool);
        }
        return GetPool<T>().Get();
    }

    public void ReleaseObject<T>(T objectToRelease) where T : MonoBehaviour
    {
        if (GetPool<T>() != null)
        {
            GetPool<T>().Release(objectToRelease);
        }
    }

    private T Spawn<T>() where T : MonoBehaviour
    {
        return Instantiate(GetPrefab<T>());
    }

    private void OnGetFromPool(MonoBehaviour spawn)
    {
        spawn.gameObject.SetActive(true);
    }

    private void OnReleaseToPool(MonoBehaviour spawn)
    {
        spawn.gameObject.SetActive(false);
    }

    private void OnDestroyAtPool(MonoBehaviour spawn)
    {
        Destroy(spawn.gameObject);
    }

    private T GetPrefab<T>() where T : MonoBehaviour
    {
        return prefabs.OfType<T>().FirstOrDefault();
    }

    private ObjectPool<T> GetPool<T>() where T : MonoBehaviour
    {        
        return pools.OfType<ObjectPool<T>>().FirstOrDefault();
    }
}
