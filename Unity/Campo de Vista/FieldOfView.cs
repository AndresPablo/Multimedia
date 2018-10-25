// Script original por Sebastian Lague
// https://www.youtube.com/watch?v=rQG9aUWarwE

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FieldOfView : MonoBehaviour {

    public float viewRadius;
    [Range(0, 360)]
    public float viewAngle;

    public LayerMask targetMask, obstacleMask; // Capas que se revelan u obstruyen el Campo de Vista (FOV)
    public List<Transform> visibleTargets = new List<Transform>();
    public float delayBeforeFindingTargets;


    private void Start()
    {
        StartCoroutine("FindTargetsWithDelay", delayBeforeFindingTargets);
    }

    IEnumerator FindTargetsWithDelay(float delay)
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            FindVisibleTargets();
			foreach(Transform t in visibleTargets)
			{
				t.GetComponent<Stealth>().Reveal();
			}
        }
    }

	void FixedUpdate(){
		
		foreach(Transform t in visibleTargets)
			{
				t.GetComponent<Stealth>().Reveal();
			}
	}


    void FindVisibleTargets()
    {
        if(visibleTargets.Count >0)
        {
            foreach(Transform t in visibleTargets)
                t.GetComponent<Stealth>().Hide();
        }
		
       visibleTargets.Clear();

        Collider2D[] targetsInViewRadius = Physics2D.OverlapCircleAll(transform.position, viewRadius, targetMask);
        
		for (int i = 0; i < targetsInViewRadius.Length; i++)
        {
            Transform target = targetsInViewRadius[i].transform;
            Vector2 dirToTarget = 
            new Vector2(target.position.x - transform.position.x, target.position.y- transform.position.y);
            if (Vector2.Angle(dirToTarget, transform.right) < viewAngle / 2)
            {
                float distToTarget = Vector2.Distance(transform.position, target.position);
                if (!Physics2D.Raycast(transform.position, dirToTarget, distToTarget, obstacleMask))
                {
                    visibleTargets.Add(target);
                }
            }
        }
    }

    public Vector2 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
    {
        if (!angleIsGlobal)
        {
            angleInDegrees += transform.eulerAngles.z;
        }
        return new Vector2(Mathf.Cos(angleInDegrees * Mathf.Deg2Rad), Mathf.Sin(angleInDegrees * Mathf.Deg2Rad));
    }
}
