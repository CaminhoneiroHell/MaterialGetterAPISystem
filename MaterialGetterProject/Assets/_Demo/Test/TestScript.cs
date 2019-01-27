using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    public int i;
    public float f;
    public string s;
    public Vector3 v;
    public Color c;
    public LayerMask l;
    public AnimationCurve a;
    public GameObject g;
    public Rigidbody r;
    public Transform t;
    public int[] ints;
    public List<float> floats;
    public C cl;
    public S sl;

    [System.Serializable]
    public class C
    {
        public int i;
        public float f;
    }


    [System.Serializable]
    public struct S
    {
        public int i;
        public float f;
    }

    private void OnEnable()
    {
        JsonUtility.FromJsonOverwrite(PlayerPrefs.GetString("json test"), this);
    }

    private void OnDisable()
    {
        
        PlayerPrefs.SetInt("test int", 666);
        PlayerPrefs.SetString("json test", JsonUtility.ToJson(this, true));
        PlayerPrefs.Save();

    }

    //This method runs whenever a changes are made to the serialized information in the editor
    private void OnValidate()
    {
    }

}
