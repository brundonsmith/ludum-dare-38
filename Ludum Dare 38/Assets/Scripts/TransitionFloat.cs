using UnityEngine;
using System.Collections;

[System.Serializable]
public class TransitionFloat {
  public class TransitionConfiguration {
    public TransitionConfiguration(float duration = 0.0f, Vector2[] bezier = null, float delay = 0.0f) {
    	this.duration = duration;
    	this.bezier = bezier;
    	this.delay = delay;
    }

    public float duration;
    public Vector2[] bezier;
    public float delay;
  }

	public static Vector2[] INSTANT {
    get {
			return new Vector2[] { new Vector2(1.0f, 1.0f), new Vector2(1.0f, 1.0f) };
    }
  }
	public static Vector2[] LINEAR {
    get {
			return new Vector2[] { new Vector2(0.25f, 0.25f), new Vector2(0.75f, 0.75f) };
    }
  }
	public static Vector2[] EASE_IN {
    get {
			return new Vector2[] { new Vector2(0.95f, 0.05f), new Vector2(0.795f, 0.35f) };
    }
  }
	public static Vector2[] EASE_OUT {
    get {
			return new Vector2[] { new Vector2(0.19f, 1.0f), new Vector2(0.22f, 1.0f) };
    }
  }
	public static Vector2[] EASE_IN_OUT {
    get {
			return new Vector2[] { new Vector2(1.0f, 0.0f), new Vector2(0.0f, 1.0f) };
    }
  }

  private float value;

	private bool isTransitioning;
  private float startTime;
  private float finishTime;
  private float sourceValue;
  private float destinationValue;

  private float duration;
  private Vector2[] bezier;
  private float delay;

	public TransitionFloat(float value, TransitionConfiguration config) {
		// initial value
		this.value = value;

		// initialize state
		this.startTime = Time.time;
		this.finishTime = Time.time;
		this.sourceValue = value;
		this.destinationValue = value;

		// assign properties
		this.duration = config.duration;
		this.bezier = config.bezier;
		this.delay = config.delay;
	}
  public TransitionFloat(float value = 0.0f, float duration = 0.0f, Vector2[] bezier = null, float delay = 0.0f) {
		// initial value
    this.value = value;

		// initialize state
    this.startTime = Time.time;
    this.finishTime = Time.time;
    this.sourceValue = value;
    this.destinationValue = value;

		// assign properties
    this.duration = duration;
    if (bezier != null) {
        this.bezier = bezier;
    } else {
        this.bezier = LINEAR;
    }
    this.delay = delay;
  }

  public void Update() {
  	if (this.isTransitioning) {
  		updateValueBezier(this.bezier);
  	}
  }

  private void updateValueBezier(Vector2[] bezier) {
		Vector2 start = new Vector2(0, 0);
		Vector2 end = new Vector2(1, 1);

		float time = this.TimeValue;
        //float progress = Mathf.Pow((1 - time), 3) * (bezier[0]) + 3 * time * Mathf.Pow((1 - time), 2) * (bezier[1]) + Mathf.Pow(3 * time, 2) * (1 - time) * (bezier[2]) + Mathf.Pow(time, 3) * (bezier[3]);
        time = Mathf.Min(Mathf.Max(time, 0.0f), 1.0f); // clamp time
		float bezierVal =
			((((-start + 3 * (bezier[0] - bezier[1]) + end) * time + (3 * (start + bezier[1]) - 6 * bezier[0])) * time + 3 * (bezier[0] - start)) * time + start).y;

		this.value =
			(destinationValue - sourceValue) * bezierVal + sourceValue;

		if (Mathf.Abs(value - destinationValue) < 0.0001) {
			isTransitioning = false;
		}
		/*
        if (Time.time < finishTime) {
            float time = GetProgress();
            float progress = Mathf.Pow((1 - time), 3) * (bezier[0]) + 3 * time * Mathf.Pow((1 - time), 2) * (bezier[1]) + Mathf.Pow(3 * time, 2) * (1 - time) * (bezier[2]) + Mathf.Pow(time, 3) * (bezier[3]);
            progress = Mathf.Min(Mathf.Max(progress, 0.0f), 1.0f);

            if (sourceValue < targetValue) {
                this.value = Mathf.Min(this.sourceValue + (this.targetValue - this.sourceValue) * progress, this.targetValue);
            } else {
                this.value = Mathf.Max(this.sourceValue + (this.targetValue - this.sourceValue) * progress, this.targetValue);
            }
        }
		 * */
  }

  private void updateValueLinearTransition() {
    if(Time.time >= this.startTime) {
      if(sourceValue < destinationValue) {
        this.value = Mathf.Min(this.sourceValue + (this.destinationValue - this.sourceValue)*this.TimeValue, this.destinationValue);
      } else {
        this.value = Mathf.Max(this.sourceValue + (this.destinationValue - this.sourceValue)*this.TimeValue, this.destinationValue);
      }
    }
  }

  public void GoTo(float value) {
    this.startTime = Time.time + this.delay;
    this.finishTime = this.startTime + this.duration;
    this.sourceValue = this.value;
    this.destinationValue = value;
		this.isTransitioning = true;
  }
  public float CurrentValue {
		get {
			return this.value;
		}
  }
  public float DestinationValue {
		get {
			return this.destinationValue;
		}
		set {
			this.GoTo(value);
		}
  }
  public float TimeValue {
		get {
			if (this.isTransitioning) {
				return (Time.time - startTime) / (finishTime - startTime);
			} else {
				return 0.0f;
			}
		}
  }
  public TransitionConfiguration Configuration {
		get {
			return new TransitionConfiguration { duration = this.duration, bezier = this.bezier, delay = this.delay };
		}
		set {
			// configure
			this.duration = value.duration;
			this.bezier = value.bezier;
			this.delay = value.delay;

			// strt on new transition
			this.GoTo(this.destinationValue);
		}
  }

  public bool Started {
    get {
      return this.isTransitioning && this.startTime < Time.time;
    }
  }
  public bool Finished {
    get {
      return this.finishTime < Time.time;
    }
  }
	public bool IsTransitioning {
		get {
			return this.isTransitioning;
		}
	}
}
