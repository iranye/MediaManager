<Query Kind="Program">
  <NuGetReference>Npgsql</NuGetReference>
  <Namespace>System.Threading.Tasks</Namespace>
  <Namespace>Npgsql</Namespace>
</Query>

async Task Main()
{
	var connString = $"Host=127.0.0.1;Port=5666;Database=mediamanagerdb;Username=dev;Password=password";

	await using var conn = new NpgsqlConnection(connString);
	await conn.OpenAsync();

	await using (var cmd = new NpgsqlCommand("select * from \"Volumes\"", conn))
	await using (var reader = await cmd.ExecuteReaderAsync())
		while (await reader.ReadAsync())
			Console.WriteLine(reader.GetInt32(0));
}
