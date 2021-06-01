using System;

namespace CocktailsApi.Models
{
    public class Cocktails
    {
        public Parametrs[] drinks { get; set; }
    }

    public class Parametrs
    {
        public string idDrink { get; set; }
        public string strDrink { get; set; }
        public object strDrinkAlternate { get; set; }
        public string strTags { get; set; }
        public string strCategory { get; set; }
        public string strIBA { get; set; }
        public string strAlcoholic { get; set; }
        public string strGlass { get; set; }
        public string strInstructions { get; set; }
        public string strDrinkThumb { get; set; }
        public string strIngredient1 { get; set; }
        public string strIngredient2 { get; set; }
        public string strIngredient3 { get; set; }
        public string strIngredient4 { get; set; }
        public string strIngredient5 { get; set; }
        public string strIngredient6 { get; set; }
        public string strIngredient7 { get; set; }
        public string strIngredient8 { get; set; }
        public string strIngredient9 { get; set; }
        public string strIngredient10 { get; set; }
        public string strIngredient11 { get; set; }
        public string strIngredient12 { get; set; }
        public string strIngredient13 { get; set; }
        public string strIngredient14 { get; set; }
        public string strIngredient15 { get; set; }
        public string strMeasure1 { get; set; }
        public string strMeasure2 { get; set; }
        public string strMeasure3 { get; set; }
        public string strMeasure4 { get; set; }
        public string strMeasure5 { get; set; }
        public string strMeasure6 { get; set; }
        public string strMeasure7 { get; set; }
        public string strMeasure8 { get; set; }
        public string strMeasure9 { get; set; }
        public string strMeasure10 { get; set; }
        public string strMeasure11 { get; set; }
        public string strMeasure12 { get; set; }
        public string strMeasure13 { get; set; }
        public string strMeasure14 { get; set; }
        public string strMeasure15 { get; set; }

        public string ingridiants()
        {
            string ing = "";
            if (this.strIngredient1 != null) ing += $"{this.strIngredient1}";
            if (this.strMeasure1 != null) ing += $"({this.strMeasure1.Trim()}),";
            if (this.strIngredient2 != null) ing += $"{this.strIngredient2}";
            if (this.strMeasure2 != null) ing += $"({this.strMeasure2.Trim()}),";
            if (this.strIngredient3 != null) ing += $"{this.strIngredient3}";
            if (this.strMeasure3 != null) ing += $"({this.strMeasure3.Trim()}),";
            if (this.strIngredient4 != null) ing += $"{this.strIngredient4}";
            if (this.strMeasure4 != null) ing += $"({this.strMeasure4.Trim()}),";
            if (this.strIngredient5 != null) ing += $"{this.strIngredient5}";
            if (this.strMeasure5 != null) ing += $"({this.strMeasure5.Trim()}),";
            if (this.strIngredient6 != null) ing += $"{this.strIngredient6}";
            if (this.strMeasure6 != null) ing += $"({this.strMeasure6.Trim()}),";
            if (this.strIngredient7 != null) ing += $"{this.strIngredient7}";
            if (this.strMeasure7 != null) ing += $"({this.strMeasure7.Trim()}),";
            if (this.strIngredient8 != null) ing += $"{this.strIngredient8}";
            if (this.strMeasure8 != null) ing += $"({this.strMeasure8.Trim()}),";
            if (this.strIngredient9 != null) ing += $"{this.strIngredient9}";
            if (this.strMeasure9 != null) ing += $"({this.strMeasure9.Trim()}),";
            if (this.strIngredient10 != null) ing += $"{this.strIngredient10}";
            if (this.strMeasure10 != null) ing += $"({this.strMeasure10.Trim()}),";
            if (this.strIngredient11 != null) ing += $"{this.strIngredient11}";
            if (this.strMeasure11 != null) ing += $"({this.strMeasure11.Trim()}),";
            if (this.strIngredient12 != null) ing += $"{this.strIngredient12}";
            if (this.strMeasure12 != null) ing += $"({this.strMeasure12.Trim()}),";
            if (this.strIngredient13 != null) ing += $"{this.strIngredient13}";
            if (this.strMeasure13 != null) ing += $"({this.strMeasure13.Trim()}),";
            if (this.strIngredient14 != null) ing += $"{this.strIngredient14}";
            if (this.strMeasure14 != null) ing += $"({this.strMeasure14.Trim()}),";
            if (this.strIngredient15 != null) ing += $"{this.strIngredient15}";
            if (this.strMeasure15 != null) ing += $"({this.strMeasure15.Trim()}),";

            ing = ing.Substring(0, ing.Length - 1);

            return ing;
        }

        public string Print_inf()
        {
            string inf, ing;

            ing = this.ingridiants();

            inf = $"<i><b>{this.strDrink}</b></i>\n\n" +
                $"<b>Glass:</b> <i>{this.strGlass}</i>\n\n" +
                $"<b>Category:</b> <i>{this.strCategory}</i>\n\n" +
                $"<b>Ingridients:</b> <i>{ing}</i>\n\n" +
                $"<b>Recipe:</b> <i>{this.strInstructions}</i>";

            if (this.strDrinkAlternate != null) inf += $"\n\n<i>Alternative: {this.strDrinkAlternate}</i>";

            return inf;
        }
    }
}