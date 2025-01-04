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

        public int CalculateTotalScore()
        {
            var totalScore = 0;
            foreach (var buildingPlaceObject in gridManager.BuildingPlacesObjects)
            {
                totalScore += CalculateCurrentBuildingPlaceScore(buildingPlaceObject.GetComponent<BuildingPlace>());
            }

            return totalScore;
        }

        public int CalculateCurrentBuildingPlaceScore(BuildingPlace buildingPlace)
        {
            var building = buildingPlace.Building;
            if (building != null)
            {
                return building.CalculateTotalBuildingScore(gridManager);
            }

            return 0;
        }
    }

}
