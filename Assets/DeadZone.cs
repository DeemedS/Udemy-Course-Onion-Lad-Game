using UnityEngine;

public class DeadZone : MonoBehaviour
{
    
    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Target")
        {
            Time.timeScale = 0;
            Debug.Log("Game Lost");
        }
    }
}
