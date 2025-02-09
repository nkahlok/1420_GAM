using UnityEngine;

public class DissolveVFXScript : MonoBehaviour
{
    public ParticleSystem ps;
    private Material materialInstance;
    void Start()
    {
        if (ps == null) return;

        // Clone the material instance to modify it without affecting other particles
        materialInstance = ps.GetComponent<ParticleSystemRenderer>().material;
    }

    void Update()
    {
        if (materialInstance != null)
        {
            float value = Mathf.PingPong(Time.time, 1.0f); // Example dynamic change
            materialInstance.SetFloat("_MyFloat", value);  // Match the property name in Shader Graph
        }
    }
    //public ParticleSystem ps;
    //private MaterialPropertyBlock propertyBlock;
    //private float dissolveAmt;
    //private float dissolveSpeed;
    //void Start()
    //{
    //    propertyBlock = new MaterialPropertyBlock();
    //}

    //void Update()
    //{
    //    ParticleSystemRenderer renderer = ps.GetComponent<ParticleSystemRenderer>();
    //    dissolveSpeed = ps.main.duration;
    //    dissolveAmt += dissolveSpeed * Time.deltaTime;
    //    renderer.SetPropertyBlock(propertyBlock);
    //    propertyBlock.SetFloat("_DissolveAmt", dissolveAmt);
    //    renderer.SetPropertyBlock(propertyBlock);
    //}
}
