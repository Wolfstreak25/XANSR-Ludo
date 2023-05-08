using UnityEngine;
using UnityEngine.SceneManagement;
public class ExitGame : MonoBehaviour
{
    public void Quit() 
    {
        #if (UNITY_EDITOR || DEVELOPMENT_BUILD)
            Debug.Log(this.name+" : "+this.GetType()+" : "+System.Reflection.MethodBase.GetCurrentMethod().Name); 
        #endif
        #if (UNITY_EDITOR)
            UnityEditor.EditorApplication.isPlaying = false;
        #elif (UNITY_STANDALONE) 
            Application.Quit();
        #elif (UNITY_WEBGL)
            SceneManager.LoadScene(0);
        #endif
    }
}
