using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utils {
    //sets our world height for the Perlin Noise
	static int maxHeight = 150;

	static float smooth = 0.01f;
    //Sets the number of iterations of perlin noise.
	static int octaves = 4;

	static float persistence = 0.5f;

	public static int GenerateHeight(float x, float z)
	{
        //uses brownian motion fBM function to modify the terrain height.
		float height = Map(0,maxHeight, 0, 1, fBM(x*smooth/4,z*smooth/4,octaves,persistence));
		return (int) height;
	}
    public static float fBM3D(float x, float y, float z, float sm, int oct)
    {
        float XY = fBM(x * sm, y * sm, oct, 0.5f);
        float YZ = fBM(y * sm, z * sm, oct, 0.5f);
        float XZ = fBM(x * sm, z * sm, oct, 0.5f);

        float YX = fBM(y * sm, x * sm, oct, 0.5f);
        float ZY = fBM(z * sm, y * sm, oct, 0.5f);
        float ZX = fBM(z * sm, x * sm, oct, 0.5f);

        return (XY + YZ + XZ + YX + ZY + ZX) / 6.0f;
    }
    public static int GenerateStoneHeight(float x, float z)
    {
        //uses brownian motion fBM function to modify the terrain height.
        float height = Map(0, maxHeight- 20, 0, 1, fBM(x * smooth, z * smooth *2 , octaves + 2, persistence));
        return (int)height;
    }


    static float Map(float newmin, float newmax, float origmin, float origmax, float value)
    {
        return Mathf.Lerp (newmin, newmax, Mathf.InverseLerp (origmin, origmax, value));
    }

    //fractal brownian motion that modifies the perlin noise for our program.
    static float fBM(float x, float z, int oct, float pers)
    {
        float total = 0;
        float frequency = 1;
        float amplitude = 1;
        float maxValue = 0;
        for(int i = 0; i < oct ; i++) 
        {
                total += Mathf.PerlinNoise(x * frequency, z * frequency) * amplitude;

                maxValue += amplitude;

                amplitude *= pers;
                frequency *= 2;
        }

        return total/maxValue;
    }

}
