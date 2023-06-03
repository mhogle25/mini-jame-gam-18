using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance => GameManager.instance;
    private static GameManager instance = null;

    private void Awake()
    {
        if (GameManager.Instance != null)
        {
            Destroy(this.gameObject);
            return;
        }

        GameManager.instance = this;
    }
}
