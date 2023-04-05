using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SimVariables : MonoBehaviour
{

	public void SetGravity(float value)
	{
		OnValueChanged2(value, 1);
	}


	public void OnValueChanged2(float value, int channel)
	{
		Physics.gravity = new Vector3(0, value, 0);
	}


}