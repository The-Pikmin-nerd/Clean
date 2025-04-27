using System;
using System.IO;
using System.Collections.Generic;

class Program
{
    
    // Categories for file extensions
    static readonly Dictionary<string, string> categories = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
    {
        { ".jpg", "Images" },
        { ".jpeg", "Images" },
        { ".png", "Images" },
        { ".gif", "Images" },
        { ".mp4", "Vidéos" },
        { ".avi", "Vidéos" },
        { ".mov", "Vidéos" },
        { ".mp3", "Audio" },
        { ".wav", "Audio" },
        { ".flac", "Audio" },
        { ".zip", "Archives" },
        { ".rar", "Archives" },
        { ".7z", "Archives" },
        { ".pdf", "Documents" },
        { ".docx", "Documents" },
        { ".txt", "Documents" },
        {".pptx", "Documents"}
    };

    // Main entry point of the application
    static void Main()
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine(@"           __   ______   __                               
          /  \ /      \ |  \                              
         /  $$|  $$$$$$\| $$  ______    ______   _______  
        /  $$ | $$   \$$| $$ /      \  |      \ |       \ 
       /  $$  | $$      | $$|  $$$$$$\  \$$$$$$\| $$$$$$$\
      /  $$   | $$   __ | $$| $$    $$ /      $$| $$  | $$
 __  /  $$    | $$__/  \| $$| $$$$$$$$|  $$$$$$$| $$  | $$
|  \|  $$      \$$    $$| $$ \$$     \ \$$    $$| $$  | $$
 \$$ \$$        \$$$$$$  \$$  \$$$$$$$  \$$$$$$$ \$$   \$$
                                                          
                                                          
                                                          

");
        Console.ForegroundColor = ConsoleColor.Magenta;
        while (true)
        {
            Console.WriteLine("Entrez le chemin du dossier à organiser :");
            string cheminDossier = Console.ReadLine();

            if (!Directory.Exists(cheminDossier))
            {
                Console.WriteLine("Dossier introuvable.");
                continue; // Prompts the user again if the directory is invalid
            }

            OrganiserFichiers(cheminDossier);

            Console.WriteLine("Organisation terminée !");
            // Break the loop after one successful run
          
        }
    }

    // Function to organize files in the specified directory
    static void OrganiserFichiers(string cheminDossier)
    {
        string[] fichiers = Directory.GetFiles(cheminDossier);

        foreach (var fichier in fichiers)
        {
            string extension = Path.GetExtension(fichier);
            string categorie = categories.ContainsKey(extension) ? categories[extension] : "Autres";

            string cheminSousDossier = Path.Combine(cheminDossier, categorie);

            Directory.CreateDirectory(cheminSousDossier); // Create subfolder if it doesn't exist

            string nouveauChemin = Path.Combine(cheminSousDossier, Path.GetFileName(fichier));

            if (!File.Exists(nouveauChemin)) // Avoid overwriting files
            {
                File.Move(fichier, nouveauChemin);
                Console.WriteLine($"Déplacé : {Path.GetFileName(fichier)} -> {categorie}");
                System.Threading.Thread.Sleep(100);
            }
            else
            {
                Console.WriteLine($"⚠️ Fichier déjà existant : {Path.GetFileName(fichier)} non déplacé.");
            }
        }
    }
}
