using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmRoot : MonoBehaviour
{
    public float RigidbodyMass = 1f;
    public float ColliderRadius = 0.1f;
    public float JointSpring = 0.1f;
    public float JointDamper = 5f;
    public Vector3 RotationOffset;
    public Vector3 PositionOffset;

    protected List<Transform> CopySource;
    protected List<Transform> CopyDestination;
    protected static GameObject RigidBodyContainer;

    private Transform child;
    private GameObject representative;
    private Rigidbody childRigidBody;
    private SphereCollider sphereCollider;
    private DistanceJoint3D joint;

    void Awake()
    {
        if (RigidBodyContainer == null)
            RigidBodyContainer = new GameObject("RopeRigidbodyContainer");

        CopySource = new List<Transform>();
        CopyDestination = new List<Transform>();
        
        AddChildren(transform);
    }

    private void AddChildren(Transform parent)
    {
        for (int i = 0; i < parent.childCount; i++)
        {
            child = parent.GetChild(i);
            representative = new GameObject(child.gameObject.name);

            representative.transform.parent = RigidBodyContainer.transform;

            //rigidbody
            childRigidBody = representative.gameObject.AddComponent<Rigidbody>();
            childRigidBody.useGravity = true;
            childRigidBody.isKinematic = false;
            childRigidBody.freezeRotation = true;
            childRigidBody.mass = RigidbodyMass;

            //collider
            sphereCollider = representative.gameObject.AddComponent<SphereCollider>();
            sphereCollider.center = Vector3.zero;
            sphereCollider.radius = ColliderRadius;

            //DistanceJoint
            joint = representative.gameObject.AddComponent<DistanceJoint3D>();
            joint.ConnectedRigidbody = parent;
            joint.DetermineDistanceOnStart = true;
            joint.Spring = JointSpring;
            joint.Damper = JointDamper;
            joint.DetermineDistanceOnStart = false;
            joint.Distance = Vector3.Distance(parent.position, child.position);

            //add copy source
            CopySource.Add(representative.transform);
            CopyDestination.Add(child);

            AddChildren(child);
        }
    }

    public void Update()
    {
        for (int i = 0; i < CopySource.Count; i++)
        {
            CopyDestination[i].position = CopySource[i].position + PositionOffset;
            CopyDestination[i].rotation = CopySource[i].rotation * Quaternion.Euler(RotationOffset);
        }
    }
}