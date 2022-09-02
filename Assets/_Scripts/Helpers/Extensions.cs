using UnityEngine;

public static class Extensions {
    public static Color ChangeAlpha( this Color original, float alphaValue ) {
        Color copy = original;
        copy.a = alphaValue;
        return copy;
    }

    public static Vector3 Change( this Vector3 original, float? x = null, float? y = null, float? z = null ) {
        return new Vector3( x ?? original.x, y ?? original.y, z ?? original.z );
    }
}