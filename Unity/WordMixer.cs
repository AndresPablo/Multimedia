using System;
using UnityEngine;
using System.Linq;

public static class WordMixer
{

    public static string MezclarPalabras(string textoOriginal)
    {
        var rd = new System.Random();
        string[] words = textoOriginal.Split(' ').OrderBy(w => rd.Next()).ToArray();

        // If you want a simple string instead of an array of words
        string rdText = string.Join(" ", words);

        return rdText;
    }

}
