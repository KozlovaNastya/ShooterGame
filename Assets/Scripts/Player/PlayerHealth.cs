using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int health = 3;
    public GameObject[] hearts;

    public void TakeDamage()
    {
        if (health <= 0) return;

        health--;
        if (health < hearts.Length)
        {
            hearts[health].SetActive(false);
        }

        Debug.Log("Жизней осталось: " + health);

        if (health <= 0)
        {
            Debug.Log("Игрок погиб!");
        }
    }
}
