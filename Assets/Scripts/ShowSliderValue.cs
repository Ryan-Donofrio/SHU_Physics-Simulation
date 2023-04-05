using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class ShowSliderValue : MonoBehaviour
{
	public void UpdateLabelGravity (float value)
	{
		Text lbl = GetComponent<Text>();
		if (lbl != null)
			lbl.text = (value) + "g";
	}
	
	public void UpdateLabelMass (float value)
	{
		Text lbl = GetComponent<Text>();
		if (lbl != null)
			lbl.text = (value) + "m";
	}

	public void UpdateLabelAngle(float value)
	{
		Text lbl = GetComponent<Text>();
		if (lbl != null)
			lbl.text = (value) + "%";
	}

	public void UpdateLabelLaunchForce(float value)
	{
		Text lbl = GetComponent<Text>();
		if (lbl != null)
			lbl.text = (value) + "f";
	}

	public void UpdateLabelObjectHeight(float value)
	{
		Text lbl = GetComponent<Text>();
		if (lbl != null)
			lbl.text = (value) + "m";
	}
}
