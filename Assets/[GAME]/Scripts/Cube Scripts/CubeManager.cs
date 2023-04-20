using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CubeWinner.Exceptions;
using CubeWinner.Controllers;

namespace CubeWinner.Managers
{
    public class CubeManager : MonoSingleton<CubeManager>
    {
        #region Variables

        [SerializeField] List<CubeController> _prefabCubes = new List<CubeController>();
        [SerializeField] private Transform _createTarget;
        [SerializeField] private float startSpeed;
        private CubeController _selectedCube;
        private bool isMouseClick;

        #endregion

        #region Methods
        private void Start()
        {
            _selectedCube = CreateNewCube(2, _createTarget.transform.position);

        }

        public void HoldClickMovement(float x)
        {
            if (_selectedCube == null)
                return;
            _selectedCube.Move(x);
        }
        public CubeController CreateNewCube(int number, Vector3 createPos)
        {
            var cubePrefab = GetPrefab(number);
            var newCube = Instantiate(cubePrefab.gameObject, createPos, Quaternion.identity);
            var cube = newCube.GetComponent<CubeController>();
            return cube;
        }

        public CubeController GetPrefab(int number)
        {
            for (int i = 0; i < _prefabCubes.Count; i++)
            {
                if (_prefabCubes[i].CubeNumber == number)
                {
                    return _prefabCubes[i];
                }
            }
            return null;
        }


        public void SetMouseUp()
        {
            if (isMouseClick == true)
                return;
            StartCoroutine(Delay());
            IEnumerator Delay()
            {
                isMouseClick = true;
                _selectedCube.SetForce(startSpeed);
                _selectedCube = null;
                yield return new WaitForSeconds(.5f);
                _selectedCube = CreateNewCube(2, _createTarget.transform.position);
                isMouseClick = false;
            }
        }



    }
    #endregion

}




