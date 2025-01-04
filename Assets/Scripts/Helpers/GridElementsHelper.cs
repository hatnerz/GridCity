
using System.Collections.Generic;
using System.Linq;
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

            var adjacentCoordinates = GetAdjacentCoordinates(gridCoordinates, gridState);
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

        public static List<Vector2Int> GetAdjacentCoordinates(Vector2Int gridCoordinates, IGridState gridState)
        {
            var x = gridCoordinates.x;
            var y = gridCoordinates.y;
            var adjacentCoordinates = new List<Vector2Int>() { new(x - 1, y), new(x, y - 1), new(x + 1, y), new(x, y + 1) };

            adjacentCoordinates.RemoveAll(coord =>
                coord.x < 0 || coord.x >= gridState.SizeX || // За межами по X
                coord.y < 0 || coord.y >= gridState.SizeY    // За межами по Y
            );
            return adjacentCoordinates;
        }

        public static Building GetBuildingIfExistsOnGrid(Vector2Int gridCoordinates, IGridState gridState)
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

        public static List<Building> GetBuildingsInRowAndColumn(Vector2Int gridCoordinates, IGridState gridState)
        {
            var rowBuildings = new List<Building>();
            var columnBuildings = new List<Building>();

            for (int x = 0; x < gridState.SizeX; x++)
            {
                var element = gridState.GridElements[x, gridCoordinates.y];
                if (element is Building building)
                {
                    rowBuildings.Add(building);
                }
            }

            for (int y = 0; y < gridState.SizeY; y++)
            {
                var element = gridState.GridElements[gridCoordinates.x, y];
                if (element is Building building)
                {
                    columnBuildings.Add(building);
                }
            }

            return rowBuildings.Concat(columnBuildings).Distinct().ToList();
        }

        public static List<Building> GetBuildingsInRadius(Vector2Int center, int radius, IGridState gridState)
        {
            var buildingsInRadius = new List<Building>();

            for (int x = -radius; x <= radius; x++)
            {
                for (int y = -radius; y <= radius; y++)
                {
                    var currentPosition = new Vector2Int(center.x + x, center.y + y);

                    if (currentPosition == center)
                        continue;

                    if (currentPosition.x < 0 || currentPosition.x >= gridState.SizeX ||
                        currentPosition.y < 0 || currentPosition.y >= gridState.SizeY)
                    {
                        continue;
                    }

                    var element = gridState.GridElements[currentPosition.x, currentPosition.y];
                    if (element is Building building)
                    {
                        buildingsInRadius.Add(building);
                    }
                }
            }

            return buildingsInRadius;
        }
    }

}
