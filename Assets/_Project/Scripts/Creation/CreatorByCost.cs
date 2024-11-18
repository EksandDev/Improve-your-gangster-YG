using UnityEngine;

public abstract class CreatorByCost<T> where T : IProduct
{
    public abstract T Create(Vector3 position, int cost);
}