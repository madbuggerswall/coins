using UnityEngine;
public static class RectTransformExtensions {
	public static void setLeft(this RectTransform rectTransform, float left) {
		rectTransform.offsetMin = new Vector2(left, rectTransform.offsetMin.y);
	}

	public static void setRight(this RectTransform rectTransform, float right) {
		rectTransform.offsetMax = new Vector2(-right, rectTransform.offsetMax.y);
	}

	public static void setTop(this RectTransform rectTransform, float top) {
		rectTransform.offsetMax = new Vector2(rectTransform.offsetMax.x, -top);
	}

	public static void setBottom(this RectTransform rectTransform, float bottom) {
		rectTransform.offsetMin = new Vector2(rectTransform.offsetMin.x, bottom);
	}
}