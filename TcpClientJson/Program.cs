using System;
using System.IO;
using System.Net.Sockets;
using System.Text.Json;

public class TCPClient
{
	public static void Main()
	{
		try
		{
			TcpClient client = new TcpClient("127.0.0.1", 7); 
			NetworkStream stream = client.GetStream();
			StreamWriter writer = new StreamWriter(stream);
			StreamReader reader = new StreamReader(stream);

			// Spørg brugeren om input
			Console.Write("Enter method (Random, Add, Subtract): ");
			string method = Console.ReadLine();
			Console.Write("Enter first number: ");
			int tal1 = int.Parse(Console.ReadLine());
			Console.Write("Enter second number: ");
			int tal2 = int.Parse(Console.ReadLine());

			// Opret JSON objekt
			var request = new Request
			{
				Command = method,
				Parameters = new int[] { tal1, tal2 }
			};

			string json = JsonSerializer.Serialize(request);
			writer.WriteLine(json);
			writer.Flush();
			Console.WriteLine("Sent: {0}", json);

			// Modtag svar
			string response = reader.ReadLine();
			Console.WriteLine("Received: {0}", response);

			stream.Close();
			client.Close();
		}
		catch (Exception e)
		{
			Console.WriteLine("Exception: {0}", e);
		}
	}
}

public class Request
{
	public string Command { get; set; }
	public int[] Parameters { get; set; }
}

