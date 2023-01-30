using UnityEngine;
using UnityEngine.SceneManagement;

namespace LazyBalls.Cheats
{
    public class CheatRestartScene : MonoBehaviour
    {
        [SerializeField] private string buttonKey;

        private void Update()
        {
            if (Input.GetButtonDown(buttonKey))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }
}
