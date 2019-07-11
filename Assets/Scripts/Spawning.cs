using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawning : MonoBehaviour
{

    private List<GameObject> GoodFood = new List<GameObject>();
    private List<GameObject> BadFood = new List<GameObject>();

    [SerializeField] private GameObject Left;
    [SerializeField] private GameObject Right;
    [SerializeField] private GameObject LeftObj;
    [SerializeField] private GameObject RightObj;

    private bool enter = true;

    private void Awake()
    {
        LoadSpawningObjects();
    }

    // Update is called once per frame
    void Update()
    {
        if (!RendererExtension.IsVisibleFrom(LeftObj.GetComponent<MeshRenderer>(), Camera.main) && !RendererExtension.IsVisibleFrom(RightObj.GetComponent<MeshRenderer>(), Camera.main) && enter)
        {
            Destroy(LeftObj);
            Destroy(RightObj);
            RightObj = Instantiate(BadFood[Random.Range(0, BadFood.Count)], Vector3.zero, Quaternion.identity) as GameObject;
            LeftObj = Instantiate(GoodFood[Random.Range(0, GoodFood.Count)], Vector3.zero, Quaternion.identity) as GameObject;

            SetTransform(LeftObj, Left);
            SetTransform(RightObj, Right);
            enter = false;
        }
        else if (RendererExtension.IsVisibleFrom(LeftObj.GetComponent<MeshRenderer>(), Camera.main) && RendererExtension.IsVisibleFrom(RightObj.GetComponent<MeshRenderer>(), Camera.main))
            enter = true;
    }

    private void LoadSpawningObjects()
    {
        GoodFood.Add(Resources.Load<GameObject>("GoodFood/1"));
        GoodFood.Add(Resources.Load<GameObject>("BadFood/Cone"));
        GoodFood.Add(Resources.Load<GameObject>("GoodFood/3"));
        GoodFood.Add(Resources.Load<GameObject>("BadFood/CupCake"));
        GoodFood.Add(Resources.Load<GameObject>("GoodFood/5"));
        GoodFood.Add(Resources.Load<GameObject>("BadFood/Hamburger"));
        GoodFood.Add(Resources.Load<GameObject>("GoodFood/7"));

        BadFood.Add(Resources.Load<GameObject>("BadFood/Chocolate"));
        BadFood.Add(Resources.Load<GameObject>("GoodFood/2"));
        BadFood.Add(Resources.Load<GameObject>("GoodFood/4"));
        BadFood.Add(Resources.Load<GameObject>("BadFood/Donut"));
        BadFood.Add(Resources.Load<GameObject>("GoodFood/6"));
        BadFood.Add(Resources.Load<GameObject>("BadFood/Muffin"));
        BadFood.Add(Resources.Load<GameObject>("GoodFood/8"));

    }

    private void SetTransform(GameObject obj, GameObject parent)
    {
        obj.transform.parent = parent.transform;
        obj.transform.localEulerAngles = Vector3.zero;
        obj.transform.localPosition = Vector3.zero;

        if (obj == LeftObj)
        {
            obj.transform.localScale = new Vector3(2, 2, 2);            
        }
        else
        {
            obj.transform.localScale = Vector3.one;
        }

        BoxCollider col = obj.AddComponent<BoxCollider>();
        col.isTrigger = false;
        Rigidbody rig = obj.AddComponent<Rigidbody>();
        rig.useGravity = false;
        rig.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezePositionY
            | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY;

    }

}
