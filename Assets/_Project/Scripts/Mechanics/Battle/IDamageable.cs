using System;

public interface IDamageable
{
    public int MaxHealth { get; }
    public int CurrentHealth { get; }

    public void ReceiveDamage(int value);
}
