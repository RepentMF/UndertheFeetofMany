using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
	public string sceneToLoad;
	public bool startTransition;
	public Vector2 playerPosition;
	public Vector2 playerDirection;
	public GameObject fadeInPanel;
	public GameObject fadeOutPanel;
	public float fadeWait;
	public RoomManager RoomScript;

	private void Awake()
	{
		if(RoomScript == null)
		{
			RoomScript = FindObjectOfType<RoomManager>();
		}
		
		if(fadeInPanel != null)
		{
			GameObject panel = Instantiate(fadeInPanel, Vector3.zero, Quaternion.identity) as GameObject;
			Destroy(panel, 1);
			//RoomScript.SetTreasureInRoom();
		}
		GameStateManager.Instance.OnGameStateChanged += OnGameStateChanged;
	}

	private void OnGameStateChanged(GameState newGameState)
    {
        enabled = newGameState == GameState.Gameplay;
    }
 
    void OnDestroy()
    {
        if (GameStateManager.Instance != null)
            GameStateManager.Instance.OnGameStateChanged -= OnGameStateChanged;
    }

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.CompareTag("P1") && !collision.isTrigger)
		{
			startTransition = true;
			collision.GetComponent<PlayerController>().transform.position = playerPosition;
			collision.GetComponent<PlayerController>().SetAnimatorFloats(playerDirection);
			collision.GetComponent<PlayerController>().NextScene = sceneToLoad;
			FindObjectOfType<MusicPlayerController>().volumeChange = true;
			GameObject.FindWithTag("Minimap").gameObject.SetActive(false);
			StartCoroutine(FadeCo());
		}
	}

	public  void EnterFromDialogue(bool enter)
	{
		if (enter)
		{
			startTransition = true;
			GameObject.FindObjectOfType<PlayerController>().transform.position = playerPosition;
			GameObject.FindObjectOfType<PlayerController>().SetAnimatorFloats(playerDirection);
			GameObject.FindObjectOfType<PlayerController>().NextScene = sceneToLoad;
			FindObjectOfType<MusicPlayerController>().volumeChange = true;
			GameObject.FindWithTag("Minimap").gameObject.SetActive(false);
			StartCoroutine(FadeCo());
		}
		
	}

	public IEnumerator FadeCo()
	{
		if(fadeOutPanel != null)
		{
			Instantiate(fadeOutPanel, Vector3.zero, Quaternion.identity);
		}
		yield return new WaitForSeconds(fadeWait);

		AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneToLoad);
		
		while (!asyncOperation.isDone)
		{
			FindObjectOfType<RoomManager>().sceneChange = true;
			startTransition = false;
			FindObjectOfType<PlayerController>().UISearch = true;
			yield return null;
		}
	}

	void FixedUpdate()
	{
		if(RoomScript == null)
		{
			RoomScript = FindObjectOfType<RoomManager>();
		}
	}
}