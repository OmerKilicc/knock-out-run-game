using System.Collections.Generic;
using UnityEngine;
using Euphrates;
using System.Threading.Tasks;

public class CloudSpawner : MonoBehaviour
{
    readonly int MAX_ITERATION = 1000000;

    [SerializeField] List<GameObject> _prefabs = new List<GameObject>();
    [Space]
    [SerializeField] List<CloudZone> _zones = new List<CloudZone>();

    [SerializeField] bool _isWorking = false;

    public void Spawn()
    {
        if (_isWorking)
            return;

        _isWorking = true;

        DeleteAll();
        SpawnAsync();
    }

    async void SpawnAsync()
    {
        List<Vector3> usedPos = new List<Vector3>();

        foreach (var zone in _zones)
        {
            float minSep = zone.MinSeperation == -1 ? float.MaxValue : zone.MinSeperation;
            float maxSep = zone.MaxSeperation == -1 ? float.MaxValue : zone.MaxSeperation;

            Vector3 realMin = zone.Min();
            Vector3 realMax = zone.Max();

            Vector3 lastPos = new Vector3(Random.Range(realMin.x, realMax.x), Random.Range(realMin.y, realMax.y), Random.Range(realMin.z, realMax.z));
            usedPos.Add(lastPos);
            GameObject go = Instantiate(_prefabs.GetRandomItem(), transform);
            go.transform.position = lastPos;
            go.isStatic = true;

            for (int i = 1; i < zone.Count; i++)
            {
                int j = 0;
                while (j++ < MAX_ITERATION)
                {
                    if (!_isWorking)
                        return;

                    if (j % 10000 == 0)
                        await Task.Yield();

                    Vector3 pos = zone.GetRandomPoint();
                    bool pass = false;

                    foreach (var p in usedPos)
                    {
                        float dist = Vector3.Distance(p, pos);
                        if (dist < minSep || dist > maxSep)
                        {
                            pass = true;
                            break;
                        }
                    }

                    if (pass)
                        continue;

                    var spawned = Instantiate(_prefabs.GetRandomItem(), transform);
                    spawned.transform.position = pos;
                    spawned.isStatic = true;
                    usedPos.Add(pos);
                    break;
                }
            }
        }

        _isWorking = false;
    }

    public void DeleteAll()
    {
        if (transform.childCount < 1)
            return;

        for (int i = 0; i < transform.childCount; i++)
        {
            DestroyImmediate(transform.GetChild(0).gameObject);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;

        foreach (var zone in _zones)
        {
            Vector3 min = zone.Min();
            Vector3 max = zone.Max();

            Vector3 dir = max - min;
            Vector3 center = min + (dir * 0.5f);

            Vector3 size = new Vector3(max.x - min.x, max.y - min.y, max.z - min.z);

            Gizmos.DrawWireCube(center, size);
        }
    }
}

[System.Serializable]
struct CloudZone
{
    public int Count;
    public Vector3 Corner1;
    public Vector3 Corner2;
    [Tooltip("Put -1 for no minimum")]
    public float MinSeperation;
    [Tooltip("Put -1 for no maximum")]
    public float MaxSeperation;

    public CloudZone(int count, Vector3 corner1, Vector3 corner2, float minsep, float maxSep)
    {
        Count = count;
        Corner1 = corner1;
        Corner2 = corner2;
        MinSeperation = minsep;
        MaxSeperation = maxSep;
    }

    public Vector3 Max()
    {
        float maxX = Mathf.Max(Corner1.x, Corner2.x);
        float maxY = Mathf.Max(Corner1.y, Corner2.y);
        float maxZ = Mathf.Max(Corner1.z, Corner2.z);

        return new Vector3(maxX, maxY, maxZ);
    }

    public Vector3 Min()
    {
        float minX = Mathf.Min(Corner1.x, Corner2.x);
        float minY = Mathf.Min(Corner1.y, Corner2.y);
        float minZ = Mathf.Min(Corner1.z, Corner2.z);

        return new Vector3(minX, minY, minZ);
    }

    public bool InZone(Vector3 point)
    {
        Vector3 min = Min();
        Vector3 max = Max();

        return point.x > min.x && point.x < max.x && point.y > min.y && point.y < max.y && point.z > min.z && point.z < max.z;
    }

    public Vector3 GetRandomPoint()
    {
        Vector3 min = Min();
        Vector3 max = Max();

        return new Vector3(Random.Range(min.x, max.x), Random.Range(min.y, max.y), Random.Range(min.z, max.z));
    }
}