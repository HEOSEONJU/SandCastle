using Skill;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopMove : SkillMove
{

    Collider2D _collider;
    public override void ObjectMove(float duration, float speed, Vector3 direction , bool fix = false)
    {
        transform.GetComponent<Animator>().SetTrigger("Skill");
        TryGetComponent<Collider2D>(out _collider);
        DIsableCollider();
        
    }

    private void OnEnable()
    {
        DIsableCollider();
    }

    public void EnableCollider()
    {
        if(_collider !=null)
        _collider.enabled = true;
    }
    public void DIsableCollider()
    {
        if (_collider != null)
            _collider.enabled = false;
    }

    public void setoff()
    {
        gameObject.SetActive(false);
    }
}
