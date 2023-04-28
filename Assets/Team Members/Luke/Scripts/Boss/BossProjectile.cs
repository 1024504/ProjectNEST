using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossProjectile : MonoBehaviour
{
	public float turnStrength;
	private Transform _target;
	
	public float velocity = 25f;
	public float lifetimePostCollision = 3f;
	public float damage = 10f;
	private bool _isSpent;
	
	private Transform _transform;
	private Rigidbody2D _rb;
	private PolygonCollider2D _collider;
	private Transform _collisionHitTransform;
	private Vector3 _collisionPositionOffset;
	private Quaternion _rotationWhenHit;
	private Quaternion _collisionRotationWhenHit;

	public GameObject projectileImpactSfx;

	private void OnEnable()
	{
		_transform = transform;
		_rb = GetComponent<Rigidbody2D>();
		_collider = GetComponent<PolygonCollider2D>();
		_rb.velocity = transform.right * velocity;
	}

	private void FixedUpdate()
	{
		if (_collisionHitTransform == null)
		{
			_rb.AddTorque(TurnToPlayer() * turnStrength);
			_rb.velocity = transform.right * velocity;
			return;
		}
		Quaternion rotation = _collisionHitTransform.rotation * Quaternion.Inverse(_collisionRotationWhenHit);
		_transform.position = _collisionHitTransform.position + rotation*_collisionPositionOffset;
		_transform.rotation = rotation * _rotationWhenHit;
	}

	private void OnTriggerEnter2D(Collider2D col)
	{
		BossTerrainIgnore terrainIgnore = col.GetComponent<BossTerrainIgnore>();
		if (terrainIgnore != null) return;
		
		if (_isSpent) return;
		_isSpent = true;
		_rb.velocity = Vector2.zero;
		_collider.enabled = false;
		_collisionHitTransform = col.transform;
		_collisionPositionOffset = _transform.position - _collisionHitTransform.position;
		_rotationWhenHit = _transform.rotation;
		_collisionRotationWhenHit = _collisionHitTransform.rotation;
		
		projectileImpactSfx.GetComponent<FMODUnity.StudioEventEmitter>().Play();

		Destroy(gameObject, lifetimePostCollision);
		ParticleSystem ps = GetComponentInChildren<ParticleSystem>();
		if (ps != null) ps.Stop();

		HealthBase health = col.GetComponentInParent<HealthBase>();
		if (health != null) health.GetComponent<HealthBase>().HealthLevel -= damage;
	}

	private float TurnToPlayer() => Mathf.Sign(Vector3.SignedAngle(_transform.right, _target.position - _transform.position, Vector3.forward));

	public void SetTarget(Transform target) => _target = target;
}
