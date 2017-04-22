using UnityEngine;
using System.Collections;

public class TransitionTransform : MonoBehaviour {
	public enum Component {
		POSITION,
		ROTATION,
		SCALE
	}

	private TransitionFloat positionX;
	private TransitionFloat positionY;
	private TransitionFloat positionZ;

	private TransitionFloat rotationX;
	private TransitionFloat rotationY;
	private TransitionFloat rotationZ;

	private TransitionFloat scaleX;
	private TransitionFloat scaleY;
	private TransitionFloat scaleZ;

	public Vector3 position {
		get {
			return new Vector3(positionX.DestinationValue, positionY.DestinationValue, positionZ.DestinationValue);
		}
		set {
			positionX.GoTo(value.x);
			positionY.GoTo(value.y);
			positionZ.GoTo(value.z);
		}
	}
	public TransitionFloat.TransitionConfiguration positionConfiguration {
		get {
			return positionX.Configuration;
		}
	}
	public Vector3 rotation {
		get {
			return new Vector3(rotationX.DestinationValue, rotationY.DestinationValue, rotationZ.DestinationValue);
		}
		set {
			rotationX.GoTo(value.x);
			rotationY.GoTo(value.y);
			rotationZ.GoTo(value.z);
		}
	}
	public TransitionFloat.TransitionConfiguration rotationConfiguration {
		get {
			return rotationX.Configuration;
		}
	}
	public Vector3 scale {
		get {
			return new Vector3(scaleX.DestinationValue, scaleY.DestinationValue, scaleZ.DestinationValue);
		}
		set {
			scaleX.GoTo(value.x);
			scaleY.GoTo(value.y);
			scaleZ.GoTo(value.z);
		}
	}
	public TransitionFloat.TransitionConfiguration scaleConfiguration {
		get {
			return scaleX.Configuration;
		}
	}

	// Use this for initialization
	void Awake( ) {
		positionX = new TransitionFloat(transform.localPosition.x);
		positionY = new TransitionFloat(transform.localPosition.y);
		positionZ = new TransitionFloat(transform.localPosition.z);

		rotationX = new TransitionFloat(transform.eulerAngles.x);
		rotationY = new TransitionFloat(transform.eulerAngles.y);
		rotationZ = new TransitionFloat(transform.eulerAngles.z);

		scaleX = new TransitionFloat(transform.localScale.x);
		scaleY = new TransitionFloat(transform.localScale.y);
		scaleZ = new TransitionFloat(transform.localScale.z);

		ConfigPosition(0.5f, TransitionFloat.LINEAR, 1.0f);
		ConfigRotation(0.5f, TransitionFloat.LINEAR, 1.0f);
		ConfigScale(0.5f, TransitionFloat.LINEAR, 1.0f);
	}

	// Update is called once per frame
	void Update( ) {
		positionX.Update();
		positionY.Update();
		positionZ.Update();

		rotationX.Update();
		rotationY.Update();
		rotationZ.Update();

		scaleX.Update();
		scaleY.Update();
		scaleZ.Update();

		transform.localPosition = new Vector3(positionX.CurrentValue, positionY.CurrentValue, positionZ.CurrentValue);
		transform.localRotation = Quaternion.Euler(rotationX.CurrentValue, rotationY.CurrentValue, rotationZ.CurrentValue);
		transform.localScale = new Vector3(scaleX.CurrentValue, scaleY.CurrentValue, scaleZ.CurrentValue);
	}

	public void Set(Component component, Vector3 value) {
		switch (component) {
			case Component.POSITION:
				this.position = value;
				break;
			case Component.ROTATION:
				this.rotation = value;
				break;
			case Component.SCALE:
				this.scale = value;
				break;
		}
	}
	public Vector3 Get(Component component) {
		switch (component) {
			case Component.POSITION:
				return this.position;
			case Component.ROTATION:
				return this.rotation;
			case Component.SCALE:
				return this.scale;
			default:
				return this.position;
		}
	}

	public TransitionFloat.TransitionConfiguration GetConfig(Component component) {
		switch (component) {
			case Component.POSITION:
				return this.positionX.Configuration;
			case Component.ROTATION:
				return this.rotationX.Configuration;
			case Component.SCALE:
				return this.scaleX.Configuration;
			default:
				return this.positionX.Configuration;
		}
	}

	public void Config(Component transform, TransitionFloat.TransitionConfiguration config) {
		switch (transform) {
			case Component.POSITION:
				ConfigPosition(config);
				break;
			case Component.ROTATION:
				ConfigRotation(config);
				break;
			case Component.SCALE:
				ConfigScale(config);
				break;
		}
	}
	public void Config(Component transform, float duration = 0.0f, Vector2[] bezier = null, float delay = 0.0f) {
		switch (transform) {
			case Component.POSITION:
				ConfigPosition(duration, bezier, delay);
				break;
			case Component.ROTATION:
				ConfigRotation(duration, bezier, delay);
				break;
			case Component.SCALE:
				ConfigScale(duration, bezier, delay);
				break;
		}
	}
	public void ConfigPosition(TransitionFloat.TransitionConfiguration config) {
		positionX.Configuration = config;
		positionY.Configuration = config;
		positionZ.Configuration = config;
	}
	public void ConfigRotation(TransitionFloat.TransitionConfiguration config) {
		rotationX.Configuration = config;
		rotationY.Configuration = config;
		rotationZ.Configuration = config;
	}
	public void ConfigScale(TransitionFloat.TransitionConfiguration config) {
		scaleX.Configuration = config;
		scaleY.Configuration = config;
		scaleZ.Configuration = config;
	}
	public void ConfigPosition(float durationP = 0.0f, Vector2[] bezierP = null, float delayP = 0.0f) {
		positionX.Configuration = new TransitionFloat.TransitionConfiguration() { duration = durationP, bezier = bezierP, delay = delayP };
		positionY.Configuration = new TransitionFloat.TransitionConfiguration() { duration = durationP, bezier = bezierP, delay = delayP };
		positionZ.Configuration = new TransitionFloat.TransitionConfiguration() { duration = durationP, bezier = bezierP, delay = delayP };
	}
	public void ConfigRotation(float durationP = 0.0f, Vector2[] bezierP = null, float delayP = 0.0f) {
		rotationX.Configuration = new TransitionFloat.TransitionConfiguration() { duration = durationP, bezier = bezierP, delay = delayP };
		rotationY.Configuration = new TransitionFloat.TransitionConfiguration() { duration = durationP, bezier = bezierP, delay = delayP };
		rotationZ.Configuration = new TransitionFloat.TransitionConfiguration() { duration = durationP, bezier = bezierP, delay = delayP };
	}
	public void ConfigScale(float durationP = 0.0f, Vector2[] bezierP = null, float delayP = 0.0f) {
		scaleX.Configuration = new TransitionFloat.TransitionConfiguration() { duration = durationP, bezier = bezierP, delay = delayP };
		scaleY.Configuration = new TransitionFloat.TransitionConfiguration() { duration = durationP, bezier = bezierP, delay = delayP };
		scaleZ.Configuration = new TransitionFloat.TransitionConfiguration() { duration = durationP, bezier = bezierP, delay = delayP };
	}
}
