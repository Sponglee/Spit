﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : Singleton<InputManager>
{


    public Joystick joystick;

    public Vector2 input;
  

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {

            input = new Vector2(joystick.Horizontal,joystick.Vertical);

        }
    }
}
