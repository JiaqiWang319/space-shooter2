using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByContactLevels : MonoBehaviour
{

    public GameObject explosion;
    public GameObject playerExplosion;
    public int scoreValue;
    private LevelOneController levelOneController;

    void Start()
    {
        GameObject levelControllerObject = GameObject.FindWithTag("LevelController");
        if (levelControllerObject != null)
        {
            levelOneController = levelControllerObject.GetComponent<LevelOneController>();
        }
        if (levelOneController == null)
        {
            Debug.Log("Cannot find 'GameController' script");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Boundary") || other.CompareTag("Enemy"))
        {
            return;
        }

        if (explosion != null)
        {
            Instantiate(explosion, transform.position, transform.rotation);
        }

        if (other.CompareTag("Player"))
        {
            Instantiate(playerExplosion, other.transform.position, other.transform.rotation);
        }

        Destroy(other.gameObject);
        Destroy(gameObject);
    }
}
