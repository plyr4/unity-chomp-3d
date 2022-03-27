using UnityEngine;
using System.Collections.Generic;

public class Food : MonoBehaviour
{
    [SerializeField]
    private List<string> crushedByTags;

    [SerializeField]
    private ParticleSystem crumbs;

    // creates crumb particles and destroys the gameObject
    public void Consume()
    {
        if (crumbs == null) return;
        ParticleSystem c = Instantiate(crumbs);
        c.transform.position = transform.position;
        Destroy(gameObject);
    }

    // the only "trigger" should be the "Crush" trigger for detecting rigidbody "smashing"
    void OnTriggerEnter(Collider other)
    {
        foreach (string tag in crushedByTags)
        {
            if (other.gameObject.tag == tag)
            {
                // TODO: handle chomp crush safely
                if (gameObject != null)
                {
                    ParticleSystem c = Instantiate(crumbs);
                    c.transform.position = gameObject.transform.position;
                    Destroy(gameObject);
                }
                return;
            }
        }
    }
}
