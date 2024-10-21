using UnityEngine;
using UnityEngine.SceneManagement;

namespace KartGame.UI
{
    public class Player : MonoBehaviour
    {
        
        public void SetPlayerName(string text)
        {
            PlayerPrefs.SetString("PlayerName", text);
        }

    }
}
