using CommonTestUtilies.RequisicaoDtos;
using controleDespesa.Application.DTOs;
using controleDespesa.Domain.Entities;

using controleDespesa.Communication.Response;
using FluentAssertions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace WebApi.Test.Despesas
{
    public class DespesaApiTest : IClassFixture<CustomWebApplicationFactory>
    {

        private readonly HttpClient _httpClient;

        public DespesaApiTest(CustomWebApplicationFactory factory)
        {
            _httpClient = factory.CreateClient();
        }

        [Fact]
        public async Task Sucesso()
        {
            var token = await RegistrarEAutenticarUsuarioExtends.RegistrarEAutenticarUsuario(_httpClient);

            _httpClient.DefaultRequestHeaders.Authorization =
           new AuthenticationHeaderValue("Bearer", token);

            var despesaDto = DespesaDTOBuilder.Build();
            var resposta =  await _httpClient.PostAsJsonAsync("Despesa/Cadastro", despesaDto);

            resposta.StatusCode.Should().Be(HttpStatusCode.Created);

            


            await using var responseBody = await resposta.Content.ReadAsStreamAsync();

            var responseData = await JsonDocument.ParseAsync(responseBody);
            var idRetornado = responseData.RootElement.GetProperty("id").GetInt32();

            idRetornado.Should().Be(despesaDto.Id);






        }

      

        [Fact]
        public async Task Sucesso_Lista_Categoria()
        {

            var token = await RegistrarEAutenticarUsuarioExtends.RegistrarEAutenticarUsuario(_httpClient);

            _httpClient.DefaultRequestHeaders.Authorization =
           new AuthenticationHeaderValue("Bearer", token);

           var resposta =  await _httpClient.GetAsync("Despesa/ListarCategoriaDespesa");

            resposta.StatusCode.Should().Be(HttpStatusCode.OK);

            var categorias = await resposta.Content.ReadFromJsonAsync<List<TipoDespesaReceitaResponse>>();


            categorias.Should().NotBeNull();
            categorias.Should().NotBeEmpty();
            categorias!.Count.Should().BeGreaterThan(0);

            // 5. Valida conteúdo da categoria 1 (se houver)
            var primeira = categorias.First();

            primeira.Id.Should().BeGreaterThan(0);
            primeira.Nome.Should().NotBeNullOrWhiteSpace();
        }


        [Fact]
        public async Task Sucesso_Obter_Por_Id()
        {
            // Autentica o usuário
            var token = await RegistrarEAutenticarUsuarioExtends.RegistrarEAutenticarUsuario(_httpClient);

            _httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);

            // Cadastra uma despesa para buscar depois
            var despesaDto = await CadastrarDespesaAux();

            // Faz a requisição GET correta
            var resposta = await _httpClient.GetAsync($"Despesa/ObterPorId/{despesaDto.Id}");

            // Verifica o status code
            resposta.StatusCode.Should().Be(HttpStatusCode.OK);

            // Verifica se o retorno não está vazio
            var body = await resposta.Content.ReadAsStringAsync();
            body.Should().NotBeNullOrWhiteSpace();

            // Desserializa e valida o conteúdo retornado
            var despesa = JsonConvert.DeserializeObject<Despesa>(body);
            despesa.Should().NotBeNull();
            despesa.Id.Should().Be(despesaDto.Id);
            despesa.Descricao.Should().Be(despesaDto.Descricao);
        }

        private async Task<DespesaDTO> CadastrarDespesaAux()
        {
            var despesaDto = DespesaDTOBuilder.Build();

            var respostaCadastro = await _httpClient.PostAsJsonAsync("Despesa/Cadastro", despesaDto);

            respostaCadastro.StatusCode.Should().Be(HttpStatusCode.Created);
            await using var responseBody = await respostaCadastro.Content.ReadAsStreamAsync();

            var responseData = await JsonDocument.ParseAsync(responseBody);
            var idRetornado = responseData.RootElement.GetProperty("id").GetInt32();

            return despesaDto;

        }
    }
}
