namespace WebAPI.Models;

public sealed class ElasticCredentials{

    public ElasticCredentials()
    {
        
    }
    public string Uri { get; set;}
    public string Username { get;set; }
    public string Password { get;set; }
}