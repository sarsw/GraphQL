When run, the HotChocolate UI called Banana Cake Pop is accessible on http://localhost:5000/graphql. I had to create an account to get to any useful tools. 
After createing a document I could enter a query like 'query {platform{id,name}}'. Also tested in Insomnia, which is a better GraphQL client.


To test the GraphQL api is thread safe & can handle several concurrent requests simultaneously
use Insomnia with alias queries:

query{a:platform{id,name}b:platform{id,name}c:platform{id,name}}

Example response:

{
	"errors": [
		{
			"message": "Unexpected Execution Error",
			"locations": [
				{
					"line": 1,
					"column": 26
				}
			],
			"path": [
				"b"
			]
		},
		{
			"message": "Unexpected Execution Error",
			"locations": [
				{
					"line": 1,
					"column": 45
				}
			],
			"path": [
				"c"
			]
		}
	],
	"data": {
		"a": [
			{
				"id": 1,
				"name": "Windows"
			},
			{
				"id": 2,
				"name": "NodeJS"
			},
			{
				"id": 3,
				"name": ".Net5"
			}
		],
		"b": null,
		"c": null
	}
}