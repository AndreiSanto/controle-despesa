using CommonTestUtilies.RequisicaoDtos;
using controleDespesa.Application.DTOs;
using controleDespesa.Communication.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace WebApi.Test
{
    public static class RegistrarEAutenticarUsuarioExtends
    {


        public static async Task<string> RegistrarEAutenticarUsuario(HttpClient httpClient)
        {
            var usuarioDTO = UsuarioDTOBuilder.Build();
            var respostaCadastro = await httpClient.PostAsJsonAsync("Usuario/Cadastro", usuarioDTO);

            respostaCadastro.EnsureSuccessStatusCode();


            var login = new LoginDTO()
            {
                Email = usuarioDTO.Email,
                Password = usuarioDTO.Password
            };

            var respostaLogin = await httpClient.PostAsJsonAsync("Login", login);

            respostaLogin.EnsureSuccessStatusCode();

            var loginResponse = await respostaLogin.Content.ReadFromJsonAsync<AuthResponse>();

            return loginResponse.Token;
        }
    }
}
