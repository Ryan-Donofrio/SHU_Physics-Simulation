using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class CullingMask : MonoBehaviour
{

    //public int cullingMask;
    public Camera cameraview;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        cameraview.cullingMask = 1 << LayerMask.NameToLayer("MenuUIElement");
        cameraview.cullingMask = 1 << LayerMask.NameToLayer("MenuUIElement");
    }


}
