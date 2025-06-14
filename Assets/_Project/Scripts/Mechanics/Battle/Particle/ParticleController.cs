using UnityEngine;

public class ParticleController
{
    private ParticleCreator _bloodParticleCreator;
    private ParticleCreator _shootParticleCreator;

    public ParticleController(ParticleCreator bloodParticleCreator, ParticleCreator shootParticleCreator)
    {
        _bloodParticleCreator = bloodParticleCreator;
        _shootParticleCreator = shootParticleCreator;
    }

    public void CreateBloodParticle(Vector3 position)
    {
        _bloodParticleCreator.Create(position);
    }

    public void CreateShootParticle(Vector3 position)
    {
        _shootParticleCreator.Create(position);
    }
}
