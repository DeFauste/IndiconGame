using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.UI
{
    internal class GameMenuContoller : MonoBehaviour
    {
        public void RestartLevel()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            OffPauseGame();
        }

        public void OnPauseGame()
        {
            Time.timeScale = 0.0f;
        }

        public void OffPauseGame()
        {
            Time.timeScale = 1.0f;
        }
    }
}
