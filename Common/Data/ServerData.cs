namespace GTA5OnlineTools.Common.Data;

public class ServerData
{
    public string Version { get; set; }
    public Latest Latest { get; set; }
    public Address Address { get; set; }
    public List<Download> Download { get; set; }
}

public class Latest
{
    public string Date { get; set; }
    public string Change { get; set; }
}

public class Address
{
    public string Notice { get; set; }
    public string Change { get; set; }
}

public class Download
{
    public string Name { get; set; }
    public string Url { get; set; }
}
