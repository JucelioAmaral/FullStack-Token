using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ProEventos.API.Extensions
{
    //Todo classe de extens�o deve ser static, sen�o n�o conseguir� chamar ele.
    public static class ClaimsPrincipalExtensions
    {
        //Todo classe de extens�o deve ser static, sen�o n�o conseguir� chamar ele.
        public static string GetUserName(this ClaimsPrincipal user)//O nome do m�todo n�o tem nenhuma rela��o com o nome da classe, tem a ver com o primeiro par�metro passado, ou seja, deve ser: "this ClaimsPrincipal <parametro>", funcionando de forma correta.
        {
            return user.FindFirst(ClaimTypes.Name)?.Value;
        }
        //Todo m�todo de extens�o deve ser static, sen�o n�o conseguir� chamar ele.
        public static int GetUserId(this ClaimsPrincipal user)//O nome do m�todo n�o tem nenhuma rela��o com o nome da classe, tem a ver com o primeiro par�metro passado, ou seja, deve ser: "this ClaimsPrincipal <parametro>", funcionando de forma correta.
        {
            return int.Parse(user.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        }
    }
}