using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowPool : MonoBehaviour
{
    public static ShadowPool instance;

    public GameObject shadowPrefab;

    public int shadowCount;

    private Queue availableObjects = new Queue();

    void Awake()
    {
        instance = this;

        //��ʼ�������
        FillPool();
    }

    public void FillPool()
    {
        for (int i = 0; i < shadowCount; i++)
        {
            var newShadow = Instantiate(shadowPrefab);
            newShadow.transform.SetParent(transform);

            //ȡ������,���ض����
            ReturnPool(newShadow);
        }
    }

    public void ReturnPool(GameObject gameObject)
    {
        gameObject.SetActive(false);

        availableObjects.Enqueue(gameObject);
    }

    public GameObject GetFormPool()
    {
        if (availableObjects.Count == 0)
        {
            FillPool();
        }
        var outShadow = (GameObject)availableObjects.Dequeue();

        outShadow.SetActive(true);

        return outShadow;
    }
}