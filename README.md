# GraphQL
Created after following a guide on Youtube : https://www.youtube.com/watch?v=HuN94qNwQmM&t=5095s

This project demonstrates using a GraphQL architecture in .Net with a MSSQL DB and using client tools such as Insomnia and BananaCake to verify and interact with the schema ans data.

The MSSQL image was spun up in docker using docker compose. 

I like the way the GraphQL api has filtering and sorting and using the Hotchocolate package gave lots of free functionality.  I also used newer packages than the guide so had to fix some code as I went along to accomodate the new packages.

Documenting the api proved cleaner in the code-first approach rather than marking-up the model with annotations.
