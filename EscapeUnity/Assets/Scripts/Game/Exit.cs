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
            StartCoroutine(ChangeSceneAfterDelay(2f, collision.GetComponent<CharacterController>()));
        }
    }

    private IEnumerator ChangeSceneAfterDelay(float sec, CharacterController character)
    {
        character.FadeIn(sec);
        yield return new WaitForSeconds(sec);
        SceneManager.LoadScene(levelName);
    }
}