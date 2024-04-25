using APIServer;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
WebApplication app = builder.Build();

app.MapGet("/", GetHomePage);
app.MapGet("/adduser/{inputUsername}/{inputPassword}", UserBase.AddUser);

app.MapPost("/addemptyuser", UserBase.AddEmptyUser);
app.MapGet("/addemptyuser", () => "Added empty user");

app.Run();

static string GetHomePage()
{
    int iteration = 1;
    string str = "Psst, add (/adduser/username/password) to the url to add a user to the following:\n";
    foreach (User u in UserBase.users)
    {
        str += "\n";
        str += "User " + iteration + "\n";
        str += "Username: " + u.username + "\n";
        str += "Password: " + u.password + "\n";
        iteration++;
    }
    return str;
}

class UserBase
{
    public static List<User> users = new();

    public static IResult AddUser(string inputUsername, string inputPassword)
    {
        if (string.IsNullOrEmpty(inputUsername) || string.IsNullOrEmpty(inputPassword))
            return Results.BadRequest("Missing username or password");

        else
        {
            users.Add(new User() {username = inputUsername, password = inputPassword});
            return Results.Ok("User added!");
        }    
    }

    public static IResult AddEmptyUser()
    {
        users.Add(new() {username = "empty", password = "empty"});
        return Results.Ok("Added!");
    }
}