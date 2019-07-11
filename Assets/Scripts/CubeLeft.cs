using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeLeft : MonoBehaviour
{
    public static int j = 0;
    public GameObject objet;
    private void OnTriggerEnter()
    {
        j++;
        if (j - Enter.cpt > 1)
            j = Enter.cpt + 1;
    }
}
