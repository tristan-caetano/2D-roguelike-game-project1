﻿using Edgar.Unity.Examples.PC2D;
using UnityEngine;
using Input = UnityEngine.Input;

namespace Edgar.Unity.Examples
{
    [RequireComponent(typeof(PlatformerMotor2D))]
    public class PlatformerPlayerController : MonoBehaviour
    {
        private PlatformerMotor2D motor;

        public void Start()
        {
            motor = GetComponent<PlatformerMotor2D>();
        }

        void Update()
        {
            // Disable one-way platforms when pressing DOWN button
            if (Input.GetAxisRaw("Vertical") < 0)
            {
                motor.enableOneWayPlatforms = false;
                motor.oneWayPlatformsAreWalls = false;
                motor.Jump(0f);
            }
            else
            {
                motor.enableOneWayPlatforms = true;
                motor.oneWayPlatformsAreWalls = true;
            }

            // Jump?
            // If you want to jump in ladders, leave it here, otherwise move it down
            if (Input.GetButtonDown(PC2D.Input.JUMP))
            {
                motor.Jump();
                motor.DisableRestrictedArea();
            }

            motor.jumpingHeld = Input.GetButton(PC2D.Input.JUMP);

            // X axis movement
            if (Mathf.Abs(Input.GetAxis(PC2D.Input.HORIZONTAL)) > 0)
            {
                motor.normalizedXMovement = Input.GetAxis(PC2D.Input.HORIZONTAL);
            }
            else
            {
                motor.normalizedXMovement = 0;
            }

            if (Input.GetAxis(PC2D.Input.VERTICAL) != 0)
            {
                bool up_pressed = Input.GetAxis(PC2D.Input.VERTICAL) > 0;
            }
            else if (Input.GetAxis(PC2D.Input.VERTICAL) < -Globals.FAST_FALL_THRESHOLD)
            {
                motor.fallFast = false;
            }

            if (Input.GetButtonDown(PC2D.Input.DASH))
            {
                motor.Dash();
            }
        }
    }
}