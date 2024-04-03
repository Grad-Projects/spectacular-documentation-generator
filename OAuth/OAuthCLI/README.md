To run the OAuth in Terminal, you can use `dotnet run --project OAuthCLI`.

Right now, this flow works on a CLI basis.
It should rather be executed on the server, so that the client ID can stay secure (Although we're using environment variables, just in case).