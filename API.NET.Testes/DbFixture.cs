using System;
using API.NET.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace API.NET.Testes;

public class DbFixture
{
	public IConfiguration Configuration { get; }
	public IServiceProvider ServiceProvider { get; private set; }
	public DbFixture()
	{
	}

	public void Init()
        {
		var serviceCollection = new ServiceCollection();

		string mySqlConnection = "Server=localhost;Database=api;uid=root;password=password";
		serviceCollection
			.AddDbContext<DataContext>(
				options =>
					options.UseMySql(mySqlConnection,
				    ServerVersion.AutoDetect(mySqlConnection)
				)
			);

		ServiceProvider = serviceCollection.AddLogging().BuildServiceProvider();

        }

	public DataContext GetDataContext() {
		return ServiceProvider.GetService<DataContext>();
	}
}

