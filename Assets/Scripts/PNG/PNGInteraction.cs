using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;


public class PNGInteraction : MonoBehaviour {

    float interactionDistance = 2f;

    void Update () {
        if (CrossPlatformInputManager.GetButtonDown("Interaction"))
        {
            Transform hitTransform;
            Vector3 hitPoint;
            Ray ray = new Ray(transform.position, transform.forward);

            hitTransform = FindClosestHitObject(ray, out hitPoint);
            if (hitTransform != null && Vector3.Distance(hitTransform.transform.position, gameObject.transform.position) < interactionDistance)
            {
                PNGInteraction_Identification id = hitTransform.GetComponent<PNGInteraction_Identification>();
                if (id != null)
                    id.identification();
            }
        }
	}

    Transform FindClosestHitObject(Ray ray, out Vector3 hitPoint)
    {
        RaycastHit[] hits = Physics.RaycastAll(ray);

        Transform closestHit = null;
        float distance = 1;
        hitPoint = Vector3.zero;

        foreach (RaycastHit hit in hits)
        {
            if (hit.transform != transform && (closestHit == null || hit.distance < distance) && (hit.transform.tag == "NPC" || hit.transform.tag == "Radio"))
            {
                closestHit = hit.transform;
                distance = hit.distance;
                hitPoint = hit.point;
            }
        }
        return closestHit;
    }
}
