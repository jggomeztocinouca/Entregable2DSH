using System.Collections;
using UnityEngine;
using UnityEngine.UI; // Usado para el componente Image
using UnityEngine.SceneManagement;


public class Countdown : MonoBehaviour
{
    public Sprite[] countdownSprites;
    public Image countdownImage; // Modificado para usar Image en lugar de GameObject

    void Start()
    {
        countdownImage.gameObject.SetActive(false);
    }

    public void StartCountdown()
    {
        countdownImage.gameObject.SetActive(true);
        StartCoroutine(CountdownCoroutine());
    }

    private IEnumerator CountdownCoroutine()
    {
        // Asegúrate de que el componente Image esté asignado
        if (countdownImage != null)
        {
            // Cambia los sprites en el componente Image
            for (int i = countdownSprites.Length - 1; i >= 0; i--)
            {
                countdownImage.sprite = countdownSprites[i];
                yield return new WaitForSeconds(1);
            }
        }

        // Cambia a la próxima escena
        SceneManager.LoadScene("Level1");
    }
}
