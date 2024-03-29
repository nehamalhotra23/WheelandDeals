using System;
using System.Collections.Generic;
using System.Linq;

namespace CarDealership.Models 
{
public class Dealership 
{
    List<Car> AllCars {get;set;}
    List<Car> SUVs {get;set;}
    List<Car> Trucks {get;set;}
    List<Car> Sedans {get;set;}
    List<Car> Vans {get;set;}



    public Dealership()
    {
        AllCars = new List<Car>();
        SUVs = new List<Car>();
        Trucks = new List<Car>();
        Sedans = new List<Car>();
        Vans = new List<Car>();
    }

    public string AddCar(Car c)
    {
        if(c is SUV)
        {
            SUVs.Add(c);
        }
        else if (c is Van)
        {
            Vans.Add(c);
        }
        else if (c is Truck)
        {
            Trucks.Add(c);
        }
        else if (c is Sedan)
        {
            Sedans.Add(c);
        }
        AllCars.Add(c);
        return "Added " + c.Make +" "+ c.Model + " at $" + c.Price;
    }

    public List<Car> SearchbyType(string type)
    {
        if(type == "suv")
        {
            return SUVs;
        }
        else if(type == "van")
        {
            return Vans;
        }
        else if (type == "truck")
        {
            return Trucks;
        }
        else if (type == "sedan")
        {
            return Sedans;
        }
        else
        {
            return AllCars;
        }
    }
    public static List<Car> SearchbyMaxPrice(List<Car> cars,int maxPrice)
    {
        List<Car> underMaxPrice = new List<Car>();
        foreach(Car c in cars)
        {
            if(c.Price <= maxPrice)
            {
                underMaxPrice.Add(c);
            }
        }
        return underMaxPrice;
    }
    public static List<Car> SearchbyYear(List<Car> cars,int maxYear)
    {
        List<Car> underMaxYear = new List<Car>();
        foreach(Car c in cars)
        {
            if(c.Year <= maxYear)
            {
                underMaxYear.Add(c);
            }
        }
        return underMaxYear;
    }

    public List<Car> Search(string type, string price, string year)
    {
        List<Car> outputList = SearchbyType(type);
        int priceInt,yearInt;
        bool successPrice = Int32.TryParse(price, out priceInt);
        bool successYear = Int32.TryParse(year, out yearInt);
        if(successPrice && successYear)
        {
            outputList = SearchbyMaxPrice(outputList, priceInt);
            if(outputList.Any())
            {
                outputList = SearchbyYear(outputList, yearInt);
            }
        }
        else if (successPrice)
        {
            outputList = SearchbyMaxPrice(outputList, priceInt);
        }
        else if (successYear)
        {
            outputList = SearchbyYear(outputList, yearInt);
        }
        return outputList;
    }


    

    public static List<string> ListofCarObjstoHTML(List<Car> cars)
    {
        List<string> output = new List<string>();
        foreach (Car c in cars)
        {
            output.Add(c.PrintCar());
        }
        return output;
    }

    public static string PrintCarListtoRows(List<Car> cars)
    {
        if(cars.Any())
        {
            List<string> carsHTML = ListofCarObjstoHTML(cars);
            double num = carsHTML.Count/3.0;
            double numberRows = Math.Ceiling(num);
            int totalDivs = (int)numberRows *3;
            string output = "";
            for(int i=0;i<totalDivs;i++)
            {
                if(i<carsHTML.Count)
                {
                    if(i==0)
                    {
                        output+="<div class=row>"+carsHTML[i];
                    }
                    else if (i%3 ==0)
                    {
                        output+="</div><div class=row>"+carsHTML[i];
                    }
                    else
                    {
                        output += carsHTML[i];
                    }
                }
                else
                {
                    output+="<div class=col-lg-4></div>";
                }
            }
            output += "</div>";
            return output;
        }
        else
        {
            return "<p>No Results were found matching your criteria. Please try again.</p><a href='/'><button type=button>New Search</button></a>";
        }
        
    }
}

public class Car
{
    public string Make {get;set;}
    public string Model {get;set;}
    public int Mileage {get;set;}
    public int Price {get;set;}
    public int Year {get;set;}
    public string Picture {get;set;}

    public Car (string make, string model, int price, int mileage, int year,
    string picture)
    {
        Make = make;
        Model = model;
        Price = price;
        Mileage = mileage;
        Year = year;
        Picture = picture;
    }

    public string PrintCar()
    {
        string output = "<div class=col-lg-4><img src="+Picture+" alt='car'><h2>"+Make+" " + Model+" " + Year+"</h2><h3>"+Mileage+" miles</h3><h3>$"+Price+"</h3></div>";
        return output;
    }


}

public class SUV:Car
{
    public SUV(string make, string model, int price, int mileage, int year,
    string picture)
        :base(make,model,price,mileage,year,picture)
    {

    }
 
}

public class Truck:Car
{
    public Truck(string make, string model, int price, int mileage, int year,
    string picture)
        :base(make,model,price,mileage,year,picture)
    {

    }
 
}

public class Sedan:Car
{
    public Sedan(string make, string model, int price, int mileage, int year,
    string picture)
        :base(make,model,price,mileage,year,picture)
    {

    }
 
}
public class Van:Car
{
    public Van(string make, string model, int price, int mileage, int year,
    string picture)
        :base(make,model,price,mileage,year,picture)
    {

    }
 
}

}
