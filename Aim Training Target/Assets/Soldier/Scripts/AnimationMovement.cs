using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AimTrainingTarget.Soldier
{
    public class AnimationMovement : MonoBehaviour
    {
        Animator anim;
        bool vAxisCrouchInUse = false;
        bool vAxisProneInUse = false;
        bool vAiming = false;
        bool vWalking = false;
        bool vRunning = false;
        bool vJumped = false;
        public static AudioClip crouchSound;
        static AudioSource audioSrc;
        // Use this for initialization
        void Start()
        {
            crouchSound = Resources.Load<AudioClip>("crouching sound");
            anim = GetComponent<Animator>();
            audioSrc = GetComponent<AudioSource>();
            audioSrc.clip = crouchSound;
        }

        // Update is called once per frame
        void Update()
        {
            #region Crouch
            if (Input.GetAxisRaw("Crouch") == 1)
            {
                audioSrc.Play();
                if (vAxisCrouchInUse == false)
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
                audioSrc.Play();
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

            #region Walking
            if (Input.GetAxisRaw("Vertical") == 1)
            {
                vWalking = true;
            }
            else
            {
                vWalking = false;
            }

            if (vWalking == true)
            {
                anim.SetBool("isWalking", true);
            }
            else
            {
                anim.SetBool("isWalking", false);
            }
            #endregion

            #region Sprint
            if (Input.GetAxisRaw("Sprint") == 1)
            {
                vRunning = true;
            }
            else
            {
                vRunning = false;
            }

            if (vRunning == true)
            {
                anim.SetBool("isRunning", true);
                anim.SetBool("isCrouch", false);
                anim.SetBool("isStanding", true);
            }
            else
            {
                anim.SetBool("isRunning", false);
            }
            #endregion

            #region Jump
            if (Input.GetAxisRaw("Jump") == 1)
            {
                audioSrc.Play();
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
            #endregion
        }
    }
}
