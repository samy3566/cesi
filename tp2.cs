using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

// Interface définissant le comportement des entités vendables
public interface IVendable
{
    void Vendre();
}

// Classe de base abstraite représentant une Personne
public abstract class Personne
{
    public string Nom { get; set; }
    public string Prenom { get; set; }
    public string Adresse { get; set; }
    public string Telephone { get; set; }
}

// Classe décrivant un Salarié, une entité héritant de Personne
public class Salarie : Personne
{
    public int Matricule { get; set; }
    public string Departement { get; set; }
    public string Poste { get; set; }
    public double Salaire { get; set; }
}

// Classe représentant un Client, une entité héritant de Personne
public class Client : Personne
{
    public int NumClient { get; set; }
    public List<Achat> HistoriqueAchats { get; set; } = new List<Achat>();
}

// Classe représentant un Fournisseur, une entité héritant de Personne
public class Fournisseur : Personne
{
    public int NumFournisseur { get; set; }
    public List<Produit> ProduitsFournis { get; set; } = new List<Produit>();
}

// Classe définissant un Produit, implémentant l'interface IVendable
public class Produit : IVendable
{
    public int Reference { get; set; }
    public string Nom { get; set; }
    public double Prix { get; set; }
    public Fournisseur Fournisseur { get; set; }

    public void Vendre()
    {
        Console.WriteLine($"Le produit '{Nom}' a été vendu.");
    }
}

// Classe représentant un Achat
public class Achat
{
    public int NumAchat { get; set; }
    public List<Produit> ProduitsAchetes { get; set; } = new List<Produit>();
    public Client Client { get; set; }
    public DateTime DateAchat { get; set; }
}

// Classe Entreprise utilisant le patron de conception Singleton
public class Entreprise
{
    private static Entreprise instance;

    public static Entreprise Instance
    {
        get
        {
            instance ??= new Entreprise();
            return instance;
        }
    }

    private Entreprise() { }

    public string Nom { get; set; }
    public string SIRET { get; set; }
    public string Adresse { get; set; }
    public List<Salarie> Salaries { get; set; } = new List<Salarie>();
    public List<Client> Clients { get; set; } = new List<Client>();
    public List<Fournisseur> Fournisseurs { get; set; } = new List<Fournisseur>();
    public List<Produit> Produits { get; set; } = new List<Produit>();
    public List<Achat> Achats { get; set; } = new List<Achat>();

    // Méthode pour ajouter un produit avec gestion d'exceptions
    public void AjouterProduit(Produit produit)
    {
        if (!Produits.Any(p => p.Reference == produit.Reference))
        {
            Produits.Add(produit);
        }
        else
        {
            throw new InvalidOperationException("Le produit existe déjà.");
        }
    }

    // Méthode pour sauvegarder l'état de l'entreprise dans un fichier
    public void SauvegarderEtat()
    {
        string filePath = "entreprise_data.txt";
        using (StreamWriter writer = new StreamWriter(filePath))
        {
            // Écriture des données dans le fichier
            writer.WriteLine($"Nom: {Nom}");
            writer.WriteLine($"SIRET: {SIRET}");
            writer.WriteLine($"Adresse: {Adresse}");

            // ... Écriture des autres données ...

            Console.WriteLine("L'état de l'entreprise a été sauvegardé avec succès.");
        }
    }

    // Méthode pour restaurer l'état de l'entreprise depuis un fichier
    public void RestaurerEtat()
    {
        string filePath = "entreprise_data.txt";
        if (File.Exists(filePath))
        {
            using (StreamReader reader = new StreamReader(filePath))
            {
                // Lecture des données depuis le fichier
                Nom = ExtractValue(reader.ReadLine());
                SIRET = ExtractValue(reader.ReadLine());
                Adresse = ExtractValue(reader.ReadLine());

                // ... Lecture des autres données ...

                Console.WriteLine("L'état de l'entreprise a été restauré avec succès.");
            }
        }
        else
        {
            Console.WriteLine("Aucun fichier d'état trouvé. L'état de l'entreprise n'a pas été restauré.");
        }
    }

    // Méthode utilitaire pour extraire la valeur d'une ligne de données
    private static string ExtractValue(string line)
    {
        return line?.Split(':').LastOrDefault()?.Trim();
    }
}

// Classe principale pour tester le système
class Program
{
    static void Main()
    {
        // Exemple d'utilisation du système
        Entreprise entreprise = Entreprise.Instance;

        // Ajout de salariés, clients, fournisseurs, produits, achats, etc.

        // Test de sauvegarde et restauration de l'état
        entreprise.SauvegarderEtat();
        entreprise.RestaurerEtat();

        // Utilisation d'une interface utilisateur (simplifiée)
        AfficherMenu();
    }

    static void AfficherMenu()
    {
        Console.WriteLine("1. Ajouter un produit");
        Console.WriteLine("2. Afficher la liste des produits");
        Console.WriteLine("3. Quitter");

        Entreprise entreprise = Entreprise.Instance;

        int choix;
        while (true)
        {
            if (int.TryParse(Console.ReadLine(), out choix))
            {
                switch (choix)
                {
                    case 1:
                        // Ajouter un produit
                        Console.WriteLine("Entrez les détails du produit:");
                        Produit nouveauProduit = new Produit
                        {
                            Reference = int.Parse(Console.ReadLine()),
                            Nom = Console.ReadLine(),
                            Prix = double.Parse(Console.ReadLine()),
                            Fournisseur = new Fournisseur { NumFournisseur = 1 } // Exemple, à adapter
                        };
                        try
                        {
                            entreprise.AjouterProduit(nouveauProduit);
                            Console.WriteLine("Produit ajouté avec succès.");
                        }
                        catch (InvalidOperationException ex)
                        {
                            Console.WriteLine($"Erreur : {ex.Message}");
                        }
                        break;

                    case 2:
                        // Afficher la liste des produits
                        Console.WriteLine("Liste des produits :");
                        foreach (var produit in entreprise.Produits)
                        {
                            Console.WriteLine($"Référence : {produit.Reference}, Nom : {produit.Nom}, Prix : {produit.Prix}");
                        }
                        break;

                    case 3:
                        // Quitter
                        return;

                    default:
                        Console.WriteLine("Choix invalide. Veuillez réessayer.");
                        break;
                }
            }
            else
            {
                Console.WriteLine("Veuillez entrer un nombre.");
            }
        }
    }
}
