using API.NET6.Context;
using API.NET6.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NuGet.ContentModel;

namespace API.NET6.Testes
{
    public class ConsultaApiTeste : IClassFixture<DbFixture>
	{
		private IServiceProvider _serviceProvider;
		protected DataContext db;
		protected IConfiguration _configuration;

		public ConsultaApiTeste(DbFixture _fixture)
        {
			_fixture.Init();
			_serviceProvider = _fixture.ServiceProvider;
			db = _fixture.GetDataContext();
			db.Database.OpenConnection();
			db.Database.EnsureCreated();
        }

		[Fact]
		public void ConsultaTodosSetores()
		{
			//arrange
			var cargos = db.Setor.ToList();

			//act
			var resultado = cargos.Count;

			//asset
			Assert.Equal(5, resultado);
		}

		[Fact]
		public async void InserirSetor()
		{
			var setor = new Setor();
			setor.Descricao = "Teste Setor";
			//arrange
			db.Setor.Add(setor);
			db.SaveChangesAsync();
			//act
			await db.SaveChangesAsync();

			//asset
			Assert.True(db.Setor is not null);
		}
	}
}

