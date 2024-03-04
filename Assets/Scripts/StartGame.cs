using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartGame : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip clip;

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StartMatch()
    {
        StartCoroutine(PlaySoundAndLoadScene("PreLevel1"));
    }

    public void BackToMenu()
    {
        StartCoroutine(PlaySoundAndLoadScene("MainMenu"));
    }

    public void GoToLevel1()
    {
        StartCoroutine(PlaySoundAndLoadScene("PreLevel1"));
    }

    public void GoToLevel2()
    {
        StartCoroutine(PlaySoundAndLoadScene("PreLevel2"));
    }

    public void GoToLevel3()
    {
        StartCoroutine(PlaySoundAndLoadScene("PreLevel3"));
    }

    IEnumerator PlaySoundAndLoadScene(string sceneName)
    {
        audioSource.PlayOneShot(clip);
        // Espera a que termine el sonido
        yield return new WaitForSeconds(clip.length);
        // Carga la escena después de que termine el sonido
        SceneManager.LoadScene(sceneName);
    }
}
