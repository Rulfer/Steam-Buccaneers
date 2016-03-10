using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour {
	private float duration = 0.5f;
	private float speed = 1.0f;
	private float magnitude = 0.1f;

	public static bool shakeCamera = true;
	private Vector3 originalPos;

	void OnEnable()
	{
		originalPos = transform.localPosition;
	}

	public void playShake()
	{
		if(this != null)
		{
			StopCoroutine("Shake");
			StartCoroutine("Shake");
		}
	}

	// Update is called once per frame
	void Update () {
		if(shakeCamera == true)
		{
			shakeCamera = false;
			shake();
		}

	}

	IEnumerator shake()
	{
		float elapsed = 0.0f;

		float randomStart = Random.Range(-1000.0f, 1000.0f);

		while(elapsed < duration)
		{
			elapsed += Time.deltaTime;

			float percentComplete = elapsed / duration;
			float damper = 1.0f - Mathf.Clamp(2.0f * percentComplete - 1.0f, 0.0f, 1.0f);

			float alpha = randomStart + speed * percentComplete;

			float x = Random.value * 2.0f - 1.0f;
			float y = Random.value * 2.0f - 1.0f;
			float sample = Mathf.PerlinNoise(x, y);
			x *= magnitude * damper;
			y *= magnitude * damper;

			Camera.main.transform.position = new Vector3(sample, sample, sample);

			yield return null;
		}

		Camera.main.transform.position = originalPos;
	}
}
