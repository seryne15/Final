using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collision : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnCollisionEnter(Collision collisiion)
    {
        if (collisiion.collider)
        {
            collisiion.gameObject.GetComponent<Renderer>().enabled = false;
        }
    }
}
