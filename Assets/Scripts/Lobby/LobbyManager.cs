using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LobbyManager : Singleton<LobbyManager>
{
    [SerializeField] private GameObject loadingCanvas;
    [SerializeField] private Image progressBar;
    private float progressFill = 0.0f;

    protected override void Awake()
    {
        base.Awake();
        loadingCanvas.SetActive(false);
        DontDestroyOnLoad(this);
    }

    private void Update()
    {
        progressBar.fillAmount = Mathf.MoveTowards(progressBar.fillAmount, progressFill, 3 * Time.deltaTime);
    }
    public void ChangeScene(int sceneNumber)
    {
        LoadScene(sceneNumber);
    }
    private async void LoadScene(int sceneNum)
    {
        progressFill = 0.0f;
        progressBar.fillAmount = 0.0f;
        
        var scene = SceneManager.LoadSceneAsync(sceneNum);
        scene.allowSceneActivation = false;
        loadingCanvas.SetActive(true);
        await LoadingScreenTransition(0, 1);

        do
        {
            progressFill = scene.progress;

        } while (scene.progress < 0.9f);

        scene.allowSceneActivation = true;
        await LoadingScreenTransition(1, 0);
        loadingCanvas.SetActive(false);
    }
    [SerializeField] private Image transitionPanel;

    public async Task LoadingScreenTransition(float from = 0, float to = 1)
    {
        transitionPanel.fillAmount = from;
        transitionPanel.fillMethod = Image.FillMethod.Horizontal;
        transitionPanel.fillOrigin = 1;

        while (transitionPanel.fillAmount != to)
        {
            transitionPanel.fillAmount = Mathf.MoveTowards(transitionPanel.fillAmount, to, 3 * Time.deltaTime);
            await Task.Yield();
        }
    }
}
