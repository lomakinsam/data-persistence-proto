using UnityEngine;

public class DeathZone : MonoBehaviour
{
    public GameManager GameManager;

    private void OnCollisionEnter(Collision other)
    {
        Destroy(other.gameObject);
        GameManager.GameOver();
    }
}
