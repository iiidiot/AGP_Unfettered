using UnityEngine;
using System.Collections;

//-----Fur Script-----

public class FurScript : MonoBehaviour
{

    [Tooltip("Attach the material with the furshader here.")]
    public Material furMaterial;

    [Space(10)]

    [Tooltip("Enable to simulate fur movement when the object is moving. Requires object to have a RigidBody.")]
    public bool simulateMovement;

    [Tooltip("Enable to fix fur being culled at the edge of screen. This does affect performance, only use if neccessary.")]
    public bool cullCorrection;

    [Tooltip("Adjust to a higher value if your fur is removing other transparent objects behind it. If you have a lot of transparent objects you will need to intelligently layer them with this.")]
    [Range(0, 10)]
    public int transparentDepth;

    private Rigidbody rb;
    private bool hasRB;
    private new Camera camera;
    private Bounds originalBounds;
    private Mesh mesh;

    void Start()
    {
        //Get RigidBody for simulating movement
        if (!GetComponent<Rigidbody>().Equals(null))
        {
            rb = GetComponent<Rigidbody>();
        }
        else if (GetComponent<Rigidbody>().Equals(null) && simulateMovement)
        {
            Debug.Log("FurShader: Simulate Movement option enabled on GameObject: '" + gameObject.name + "', but this object does not have a RigidBody. Attach a RigidBody Component to simulate movement.");
        }

        //Render Queue Order. Transparency can affect other transparent objects behind.
        furMaterial.renderQueue = 3000 + transparentDepth;

        //Culling Correction stuff
        camera = Camera.main;
        mesh = GetComponent<MeshFilter>().mesh;
        originalBounds = mesh.bounds;

    }

    void Update()
    {

        //Set the shader's velocity
        if (!GetComponent<Rigidbody>().Equals(null) && simulateMovement)
        {
            furMaterial.SetVector("_Velocity", transform.InverseTransformDirection(rb.velocity));
        }
        else
        {
            furMaterial.SetVector("_Velocity", new Vector3(0, 0, 0));
        }

        //Culling Correction Continued
        //Unity's Frustrum Culling will cull a mesh that has a vertex shader applied to it that scales its vertices larger than they are
        //If you find your fur dissapering near the edge of the screen, enable Cull Correction
        if (cullCorrection)
        {
            Vector3 camPosition = camera.transform.position;
            Vector3 normCamForward = Vector3.Normalize(camera.transform.forward);

            float boundsDistance = (camera.farClipPlane - camera.nearClipPlane) / 2 + camera.nearClipPlane;
            Vector3 boundsTarget = camPosition + (normCamForward * boundsDistance);
            Vector3 realtiveBoundsTarget = this.transform.InverseTransformPoint(boundsTarget);
            mesh.bounds = new Bounds(realtiveBoundsTarget, Vector3.one);
        }
        else
        {
            mesh.bounds = originalBounds;
        }


    }

}