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
        bool vWalkingRight = false;
        bool vWalkingLeft = false;
        bool vWalkingBack = false;
        bool vRunning = false;
        bool vJumped = false;
        bool gunPositionChanged = false;
        AudioSource audioSrc;
        AudioClip movementClip;
        bool isPause;
        public GameObject rightHandObj;
        public GameObject leftHandObj;
        public GameObject GunSlot;
        public GameObject AimingGunSlot;
        public GameObject HipFireGunSlot;
        public GameObject Crosshair;

        // Use this for initialization
        void Start()
        {
            anim = GetComponent<Animator>();
            movementClip = Resources.Load<AudioClip>("crouching sound");
            audioSrc = GetComponent<AudioSource>();
            isPause = false;
        }

        void OnAnimatorIK(int layerIndex)
        {
            if (rightHandObj != null)
            {
                anim.SetIKPositionWeight(AvatarIKGoal.RightHand, 1);
                anim.SetIKRotationWeight(AvatarIKGoal.RightHand, 1);
                anim.SetIKPosition(AvatarIKGoal.RightHand, rightHandObj.transform.position);
                anim.SetIKRotation(AvatarIKGoal.RightHand, rightHandObj.transform.rotation);
            }
            if (leftHandObj != null)
            {
                anim.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1);
                anim.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1);
                anim.SetIKPosition(AvatarIKGoal.LeftHand, leftHandObj.transform.position);
                anim.SetIKRotation(AvatarIKGoal.LeftHand, leftHandObj.transform.rotation);
            }
        }

        // Update is called once per frame
        void Update()
        {
            #region Crouch
            if (Input.GetAxisRaw("Crouch") == 1)
            {
                if (vAxisCrouchInUse == false)
                {
                    audioSrc.PlayOneShot(movementClip);
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
                    audioSrc.PlayOneShot(movementClip);
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
            if (Input.GetAxisRaw("Aim") != 0)
            {
                vAiming = true;
                MoveGunToAim();
            }
            else
            {
                vAiming = false;
                MoveGunToAim();
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

            #region WalkingRight
            if (Input.GetAxisRaw("Horizontal") == 1)
            {
                vWalkingRight = true;
            }
            else
            {
                vWalkingRight = false;
            }

            if (vWalkingRight == true)
            {
                anim.SetBool("isWalkingRight", true);
            }
            else
            {
                anim.SetBool("isWalkingRight", false);
            }
            #endregion

            #region WalkingLeft
            if (Input.GetAxisRaw("Horizontal") == -1)
            {
                vWalkingLeft = true;
            }
            else
            {
                vWalkingLeft = false;
            }

            if (vWalkingLeft == true)
            {
                anim.SetBool("isWalkingLeft", true);
            }
            else
            {
                anim.SetBool("isWalkingLeft", false);
            }
            #endregion

            #region WalkingBack
            if (Input.GetAxisRaw("Vertical") == -1)
            {
                vWalkingBack = true;
            }
            else
            {
                vWalkingBack = false;
            }

            if (vWalkingBack == true)
            {
                anim.SetBool("isWalkingBack", true);
            }
            else
            {
                anim.SetBool("isWalkingBack", false);
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

            #region Escape
            if (Input.GetKeyDown("escape"))
            {
                if (isPause)
                {
                    //Time.timeScale = 1;
                    //EscapeMenu.SetActive(false);
                    isPause = false;
                }
                else
                {
                    //Time.timeScale = 0;
                    //EscapeMenu.SetActive(true);                    
                    isPause = true;
                }
            }
            #endregion
        }

        void MoveGunToAim()
        {
            if (vAiming == true)
            {
                if (!gunPositionChanged)
                {
                    audioSrc.PlayOneShot(movementClip);
                    gunPositionChanged = true;
                    GunSlot.transform.localPosition = AimingGunSlot.transform.localPosition;
                    GunSlot.transform.localRotation = AimingGunSlot.transform.localRotation;
                    Crosshair.SetActive(false);
                }
            }
            else
            {
                gunPositionChanged = false;
                GunSlot.transform.localPosition = HipFireGunSlot.transform.localPosition;
                GunSlot.transform.localRotation = HipFireGunSlot.transform.localRotation;
                Crosshair.SetActive(true);
            }
        }
    }
}