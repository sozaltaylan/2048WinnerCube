using CubeWinner.Managers;
using TaylanGame.controllers;
using UnityEngine;
using DG.Tweening;

namespace CubeWinner.Controllers
{
    public class CubeController : MonoBehaviour
    {
        #region Variables

        [SerializeField] private int cubeNumber;
        private int _newCubeNumber;
        private int _ID;

        private Vector3 _newCubePos;

        private bool _hitted;

        [SerializeField] private Rigidbody rb;
        [SerializeField] private GameObject particle;

        [SerializeField] private float torqueSpeed;
        [SerializeField] private float upForce;
        [SerializeField] private float backForce;
        [SerializeField] private float sideForce;


        #endregion
        #region Properties
        public int CubeNumber => cubeNumber;

        #endregion


        public void SetForce(float speed)
        {
            rb.AddForce(Vector3.forward * speed, ForceMode.Impulse);
        }
        public void Move(float x)
        {
            transform.position = new Vector3(x, transform.position.y, transform.position.z);
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.TryGetComponent(out CubeController cube))
            {
                if (cube._hitted || _hitted)
                    return;
                if (cube.cubeNumber == this.cubeNumber)
                {
                    cube._hitted = true;
                    _hitted = true;


                    _newCubeNumber = cube.CubeNumber + CubeNumber;
                    _newCubePos = new Vector3(transform.position.x, transform.position.y, transform.position.z);

                    Destroy(cube.gameObject);
                    Destroy(this.gameObject);

                    Instantiate(particle, _newCubePos, Quaternion.identity);
                    var obj = CubeManager.Instance.CreateNewCube(_newCubeNumber, _newCubePos);
                    obj.SetShake();
                    obj.GetForceNewCube();
                }

            }
        }
        private void GetForceNewCube()
        {

            var UpForce = Vector3.up * upForce;
            var BackForce = Vector3.forward * backForce;
            var randomSideNumber = Random.Range(-2, 2);
            var SideWayForce = Vector3.right * randomSideNumber * sideForce;
            var FinalForce = UpForce + BackForce + SideWayForce;

            rb.AddForce(FinalForce, ForceMode.Impulse);
            rb.AddTorque(Random.insideUnitSphere.normalized * torqueSpeed, ForceMode.Impulse);

        }

        private void SetShake() 
        {
            transform.DOKill(true);
            transform.DOShakeScale(.5f, 1f, 1, 5);
        }


    }
}
