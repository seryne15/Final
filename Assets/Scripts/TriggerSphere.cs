using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerSphere : MonoBehaviour
{
    public GameObject objet;
    int i = 0;
    private void OnTriggerEnter()
    {
        objet.gameObject.GetComponent<Renderer>().material.color = Color.green;
    }
}
