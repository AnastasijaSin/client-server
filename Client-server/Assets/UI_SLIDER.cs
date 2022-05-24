using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class UI_SLIDER : MonoBehaviour
{
    public Functionyren functionyren;
    Slider slider;

    private void Start()
    {
        slider = GetComponent<Slider>();
        slider.onValueChanged.AddListener(UpdateValue);
    }

    private void UpdateValue(float value)
    {
        functionyren.ChangeFreque((int)value);
    }
}
