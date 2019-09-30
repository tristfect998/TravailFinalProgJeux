using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {

    bool isCrouch =false;
    bool isProne = false;
    bool vAxisCrouchInUse = false;
    bool vAxisProneInUse = false;
    bool vJumped = false;
    string CurrentIdle = "Standing";


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

        #region Prone
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
        #endregion

        #region Jump
        if (Input.GetAxisRaw("Jump") == 1)
        {
 
            if (vJumped == false)
            {
                if (CurrentIdle != "Standing")
                {
                    if (isCrouch)
                    {
                        transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y + 0.6f, transform.localPosition.z - 0.28f);
                        isCrouch = false;
                        isProne = false;
                        CurrentIdle = "Standing";
                    }
                    else
                    {
                        transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y + 1.28f, transform.localPosition.z - 0.54f);
                        CurrentIdle = "Standing";
                        isCrouch = false;
                        isProne = false;
                    }
                }
                vJumped = true;
            }
           
            }
        else
        {
            vJumped = false;
        }
        #endregion
    }
}
