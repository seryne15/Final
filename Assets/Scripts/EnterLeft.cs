using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterLeft : MonoBehaviour
{
    int i = 0;
    public GameObject objet;
    private void OnTriggerEnter()
    {

        i++;
        switch (i)
        {
            case 1:
                objet.gameObject.GetComponent<Renderer>().material.color = Color.red;
                objet.gameObject.transform.position = new Vector3(12, 12.02f, -47.35f);
                break;
            case 2:
                objet.gameObject.GetComponent<Renderer>().material.color = Color.green;
                objet.gameObject.transform.position = new Vector3(12, 4, -47.35f);
                break;
            case 3:
                objet.gameObject.GetComponent<Renderer>().material.color = Color.black;
                objet.gameObject.transform.position = new Vector3(8, 9, -47.35f);
                break;
            case 4:
                objet.gameObject.GetComponent<Renderer>().material.color = Color.cyan;
                objet.gameObject.transform.position = new Vector3(16.5f, 9, -47.35f);
                break;
            case 5:
                objet.gameObject.GetComponent<Renderer>().material.color = Color.magenta;
                break;
        }
    }
}
