using System;
using UnityEngine;

[System.Serializable]
public enum SignalType
{
	Sin = 1,
	Square = 2,
	Triang = 3
}

[System.Serializable]
public class FunctionData
{
	public SignalType Type = SignalType.Sin;

	public bool Accept = false;

	public int Frequence = 1;
}

public class Functionyren : MonoBehaviour
{
	public FunctionData Data;

	public float Sin { 
		get 
		{
			// When calling Functionyren.Sin will be the calculation of the value and then the return of the result
			return (float)Math.Sin(2f * Math.PI * t); 
		}
	}
	public float Square { 
		get 
		{
			// When calling Functionyren.Square will be the calculation of the value and then the return of the result
			return Math.Sign(Math.Sin(2f * Math.PI * t)); 
		} 
	}
	public float Triangle { 
		get 
		{
			// When calling Functionyren.Triangle will be the calculation of the value and then the return of the result
			return 1f - 4f * (float)Math.Abs(Math.Round(t - 0.25f) - (t - 0.25f)); 
		} 
	}

	// Event when data changes
	public event Action OnDataUpdated;

	public float t = 1f;

	// Function for changing the type
	public void ChangeSignalType(SignalType type)
    {
		//Смена типа
		Data.Type = type;
    }

	// Function for changing the type through a digit
	public void ChangeSignalType(int type)
    {
		//Смена типа
		Data.Type = (SignalType)type;

		OnDataUpdated?.Invoke();
	}

	// Function for changing the frequency
	public void ChangeFreque(int value)
    {
		//Смена частоты
		Data.Frequence = value;

		OnDataUpdated?.Invoke();
	}

	//Function to turn on/off
	public void SetAccept(bool value)
    {
		Data.Accept = value;

		OnDataUpdated?.Invoke();
	}
}
