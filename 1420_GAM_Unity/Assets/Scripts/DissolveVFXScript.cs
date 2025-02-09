using UnityEngine;

public class DissolveVFXScript : MonoBehaviour
{
    public ParticleSystem ps;
    private MaterialPropertyBlock propertyBlock;
    private float dissolveAmt;
    private float dissolveSpeed;
    void Start()
    {
        propertyBlock = new MaterialPropertyBlock();
    }

    void Update()
    {
        ParticleSystemRenderer renderer = ps.GetComponent<ParticleSystemRenderer>();
        dissolveSpeed = ps.main.duration;
        dissolveAmt += dissolveSpeed * Time.deltaTime;
        renderer.SetPropertyBlock(propertyBlock);
        propertyBlock.SetFloat("Dissolve", dissolveAmt);
        renderer.SetPropertyBlock(propertyBlock);
    }
}
