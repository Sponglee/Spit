using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnegryChangeEvent : UnityEvent<float> { };


public class EnergyManager : IncludeManagers
{
    public static EnegryChangeEvent OnEnergyChange = new EnegryChangeEvent();


    [SerializeField] private float energyRate = 1;
    [SerializeField] private float maxEnergy = 100f;
    [SerializeField] private float energy;
    public float Energy
    {
        get
        {
            return energy;
        }

        set
        {
            energy = value;
            OnEnergyChange?.Invoke(value / maxEnergy);

            if(value <= 0)
            {
                gameManager.GameState = GameManager.GameStates.NoEnergy;
            }
            else if(value > maxEnergy)
            {
                value = maxEnergy;
                GameManager.Instance.GameState = GameManager.GameStates.CanFly;
            }
        }
    }



 
    void Start()
    {
        Energy = maxEnergy;
    }


    void FixedUpdate()
    {
        if(gameManager.GameState == GameManager.GameStates.IsFlying)
        {
            Energy -= energyRate * (/*Mathf.Abs(inputManager.input.x)*/ + Mathf.Clamp(inputManager.input.y,0f,1f));
            
        }
        else if(gameManager.GameState == GameManager.GameStates.NoEnergy)
        {
            if (energy >= maxEnergy)
            {
                GameManager.Instance.GameState = GameManager.GameStates.CanFly;

            }

        }
        else if(gameManager.GameState == GameManager.GameStates.Rest)
        {
            if (energy <= maxEnergy)
            {
                Energy += energyRate * 5f;
            }
          
        }
    }



}
