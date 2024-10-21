using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

namespace KartGame.UI
{    
    public class LeaderboardUI : MonoBehaviour
    {
        [Tooltip("The index of the leaderboard entry")]
        public int index;
        public bool isName;

        public void OnEnable()
        {
            if (isName)
                GetLeaderboardName();
            else
                GetLeaderboardScore();
        }

        public void GetLeaderboardScore()
        {
            this.GetComponent<TextMeshProUGUI>().text = PlayerPrefs.GetFloat($"{LoadSceneButton.SceneName}_{index}_score", float.PositiveInfinity).ToString("#.00");
        }

        public void GetLeaderboardName()
        {
            this.GetComponent<TextMeshProUGUI>().text = PlayerPrefs.GetString($"{LoadSceneButton.SceneName}_{index}_name", "NONE");
        }
    }
}
