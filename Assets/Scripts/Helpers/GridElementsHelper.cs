
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Helpers
{
    public static class GridElementsHelper 
    {
        public static List<Building> GetAdjacentBuildings(Vector2Int gridCoordinates, IGridState gridState)
        {
            var adjacentBuildings = new List<Building>();
            var x = gridCoordinates.x;
            var y = gridCoordinates.y;

            var adjacentCoordinates = GetAdjacentCoordinates(gridCoordinates);
            foreach(var coordinate in adjacentCoordinates)
            {
                var potentialBuilding = GetBuildingIfExistsOnGrid(new Vector2Int(coordinate.x, coordinate.y), gridState);
                if(potentialBuilding != null)
                {
                    adjacentBuildings.Add(potentialBuilding);
                }
            }

            return adjacentBuildings;
        }

        private static List<Vector2Int> GetAdjacentCoordinates(Vector2Int gridCoordinates)
        {
            var x = gridCoordinates.x;
            var y = gridCoordinates.y;
            var adjacentCoordinates = new List<Vector2Int>() { new(x - 1, y), new(x, y - 1), new(x + 1, y), new(x, y + 1) };
            return adjacentCoordinates;
        }

        private static Building GetBuildingIfExistsOnGrid(Vector2Int gridCoordinates, IGridState gridState)
        {
            var x = gridCoordinates.x;
            var y = gridCoordinates.y;
            if (x < 0 || x >= gridState.SizeX || y < 0 || y >= gridState.SizeY)
                return null;

            var potentialBuilding = gridState.GridElements[x, y];

            if (potentialBuilding == null || potentialBuilding is not Building)
                return null;

            return potentialBuilding as Building;
        }
    }

}
