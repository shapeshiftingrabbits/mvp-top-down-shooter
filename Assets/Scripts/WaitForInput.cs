﻿using Managers; using UnityEngine;  public class WaitForInput : StateMachineBehaviour {      public GameObject gameOverManagerObject;      // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state     override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)     {         GameOverManager gameOverManager = gameOverManagerObject.GetComponent<GameOverManager>();         gameOverManager.enableListeningForUserInput();     } } 