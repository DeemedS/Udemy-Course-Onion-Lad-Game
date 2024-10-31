using UnityEngine;
using UnityEngine.Assertions.Must;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Sprite[] targerSprite;

    [SerializeField] private BoxCollider2D cd;
    [SerializeField] private GameObject targerPrefab;
    [SerializeField] private float cooldown;
    private float timer;

    private int sushiCreated;
    private int sushiMilestone = 10;

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;

        if(timer < 0)
        {
            timer = cooldown;
            sushiCreated++;

            if(sushiCreated > sushiMilestone && cooldown > 0.5f)
            {
                sushiMilestone += 10;
                cooldown -= 0.3f;
            }

            GameObject newTarget = Instantiate(targerPrefab);

            float randomX = Random.Range(cd.bounds.min.x, cd.bounds.max.x);
            newTarget.transform.position = new Vector2(randomX, transform.position.y);
            newTarget.GetComponent<SpriteRenderer>().sprite = targerSprite[Random.Range(0, targerSprite.Length)];
            
        }
    }
}
