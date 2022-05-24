using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_VALUES : MonoBehaviour
{
    public TextMeshProUGUI FrequenceText;
    public TextMeshProUGUI AcceptText;
    public TextMeshProUGUI ValueText;

    public Functionyren functionyren;

    private void Update()
    {
        UpdateUI();
    }

    private void UpdateUI()
    {
        FrequenceText.text = "Frequence: " + functionyren.Data.Frequence;

        AcceptText.text = "Accept: " + functionyren.Data.Accept.ToString();

        //????????? ????? ?? ???????? ???????? ??? ??? ??????? ?????????
        if (functionyren.Data.Accept)
        {
            string value = "";

            // Selecting the type of signal to update the data
            switch (functionyren.Data.Type)
            {
                case SignalType.Sin:
                    value = "Sin: " + functionyren.Sin;
                    break;
                case SignalType.Square:
                    value = "Square: " + functionyren.Square;
                    break;
                case SignalType.Triang:
                    value = "Triangle: " + functionyren.Triangle;
                    break;
            }

            ValueText.text = value;
        }
        else
        {
            ValueText.text = "";
        }
    }
}
