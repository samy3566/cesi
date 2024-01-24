using System;

public class Pizza
{
    public int Code { get; set; }
    public string Nom { get; set; }
    public double Prix { get; set; }

    public override string ToString()
    {
        return $"{Nom} -> {Code} ({Prix:C} €)";
    }
}

public class Program
{
    private static Pizza[] pizzas;

    public static void Main()
    {
        InitializePizzas();
        AfficherMenuPrincipal();
    }

    static void InitializePizzas()
    {
        pizzas = new Pizza[]
        {
            new Pizza { Code = 0, Nom = "PEP", Prix = 12.50 },
            new Pizza { Code = 1, Nom = "MAR", Prix = 14.00 },
            new Pizza { Code = 2, Nom = "REIN", Prix = 11.50 },
            new Pizza { Code = 3, Nom = "FRO", Prix = 12.00 },
            new Pizza { Code = 4, Nom = "CAN", Prix = 12.50 },
            new Pizza { Code = 5, Nom = "SAV", Prix = 13.00 },
            new Pizza { Code = 6, Nom = "ORI", Prix = 13.50 },
            new Pizza { Code = 7, Nom = "IND", Prix = 14.00 },
        };
    }

    static void AfficherMenuPrincipal()
    {
        int option;

        do
        {
            Console.WriteLine("Menu Principal");
            Console.WriteLine("1. Afficher les pizzas");
            Console.WriteLine("2. Ajouter une pizza");
            Console.WriteLine("3. Modifier une pizza");
            Console.WriteLine("4. Supprimer une pizza");
            Console.WriteLine("0. Quitter");

            Console.Write("Veuillez choisir une option : ");
            if (int.TryParse(Console.ReadLine(), out option))
            {
                switch (option)
                {
                    case 1:
                        AfficherPizzas();
                        break;
                    case 2:
                        AjouterPizza();
                        break;
                    case 3:
                        ModifierPizza(0); // Utilisez le code de la pizza à modifier (ici, 0) pour la simulation
                        break;
                    case 4:
                        SupprimerPizza(0); // Utilisez le code de la pizza à supprimer (ici, 0) pour la simulation
                        break;
                    case 0:
                        Console.WriteLine("Fin de l'application.");
                        break;
                    default:
                        Console.WriteLine("Option non valide. Veuillez réessayer.");
                        break;
                }
            }
            else
            {
                Console.WriteLine("Veuillez entrer un numéro valide.");
            }

        } while (option != 0);
    }

    static void AfficherPizzas()
    {
        Console.WriteLine("Liste des pizzas :");
        foreach (var pizza in pizzas)
        {
            Console.WriteLine(pizza);
        }
    }

    static void AjouterPizza()
    {
        Console.WriteLine("Ajout d'une nouvelle pizza :");
        Console.Write("Veuillez saisir le code : ");
        int code = int.Parse(Console.ReadLine());

        Console.Write("Veuillez saisir le nom (sans espace) : ");
        string nom = Console.ReadLine();

        Console.Write("Veuillez saisir le prix : ");
        double prix = double.Parse(Console.ReadLine());

        Pizza nouvellePizza = new Pizza { Code = code, Nom = nom, Prix = prix };
        Array.Resize(ref pizzas, pizzas.Length + 1);
        pizzas[pizzas.Length - 1] = nouvellePizza;

        Console.WriteLine("Pizza ajoutée avec succès !");
    }

    static void ModifierPizza(int codeModifier)
    {
        Console.WriteLine("Modification d'une pizza :");
        AfficherPizzas();
        Console.Write($"Veuillez saisir le nouveau code pour la pizza {codeModifier} : ");
        int nouveauCode = int.Parse(Console.ReadLine());

        Console.Write($"Veuillez saisir le nouveau nom (sans espace) pour la pizza {codeModifier} : ");
        string nouveauNom = Console.ReadLine();

        Console.Write($"Veuillez saisir le nouveau prix pour la pizza {codeModifier} : ");
        double nouveauPrix = double.Parse(Console.ReadLine());

        Pizza pizzaAModifier = Array.Find(pizzas, pizza => pizza.Code == codeModifier);

        if (pizzaAModifier != null)
        {
            pizzaAModifier.Code = nouveauCode;
            pizzaAModifier.Nom = nouveauNom;
            pizzaAModifier.Prix = nouveauPrix;

            Console.WriteLine("Pizza modifiée avec succès !");
        }
        else
        {
            Console.WriteLine("Code de pizza non trouvé.");
        }
    }

       static void SupprimerPizza(int codeSupprimer)
    {
        Console.WriteLine("Suppression d'une pizza :");
        AfficherPizzas();
        Console.Write($"Veuillez choisir le code de la pizza à supprimer ({codeSupprimer}) : ");
        int codeSaisi = int.Parse(Console.ReadLine());

        int indexASupprimer = Array.FindIndex(pizzas, pizza => pizza.Code == codeSaisi);

        if (indexASupprimer != -1)
        {
            Pizza[] nouveauTableau = new Pizza[pizzas.Length - 1];
            Array.Copy(pizzas, 0, nouveauTableau, 0, indexASupprimer);
            Array.Copy(pizzas, indexASupprimer + 1, nouveauTableau, indexASupprimer, pizzas.Length - indexASupprimer - 1);
            pizzas = nouveauTableau;

            Console.WriteLine("Pizza supprimée avec succès !");
        }
        else
        {
            Console.WriteLine("Code de pizza non trouvé.");
        }
    }
}
