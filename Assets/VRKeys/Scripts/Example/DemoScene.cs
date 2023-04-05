
using UnityEngine;
using System;
using System.Text.RegularExpressions;
using System.Collections;

namespace VRKeys {

	/// <summary>
	/// Example use of VRKeys keyboard.
	/// </summary>
	public class DemoScene : MonoBehaviour
	{

		/// <summary>
		/// Reference to the VRKeys keyboard.
		/// </summary>
		public Keyboard keyboard;


		private void OnEnable()
		{

			keyboard.Enable();
			keyboard.SetPlaceholderMessage("Please enter your email address");


		}


		private void Update()
		{
			if (Input.GetKeyDown(KeyCode.Space))
			{
				if (keyboard.disabled)
				{
					keyboard.Enable();
				}
				else
				{
					keyboard.Disable();
				}
			}

			if (keyboard.disabled)
			{
				return;
			}

			if (Input.GetKeyDown(KeyCode.Q))
			{
				//keyboard.SetLayout(KeyboardLayout.Qwerty);
			}
			else if (Input.GetKeyDown(KeyCode.F))
			{
				//keyboard.SetLayout(KeyboardLayout.French);
			}
			else if (Input.GetKeyDown(KeyCode.D))
			{
				//keyboard.SetLayout(KeyboardLayout.Dvorak);
			}

		}
	}

		
	}
