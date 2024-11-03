using System;

public interface IDamageable
{
    public float MaxHealth { get; }
    public float CurrentHealth { get; }

    public void ReceiveDamage(float value);
}
