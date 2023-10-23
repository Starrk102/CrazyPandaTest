using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeDilation : MonoBehaviour
{
    [SerializeField] private float timeDilation;
    private const float ExitTime = 1.0f;
    
    
    Dictionary<Rigidbody2D, Body> bodys = new Dictionary<Rigidbody2D, Body>();

    private void FixedUpdate()
    {
        foreach (var (rb, body) in bodys)
        {
            if (body.PrevVelocity != null)
            {
                var acceleration = rb.velocity - body.PrevVelocity.Value;
                var accelerationAngular = rb.angularVelocity - body.PrevAngularVelocity.Value;
                
                body.PrevVelocity = rb.velocity = body.UnscaledVelocity * timeDilation;
                body.PrevAngularVelocity = rb.angularVelocity = body.UnscaledAngularVelocity * timeDilation;
                
                body.UnscaledVelocity += acceleration;
                body.UnscaledAngularVelocity += accelerationAngular;
            }
            else
            {
                body.PrevVelocity = rb.velocity = body.UnscaledVelocity * timeDilation;
                body.PrevAngularVelocity = rb.angularVelocity = body.UnscaledAngularVelocity * timeDilation;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var newBody = new Body();
        newBody.PrevVelocity = null;
        var attachedRigidbody = other.attachedRigidbody;
        newBody.UnscaledVelocity = attachedRigidbody.velocity;
        newBody.UnscaledAngularVelocity = attachedRigidbody.angularVelocity;
        bodys.Add(attachedRigidbody, newBody);
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (bodys.TryGetValue(other.attachedRigidbody, out var body))
        {
            var attachedRigidbody = other.attachedRigidbody;
            attachedRigidbody.angularVelocity = body.UnscaledAngularVelocity * ExitTime;
            attachedRigidbody.velocity = body.UnscaledVelocity * ExitTime;
            bodys.Remove(attachedRigidbody);
        }
    }
}

class Body
{
    public Vector2 UnscaledVelocity;
    public float UnscaledAngularVelocity;
    public Vector2? PrevVelocity;
    public float? PrevAngularVelocity;
}
