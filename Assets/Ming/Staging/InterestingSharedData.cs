using System;
using UnityEngine;

// ex player and health UI both adds this and player updates RuntimeValue
// compared to pub/poll engine named objects? Player publishes and updates, hp bar polls value. Main diff: engine has a list
// 
[CreateAssetMenu]
public class FloatVariable : ScriptableObject, ISerializationCallbackReceiver
{
	public float InitialValue;

	[NonSerialized]
	public float RuntimeValue;

	public void OnAfterDeserialize()
	{
		RuntimeValue = InitialValue;
	}

	public void OnBeforeSerialize() { }
}
