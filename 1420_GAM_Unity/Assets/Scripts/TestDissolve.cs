using UnityEngine;

public class TestDissolve : MonoBehaviour
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
            float value = Mathf.PingPong(Time.time, 10f); // Example dynamic change
            materialInstance.SetFloat("dissolveAmt", value);  // Match the property name in Shader Graph
        }
    }
    //[SerializeField] private Material material;
    //private float dissolveAmt;
    //void Start()
    //{
    //    dissolveAmt = 0;
    //}

    //void Update()
    //{
    //    float dissolveSpeed = 10f;
    //    dissolveAmt += dissolveSpeed * Time.deltaTime;

    //    dissolveAmt = Mathf.Clamp(dissolveAmt, 0f, 10f);
    //    material.SetFloat("Dissolve", dissolveAmt);
    //}
}
