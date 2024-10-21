using UnityEngine;
using UnityEngine.SceneManagement;

namespace KartGame.UI
{    
    public class LoadSceneButton : MonoBehaviour
    {
        [Tooltip("What is the name of the scene we want to load when clicking the button?")]
        public static string SceneName;

        public void LoadTargetScene() 
        {
            PlayerPrefs.SetString("CurrentSceneName", SceneName);
            SceneManager.LoadSceneAsync(SceneName);
        }

        public void LoadPreviousScene()
        {
            SceneManager.LoadSceneAsync(PlayerPrefs.GetString("CurrentSceneName"));
        }

        public void SetSceneName(string name)
        {
            SceneName = name;
        }

    }
}
