# OrderApp

A console application that allows order management. Users can add orders, change their status, and track the delivery process.

# Technologies

    C# (.NET)
    PostgreSQL 
    Entity Framework Core
    MSTest
    Docker & Docker Compose
   
# Running the PostgreSQL Database
To correctly start the database, enter the following command in the terminal:

    docker-compose up -d


# Migrations
In your terminal, navigate to the project directory where your Entity Framework Core migrations are located. Run the following command to apply migrations to the database:
   

     dotnet ef database update
If you need to create a new migration, use the following command:

     dotnet ef migrations add init
