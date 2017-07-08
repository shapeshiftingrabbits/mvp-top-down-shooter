﻿using UnityEngine;

public class EnemyBulletCollision : MonoBehaviour {

    public GameObject ragdollPrefab;
    private GameObject playerGameObject;
    private PlayerScript playerScript;
    private GameObject enemyRagdoll;
    private string bulletTag = "Bullet";

    void Start () {
        playerGameObject = GameObject.Find ("Player");
        playerScript = playerGameObject.GetComponent<PlayerScript> ();

        enemyRagdoll = (GameObject) Instantiate(ragdollPrefab, gameObject.transform.position, gameObject.transform.rotation);
        enemyRagdoll.SetActive(false);
    }

    void OnCollisionEnter (Collision collision) {
        if (collision.gameObject.tag == bulletTag) {
            playerScript.player.incrementEnemyKills ();

            CopyTransformHierachy(enemyRagdoll.transform, gameObject.transform);

            Destroy (gameObject);

            enemyRagdoll.SetActive(true);

            foreach (ContactPoint contactPoint in collision.contacts) {
                Collider[] ragdollColliders = enemyRagdoll.GetComponentsInChildren<Collider>();
                foreach (Collider ragdollCollider in ragdollColliders) {
                    if (ragdollCollider.bounds.Contains(contactPoint.point)) {
                        Rigidbody ragdollLimbRigidbody = ragdollCollider.gameObject.GetComponent<Rigidbody>();
                        if (ragdollLimbRigidbody != null) {
                            ragdollLimbRigidbody.AddForceAtPosition(collision.relativeVelocity, contactPoint.point, ForceMode.Impulse);
                            break;
                        }
                    }
                }
            }
        }
    }

    void CopyTransformHierachy(Transform destinationTransform, Transform referenceTransform) {
        destinationTransform.position = referenceTransform.position;
        destinationTransform.rotation = referenceTransform.rotation;
        destinationTransform.localScale = referenceTransform.localScale;

        for (int i = 0; i < referenceTransform.childCount; i++)
        {
            Transform referenceTransformChild = referenceTransform.GetChild(i);
            Transform destinationTransformChild = destinationTransform.GetChild(i);

            if(referenceTransformChild != null && destinationTransformChild != null)
                CopyTransformHierachy(destinationTransformChild, referenceTransformChild);
        }
    }
}
