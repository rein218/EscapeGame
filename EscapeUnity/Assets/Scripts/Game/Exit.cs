using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Exit : MonoBehaviour
{
    public string levelName;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<CharacterController>(out var player))
        {
            StartCoroutine(ChangeSceneAfterDelay(2f, player));
        }
    }

    private IEnumerator ChangeSceneAfterDelay(float sec, CharacterController character)
    {
        character.FadeIn();
        yield return new WaitForSeconds(sec);
        PauseManager.Instance.Resume();
        yield return null;
        PlayerPrefs.SetInt(levelName, 1);
        SceneManager.LoadScene(levelName);
    }
}