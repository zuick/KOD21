using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

namespace Game.Common
{
    public class LoadScene : MonoBehaviour
    {
        [SerializeField] private string sceneName;
        [SerializeField] private float delay;

        private IEnumerator Start()
        {
            yield return new WaitForSeconds(delay);
            SceneManager.LoadScene(sceneName);
        }
    }
}