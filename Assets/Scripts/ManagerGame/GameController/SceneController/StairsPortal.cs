using UnityEngine;
using UnityEngine.SceneManagement;

public class StairsPortal : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D Stair01)
    {
        if(Stair01.gameObject.tag == "Stairs")
        {
            SceneManager.LoadScene("Room01");
        }
    }
}