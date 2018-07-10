using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGenerator : MonoBehaviour {

	public int depth = 20;

	public int width = 256;
	public int height = 256;

	public float scale = 20;

	public float offsetX = 100f;
	public float offsetY = 100f;

	void Start () {
		offsetX = Random.Range(0f,9999f);
		offsetY = Random.Range(0f,9999f);
		NewTerrain();
	}

	[ExecuteInEditMode]
	public void NewTerrain(){

		Terrain terrain = GetComponent<Terrain>();
		terrain.terrainData = GenerateTerrain(terrain.terrainData);
	}

	void Update(){
		NewTerrain();
	}

	TerrainData GenerateTerrain(TerrainData data){
		
		data.heightmapResolution = width +1;

		data.size = new Vector3(width, depth, height);

		data.SetHeights(0,0, GenerateHeights());

		return data;
	}

	float[,] GenerateHeights(){
		float [,] heights = new float[width, height];
		
		for(int x = 0; x<width; x++)
		{
			for(int y = 0; y< height; y++)
			{
				heights[x,y] = CalculateHeight(x,y);
			}
		}

		return heights;
	}

	float CalculateHeight(int x, int y){
		float xCord = (float)x/width * scale + offsetX;
		float yCord = (float)y/height * scale + offsetY;

		return Mathf.PerlinNoise(xCord, yCord);
	}
	
}
