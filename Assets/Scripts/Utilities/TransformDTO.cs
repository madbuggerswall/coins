using UnityEngine;

class TransformDTO {
	public Vector3 localPosition;
	public Vector3 localEulerAngles;
	public Vector3 localScale;
	public Quaternion localRotation;

	public TransformDTO() { }
	public TransformDTO(Transform transform) {
		localPosition = transform.localPosition;
		localEulerAngles = transform.localEulerAngles;
		localScale = transform.localScale;
		localRotation = transform.localRotation;
	}

	public void applyValuesTo(Transform transform) {
		transform.localPosition = localPosition;
		transform.localEulerAngles = localEulerAngles;
		transform.localScale = localScale;
		transform.localRotation = localRotation;
	}

	public static TransformDTO Slerp(TransformDTO first, TransformDTO second, float interpolant) {
		TransformDTO result = new TransformDTO();
		result.localPosition = Vector3.Slerp(first.localPosition, second.localPosition, interpolant);
		result.localRotation = Quaternion.Slerp(first.localRotation, second.localRotation, interpolant);
		return result;
	}
}