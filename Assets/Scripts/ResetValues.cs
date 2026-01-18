using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetValues : MonoBehaviour
{
    public void Reset()
    {
        GameManager.Instance.ResetAllValues();
    }

    public void ReturnToStart()
    {
        SceneManager.LoadScene(0);
    }
}
