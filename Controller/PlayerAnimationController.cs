using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : CharacterController
{ 
   private Animator animator;
    
    private void Awake()
    {
        animator = GetComponent<Animator>(); // 추가
    }
}
