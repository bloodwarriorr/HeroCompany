# Hero company web api:

## In order to access the api:
Use Postman:

Generate a refresh token with the route:https://localhost:44372/token, 
attach to your request in a x-www-form-urlencoded method the following:

key-value:

grant_type-password

username-"any user name from db"

password-"specific user pwd"


You should insert data into sql+change the sql connection string to your local machine, with 2 options:

1)

Use the migration file inside Dal layer, with the following commands inside PM command line:

Enable-Migrations(if needed)
Add-Migration Initial
Update-Database

2)

Use the api itself route to register a trainer, then insert trainers. (swaager ui shows route)


