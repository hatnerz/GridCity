using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Score
{
    public class ScoreManager : MonoBehaviour
    {
        [SerializeField] private GridManager gridManager;

        /*void Start()
        {

        }*/

        public int CalculateTotalScore()
        {
            var totalScore = 0;
            foreach (var buildingPlaceObjects in gridManager.BuildingPlacesObjects)
            {
                var building = buildingPlaceObjects.GetComponent<BuildingPlace>().Building;
                if (building != null)
                {
                    totalScore += building.CalculateTotalBuildingScore(gridManager);
                }
            }

            return totalScore;
        }
    }

}
