using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cubeRight : MonoBehaviour
{
    public GameObject objet;
    public static int i = 0;
    private void OnTriggerEnter()
    {
        i++;
        if (i - Enter.cpt > 1)
            i = Enter.cpt+1;
        
    }
}
