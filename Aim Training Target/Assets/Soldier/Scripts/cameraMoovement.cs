using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraMoovement : MonoBehaviour {
    bool isCrouch =false;
    bool isProne = false;
    bool vAxisCrouchInUse = false;
    bool vAxisProneInUse = false;
    string CurrentIdle = "Standing";
    // Use this for initialization

    // Update is called once per frame
    void Update()
    {
  
        #region crouch
        if (Input.GetAxisRaw("Crouch") == 1 )
        {
            if (CurrentIdle != "Prone")
            {
                if (vAxisCrouchInUse == false)
                {
                    if (isCrouch == false && CurrentIdle != "Prone")
                    {
                        CurrentIdle = "Crouch";
                        transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y - 0.6f, transform.localPosition.z + 0.26f);
                        isCrouch = true;
                    }
                    else
                    {
                        CurrentIdle = "Standing";
                        isCrouch = false;
                        transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y + 0.6f, transform.localPosition.z - 0.26f);

                    }
                    vAxisCrouchInUse = true;
                }
            }
            else
            {
                CurrentIdle = "Crouch";
                isProne = false;
                isCrouch = true;
                transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y + 0.68f, transform.localPosition.z -0.28f);
                vAxisCrouchInUse = true;
            }
        }
        else
        {
            vAxisCrouchInUse = false;
        }
        #endregion


        if (Input.GetAxisRaw("Prone") == 1)
        {
                if (vAxisProneInUse == false)
                {
                    if (isProne == false)
                    {
                    
                         if (CurrentIdle == "Standing")
                         {
                             transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y - 1.28f, transform.localPosition.z + 0.54f); 
                          }
                         else
                         {
                             transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y - 0.68f, transform.localPosition.z + 0.28f);
                         }
                         CurrentIdle = "Prone";
                         isProne = true;
                         isCrouch = false;
                    }
                    else
                    {
                        CurrentIdle = "Crouch";
                        isProne = false;
                        isCrouch = true;
                        transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y + 0.68f, transform.localPosition.z - 0.28f);
                    }
                    vAxisProneInUse = true;
                }
        }
        else
        {
            vAxisProneInUse = false;
        }
       
    }
}
