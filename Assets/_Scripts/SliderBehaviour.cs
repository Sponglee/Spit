using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderBehaviour : MonoBehaviour
{
    public Slider slider;
    // Start is called before the first frame update
    void Start()
    {
        EnergyManager.OnEnergyChange.AddListener(UpdateSlider);
    }


    public void UpdateSlider(float targetValue)
    {
        slider.value = targetValue;

    }
  
}
