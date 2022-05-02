using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class ActionBouton : MonoBehaviour
{
	public Button.ButtonClickedEvent onClick;

	public Button ReStart;
	public Button ExitGame;


	public void Start()
	{
		Button btn = ReStart.GetComponent<Button>();
		Button btn_exit = ExitGame.GetComponent<Button>();

		btn.onClick.AddListener(TaskOnClick);
		btn_exit.onClick.AddListener(TaskOnClickExit);

	}

	public void TaskOnClick()
	{
		Debug.Log("La partie se recharge");
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		Time.timeScale = 1f;
		Cursor.lockState = CursorLockMode.Locked;
	}

	public void TaskOnClickExit()
	{
		Debug.Log("Game Terminer");
		Application.Quit();



	}
}
