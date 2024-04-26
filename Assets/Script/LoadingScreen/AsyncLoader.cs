using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class AsyncLoader : MonoBehaviour
{
    [Header("Menu Screens")]
    [SerializeField] private GameObject LoadingScreen;
    [SerializeField] private GameObject mainMenu;

    [Header("Slider")]
    [SerializeField] private TextMeshProUGUI Percentage;
    [SerializeField] private Slider loadingSlider;

    public void LoadLevelBtn(string levelToLoad)
    {
        mainMenu.SetActive(false);
        LoadingScreen.SetActive(true);

        StartCoroutine(LoadLevelASync(levelToLoad));
    }

    IEnumerator LoadLevelASync(string levelToLoad)
    {
        AsyncOperation loadOperation = SceneManager.LoadSceneAsync(levelToLoad);

        while (!loadOperation.isDone)
        {
            float progressValue = Mathf.Clamp01(loadOperation.progress / 0.9f);
            int intValue = (int)(progressValue * 100);
            Percentage.text = intValue.ToString() + "%";
            loadingSlider.value = progressValue;
            yield return null;
        }

        // Attendiamo 5 secondi
        yield return new WaitForSeconds(5f);

        // Ora puoi eseguire eventuali altre operazioni post-caricamento
        // ad esempio, se devi inizializzare il NavMesh, fai ciò qui
    }

}
