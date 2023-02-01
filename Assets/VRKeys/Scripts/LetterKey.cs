
using UnityEngine;
using System.Collections;
using TMPro;

namespace VRKeys {

	/// <summary>
	/// An individual letter key.
	/// </summary>
	public class LetterKey : Key {
		public TextMeshPro shiftedLabel;

		public string character = "";

		public string shiftedChar = "";

		private bool _shifted = false;

		public bool shifted {
			get { return _shifted; }
			set {
				_shifted = value;
				label.text = _shifted ? shiftedChar : character;
				shiftedLabel.text = _shifted ? character : shiftedChar;
			}
		}

		public string GetCharacter () {
			return _shifted ? shiftedChar : character;
		}

		public override void HandleTriggerEnter (Collider other) {
			keyboard.AddCharacter (GetCharacter ());

			ActivateFor (0.3f);
		}
	}
}