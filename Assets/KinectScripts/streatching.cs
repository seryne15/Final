using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class streatching : MonoBehaviour
{

    //public GUITexture backgroundImage;
    public KinectWrapper.NuiSkeletonPositionIndex TrackedJoint = KinectWrapper.NuiSkeletonPositionIndex.HandLeft;
    public GameObject OverlayObject;
    public GameObject OverlayObject2;
    public float smoothFactor = 5f;

   // public GUIText debugText;

    private float distanceToCamera = 10f;

    // Start is called before the first frame update

    private void OnCollisionEnter(Collision collisiion)
    {
        if (collisiion.gameObject.name == "cube (1)")
        {
            
            OverlayObject2.GetComponent<Renderer>().enabled = false;
        }
    }

    void Start()
    {
        if (OverlayObject)
        {
            distanceToCamera = (OverlayObject.transform.position - Camera.current.transform.position).magnitude;
            distanceToCamera = 10.9f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        KinectManager manager = KinectManager.Instance;
        

        if (manager && manager.IsInitialized())
        {
            //backgroundImage.renderer.material.mainTexture = manager.GetUsersClrTex();
           // if (backgroundImage && (backgroundImage.texture == null))
            //{
             //   backgroundImage.texture = manager.GetUsersClrTex();
            //}

            //			Vector3 vRight = BottomRight - BottomLeft;
            //			Vector3 vUp = TopLeft - BottomLeft;

            int iJointIndex = (int)TrackedJoint;

            if (manager.IsUserDetected())
            {
                uint userId = manager.GetPlayer1ID();

                if (manager.IsJointTracked(userId, iJointIndex))
                {
                    Vector3 posJoint = manager.GetRawSkeletonJointPos(userId, iJointIndex);

                    if (posJoint != Vector3.zero)
                    {

                        // 3d position to depth
                        Vector2 posDepth = manager.GetDepthMapPosForJointPos(posJoint);

                        // depth pos to color pos
                        Vector2 posColor = manager.GetColorMapPosForDepthPos(posDepth);

                        float scaleX = (float)posColor.x / KinectWrapper.Constants.ColorImageWidth;
                        float scaleY = 1.0f - (float)posColor.y / KinectWrapper.Constants.ColorImageHeight;

                        //						Vector3 localPos = new Vector3(scaleX * 10f - 5f, 0f, scaleY * 10f - 5f); // 5f is 1/2 of 10f - size of the plane
                        //						Vector3 vPosOverlay = backgroundImage.transform.TransformPoint(localPos);
                        //Vector3 vPosOverlay = BottomLeft + ((vRight * scaleX) + (vUp * scaleY));


                        if (OverlayObject)
                        {
                            Vector3 vPosOverlay = Camera.current.ViewportToWorldPoint(new Vector3(scaleX, scaleY, distanceToCamera));
                            //OverlayObject.transform.position = new Vector3(OverlayObject.transform.position.x,Vector3.Lerp(OverlayObject.transform.position, vPosOverlay, smoothFactor * Time.deltaTime).y, Vector3.Lerp(OverlayObject.transform.position, vPosOverlay, smoothFactor * Time.deltaTime).z);
                            OverlayObject.transform.position = new Vector3(Vector3.Lerp(OverlayObject.transform.position, vPosOverlay, smoothFactor * Time.deltaTime).x, Vector3.Lerp(OverlayObject.transform.position, vPosOverlay, smoothFactor * Time.deltaTime).y, OverlayObject.transform.position.z);

                        }
                        //Debug.Log("x1  " + OverlayObject.transform.position.x);
                        //Debug.Log("x2  " + OverlayObject2.transform.position.x);
                        
                    }
                }


            }

        }
    }

   
}
