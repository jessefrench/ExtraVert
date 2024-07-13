List<Plant> plants = new List<Plant>() // list to store plants
{
    // instances
    new Plant()
    {
        Species = "Monstera",
        LightNeeds = 4,
        AskingPrice = 30.00M,
        City = "Naperville",
        ZIP = "60540",
        AvailableUntil = new DateTime(2025, 6, 1),
        Sold = false
    },
    new Plant()
    {
        Species = "Snake Plant",
        LightNeeds = 3,
        AskingPrice = 15.00M,
        City = "Madison",
        ZIP = "53703",
        AvailableUntil = new DateTime(2025, 2, 1),
        Sold = false
    },
    new Plant()
    {
        Species = "Peace Lily",
        LightNeeds = 2,
        AskingPrice = 21.00M,
        City = "Ann Arbor",
        ZIP = "48104",
        AvailableUntil = new DateTime(2024, 6, 1),
        Sold = true
    },
    new Plant()
    {
        Species = "Philodendron",
        LightNeeds = 3,
        AskingPrice = 10.00M,
        City = "Des Moines",
        ZIP = "50309",
        AvailableUntil = new DateTime(2024, 7, 1),
        Sold = false
    },
    new Plant()
    {
        Species = "Pothos",
        LightNeeds = 3,
        AskingPrice = 14.00M,
        City = "Columbus",
        ZIP = "43215",
        AvailableUntil = new DateTime(2025, 4, 1),
        Sold = true
    }
};

// greeting message
string greeting = @"Welcome to ExtraVert!
The best place to browse, list, and buy secondhand plants.";
Console.WriteLine(greeting);

// main menu loop
string choice = null;
while (choice != "0")
{
    Console.Clear();
    Console.WriteLine(@"Choose an option:
                        0. Exit
                        1. Display all plants
                        2. Post a plant to be adopted
                        3. Adopt a plant
                        4. Delist a plant
                        5. Plant of the day
                        6. Search plants by light needs
                        7. View plant stats");
    choice = Console.ReadLine();
    switch (choice)
    {
        case "0":
            Console.WriteLine("Goodbye!");
            break;
        case "1":
            DisplayAllPlants();
            break;
        case "2":
            PostPlant();
            break;
        case "3":
            AdoptPlant();
            break;
        case "4":
            DelistPlant();
            break;
        case "5":
            ShowRandomPlant();
            break;
        case "6":
            SearchPlantsByLightNeeds();
            break;
        case "7":
            ShowPlantStats();
            break;
        default:
            Console.WriteLine("Invalid choice. Please choose a valid option.");
            break;
    }
    if (choice != "0")
    {
        Console.WriteLine("\nPress any key to continue...");
        Console.ReadKey();
    }
}

void DisplayAllPlants() // method to display all plants
{
    for (int i = 0; i < plants.Count; i++)
    {
        Console.WriteLine($"{i + 1}. {PlantDetails(plants[i])}");
    }
}

void PostPlant() // method to post a new plant
{
    Console.WriteLine("Enter plant details:");

    // species
    Console.Write("Species: ");
    string species = Console.ReadLine();

    // light needs
    Console.Write("Light needs (1-5): ");
    int lightNeeds;
    while (!int.TryParse(Console.ReadLine(), out lightNeeds) || lightNeeds < 1 || lightNeeds > 5)
    {
        Console.WriteLine("Invalid input. Please enter a number between 1 and 5.");
        Console.Write("Light needs (1-5): ");
    }

    // asking price
    Console.Write("Asking price: ");
    decimal askingPrice;
    while (!decimal.TryParse(Console.ReadLine(), out askingPrice) || askingPrice <= 0)
    {
        Console.WriteLine("Invalid input. Asking price must be a positive number.");
        Console.Write("Asking price: ");
    }

    // city
    Console.Write("City: ");
    string city = Console.ReadLine();

    // zip
    Console.Write("ZIP code: ");
    string zip = Console.ReadLine();

    // expiration year
    Console.Write("Expiration year: ");
    int expirationYear;
    while (!int.TryParse(Console.ReadLine(), out expirationYear) || expirationYear < DateTime.Now.Year)
    {
        Console.WriteLine("Invalid input. Year must be a number greater than or equal to the current year.");
        Console.Write("Expiration year: ");
    }

    // expiration month
    Console.Write("Expiration month: ");
    int expirationMonth;
    while (!int.TryParse(Console.ReadLine(), out expirationMonth) || expirationMonth < 1 || expirationMonth > 12)
    {
        Console.WriteLine("Invalid input. Month must be a number between 1 and 12.");
        Console.Write("Expiration month: ");
    }

    Console.Write("Expiration day: ");
    int expirationDay;
    while (!int.TryParse(Console.ReadLine(), out expirationDay) || expirationDay < 1 || expirationDay > DateTime.DaysInMonth(expirationYear, expirationMonth))
    {
        Console.WriteLine("Invalid input. Day must be a number within the days of the given month.");
        Console.Write("Expiration day: ");
    }

    DateTime availableUntil = new DateTime(expirationYear, expirationMonth, expirationDay);

    // create new plant object
    Plant newPlant = new Plant()
    {
        Species = species,
        LightNeeds = lightNeeds,
        AskingPrice = askingPrice,
        City = city,
        ZIP = zip,
        Sold = false,
        AvailableUntil = availableUntil
    };

    // add new plant to the list
    plants.Add(newPlant);
    Console.WriteLine("Plant posted successfully!");
}

void AdoptPlant() // method to adopt a plant
{
    Console.WriteLine("Available plants: ");

    // display only available plants
    List<Plant> availablePlants = plants.FindAll(p => !p.Sold && p.AvailableUntil >= DateTime.Now);
    if (availablePlants.Count == 0)
    {
        Console.WriteLine("No plants currently available.");
        return;
    }

    // display available plants with index
    for (int i = 0; i < availablePlants.Count; i++)
    {
        Console.WriteLine($"{i + 1}. {PlantDetails(availablePlants[i])}");
    }

    // prompt user to choose a plant
    Console.Write("Enter the number of the plant you want to adopt: ");
    if (int.TryParse(Console.ReadLine(), out int plantIndex) && plantIndex > 0 && plantIndex <= availablePlants.Count)
    {
        // update the selected plant's sold status
        Plant selectedPlant = availablePlants[plantIndex - 1];
        selectedPlant.Sold = true;
        Console.WriteLine($"You have adopted the {selectedPlant.Species}. Enjoy your new plant!");
    }
    else
    {
        Console.WriteLine("Invalid input. Please make a valid selection.");
    }
}

void DelistPlant() // method to delist a plant
{
    Console.WriteLine("All plants:");

    // display all plants with index
    for (int i = 0; i < plants.Count; i++)
    {
        Console.WriteLine($"{i + 1}. {PlantDetails(plants[i])}");
    }

    // prompt user to choose a plant to delist
    Console.Write("Enter the number of the plant you want to delist: ");
    if (int.TryParse(Console.ReadLine(), out int plantIndex) && plantIndex > 0 && plantIndex <= plants.Count)
    {
        // remove the selected plant from the list
        plants.RemoveAt(plantIndex - 1);
        Console.WriteLine("The plant has been delisted.");
    }
    else
    {
        Console.WriteLine("Invalid input. Please make a valid selection.");
    }
}

void ShowRandomPlant() // method to show a random plant
{
    // filter the list to get only available plants
    List<Plant> availablePlants = plants.Where(p => !p.Sold).ToList();

    if (plants.Count == 0)
    {
        Console.WriteLine("No plants available.");
        return;
    }
    
    // generate random index for available plants
    Random random = new Random();
    int randomIndex = random.Next(availablePlants.Count);
    Plant randomPlant = availablePlants[randomIndex];

    // display random plant details
    Console.WriteLine("Plant of the day: ");
    Console.WriteLine($"Species: {randomPlant.Species}");
    Console.WriteLine($"Location: {randomPlant.City}");
    Console.WriteLine($"Light needs: {randomPlant.LightNeeds}");
    Console.WriteLine($"Price: ${randomPlant.AskingPrice}");
}

void SearchPlantsByLightNeeds() // method to search plants by light needs
{
    Console.Write("Enter the maximum light needs (1-5): ");
    int maxLightNeeds;
    while (!int.TryParse(Console.ReadLine(), out maxLightNeeds) || maxLightNeeds < 1 || maxLightNeeds > 5)
    {
        Console.WriteLine("Invalid input. Light needs must be a number between 1 and 5.");
        Console.Write("Enter the maximum light needs (1-5): ");
    }

    // find plants that meet the light needs criteria
    List<Plant> matchingPlants = plants.Where(p => p.LightNeeds <= maxLightNeeds).ToList();

    if (matchingPlants.Count == 0)
    {
        Console.WriteLine("No plants match the specified light needs.");
        return;
    }

    // display matching plants
    Console.WriteLine("Plants that match the specified light needs: ");
    for (int i = 0; i < matchingPlants.Count; i++)
    {
        Console.WriteLine($"{i + 1}. A {matchingPlants[i].Species} in {matchingPlants[i].City} {(matchingPlants[i].Sold ? "was sold" : "is available")} for ${matchingPlants[i].AskingPrice}.");
    }
}

void ShowPlantStats() // method to view stats
{
    if (plants.Count == 0)
    {
        Console.WriteLine("No plants available.");
        return;
    }

    // initialize variables
    Plant lowestPricePlant = plants[0];
    Plant highestLightNeedsPlant = plants[0];
    int availablePlantsCount = 0;
    int totalLightNeeds = 0;
    int adoptedPlantsCount = 0;

    // loop through plants to collect data
    foreach (var plant in plants)
    {
        if (plant.AskingPrice < lowestPricePlant.AskingPrice)
        {
            lowestPricePlant = plant;
        }
        if (plant.LightNeeds > highestLightNeedsPlant.LightNeeds)
        {
            highestLightNeedsPlant = plant;
        }
        if (!plant.Sold)
        {
            availablePlantsCount++;
        }
        else
        {
            adoptedPlantsCount++;
        }
        totalLightNeeds += plant.LightNeeds;
    }

    // calculate stats
    double averageLightNeeds = (double)totalLightNeeds / plants.Count;
    double adoptionPercentage = (double)adoptedPlantsCount / plants.Count * 100;

    // display stats
    Console.WriteLine("Plant stats: ");
    Console.WriteLine($"Lowest price plant name: {lowestPricePlant.Species}");
    Console.WriteLine($"Number of plants available: {availablePlantsCount}");
    Console.WriteLine($"Name of plant with highest light needs: {highestLightNeedsPlant.Species}");
    Console.WriteLine($"Average light needs: {averageLightNeeds}");
    Console.WriteLine($"Percentage of plants adopted: {adoptionPercentage}%");
}

string PlantDetails(Plant plant) // method to format plant object string
{
    string plantString = $"{plant.Species} in {plant.City} {(plant.Sold ? "sold" : "available")} for ${plant.AskingPrice}.";
    return plantString;
}