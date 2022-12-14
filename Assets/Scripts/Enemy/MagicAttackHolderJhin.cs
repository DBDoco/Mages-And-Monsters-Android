using UnityEngine;

public class MagicAttackHolderJhin : MonoBehaviour
{
    [SerializeField] private Transform enemy;

    private void Update()
    {
        transform.localScale = -enemy.localScale;
    }
}