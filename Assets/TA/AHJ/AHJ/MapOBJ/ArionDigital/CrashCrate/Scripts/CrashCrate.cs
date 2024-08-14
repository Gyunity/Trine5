namespace ArionDigital
{
    using UnityEngine;

    public class CrashCrate : MonoBehaviour
    {
        [Header("Whole Create")]
        public MeshRenderer wholeCrate;
        public BoxCollider boxCollider;
        [Header("Fractured Create")]
        public GameObject fracturedCrate;
        [Header("Audio")]
        public AudioSource crashAudioClip;

        public GameObject cabbagesFac;

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.layer == LayerMask.NameToLayer("Arrow"))
            {
                Instantiate(cabbagesFac, transform.position, transform.rotation);
                wholeCrate.enabled = false;
                boxCollider.enabled = false;
                fracturedCrate.SetActive(true);
                crashAudioClip.Play();

            }

        }
        //private void OnTriggerEnter(Collider other)
        //{
        //    if (other.gameObject.layer == LayerMask.NameToLayer("Arrow"))
        //    {
        //        wholeCrate.enabled = false;
        //        boxCollider.enabled = false;
        //        fracturedCrate.SetActive(true);
        //        crashAudioClip.Play();

        //    }
        //}

        [ContextMenu("Test")]
        public void Test()
        {
            wholeCrate.enabled = false;
            boxCollider.enabled = false;
            fracturedCrate.SetActive(true);
        }
    }
}