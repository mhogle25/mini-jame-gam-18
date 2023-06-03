using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraFollow : MonoBehaviour
{
    private void Update()
    {
        if (!GameManager.Instance.Player)
            return;

        Transform player = GameManager.Instance.Player.transform;
        this.transform.position = new(player.position.x, player.position.y, this.transform.position.z);
    }
}
