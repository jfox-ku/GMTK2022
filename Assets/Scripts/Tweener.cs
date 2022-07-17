using System;
using DefaultNamespace;
using Sirenix.OdinInspector;
using UnityEngine;

public class Tweener : MonoBehaviour
{
	public Action TargetReached;
    [SerializeField] private Transform target;
	
    [SerializeField,TitleGroup("Speed")] private float
		speed = 10,
		rotationSpeed = 10,
		scaleSpeed = 10;
	
	[SerializeField,TitleGroup("Tween")]
	public bool
		position = true,
		rotation = true,
		scaling = true;
	
	
	[SerializeField,TitleGroup("Settings"),MinValue(0.001f)] private float ReachedDistance = 0.05f;
	[SerializeField,TitleGroup("Settings")] private bool ContinueChasing = false , FixedDistance = false;

	[SerializeField, TitleGroup("Settings")]
	public bool FaceTarget;
	private bool _reachedTarget = false;
		
	public enum UpdateMode{ Default, Physics, UI }
	[SerializeField] UpdateMode updateMode;
	
	private void OnUpdate(){
		if(!target) return;
		var dir = Vector3.zero;
        if(position) {
	        var pos = Vector3.Lerp(transform.position, target.position, Time.deltaTime * speed);
	        dir = (transform.position - pos).normalized;
	        transform.position = pos;
	        
	        if (FaceTarget)
	        {
		        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(dir), 0.2f);
	        }
	        
        }
		
        if(rotation) transform.rotation = Quaternion.Lerp(
			transform.rotation,
			target.rotation,
			Time.deltaTime * rotationSpeed
		);
		
        if(scaling) transform.localScale = Vector3.Lerp(
			transform.localScale,
			target.localScale,
			Time.deltaTime * scaleSpeed
		);

        

        if (transform.GetDistance(target.position) <= ReachedDistance)
        {
	        if (!_reachedTarget)
	        {
		        _reachedTarget = true;
		        TargetReached?.Invoke();
		        if (!ContinueChasing) target = null;
	        }
        }
        else
        {
	        _reachedTarget = false;
        }
        
	}
	
    private void Update(){
        if(updateMode == UpdateMode.Default) OnUpdate();
    }
	
	private void FixedUpdate(){
        if(updateMode == UpdateMode.Physics) OnUpdate();
    }
	
	private void LateUpdate(){
        if(updateMode == UpdateMode.UI) OnUpdate();
    }

    public void UpdateTarget(Transform newTarget){ target = newTarget; }
    public void SetSpeed(float newSpeed){ speed = newSpeed; }
    public void SetScaleSpeed(float newSpeed){ scaleSpeed = newSpeed; }
}