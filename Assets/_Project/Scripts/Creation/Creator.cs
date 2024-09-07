using UnityEngine;

public abstract class Creator<T> where T : IProduct
{
    public abstract T Create(Vector3 position, Transform parent = null);
}