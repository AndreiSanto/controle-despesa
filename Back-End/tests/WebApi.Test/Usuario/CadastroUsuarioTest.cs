using CommonTestUtilies.RequisicaoDtos;
using controleDespesa.Application.DTOs;
using FluentAssertions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace WebApi.Test.Usuario
{
    public class CadastroUsuarioTest : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly HttpClient _httpClient;
        public CadastroUsuarioTest(CustomWebApplicationFactory factory)
        {
            _httpClient = factory.CreateClient();
        }

        [Fact]
        public async Task Sucesso()
        {
            var usuarioDTO = UsuarioDTOBuilder.Build();
            var resposta = await _httpClient.PostAsJsonAsync("Usuario/Cadastro", usuarioDTO);

            resposta.StatusCode.Should().Be(HttpStatusCode.Created);

            await using var responseBody = await resposta.Content.ReadAsStreamAsync();

            var responseData = await JsonDocument.ParseAsync(responseBody);
            responseData.RootElement.GetProperty("nome").GetString().Should().NotBeNullOrWhiteSpace().And.
                Be(usuarioDTO.Nome);

        }
    }
}
