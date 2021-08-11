using UnityEngine;

public static class CustomExtensions
{

    public static Vector2 ClampMagnitude(this Vector2 vector, float minLength, float maxLength)
    {
        double sqrMag = vector.sqrMagnitude;
        if (sqrMag > (double)maxLength * (double)maxLength)
        {
            return vector.normalized * maxLength;
        }
        else if (sqrMag < (double)minLength * (double)minLength)
        {
            return vector.normalized * minLength;
        }
        return vector;
    }


    public static Vector2 Center(this Transform transform)
    {
        Vector2 center = transform.GetComponent<BoxCollider2D>().bounds.center;
        return center;
    }


    public static Vector2 Direction(this Vector2 vector)
    {
        if (Mathf.Abs(vector.x) > Mathf.Abs(vector.y))
        {
            vector = new Vector2(Mathf.Sign(vector.x), 0.1f);
        }
        else
        {
            vector = new Vector2(0.1f, Mathf.Sign(vector.y));
        }

        return vector;
    }


    public static bool IsPlaying(this Animator animator, int layerIndex, string stateName)
    {
        return animator.GetCurrentAnimatorStateInfo(layerIndex).IsName(stateName)
            && animator.GetCurrentAnimatorStateInfo(layerIndex).normalizedTime < 1.0f;
    }

}
