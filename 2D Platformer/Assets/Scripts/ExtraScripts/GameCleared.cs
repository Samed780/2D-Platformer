using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCleared : MonoBehaviour
{
    [SerializeField] GameObject gameClearedPanel;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            StartCoroutine(LevelCleared());
    }

    IEnumerator LevelCleared()
    {
        if (gameClearedPanel)
        {
            gameClearedPanel.SetActive(true);

            yield return new WaitForSeconds(1f);

            Time.timeScale = 0;

            #if (UNITY_EDITOR || DEVELOPMENT_BUILD)
                Debug.Log(this.name + " : " + this.GetType() + " : " + System.Reflection.MethodBase.GetCurrentMethod().Name);
            #endif
            #if (UNITY_EDITOR)
                UnityEditor.EditorApplication.isPlaying = false;
            #elif (UNITY_STANDALONE)
                Application.Quit();
            #elif (UNITY_WEBGL)
                SceneManager.LoadScene("QuitScene");
            #endif
        }
    }
}
