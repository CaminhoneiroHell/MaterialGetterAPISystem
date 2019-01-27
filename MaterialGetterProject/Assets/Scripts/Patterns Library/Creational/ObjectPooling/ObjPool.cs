using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Patterns.Creational.Singleton;

public class ObjPool : MonoBehaviour
{

    //Prepare the object pool
    //1. Instantiate certain amount of objects
    //2. Deactivate all these objects
    //3. Save them in a Collection (Array/Queue)

    //When an Object is needed:
    //Instead of instantiating the object, just activate one from the pool.

    //When an object is no longer needed:
    //Deactivate it and put it back into the pool


    //Prepare the object pool

    public int poolSize;
    public List<GameObject> _objToPoolPrefab;
    
    private Queue<GameObject> _objToPool;

    private static ObjPool _myInstance;
    public static ObjPool _MyInstance
    {
        get
        {
            return _myInstance;
        }
    }

    public void Awake()
    {
        if (_myInstance == null)
        {
            _myInstance = this;
        }
        else if (_myInstance != this)
        {

        }

        InitPool(poolSize);
    }
    public void InitPool(int poolSize)
    {
        _objToPool = new Queue<GameObject>(); 
        for (int i = 0; i < poolSize; i++)
        {
            GameObject newPoolObj = Instantiate(_objToPoolPrefab[Random.Range(0, _objToPoolPrefab.Count)]);
            _objToPool.Enqueue(newPoolObj);
            newPoolObj.SetActive(false);
            //_bulletPool.ElementAt(i).SetActive(false);
            //_bulletPool[i].SetActive(false);
            //print(_bulletPool.ElementAt(i).name.ToString());
        };
    }

    public GameObject GetObjFromPool(Vector3 targetPos, Quaternion rotation)
    {
        //Extend our pool dynamicly in case of necessity
        if(_objToPool.Count <= 0)
        {
            GameObject objToPoolOverloadReplacer = Instantiate(_objToPoolPrefab[Random.Range(0, _objToPoolPrefab.Count)]);
            objToPoolOverloadReplacer.name = "NewBullet";
            objToPoolOverloadReplacer.SetActive(false);
            _objToPool.Enqueue(objToPoolOverloadReplacer);
        }


        GameObject newObjToPool = _objToPool.Dequeue();
        newObjToPool.SetActive(true);
        newObjToPool.transform.position = targetPos;
        newObjToPool.transform.rotation = rotation;
        
        newObjToPool.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        newObjToPool.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 20, ForceMode2D.Impulse);

        return newObjToPool;
    }

    public void ReturnObjToPool(GameObject go)
    {
        print("Return to pool");
        _objToPool.Enqueue(go);
        go.SetActive(false);
    }

}
