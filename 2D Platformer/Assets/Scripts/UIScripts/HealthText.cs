using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HealthText : MonoBehaviour
{
    [SerializeField] Vector3 moveSpeed = new Vector3(0, 75, 0);
    [SerializeField] float timeToFade = 1.5f;

    RectTransform textTransform;
    TextMeshProUGUI textMeshProUGUI;

    float timeElapsed = 0;
    Color startColor;


    private void Awake()
    {
        textTransform = GetComponent<RectTransform>();
        textMeshProUGUI = GetComponent<TextMeshProUGUI>();
    }

    // Start is called before the first frame update
    void Start()
    {
        startColor = textMeshProUGUI.color;
    }

    // Update is called once per frame
    void Update()
    {
        textTransform.position += moveSpeed * Time.deltaTime;

        timeElapsed += Time.deltaTime;

        if (timeElapsed < timeToFade)
        {
            float fadeAlpha = startColor.a * 1 - (timeElapsed / timeToFade);
            textMeshProUGUI.color = new Color(startColor.r, startColor.g, startColor.b, fadeAlpha);
        }
        else
            Destroy(gameObject);
    }
}
