using UnityEngine;
using UnityEngine.UI;

using System.Text;
using System.Collections;

public class ReadPlayerPrefs : MonoBehaviour {

	#region Button Click handlers
	public void ReadPreferences()
	{
		AppendResult (LoadBoardsStatus());
	}

	public void Email()
	{
		var email = "fly2priyank@gmail.com";
		var subject = MyEscapeURL("debug report for wordsaway");
		var body = MyEscapeURL (LoadInitialStats() + "\n\n" + LoadBoardsStatus ());
		Application.OpenURL ("mailto:" + email + "?subject=" + subject + "&body=" + body);
	}

	string MyEscapeURL (string url) 
	{
		return WWW.EscapeURL(url).Replace("+","%20");
	}
	#endregion

	#region Mono Methods
	void Start()
	{
		AppendResult (LoadInitialStats ());
	}
	#endregion

	#region Stats methods

	private string LoadInitialStats()
	{
		var strBuilder = new StringBuilder ();

		bool previousRun = PlayerPrefs.HasKey (Constants.Persist.PREVIOUS_RUN);
		if (!previousRun) {
			AppendError( "Game has never been installed before. ");
			return "Game has never been installed before.";
		} else
			strBuilder.AppendLine ("Game has been installed on this device. ");

		strBuilder.AppendLine ("Version: " + ReadF (Constants.Persist.VERSION));
		strBuilder.AppendLine ("Current Board: " + ReadI (Constants.Persist.CURRENT_BOARD_ID));

		return strBuilder.ToString ();
	}

	private string LoadBoardsStatus()
	{
		var strBuilder = new StringBuilder ();
		strBuilder.AppendLine ("Boards Status:");
		// read board info
		for (int boardId = 1; boardId < 100; boardId++) {
			int status = ReadI (boardId + Constants.Persist.Board.STATUS);
			var boardStatus = ConvertToBoardStatus(status);
			strBuilder.AppendFormat ("Board {0}: {1}\n", boardId, boardStatus);	
		}

		return strBuilder.ToString ();
	}

	#endregion

	#region Utility Methods

	private string ConvertToBoardStatus(int status)
	{
		switch (status) {
		case -1:
			return "LOCKED";
		case 0:
			return "INCOMPLETE";
		case 1:
			return "COMPLETED";
		default:
			{
				var err = "Unknown status: " + status;
				AppendError (err);
				return err;
			}
		}
	}

	private float ReadF(string key){
		Debug.Assert (key.Length != 0, "[ReadPlayerPrefs]: Key cannot be empty");
		if(key.Length == 0)
			AppendError("[ReadPlayerPrefs]: Key cannot be empty");

		float notFound = 140395f;

		var value = PlayerPrefs.GetFloat (key, notFound);

		Debug.Assert(value != notFound, "[ReadPlayerPrefs]: Key " + key + " not found");

		return value;
	}

	private int ReadI(string key){
		Debug.Assert (key.Length != 0, "[ReadPlayerPrefs]: Key cannot be empty");
		if (key.Length == 0)
			AppendError ("[ReadPlayerPrefs]: Key cannot be empty");

		int notFound = 140395;

		var value = PlayerPrefs.GetInt (key, notFound);

		Debug.Assert(value != notFound, "[ReadPlayerPrefs]: Key " + key + " not found");

		return value;
	}

	private void AppendResult(string result)
	{
		AppendText (resultTextUI, result);
	}

	private void AppendError(string errorString)
	{
		AppendText (errorTextUI, errorString);
	}

	private void AppendText(Text textUI, string str)
	{
		textUI.text += '\n' + str;
	}

	#endregion

	#region Members
	[SerializeField]
	private Text resultTextUI;

	[SerializeField]
	private Text errorTextUI;
	#endregion
}
