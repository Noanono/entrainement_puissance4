using Microsoft.VisualBasic;
using System;

Console.WriteLine("Entrez la taille du plateau :");
var taille = int.Parse(Console.ReadLine());
var plateau = new string[taille, taille];
for (int i = 0; i < taille; i++)
{
    for (int j = 0; j < taille; j++)
    {
        plateau[i, j] = null;
    }
}

Console.WriteLine("Entrez le nombre de joueurs :");
var nbJoueurs = int.Parse(Console.ReadLine());
var joueurs = new string[nbJoueurs, 2];
var joueurs_c = new string[nbJoueurs];
for (int i = 0; i < nbJoueurs; i++)
{
    Console.WriteLine($"Entrez le nom du joueur {i + 1} :");
    joueurs[i, 0] = Console.ReadLine();
    Console.WriteLine($"Entrez la couleur du joueur {i + 1} :");
    Coul:
    joueurs[i, 1] = Console.ReadLine();
    switch (joueurs[i, 1])
    {
        case "rouge":
            joueurs[i, 1] = "R";
            break;
        case "bleu":
            joueurs[i, 1] = "B";
            break;
        case "vert":
            joueurs[i, 1] = "V";
            break;
        case "jaune":
            joueurs[i, 1] = "J";
            break;
        case "orange":
            joueurs[i, 1] = "O";
            break;
        case "violet":
            joueurs[i, 1] = "P";
            break;
        default:
            Console.WriteLine("Couleur invalide, veuillez entrer une couleur valide :");
            goto Coul;
    }
    if (joueurs_c.Contains(joueurs[i, 1]))
    {
        Console.WriteLine("Couleur deja utilisee, veuillez entrer une couleur valide :");
        goto Coul;
    }
    joueurs_c[i] = joueurs[i, 1];
}

Debut_jeu:
var joueurActuel = 0;
var nbCases = taille * taille;
var casesJouees = 0;
while (true)
{
    Console.WriteLine($"C'est au tour de {joueurs[joueurActuel, 0]} de jouer.");
    Afficher_plateau();
    Console.WriteLine("Entrez la colonne de la case à jouer :");
    mauvaise_colonne:
    var colonne = int.Parse(Console.ReadLine()) - 1;
    if (colonne < 0 || colonne > taille)
    {
        Console.WriteLine("Colonne inexistante, veuillez entrer une colonne valide.");
        goto mauvaise_colonne;
    }
    for (int ligne = taille-1; ligne >= 0; ligne--)
    {
        if (plateau[ligne, colonne] == null)
        {
            plateau[ligne, colonne] = joueurs[joueurActuel, 1];
            casesJouees++;
            break;
        }
        if (ligne == 0)
        {
            Console.WriteLine("Colonne pleine, veuillez entrer une autre colonne.");
            goto mauvaise_colonne;
        }
    }
    if (CheckVictory() != null)
    {
        Console.WriteLine($"{CheckVictory()} a gagné !");
        break;
    }else if (casesJouees == nbCases)
    {
        Console.WriteLine("Match nul !");
        break;
    }
    joueurActuel = (joueurActuel + 1) % nbJoueurs;
}

Console.WriteLine("Voulez-vous rejouer ? (oui/non)");
var rejouer = Console.ReadLine();
if (rejouer == "oui")
{
    goto Debut_jeu;
}

/*/
 * Fonction qui vérifie si un joueur a gagné en suivant les règles du puissance 4
/*/
string CheckVictory()
{
    for (int i = 0; i < taille; i++)
    {
        for (int j = 0; j < taille; j++)
        {
            if (plateau[i, j] != null)
            {
                if (j + 3 < taille && plateau[i, j] == plateau[i, j + 1] && plateau[i, j] == plateau[i, j + 2] && plateau[i, j] == plateau[i, j + 3])
                {

                    return joueurs[Array.IndexOf(joueurs_c, plateau[i, j]), 0];
                }
                if (i + 3 < taille && plateau[i, j] == plateau[i + 1, j] && plateau[i, j] == plateau[i + 2, j] && plateau[i, j] == plateau[i + 3, j])
                {
                    return joueurs[Array.IndexOf(joueurs_c, plateau[i, j]), 0];
                }
                if (i + 3 < taille && j + 3 < taille && plateau[i, j] == plateau[i + 1, j + 1] && plateau[i, j] == plateau[i + 2, j + 2] && plateau[i, j] == plateau[i + 3, j + 3])
                {
                    return joueurs[Array.IndexOf(joueurs_c, plateau[i, j]), 0];
                }
                if (i - 3 >= 0 && j + 3 < taille && plateau[i, j] == plateau[i - 1, j + 1] && plateau[i, j] == plateau[i - 2, j + 2] && plateau[i, j] == plateau[i - 3, j + 3])
                {
                    return joueurs[Array.IndexOf(joueurs_c, plateau[i, j]), 0];
                }
            }
        }
    }
    return null;
}

/*/
 * Fonction qui affiche le plateau de jeu
/*/
void Afficher_plateau(int[] dep = null, int sens = 0)
{
    if (dep != null)
    {

    }
    for (int i = 0; i < taille; i++)
    {
        for (int j = 0; j < taille; j++)
        {
            Console.Write(plateau[i, j] == null ? "| - |" : $"| {plateau[i, j]} |");
        }
        Console.WriteLine();
        Console.WriteLine(new string('-', 5 *taille));
    }
    for (int i = 1; i <= taille; i++)
    {
        Console.Write($"  {i}  ");
    }
    Console.WriteLine();
}
