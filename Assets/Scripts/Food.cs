using UnityEngine;
using System.Collections.Generic;

public class Food : MonoBehaviour
{
    [SerializeField]
    public List<string> crushedByTags;
    public ParticleSystem crumbs;

    // Consume: creates crumb particles and destroys the gameObject
    public void Consume() {
        ParticleSystem c = Instantiate(crumbs);
        c.transform.position = transform.position;
        Destroy(gameObject);
    }

    // OnTriggerEnter: 
    //   the only "trigger" should be the "Crush" trigger for detecting rigidbody "smashing"
    void OnTriggerEnter (Collider other)
    {
        foreach (string tag in crushedByTags) {
            if (other.gameObject.tag == tag) {
                // TODO: handle chomp crush safely
                if (gameObject != null) {
                    ParticleSystem c = Instantiate(crumbs);
                    c.transform.position = gameObject.transform.position;
                    Destroy(gameObject);
                }
                return;
            }
        }
    }
}
