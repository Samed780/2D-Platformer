using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject damageTextPrefab;
    [SerializeField] GameObject healthTextPrefab;

    [SerializeField] Canvas gameCanvas;


    private void Awake()
    {
        gameCanvas = FindObjectOfType<Canvas>();
    }

    private void OnEnable()
    {
        CharacterEvent.characterDamaged += CharacterDamage;
        CharacterEvent.characterHealed += CharacterHeal;
    }

    private void OnDisable()
    {
        CharacterEvent.characterDamaged -= CharacterDamage;
        CharacterEvent.characterHealed -= CharacterHeal;
    }

    public void CharacterDamage(GameObject character, float damage)
    {
        Vector3 spawnPos = Camera.main.WorldToScreenPoint(character.transform.position);
        TMP_Text tm_Text = Instantiate(damageTextPrefab, spawnPos, Quaternion.identity, gameCanvas.transform).GetComponent<TMP_Text>();
        tm_Text.text = damage.ToString();
    } 

    public void CharacterHeal(GameObject character, float heal)
    {
        Vector3 spawnPos = Camera.main.WorldToScreenPoint(character.transform.position);
        TMP_Text tm_Text = Instantiate(healthTextPrefab, spawnPos, Quaternion.identity, gameCanvas.transform).GetComponent<TMP_Text>();
        tm_Text.text = heal.ToString();
    }

    public void OnExitGame(InputAction.CallbackContext context)
    {
        if (context.started)
        {
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
