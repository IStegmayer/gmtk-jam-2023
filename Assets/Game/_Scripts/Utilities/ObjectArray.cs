using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectArray : MonoBehaviour
{
    public GameObject preFab;
    public Vector3 offset;
    public Vector3 rotationOffset;
    //public bool localOffsetFromRotation = true;
    public int arrayCount = 1;

    public bool LOCK = false;

    void OnValidate()
    {
        if(preFab == null || LOCK) return;
        //Destroy all children
        foreach(Transform child in transform)
            StartCoroutine(Destroy(child.gameObject));
        //Instantitate objects with offsets
        if(arrayCount <1 || arrayCount > 5000)
            Debug.Log("Invalid array length!");
        arrayCount = Mathf.Clamp(arrayCount,1,5000); // DONT DO TOO MANY
        Transform helper = new GameObject().transform;
        helper.position = transform.position;
        helper.rotation = transform.rotation;
        for(int i = 0; i < arrayCount;i++)
        {
            Instantiate(preFab, helper.position, helper.rotation, transform);
            helper.Translate(offset);
            helper.Rotate(rotationOffset);
        }

        StartCoroutine(Destroy(helper.gameObject));
    }

    IEnumerator Destroy(GameObject go)
    {
        yield return null;
        DestroyImmediate(go);
    }
}
