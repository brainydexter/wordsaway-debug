using UnityEngine;
using UnityEngine.UI;

using System.Collections;

public class ReadPlayerPrefs : MonoBehaviour {

	public void ReadPreferences()
	{
		resultTextUI.text = "Hello World";
	}

	#region Members
	[SerializeField]
	private Text resultTextUI;
	#endregion
}
