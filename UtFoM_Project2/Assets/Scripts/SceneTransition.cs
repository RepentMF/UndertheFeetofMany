using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
	public string sceneToLoad;
	public Vector2 playerPosition;
	public Vector2 playerDirection;
	public VectorValue playerNewPos;
	public VectorValue playerNewDir;
	public GameObject fadeInPanel;
	public GameObject fadeOutPanel;
	public float fadeWait;

	private void Awake()
	{
		if(fadeInPanel != null)
		{
			GameObject panel = Instantiate(fadeInPanel, Vector3.zero, Quaternion.identity) as GameObject;
			Destroy(panel, 1);
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.CompareTag("P1") && !collision.isTrigger)
		{
			FindObjectOfType<RoomManager>().sceneChange = true;
			playerNewPos.initialValue = playerPosition;
			playerNewDir.initialValue = playerDirection;
			collision.GetComponent<PlayerMovement>().newScene = sceneToLoad;
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
			yield return null;
		}
	}
}
