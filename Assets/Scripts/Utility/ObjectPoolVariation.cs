using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using VInspector;

// Game Object Pool. Written by GnoxNahte
public class ObjectPoolVariation : MonoBehaviour
{
    [SerializeField] GameObject[] prefabs;
    [SerializeField] List<GameObject>[] objs;
    [SerializeField] List<GameObject>[] inactiveObjs;

    [SerializeField] private int[] totalObjsCount;
    [SerializeField] private int[] activeObjsCount;
    [SerializeField] private int[] inactiveObjsCount;

    [SerializeField] private bool ifCheckInactive;
    int variationCount => prefabs.Length;

    //public int ActiveCount => objs.Count - inactiveObjs.Count;

    public bool initDone { get; private set; }

    public void Init(int startCapacity = 100)
    {
        int splitCapacity = startCapacity / variationCount;

        objs = new List<GameObject>[variationCount];
        for (int i = 0; i < objs.Length; i++)
            objs[i] = new List<GameObject>(splitCapacity);
        
        inactiveObjs = new List<GameObject>[variationCount];
        for (int i = 0; i < inactiveObjs.Length; i++)
            inactiveObjs[i] = new List<GameObject>(splitCapacity);

        for (int i = 0; i < variationCount; i++)
        {
            GameObject prefab = prefabs[i];
            for (int j = 0; j < splitCapacity; j++)
            {
#if UNITY_EDITOR
                GameObject go = (GameObject)PrefabUtility.InstantiatePrefab(prefab, transform);
                go.name = go.name + " " + j;
#else
            GameObject go = Instantiate(prefabs[i], transform);
            //TOOD: Try InstantiateAsync()
#endif
                go.SetActive(false);
                if (go.GetComponent<ObjectPoolVariation_Id>() == null)
                    go.AddComponent<ObjectPoolVariation_Id>().Init(i);

                objs[i].Add(go);
                inactiveObjs[i].Add(go);
            }
        }

        initDone = true;

        totalObjsCount = null;
        inactiveObjsCount = null;
        activeObjsCount = null;

        totalObjsCount = new int[variationCount];
        inactiveObjsCount = new int[variationCount];
        activeObjsCount = new int[variationCount];
    }

    private void Update()
    {
#if UNITY_EDITOR
        for (int i = 0; i < variationCount; i++)
        {
            totalObjsCount[i] = objs[i].Count;
            inactiveObjsCount[i] = inactiveObjs[i].Count;
            activeObjsCount[i] = totalObjsCount[i] - inactiveObjsCount[i];
        }
#endif
    }

    public GameObject GetRandom(Vector2 position)
    {
        return Get(UnityEngine.Random.Range(0, variationCount), position, Quaternion.identity);
    }

    public GameObject Get(int variation, Vector2 position)
    {
        return Get(variation, position, Quaternion.identity);
    }

    public GameObject Get(int variation, Vector2 position, Quaternion rotation)
    {
        GameObject go;

        int inactiveCount = inactiveObjs[variation].Count;
        if (inactiveCount > 0)
        {
            go = inactiveObjs[variation][inactiveCount - 1];
            inactiveObjs[variation].RemoveAt(inactiveCount - 1);
#if UNITY_EDITOR
            if (go == null)
                Debug.LogError("GameObject in inactiveObjs[] in ObjectPool.cs is null/destoryed");
#endif
            go?.transform.SetPositionAndRotation(position, rotation);
        }
        else
        {
#if UNITY_EDITOR
            if (Application.isPlaying)
                go = Instantiate(prefabs[variation], position, rotation, transform);
            else
                go = (GameObject)PrefabUtility.InstantiatePrefab(prefabs[variation], transform);
            go.transform.SetPositionAndRotation(position, rotation);
#else
            go = Instantiate(prefabs[variation], position, rotation, transform);
#endif

            if (go.GetComponent<ObjectPoolVariation_Id>() == null)
                go.AddComponent<ObjectPoolVariation_Id>().Init(variation);

            objs[variation].Add(go);
        }

        go.SetActive(true);

        return go;
    }

    public void Release(int variation, GameObject go)
    {
#if UNITY_EDITOR
        if (ifCheckInactive && inactiveObjs[variation].Contains(go))
        {
            Debug.LogError("Releasing an object that's already inside the inactive list");
            // NOT returning to catch the bug in case I missed the error message
            //return;
        }
#endif

        go.SetActive(false);
        inactiveObjs[variation].Add(go);
    }

    public void Release(GameObject go)
    {
        int variation = go.GetComponent<ObjectPoolVariation_Id>().VariationId;

#if UNITY_EDITOR
        if (ifCheckInactive && inactiveObjs[variation].Contains(go))
        {
            Debug.LogError("Releasing an object that's already inside the inactive list");
            // NOT returning to catch the bug in case I missed the error message
            //return;
        }
#endif

        go.SetActive(false);
        inactiveObjs[variation].Add(go);
    }

    public void DestroyAll()
    {
        if (objs == null)
            return;

        foreach (var objVariation in objs) 
        {
            foreach (var obj in objVariation)
            {
                GameObject go = obj.gameObject;

                DestroyImmediate(go);
            }

            objVariation.Clear();
        }

        foreach (var obj in inactiveObjs)
        {
            obj.Clear();
        }

        for(int i = 0; i < variationCount; i++)
        {
            objs[i] = null;
            inactiveObjs[i] = null;
        }

        objs = null;
        inactiveObjs = null;

        initDone = false;
    }

    [Button]
    public void DebugText()
    {
        for (int i = 0; i < variationCount; i++)
        {
            print(i + " - Object count: " + objs[i]);
            print(i + " - Inactive Object count: " + inactiveObjs[i]);

        }
    }
}