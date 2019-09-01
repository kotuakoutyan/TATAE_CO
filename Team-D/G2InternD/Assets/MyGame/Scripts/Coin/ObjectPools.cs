using UnityEngine;
using System.Collections.Generic;

/// <typeparam name="T">MonoBehaviourを継承したクラス</typeparam>
public class ObjectPool<T> where T : MonoBehaviour
{
    private List<T> ObjectsList = new List<T>();
    [SerializeField] private GameObject Object;
    
    public ObjectPool(GameObject @object)
    {
        Object = @object;
    }

    public void CreatePool(int maxCount)
    {
        for (int i = 0; i < maxCount; i++)
        {
            var newObj = CreateNewObject();
            newObj.gameObject.SetActive(false);
            ObjectsList.Add(newObj);
        }
    }

    public T GetObject()
    {
        

        var objectsNum = ObjectsList.Count;
        
        for (int i = 0; i < objectsNum; i++)
        {
            var @object = ObjectsList[i];
            if (@object.gameObject.activeSelf == false) return @object;
        }
        return null;
        //var newObj = CreateNewObject();
        //ObjectsList.Add(newObj);
        //return newObj;
    }

    /// <summary>
    /// 使用中でないObjectsを探しでArrayで返す
    /// </summary>
    /// <param name="requestNum">必要なObjectsの数</param>
    public T[] GetObjects(int requestNum = 1)
    {
        if (requestNum < 1) return null;

        var array = new T[requestNum];
        var supplyNum = 0;
        var objectsNum = ObjectsList.Count;
        for (int i = 0; i < objectsNum; i++)
        {
            var @object = ObjectsList[i];
            if (@object.gameObject.activeSelf == false)
            {
                array[supplyNum] = @object;
                supplyNum++;
                if (supplyNum == requestNum) return array;
            }
        }

        return array;

        // 全て使用中だったら新しく作って返す
        while (supplyNum < requestNum)
        {
            var newObj = CreateNewObject();
            ObjectsList.Add(newObj);
            array[supplyNum] = newObj;
            supplyNum++;
        }
        return array;
    }

    /// <summary>
    /// Bulletを作り、それを返すメソッド
    /// </summary>
    private T CreateNewObject()
    {
        var newObj = UnityEngine.Object.Instantiate(Object);
        if(!newObj.GetComponent<T>()) newObj.AddComponent<T>();
        newObj.name = Object.name + (ObjectsList.Count + 1);

        return newObj.GetComponent<T>();
    }
}

