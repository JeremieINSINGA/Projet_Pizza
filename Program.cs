using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace premier_programme
{

    class PizzaPersonnalisee : Pizza
    {
        static int nbPizzasPersonnalisee = 0;
        public PizzaPersonnalisee() : base("Personnalisee", 5, false, null)
        {

            nbPizzasPersonnalisee++;
            nom = "Personnalisée" + nbPizzasPersonnalisee;
            this.ingredients = new List<string>();

            while(true)
            {
            Console.Write("Rentrez un ingrédient pour la pizza personnalisée" + nbPizzasPersonnalisee + " (ENTER pour terminer) : ");
            var ingredient = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(ingredient))
            {
                break;
            }
            if (ingredients.Contains(ingredient))
            {
                Console.WriteLine("ERREUR : Cet ingrédient est déjà présent dans la pizza.");
            }
            else
            {
                ingredients.Add(ingredient);
                Console.WriteLine(string.Join(", ", ingredients));
            }
            Console.WriteLine();
            }
            
        }
    }
    class Pizza
    {
protected string nom;
public float prix { get; private set; }
public bool vegetarienne { get; private set; }
public List<string> ingredients { get; protected set; }
    
    public Pizza(string nom, float prix, bool vegetarienne, List<string> ingredients)
    {
        this.nom = nom;
        this.prix = prix;
        this.vegetarienne = vegetarienne;
        this.ingredients = ingredients;

    }
    public void Afficher()
    {
        string badgeVegetarienne = vegetarienne ? " (V) " : "";

        string nomAfficher = FormatPremiereLettreMajuscules(nom);

        /*var ingredientsAfficher = new List<string>();
        foreach(var ingredient in ingredients)
        {
            ingredientsAfficher.Add(FormatPremiereLettreMajuscules(ingredient))
        }*/

        var ingredientsAfficher = ingredients.Select(i => FormatPremiereLettreMajuscules(i)).ToList();

        Console.WriteLine(nomAfficher + badgeVegetarienne + " - " + prix + "€");
        Console.WriteLine(string.Join(", ", ingredients));
        Console.WriteLine();
    }

    private static string FormatPremiereLettreMajuscules(string s)
    {
        if(string.IsNullOrEmpty(s))
        return s;
        string minuscules = s.ToLower();
        string majuscules = s.ToUpper();

        string resultat = majuscules[0] + minuscules.Substring(1);

        return resultat;

    }
    public bool ContientIngredient(string ingredient) 
    { 
        return ingredients.Where(i => i.ToLower().Contains(ingredient)).ToList().Count > 0;
    }
    }
    class Program
    {
static void Main (string[] args)
{
//var pizza1 = new Pizza("4 fromages", 11.5f, false);
//pizza1.Afficher();
var pizzas = new List<Pizza>(){
    new Pizza("4 fromages", 11.5f, true, new List<string> {"cantal", "mozzarella", "fromage de chèvre", "gruyère"}),
    new Pizza("indienne", 10.5f, false, new List<string> {"curry", "mozzarella", "poulet", "poivron", "oignon"}),
    new Pizza ("MEXICAINE", 13f, false, new List<string> {"boeuf", "mozzarella", "mais", "tomates", "oignon", "coriandre"}),
    new Pizza ("margherita", 8f, true, new List<string> {"sauce tomate", "mozzarella", "basilic"}),
    new Pizza("Calzone", 12f, false, new List<string> {"tomate", "jambon", "persil", "oignons"}),
    new Pizza ("complète", 9.5f, false, new List<string> {"jambon", "oeuf", "fromage"}),
    new PizzaPersonnalisee(),
    new PizzaPersonnalisee()
};

// pizzas = pizzas.OrderByDescending(p => p.prix).ToList();

//pizzas = pizzas.Where(p => p.vegetarienne).ToList();
string json = JsonConvert.SerializeObject(pizzas);
Console.WriteLine(json);

foreach(var pizza in pizzas)
{
pizza.Afficher();
}
/*var ingredients =new List<string> { "sauce tomate", "mozzarella", "basilic "};
bool contientTomate = ingredients.Where(i => i.Contains("tomate")).ToList().Count > 0;
Console.WriteLine(contientTomate);*/
    }
    }
}