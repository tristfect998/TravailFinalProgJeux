using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moovement : MonoBehaviour
{
    Animator anim;
    bool vAxisCrouchInUse = false;
    bool vAxisProneInUse = false;
    bool vAiming = false;
    bool vJumped = false;
    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        #region Crouch
        if (Input.GetAxisRaw("Crouch") == 1)
        {
            
            if(vAxisCrouchInUse == false)
            {
                if (!anim.GetBool("isCrouch"))
                {
                    anim.SetBool("isCrouch", true);
                    anim.SetBool("isStanding", false);
                    anim.SetBool("isProne", false);
                }
                else
                {
                    anim.SetBool("isCrouch", false);
                    anim.SetBool("isStanding", true);

                }
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
                if (!anim.GetBool("isProne"))
                {
                    anim.SetBool("isCrouch", false);
                    anim.SetBool("isStanding", false);
                    anim.SetBool("isProne", true);
                }
                else
                {
                    anim.SetBool("isCrouch", true);
                    anim.SetBool("isProne", false);

                }
                vAxisProneInUse = true;
            }

        }
        else
        {
            vAxisProneInUse = false;
        }
        #endregion

        #region Aiming
        if (Input.GetAxisRaw("Aim") == 1)
        {
            vAiming = true;
        }
        else
        {
            vAiming = false;
        }

        if (vAiming == true)
        {
            anim.SetBool("isAiming", true);
        }
        else
        {
            anim.SetBool("isAiming", false);
        }
        #endregion

        if (Input.GetAxisRaw("Jump") == 1)
        {

            if (vJumped == false)
            {
                if (anim.GetBool("isStanding"))
                {
                    anim.SetBool("isJumping", true);
                }
                else
                {
                    anim.SetBool("isCrouch", false);
                    anim.SetBool("isProne", false);
                    anim.SetBool("isStanding", true);

                }
                vJumped = true;
            }

        }
        else
        {
            anim.SetBool("isJumping", false);
            vJumped = false;
        }

    }
}
