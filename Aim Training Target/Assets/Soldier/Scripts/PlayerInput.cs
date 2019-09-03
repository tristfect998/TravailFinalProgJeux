using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    PlayerAnimator animator = new PlayerAnimator();
    void Start()
    {

    }
    void Update()
    {
        if(Input.GetAxis("Sprint") == 1)
        {
            animator.Run(); //marche pas, juste un exemple.
        }
    }
}
